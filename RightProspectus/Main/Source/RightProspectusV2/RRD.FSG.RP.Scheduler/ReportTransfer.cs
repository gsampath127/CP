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
using System;

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
            if (!reportContentObjectModel.DoNotSendReport)
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
                else if (entry.IsSFTP)
                {
                    //FTPHelper to send SFTP requests                
                    SFTPHelper.SFTPFileUpload(reportContentObjectModel.FileDir, reportContentObjectModel.FileName, entry.FTPServerIP, entry.FTPUsername, EmailHelper.DecodePassword(entry.FTPPassword));
                }
                else
                {
                    //FTPHelper to send FTP requests                
                    SFTPHelper.FTPFileUpload(reportContentObjectModel.FileDir, reportContentObjectModel.FileName, entry.FTPServerIP, entry.FTPUsername, EmailHelper.DecodePassword(entry.FTPPassword));
                }
            }
            else 
            {
                if (!(string.IsNullOrWhiteSpace(entry.ErrorEmailSub) || string.IsNullOrWhiteSpace(entry.ErrorEmailSub)))
                {
                    string errorEmailTemplate = "";
                    if (!string.IsNullOrWhiteSpace(entry.ErrorEmailTemplate))
                    {
                        errorEmailTemplate = entry.ErrorEmailTemplate.Replace("<<clientname>>", entry.ClientName).Replace("<<errordate>>", DateTime.Now.AddHours(1).ToString()); 
                    }

                    string errorEmailSub = entry.ErrorEmailSub.Replace("<<clientname>>", entry.ClientName);
                    EmailHelper.SendEmail(ConfigValues.ReportContentEmailFrom.ToString(), entry.ErrorEmail, errorEmailSub, errorEmailTemplate, "", "", null, "Support", null);
                }
            }
           
        }
    }
}
