using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class EmailUtility
    {
        #region Properties
        /// <summary>
        /// The SMTP server
        /// </summary>
        private static string SMTP = ConfigurationManager.AppSettings["SMTP"];
        private static string From = ConfigurationManager.AppSettings["From"];
        #endregion

        #region SendMail
        /// <summary>
        /// Method to send mail
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static bool SendMail(string To, string subject, string body)
        {
            return SendMail(string.Empty, To, string.Empty, body, subject, string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Method to send mail.
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <param name="CC"></param>
        /// <param name="BCC"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="FromName"></param>
        /// <param name="ToName"></param>
        /// <returns>A string value of successful or the exception</returns>
        public static bool SendMail(string FromName, string To, string ToName, string body, string subject, string fileAttachment, string CC, string BCC)
        {
            string[] toAddr;
            string[] ccAddr;
            string[] BccAddr;
            MailMessage msg = new MailMessage();
            MailAddress fromAddress = null;
            MailAddress to = null;
            MailAddress cc = null;
            MailAddress Bcc = null;
            try
            {
                if (!String.IsNullOrEmpty(FromName))
                {
                    fromAddress = new MailAddress(From, FromName);
                }
                else
                {
                    fromAddress = new MailAddress(From, "");
                }

                msg.From = fromAddress;

                toAddr = To.Split(';');
                foreach (var addr in toAddr)
                {
                    if (!String.IsNullOrEmpty(ToName))
                    {
                        to = new MailAddress(addr, ToName);
                    }
                    else
                    {
                        to = new MailAddress(addr, "");
                    }
                    msg.To.Add(to);
                }


                if (!String.IsNullOrEmpty(CC))
                {
                    ccAddr = CC.Split(';');
                    foreach (var addrcc in ccAddr)
                    {
                        cc = new MailAddress(addrcc, "");
                        msg.CC.Add(cc);
                    }
                }

                if (!String.IsNullOrEmpty(BCC))
                {
                    BccAddr = BCC.Split(';');
                    foreach (var addrbcc in BccAddr)
                    {
                        Bcc = new MailAddress(addrbcc, "");
                        msg.Bcc.Add(Bcc);
                    }
                }

                msg.Subject = subject;
                msg.IsBodyHtml = true;
                msg.Body = body;

                if (!String.IsNullOrEmpty(fileAttachment))
                {
                    foreach (string f in fileAttachment.Split('|'))
                    {
                        Attachment data = new Attachment(f, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(f);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(f);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(f);
                        msg.Attachments.Add(data);
                    }
                }

                SmtpClient smtpClient = new SmtpClient(SMTP);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;

                smtpClient.Send(msg);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
