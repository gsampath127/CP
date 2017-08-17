
namespace BCS.Core
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Transactions;    
    using SystemTransaction = System.Transactions.Transaction;

    /// <summary>
    /// Provides transactional support.
    /// </summary>
    public static class Transaction
    {
        #region Fields

        /// <summary>
        /// The number of attempts to retry any failed transactions, defaulted to 10 if not set in the config.
        /// </summary>
        private static readonly int NumberOfAttemptsOnRetry = GetFromConfig<int>("NumberOfAttemptsOnRetry", 10);

        /// <summary>
        /// The number of milliseconds to sleep when retrying any failed transactions, defaulted to 10 if not set in the config.
        /// </summary>
        private static readonly int NumberOfMillisecondsToSleepOnRetry = GetFromConfig<int>("NumberOfMillisecondsToSleepOnRetry", 10);

        /// <summary>
        /// The number of seconds to wait before timing out a transaction, defaulted to 180 if not set in the config.
        /// </summary>
        private static readonly int TransactionTimeOutInSeconds = GetFromConfig<int>("TransactionTimeOutInSeconds", 180);

        #endregion

        #region Methods
        /// <summary>
        /// Executes the specified action in the context of a transaction.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void Execute(Action action)
        {
            if (action != null)
            {
                using (var transactionScope = new TransactionScope(
                    TransactionScopeOption.Required,
                    TimeSpan.FromSeconds(TransactionTimeOutInSeconds)))
                {
                    action();
                    transactionScope.Complete();
                }
            }
        }

        /// <summary>
        /// Executes the action in the context of a transaction.
        /// If the action fails due to a deadlock or a timeout and the transaction is not nested, 
        /// the action will be retried a configured number of times.
        /// If the transaction is nested, the exception will bubble up to the 
        /// outermost transaction, which may then be retried.
        /// Note: This only will retry if it is the outermost transaction... so if you called
        /// Execute for the outermost transaction, no retries will be done on a deadlock or a timeout.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void ExecuteWithRetryOnDeadlockOrTimeout(Action action)
        {
            bool isAlreadyInTransaction = SystemTransaction.Current != null;
            int numberOfTimesAttempted = 0;
            int deadlocks = 0;
            int timeouts = 0;
            Exception lastException = null;

            while (numberOfTimesAttempted++ < NumberOfAttemptsOnRetry)
            {
                try
                {
                    Execute(action);
                    break;
                }
                catch (Exception exception)
                {
                    SqlException sqlException = exception as SqlException;
                    TransactionAbortedException transactionAbortException = exception as TransactionAbortedException;

                    bool isDeadlock = ExceptionHandler.IsDeadlockException(sqlException);
                    deadlocks += isDeadlock ? 1 : 0;
                    bool isTimeout = ExceptionHandler.IsTimeoutException(sqlException) || (transactionAbortException != null && transactionAbortException.InnerException != null && transactionAbortException.InnerException is TimeoutException);
                    timeouts += isTimeout ? 1 : 0;

                    if (isAlreadyInTransaction ||
                        numberOfTimesAttempted >= NumberOfAttemptsOnRetry ||
                        !(isDeadlock || isTimeout))
                    {
                        throw;
                    }

                    lastException = exception;
                    Thread.Sleep(NumberOfMillisecondsToSleepOnRetry);
                }
            }

            if (lastException != null && numberOfTimesAttempted > 1)
            {
                LogRetries(deadlocks, timeouts, lastException);
            }
        }

        /// <summary>
        /// Logs any retry attempts to the exception handler.
        /// </summary>
        /// <param name="deadlocks">The number of deadlocks.</param>
        /// <param name="timeouts">The number of timeouts.</param>
        /// <param name="lastException">The last exception.</param>
        private static void LogRetries(int deadlocks, int timeouts, Exception lastException)
        {
            if (deadlocks > 0)
            {
                ExceptionHandler.HandleWorkerServiceException(
                    new ApplicationException("A deadlock has occurred " + deadlocks + " time(s) in the database.", lastException));
            }

            if (timeouts > 0)
            {
                ExceptionHandler.HandleWorkerServiceException(
                    new ApplicationException("A timeout has occurred " + timeouts + " time(s) in the database.", lastException));
            }
        }

        /// <summary>
        /// Gets a value from the config if it exists, otherwise returning the default value of T.
        /// </summary>
        /// <typeparam name="T">The type of the return value.</typeparam>
        /// <param name="key">The key in the config.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Returns a value from the config if it exists, otherwise returning the default value of T.</returns>
        private static T GetFromConfig<T>(string key, T defaultValue) where T : IConvertible, new()
        {
            string value = ConfigurationManager.AppSettings[key];

            return !string.IsNullOrEmpty(value) ?
                (T)Convert.ChangeType(value, typeof(T)) :
                defaultValue;
        }
        #endregion
    }
}
