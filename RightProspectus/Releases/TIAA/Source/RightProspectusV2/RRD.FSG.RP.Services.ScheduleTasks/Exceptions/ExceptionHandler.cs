﻿using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Services.ScheduleTasks
{
    /// <summary>
    /// Wrapper for Microsoft ExceptionHandling Application Block
    /// </summary>
    public static class ExceptionHandler
    {
        #region Enumerations

        /// <summary>
        /// Enumeration depicting different policy types used for logging exceptions.
        /// </summary>
        public enum ExceptionPolicyType
        {
            WebSite,
            WebService,
            WorkerService,
            WorkerServiceProcess,
            Thread,
            QueueListener,
            ClassLibrary,
            Default
        }

        #endregion

        #region Methods

        /// <summary>
        /// Used for handling worker service process exceptions in Tasks.
        /// </summary>
        /// <param name="faultedTask">Task that completed with an exception.</param>
        public static void FaultedWorkserServiceProcessTaskHandler(Task faultedTask)
        {
            foreach (Exception exception in faultedTask.Exception.Flatten().InnerExceptions)
            {
                HandleException(exception, ExceptionPolicyType.WorkerServiceProcess);
            }
        }

        /// <summary>
        /// Used for handling worker service exceptions in Tasks.
        /// </summary>
        /// <param name="faultedTask">Task that completed with an exception.</param>
        public static void FaultedWorkerServiceTaskHandler(Task faultedTask)
        {
            foreach (Exception exception in faultedTask.Exception.Flatten().InnerExceptions)
            {
                HandleException(exception, ExceptionPolicyType.WorkerService);
            }
        }

        /// <summary>
        /// Used for handling class library exceptions in Tasks.
        /// </summary>
        /// <param name="faultedTask">Task that completed with an exception.</param>
        public static void FaultedClassLibraryTaskHandler(Task faultedTask)
        {
            foreach (Exception exception in faultedTask.Exception.Flatten().InnerExceptions)
            {
                HandleException(exception, ExceptionPolicyType.ClassLibrary);
            }
        }

        /// <summary>
        /// Used for handling exceptions in a web site.
        /// </summary>
        /// <param name="ex">The Exception</param>
        public static void HandleWebsiteException(Exception ex)
        {
            HandleException(ex, ExceptionPolicyType.WebSite);
        }

        /// <summary>
        /// Used for handling exceptions in a web service.
        /// </summary>
        /// <param name="ex">The Exception</param>
        public static void HandleWebServiceException(Exception ex)
        {
            HandleException(ex, ExceptionPolicyType.WebService);
        }

        /// <summary>
        /// Used for handling exceptions in a windows service which affect the operation of the service.
        /// </summary>
        /// <param name="ex">The Exception</param>
        public static void HandleWorkerServiceException(Exception ex)
        {
            HandleException(ex, ExceptionPolicyType.WorkerService);
        }

        /// <summary>
        /// Used for handling exceptions in a windows service which only affect the operation of a particular thread/process.
        /// </summary>
        /// <param name="ex">The Exception</param>
        public static void HandleWorkerServiceProcessException(Exception ex)
        {
            HandleException(ex, ExceptionPolicyType.WorkerServiceProcess);
        }

        /// <summary>
        /// Used for handling exceptions caused by aborting of a thread.
        /// </summary>
        /// <param name="ex">The Exception</param>
        public static void HandleThreadException(Exception ex)
        {
            HandleException(ex, ExceptionPolicyType.Thread);
        }

        /// <summary>
        /// Used for handling exceptions when a QueueListener can't be contacted.
        /// </summary>
        /// <param name="ex">The Exception</param>
        public static void HandleQueueListenerException(Exception ex)
        {
            HandleException(ex, ExceptionPolicyType.QueueListener);
        }

        /// <summary>
        /// Used for handling exceptions.
        /// </summary>
        /// <param name="ex">The Exception</param>
        /// <param name="policyType">Type of exception that is being handled.</param>
        public static void HandleException(Exception ex, ExceptionPolicyType policyType)
        {
            string policyName = "Default Policy";
            switch (policyType)
            {
                case ExceptionPolicyType.WebSite:
                    policyName = "Web Site Policy";
                    break;
                case ExceptionPolicyType.WebService:
                    policyName = "Web Service Policy";
                    break;
                case ExceptionPolicyType.WorkerService:
                    policyName = "Worker Service Policy";
                    break;
                case ExceptionPolicyType.WorkerServiceProcess:
                    policyName = "Worker Service Process Policy";
                    break;
                case ExceptionPolicyType.Thread:
                    policyName = "Thread Policy";
                    break;
                case ExceptionPolicyType.QueueListener:
                    policyName = "Queue Listener Policy";
                    break;
                case ExceptionPolicyType.ClassLibrary:
                    policyName = "Class Library Policy";
                    break;
            }
            HandleException(ex, policyName);
        }

        #endregion

        /// <summary>
        /// Handles exceptions of all types.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="policyName">Name of the policy.</param>
        private static void HandleException(Exception ex, string policyName)
        {
            bool rethrow = false;
            try
            {
                rethrow = ExceptionPolicy.HandleException(ex, policyName);
            }
            catch (Exception loggingEx)
            {
                try
                {
                    EventLog.WriteEntry(AppDomain.CurrentDomain.ApplicationIdentity.FullName, loggingEx.ToString());
                }
                catch { }
            }

            if (rethrow)
            {
                throw ex;
            }
        }
    }
}
