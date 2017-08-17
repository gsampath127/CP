// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 10-27-2015
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.Factories;
using System.Collections.Specialized;

/// <summary>
/// The HostedAdmin namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin
{
    /// <summary>
    /// Class ErrorLogHandler.
    /// </summary>
    [ConfigurationElementType(typeof(CustomHandlerData))]
    public class ErrorLogHandler : IExceptionHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLogHandler"/> class.
        /// </summary>
        /// <param name="configValues">The configuration values.</param>
        public ErrorLogHandler(NameValueCollection configValues)
        {
            
        }

        /// <summary>
        /// When implemented by a class, handles an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="exception">The exception to handle.</param>
        /// <param name="handlingInstanceId">The unique ID attached to the handling chain for this handling instance.</param>
        /// <returns>Modified exception to pass to the next exceptionHandlerData in the chain.</returns>
        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            int errorCode, errorPriority, errorEventId, errorSiteActivityId;

            ErrorLogObjectModel errorLog = new ErrorLogObjectModel();

            if (exception != null && exception.Data != null && exception.Data.Count > 0)
            {
                string errorCd = exception.Data["ErrorCode"] != null ? exception.Data["ErrorCode"].ToString() : string.Empty;
                if (int.TryParse(errorCd, out errorCode))
                    errorLog.ErrorCode = errorCode;

                string errorPr = exception.Data["Priority"] != null ? exception.Data["Priority"].ToString() : string.Empty;
                if (int.TryParse(errorPr, out errorPriority))
                    errorLog.Priority = errorPriority;

                errorLog.Severity = exception.Data["Severity"] != null ? exception.Data["Severity"].ToString() : string.Empty;
                errorLog.Title = exception.Data["Title"] != null ? exception.Data["Title"].ToString() : string.Empty;
                errorLog.MachineName = exception.Data["MachineName"] != null ? exception.Data["MachineName"].ToString() : string.Empty;
                errorLog.AppDomainName = exception.Data["AppDomainName"] != null ? exception.Data["AppDomainName"].ToString() : string.Empty;
                errorLog.ProcessID = exception.Data["ProcessID"] != null ? exception.Data["ProcessID"].ToString() : string.Empty;
                errorLog.ProcessName = exception.Data["ProcessName"] != null ? exception.Data["ProcessName"].ToString() : string.Empty;
                errorLog.ThreadName = exception.Data["ThreadName"] != null ? exception.Data["ThreadName"].ToString() : string.Empty;
                errorLog.Win32ThreadId = exception.Data["Win32ThreadId"] != null ? exception.Data["Win32ThreadId"].ToString() : string.Empty;

                string eventId = exception.Data["EventId"] != null ? exception.Data["EventId"].ToString() : string.Empty;
                if (int.TryParse(eventId, out errorEventId))
                    errorLog.EventId = errorEventId;

                string activityID = exception.Data["SiteActivityId"] != null ? exception.Data["SiteActivityId"].ToString() : string.Empty;
                if (int.TryParse(activityID, out errorSiteActivityId))
                    errorLog.SiteActivityID = errorSiteActivityId;

                errorLog.Message = exception.StackTrace;
                errorLog.FormattedMessage = exception.Message;

                errorLog.URL = exception.Data["URL"] != null ? exception.Data["URL"].ToString() : string.Empty;
                errorLog.AbsoluteURL = exception.Data["AbsoluteUrl"] != null ? exception.Data["AbsoluteUrl"].ToString() : string.Empty;
            }
            
            ErrorLogFactory factory = new ErrorLogFactory(new RRD.DSA.Core.DAL.DataAccess());
            factory.SaveEntity(errorLog);

            return exception;
        }
    }
}