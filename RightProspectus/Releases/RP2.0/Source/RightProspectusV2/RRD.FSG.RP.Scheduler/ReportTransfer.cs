// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 11-09-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Scheduler.Interfaces;
using RRD.FSG.RP.Utilities;

namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// Class ReportTransfer.
    /// </summary>
    public class ReportTransfer
    {
        /// <summary>
        /// Email
        /// </summary>
        /// <param name="reportContentObjectModel">The report content object model.</param>
        /// <param name="entry">The entry.</param>
        public void Process(ReportContentObjectModel reportContentObjectModel, IReportScheduleEntry entry)
        {
            if (!string.IsNullOrEmpty(entry.Email))
            {
                if (reportContentObjectModel.IsAttachedZipFile)
                {
                    EmailHelper.SendEmail(ConfigValues.ReportContentEmailFrom.ToString(), entry.Email, reportContentObjectModel.EmailSubject, reportContentObjectModel.EmailBody, "", "", reportContentObjectModel.ContentUri + reportContentObjectModel.FileName);
                }
                else
                {
                    EmailHelper.SendEmailwithByteArray(ConfigValues.ReportContentEmailFrom.ToString(), entry.Email, reportContentObjectModel.EmailSubject, reportContentObjectModel.EmailBody, reportContentObjectModel.Data, reportContentObjectModel.FileName, null, null);
                }
            }
            else
            {
                //FTPHelper to send FTP requests                
                SFTPHelper.SecureFTPFile(reportContentObjectModel.ContentUri, reportContentObjectModel.FileName, entry.FTPServerIP, entry.FTPUsername, EmailHelper.DecodePassword(entry.FTPPassword));
            }
           
        }
    }
}
