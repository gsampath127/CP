
namespace BCS.Core
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

    /// <summary>
    /// Wrapper for Microsoft ExceptionHandling Application Block
    /// </summary>
    public class ExceptionHandler : IExceptionHandler
    {
        #region Public Methods

        /// <summary>
        /// Determines whether the given exception represents a timeout exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        ///   <c>true</c> if the given exception is a timeout exception; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsTimeoutException(SqlException exception)
        {
            // -2 is the value of System.Data.SqlClient.TdsEnums.TIMEOUT_EXPIRED
            // You have to use reflector to see this enum, since it is private.
            // Timeout expired. The timeout period elapsed prior to completion of the operation or the server is not responding
            return exception != null && exception.Number == -2;
        }

        /// <summary>
        /// Determines whether the given exception is a deadlock exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        ///   <c>true</c> if the given exception is a deadlock exception; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDeadlockException(SqlException exception)
        {
            // To find the sql error codes, run the following select statement...
            // We are only catching deadlock errors thought to be pertinent... 
            // 1205 is really the most important one.
            // SELECT * FROM [Master].[dbo].[SysMessages] WHERE [Description] LIKE '%deadlock%' AND [MsgLangId] = '1033'
            return exception != null && (
                //// Transaction (Process ID %d) was deadlocked on %.*ls resources with another process and has been chosen as the deadlock victim. Rerun the transaction.
                exception.Number == 1205 ||
                //// The marked transaction '%.*ls' failed. A Deadlock was encountered while attempting to place the mark in the log.
                exception.Number == 3928 ||
                //// Object ID %ld (object '%.*ls'): A deadlock occurred while trying to lock this object for checking. This object has been skipped and will not be processed.
                exception.Number == 5231);
        }

        /// <summary>
        /// Used for handling exceptions in a web service.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public static void HandleWebServiceException(Exception ex)
        {
            HandleInternal(ex, "Web Service Policy");
        }

        /// <summary>
        /// Used for handling exceptions in a windows service which affect the operation of the service.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public static void HandleWorkerServiceException(Exception ex)
        {
            HandleInternal(ex, "Worker Service Policy");
        }

        /// <summary>
        /// Used for handling exceptions in a windows service which only affect the operation of a particular thread/process.
        /// </summary>
        /// <param name="ex">The Exception</param>
        public static void HandleWorkerServiceProcessException(Exception ex)
        {
            // Even though the worker server shouldn't rethrow errors so it can keep processing
            // this is configured in the Worker Service Process Policy so it will always return false
            HandleInternal(ex, "Worker Service Process Policy");
        }

        /// <summary>
        /// Used for handling exceptions caused by aborting of a thread.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public static void HandleThreadException(Exception ex)
        {
            HandleInternal(ex, "Thread Policy");
        }

        /// <summary>
        /// Used for handling exceptions when a QueueListener can't be contacted.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public static void HandleQueueListenerException(Exception ex)
        {
            HandleInternal(ex, "Queue Listener Policy");
        }

        /// <summary>
        /// Handles the style sync exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public static void HandleStyleSyncException(Exception ex)
        {
            HandleInternal(ex, "StyleSync Exception");
        }

        /// <summary>
        /// Executes the given method, handling any exceptions thrown from the call.
        /// </summary>
        /// <param name="method">The method to call, handling any exceptions thrown from the call.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is our highest level Exception Handler. It is necessary to catch the most general Exception type.")]
        public static void HandleIfThrown(Action method)
        {
            if (method != null)
            {
                try
                {
                    method();
                }
                catch (Exception exception)
                {
                    HandleWorkerServiceException(exception);
                }
            }
        }

        /// <summary>
        /// Executes the given method with the given argument, handling any exceptions thrown from the call.
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument</typeparam>
        /// <param name="method">The method to call with the given argument, handling any exceptions thrown from the call.</param>
        /// <param name="argument">The argument to pass to the method.</param>
        public static void HandleIfThrown<TArgument>(Action<TArgument> method, TArgument argument)
        {
            HandleIfThrown(() => method(argument));
        }

        /// <summary>
        /// Executes the given method with the given arguments, handling any exceptions thrown from the call.
        /// </summary>
        /// <typeparam name="TArgumentOne">The type of the first argument.</typeparam>
        /// <typeparam name="TArgumentTwo">The type of the second argument.</typeparam>
        /// <param name="method">The method to call with the given arguments, handling any exceptions thrown from the call.</param>
        /// <param name="argumentOne">The first argument to the method.</param>
        /// <param name="argumentTwo">The second argument to the method.</param>
        public static void HandleIfThrown<TArgumentOne, TArgumentTwo>(Action<TArgumentOne, TArgumentTwo> method, TArgumentOne argumentOne, TArgumentTwo argumentTwo)
        {
            HandleIfThrown(x => HandleIfThrown(y => method(x, y), argumentTwo), argumentOne);
        }

        /// <summary>
        /// Executes the given method with the given arguments, handling any exceptions thrown from the call.
        /// </summary>
        /// <typeparam name="TArgumentOne">The type of the first argument.</typeparam>
        /// <typeparam name="TArgumentTwo">The type of the second argument.</typeparam>
        /// <typeparam name="TArgumentThree">The type of the third argument.</typeparam>
        /// <param name="method">The method to call with the given arguments, handling any exceptions thrown from the call.</param>
        /// <param name="argumentOne">The first argument to the method.</param>
        /// <param name="argumentTwo">The second argument to the method.</param>
        /// <param name="argumentThree">The third argument to the method.</param>
        public static void HandleIfThrown<TArgumentOne, TArgumentTwo, TArgumentThree>(Action<TArgumentOne, TArgumentTwo, TArgumentThree> method, TArgumentOne argumentOne, TArgumentTwo argumentTwo, TArgumentThree argumentThree)
        {
            HandleIfThrown((x, y) => HandleIfThrown(z => method(x, y, z), argumentThree), argumentOne, argumentTwo);
        }

        /// <summary>
        /// Executes the given method with the given arguments, handling any exceptions thrown from the call.
        /// </summary>
        /// <typeparam name="TArgumentOne">The type of the first argument.</typeparam>
        /// <typeparam name="TArgumentTwo">The type of the second argument.</typeparam>
        /// <typeparam name="TArgumentThree">The type of the third argument.</typeparam>
        /// <typeparam name="TArgumentFour">The type of the fourth argument.</typeparam>
        /// <param name="method">The method to call with the given arguments, handling any exceptions thrown from the call.</param>
        /// <param name="argumentOne">The first argument to the method.</param>
        /// <param name="argumentTwo">The second argument to the method.</param>
        /// <param name="argumentThree">The third argument to the method.</param>
        /// <param name="argumentFour">The fourth argument to the method.</param>
        public static void HandleIfThrown<TArgumentOne, TArgumentTwo, TArgumentThree, TArgumentFour>(Action<TArgumentOne, TArgumentTwo, TArgumentThree, TArgumentFour> method, TArgumentOne argumentOne, TArgumentTwo argumentTwo, TArgumentThree argumentThree, TArgumentFour argumentFour)
        {
            HandleIfThrown((w, x) => HandleIfThrown((y, z) => method(w, x, y, z), argumentThree, argumentFour), argumentOne, argumentTwo);
        }

        /// <summary>
        /// Executes the given method, handling any exceptions thrown from the call.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="method">The method to call, handling any exceptions thrown from the call.</param>
        /// <returns>
        /// Returns the result of the given method.
        /// </returns>        
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is our highest level Exception Handler. It is necessary to catch the most general Exception type.")]
        public static TResult HandleIfThrown<TResult>(Func<TResult> method)
        {
            TResult r = default(TResult);

            try
            {
                if (method != null)
                {
                    r = method();
                }
            }
            catch (Exception exception)
            {
                HandleWorkerServiceException(exception);
            }

            return r;
        }

        /// <summary>
        /// Executes the given method with the given argument, handling any exceptions thrown from the call.
        /// </summary>
        /// <typeparam name="TArgument">The type of the first argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="method">The method to call with the given argument, handling any exceptions thrown from the call.</param>
        /// <param name="argument">The argument to the method.</param>
        /// <returns>
        /// Returns the result of the given method.
        /// </returns>
        public static TResult HandleIfThrown<TArgument, TResult>(Func<TArgument, TResult> method, TArgument argument)
        {
            return HandleIfThrown(() => method(argument));
        }

        /// <summary>
        /// Executes the given method with the given arguments, handling any exceptions thrown from the call.
        /// </summary>
        /// <typeparam name="TArgumentOne">The type of the first argument.</typeparam>
        /// <typeparam name="TArgumentTwo">The type of the second argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="method">The method to call with the given arguments, handling any exceptions thrown from the call.</param>
        /// <param name="argumentOne">The first argument to the method.</param>
        /// <param name="argumentTwo">The second argument to the method.</param>
        /// <returns>
        /// Returns the result of the given method.
        /// </returns>
        public static TResult HandleIfThrown<TArgumentOne, TArgumentTwo, TResult>(
            Func<TArgumentOne, TArgumentTwo, TResult> method,
            TArgumentOne argumentOne,
            TArgumentTwo argumentTwo)
        {
            return HandleIfThrown(x => HandleIfThrown(y => method(x, y), argumentTwo), argumentOne);
        }

        /// <summary>
        /// Executes the given method with the given arguments, handling any exceptions thrown from the call.
        /// </summary>
        /// <typeparam name="TArgumentOne">The type of the first argument.</typeparam>
        /// <typeparam name="TArgumentTwo">The type of the second argument.</typeparam>
        /// <typeparam name="TArgumentThree">The type of the third argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="method">The method to call with the given arguments, handling any exceptions thrown from the call.</param>
        /// <param name="argumentOne">The first argument to the method.</param>
        /// <param name="argumentTwo">The second argument to the method.</param>
        /// <param name="argumentThree">The third argument to the method.</param>
        /// <returns>
        /// Returns the result of the given method.
        /// </returns>
        public static TResult HandleIfThrown<TArgumentOne, TArgumentTwo, TArgumentThree, TResult>(
            Func<TArgumentOne, TArgumentTwo, TArgumentThree, TResult> method,
            TArgumentOne argumentOne,
            TArgumentTwo argumentTwo,
            TArgumentThree argumentThree)
        {
            return HandleIfThrown((x, y) => HandleIfThrown(z => method(x, y, z), argumentThree), argumentOne, argumentTwo);
        }

        /// <summary>
        /// Executes the given method with the given arguments, handling any exceptions thrown from the call.
        /// </summary>
        /// <typeparam name="TArgumentOne">The type of the first argument.</typeparam>
        /// <typeparam name="TArgumentTwo">The type of the second argument.</typeparam>
        /// <typeparam name="TArgumentThree">The type of the third argument.</typeparam>
        /// <typeparam name="TArgumentFour">The type of the fourth argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="method">The method to call with the given arguments, handling any exceptions thrown from the call.</param>
        /// <param name="argumentOne">The first argument to the method.</param>
        /// <param name="argumentTwo">The second argument to the method.</param>
        /// <param name="argumentThree">The third argument to the method.</param>
        /// <param name="argumentFour">The fourth argument to the method.</param>
        /// <returns>
        /// Returns the result of the given method.
        /// </returns>
        public static TResult HandleIfThrown<TArgumentOne, TArgumentTwo, TArgumentThree, TArgumentFour, TResult>(
            Func<TArgumentOne, TArgumentTwo, TArgumentThree, TArgumentFour, TResult> method,
            TArgumentOne argumentOne,
            TArgumentTwo argumentTwo,
            TArgumentThree argumentThree,
            TArgumentFour argumentFour)
        {
            return HandleIfThrown((w, x) => HandleIfThrown((y, z) => method(w, x, y, z), argumentThree, argumentFour), argumentOne, argumentTwo);
        }

        #region IExceptionHandler Members

        /// <summary>
        /// Determines whether the given exception represents a timeout exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        ///   <c>true</c> if the given exception is a timeout exception; otherwise, <c>false</c>.
        /// </returns>
        bool IExceptionHandler.IsTimeoutException(SqlException exception)
        {
            return ExceptionHandler.IsTimeoutException(exception);
        }

        /// <summary>
        /// Determines whether the given exception is a deadlock exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        ///   <c>true</c> if the given exception is a deadlock exception; otherwise, <c>false</c>.
        /// </returns>
        bool IExceptionHandler.IsDeadlockException(SqlException exception)
        {
            return ExceptionHandler.IsDeadlockException(exception);
        }

        /// <summary>
        /// Used for handling exceptions in a web service.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void IExceptionHandler.HandleWebServiceException(Exception exception)
        {
            ExceptionHandler.HandleWebServiceException(exception);
        }

        /// <summary>
        /// Used for handling exceptions in a windows service which affect the operation of the service.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void IExceptionHandler.HandleWorkerServiceException(Exception exception)
        {
            ExceptionHandler.HandleWorkerServiceException(exception);
        }

        /// <summary>
        /// Used for handling exceptions in a windows service which only affect the operation of a particular thread/process.
        /// </summary>
        /// <param name="exception">The Exception</param>
        void IExceptionHandler.HandleWorkerServiceProcessException(Exception exception)
        {
            ExceptionHandler.HandleWorkerServiceProcessException(exception);
        }

        /// <summary>
        /// Used for handling exceptions caused by aborting of a thread.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void IExceptionHandler.HandleThreadException(Exception exception)
        {
            ExceptionHandler.HandleThreadException(exception);
        }

        /// <summary>
        /// Used for handling exceptions when a QueueListener can't be contacted.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void IExceptionHandler.HandleQueueListenerException(Exception exception)
        {
            ExceptionHandler.HandleQueueListenerException(exception);
        }

        /// <summary>
        /// Handles the style sync exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void IExceptionHandler.HandleStyleSyncException(Exception exception)
        {
            ExceptionHandler.HandleStyleSyncException(exception);
        }

        /// <summary>
        /// Executes the given method, handling any exceptions thrown from the call.
        /// </summary>
        /// <param name="method">The method to call, handling any exceptions thrown from the call.</param>
        void IExceptionHandler.HandleIfThrown(Action method)
        {
            ExceptionHandler.HandleIfThrown(method);
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the given exception using the given exception handling policy.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="policy">The policy.</param>
        private static void HandleInternal(Exception exception, string policy)
        {
            if (!(exception is ThreadAbortException))
            {
                bool rethrow = ExceptionPolicy.HandleException(exception, policy);

                if (rethrow)
                {
                    throw exception;
                }
            }
        }

        #endregion
    }
}
