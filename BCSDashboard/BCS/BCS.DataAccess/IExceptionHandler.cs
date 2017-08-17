
namespace BCS.Core
{
    using System;
    using System.Data.SqlClient;

    /// <summary>
    /// Provides exception handling functionality.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Determines whether the given exception represents a timeout exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        ///   <c>true</c> if the given exception is a timeout exception; otherwise, <c>false</c>.
        /// </returns>
        bool IsTimeoutException(SqlException exception);

        /// <summary>
        /// Determines whether the given exception is a deadlock exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        ///   <c>true</c> if the given exception is a deadlock exception; otherwise, <c>false</c>.
        /// </returns>
        bool IsDeadlockException(SqlException exception);

        /// <summary>
        /// Used for handling exceptions in a web service.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void HandleWebServiceException(Exception exception);

        /// <summary>
        /// Used for handling exceptions in a windows service which affect the operation of the service.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void HandleWorkerServiceException(Exception exception);

        /// <summary>
        /// Used for handling exceptions in a windows service which only affect the operation of a particular thread/process.
        /// </summary>
        /// <param name="exception">The Exception</param>
        void HandleWorkerServiceProcessException(Exception exception);

        /// <summary>
        /// Used for handling exceptions caused by aborting of a thread.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void HandleThreadException(Exception exception);

        /// <summary>
        /// Used for handling exceptions when a QueueListener can't be contacted.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void HandleQueueListenerException(Exception exception);

        /// <summary>
        /// Handles the style sync exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void HandleStyleSyncException(Exception exception);

        /// <summary>
        /// Executes the given method, handling any exceptions thrown from the call.
        /// </summary>
        /// <param name="method">The method to call, handling any exceptions thrown from the call.</param>
        void HandleIfThrown(Action method);
    }
}
