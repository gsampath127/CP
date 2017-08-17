﻿// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 11-13-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
using System;
using System.Configuration;
using System.Globalization;

namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// Class WorkerThreadTimeoutException.
    /// </summary>
    public class WorkerThreadTimeoutException : ApplicationException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerThreadTimeoutException" /> class.
        /// </summary>
        public WorkerThreadTimeoutException()
            : base("Report generation aborted: exceeded the " +
                "allowed " + TimeoutInSeconds + " second timeout.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerThreadTimeoutException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public WorkerThreadTimeoutException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerThreadTimeoutException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">Inner exception to wrap with this exception.</param>
        public WorkerThreadTimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        
        #endregion

        #region Properties

        /// <summary>
        /// Gets the seconds timeout that was exceeded to cause this exception.
        /// </summary>
        /// <value>The timeout in seconds.</value>
        private static int TimeoutInSeconds
        {
            get
            {
                int millisecondTimeout = 180000;
                if (ConfigurationManager.AppSettings["WorkerThreadTimeOutMilliseconds"] != null)
                {
                    millisecondTimeout = int.Parse(ConfigurationManager.AppSettings["WorkerThreadTimeOutMilliseconds"], CultureInfo.InvariantCulture);
                }

                return millisecondTimeout / 1000;
            }
        }

        #endregion
    }
}