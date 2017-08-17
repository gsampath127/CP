//-----------------------------------------------------------------------
// <copyright file="WorkerServiceConfiguration.cs" company="R. R. Donnelley &amp; Sons Company">
//     Copyright (c) R. R. Donnelley &amp; Sons Company. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace RRD.FSG.RP.Services.ScheduleTasks
{
    using System;
    using System.Configuration;
    using System.IO;

    /// <summary>
    /// Static helper file for reading configuration values.
    /// </summary>
    internal static class WorkerServiceConfiguration
    {
        #region Public Constants

        /// <summary>
        /// The interval to wait when retrying to get the service name.
        /// </summary>
        public const int ServiceNameRetryInterval = 5000;

        /// <summary>
        /// The maximum number of retries to attempt to get the service name.
        /// </summary>
        public const int ServiceNameMaxRetries = 12;

        /// <summary>
        /// Initial workser service timer interval (10000 ms).
        /// </summary>
        public const int InitialWorkerServiceTimerInterval = 10000;

        #endregion

        #region Private Constants

        /// <summary>
        /// The key in AppSettings in the config for the temp output file path.
        /// </summary>
        private const string OutputPathKey = "OutputPath";

        /// <summary>
        /// The key in AppSettings in the config for the number of days to delete.
        /// </summary>
        private const string DeleteFilesOlderThanDaysKey = "DeleteFilesOlderThanDays";

        /// <summary>
        /// The key for Max Threads.
        /// </summary>
        private const string MaxThreadsKey = "maxThreads";

        /// <summary>
        /// The key for worker service timer interval.
        /// </summary>
        private const string WorkerServiceTimerIntervalKey = "TimerInterval";

        /// <summary>
        /// The key for cleanup timer interval.
        /// </summary>
        private const string CleanupTimerIntervalKey = "CleanupTimerInterval";

        /// <summary>
        /// The key for thread join wait interval in MS.
        /// </summary>
        private const string ThreadJoinWaitIntervalMSKey = "ThreadJoinWaitIntervalMS";

        /// <summary>
        /// The key for add delay for debug flag.
        /// </summary>
        private const string AddDelayForDebugKey = "AddDelayForDebug";

        /// <summary>
        /// The key for the service connection string.
        /// </summary>
        private const string SystemDBConnectionStringKey = "SystemDB";

        /// <summary>
        /// The key for the error queue name.
        /// </summary>
        private const string ErrorQueueNameKey = "ErrorQueueName";

        /// <summary>
        /// The key for the worker thread timeout.
        /// </summary>
        private const string WorkerThreadTimeOutMillisecondsKey = "WorkerThreadTimeOutMilliseconds";

        /// <summary>
        /// The key for the total time to wait for Tasks to complete.
        /// </summary>
        private const string WorkerThreadMaxWaitTimeKey = "WorkerThreadMaxWaitTime";

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes static members of the <see cref="T:WorkerServiceConfiguration"/> class. Retrieves values from config.
        /// </summary>
        static WorkerServiceConfiguration()
        {
            // Retrieve task spooling interval from configuration.
            WorkerServiceTimerInterval = GetConfigInt(WorkerServiceTimerIntervalKey, 5000);
            if (WorkerServiceTimerInterval < 5000)
            {
                WorkerServiceTimerInterval = 5000;
            }

            // Retrieve clean up interval from configuration.
            CleanupTimerInterval = GetConfigInt(CleanupTimerIntervalKey, 600000);
            if (CleanupTimerInterval < 600000)
            {
                CleanupTimerInterval = 600000;
            }

            // Retrieve output path from configuration.
            OutputFilePath = ConfigurationManager.AppSettings[OutputPathKey] ?? "WorkerServiceOutput";
            if (!Path.IsPathRooted(OutputFilePath))
            {
                OutputFilePath = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), OutputFilePath);
            }

            // Retrieve output file retention period in days from configuration.
            DaysOld = GetConfigInt(DeleteFilesOlderThanDaysKey, 1);

            // Retrieve maximum number of parallel worker tasks from configuration.
            MaxWorkerThreads = GetConfigInt(MaxThreadsKey, 4);

            // Retrieve max wait time for joining threads and tasks.
            ThreadJoinWaitIntervalMS = GetConfigInt(ThreadJoinWaitIntervalMSKey, 5000);

            // Retrieve the setting for the debug delay flag.
            AddDelayForDebug = GetConfigBool(AddDelayForDebugKey);

            // Retrieve the connection string for the worker service.
            WorkerServiceDBConnectionString = ConfigurationManager.ConnectionStrings[SystemDBConnectionStringKey].ConnectionString;

            // Retrieve the Error Queue table name.
            ErrorQueueName = ConfigurationManager.AppSettings[ErrorQueueNameKey];

            // Retrieve the max time to wait for all worker thread to join on stop event.
            WorkerThreadMaxWaitTime = GetConfigInt(WorkerThreadMaxWaitTimeKey, 20000);

            // Retrieve the worker thread timeout in milliseconds from configuration.
            WorkerThreadTimeoutMS = GetConfigInt(WorkerThreadTimeOutMillisecondsKey, 1800000);
        }

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets worker service timer polling interval in milliseconds.
        /// <para>
        /// The default value is 5,000 (5 seconds).
        /// </para>
        /// </summary>
        public static int WorkerServiceTimerInterval { get; private set; }

        /// <summary>
        /// Gets interval in milliseconds for running cleanup tasks.
        /// <para>
        /// The default and maximum value is 60,000 (1 minute).
        /// </para>
        /// </summary>
        public static double CleanupTimerInterval { get; private set; }

        /// <summary>
        /// Gets number of milliseconds to wait for threads to complete during an OnStop event.
        /// <para>
        /// The default value is 5,000 (5 seconds).
        /// </para>
        /// </summary>
        public static int ThreadJoinWaitIntervalMS { get; private set; }

        /// <summary>
        /// Gets the maximum amount of worker threads to use. This can be configured in the config file.
        /// <para>
        /// The default value is 4.
        /// </para>
        /// </summary>
        public static int MaxWorkerThreads { get; private set; }

        /// <summary>
        /// Gets the path to the temp location for storing output files.
        /// <para>
        /// The default value is "WorkerServiceOutput".
        /// </para>
        /// </summary>
        public static string OutputFilePath { get; private set; }

        /// <summary>
        /// Gets the number of days old to delete files.
        /// <para>
        /// The default value is 1.
        /// </para>
        /// </summary>
        public static int DaysOld { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to add delay on start up for attaching a debugger.
        /// <para>
        /// The default value is false.
        /// </para>
        /// </summary>
        public static bool AddDelayForDebug { get; private set; }

        /// <summary>
        /// Gets the connection string for the workser service database.
        /// <para>
        /// There is no default value - if the entry is missing the application will error out.
        /// </para>
        /// </summary>
        public static string WorkerServiceDBConnectionString { get; private set; }

        /// <summary>
        /// Gets the name of the Error Queue table in the database.
        /// <para>
        /// There is no default value.
        /// </para>
        /// </summary>
        public static string ErrorQueueName { get; private set; }

        /// <summary>
        /// Gets the timeout for worker threads in milliseconds.
        /// <para>
        /// The default value is 1,800,000 (30 minutes).
        /// </para>
        /// </summary>
        public static int WorkerThreadTimeoutMS { get; private set; }

        /// <summary>
        /// Gets the maximum time to wait for a task or join a thread.
        /// <para>
        /// The default value is 20,000 (20 seconds).
        /// </para>
        /// </summary>
        public static int WorkerThreadMaxWaitTime { get; private set; }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Retrieves an integer value from the configuration.
        /// </summary>
        /// <param name="key">key of the app setting.</param>
        /// <param name="defaultValue">Optional default value if unable to parse configuration value (defaults to 0).</param>
        /// <returns>The parsed value or the default.</returns>
        private static int GetConfigInt(string key, int defaultValue = 0)
        {
            int output;
            if (!int.TryParse(ConfigurationManager.AppSettings[key], out output))
            {
                output = defaultValue;
            }

            return output;
        }

        /// <summary>
        /// Retrieves an boolean value from the configuration.
        /// </summary>
        /// <param name="key">key of the app setting.</param>
        /// <param name="defaultValue">Optional default value if unable to parse configuration value (defaults to 0).</param>
        /// <returns>The parsed value or the default.</returns>
        private static bool GetConfigBool(string key, bool defaultValue = false)
        {
            bool output;
            if (!bool.TryParse(ConfigurationManager.AppSettings[key], out output))
            {
                output = defaultValue;
            }

            return output;
        }
        #endregion
    }
}