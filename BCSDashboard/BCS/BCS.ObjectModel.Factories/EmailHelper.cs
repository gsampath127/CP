
// ***********************************************************************
// Assembly         : RRD.FSG.RP.Utilities
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace BCS.ObjectModel.Factories
{
    /// <summary>
    /// Class EmailHelper.
    /// </summary>
    public static class EmailHelper
    {
        #region Send Email
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="emailFrom">The email from.</param>
        /// <param name="emailTo">The email to.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="emailCC">The email cc.</param>
        /// <param name="emailBCC">The email BCC.</param>
        /// <param name="attachedFile">The attached file.</param>
        /// <param name="fromDisplayName">From display name.</param>
        /// <param name="toDisplayName">To display name.</param>
        public static void SendEmail(string emailFrom, string emailTo, string subject, string body, string emailCC = null, string emailBCC = null, string attachedFile = null, string fromDisplayName = null, string toDisplayName = null)
        {
            MailMessage message = new MailMessage();
            string strToEmail = emailTo;
            string strFromEmail = emailFrom;            

            message.From = new MailAddress(strFromEmail, fromDisplayName);

            foreach (string email in strToEmail.Split(','))
            {
                message.To.Add(new MailAddress(email, toDisplayName));
            }

            if (!string.IsNullOrWhiteSpace(emailCC))
            {
                message.CC.Add(emailCC);
            }
            if (!string.IsNullOrWhiteSpace(emailBCC))
            {
                message.Bcc.Add(emailBCC);
            }
            if (!string.IsNullOrWhiteSpace(attachedFile))
            {
                Attachment fileAttachment = new Attachment(attachedFile);
                message.Attachments.Add(fileAttachment);
            }
           
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            //We have changed the email to alternate View instead of the Main body view.This was useful for handling bounced emails.

            using (AlternateView alternateview =
                AlternateView.CreateAlternateViewFromString(
                    body,
                    message.BodyEncoding,
                    message.IsBodyHtml ? "text/html" : null))
            {
                alternateview.TransferEncoding =
                    TransferEncoding.SevenBit;
                message.AlternateViews.Add(alternateview);
                
                SmtpClient client = new SmtpClient(ConfigValues.SMTP);
                client.UseDefaultCredentials = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = CredentialCache.DefaultNetworkCredentials;

                client.Send(message);                
            }
        }

        /// <summary>
        /// Encodes the to64.
        /// </summary>
        /// <param name="toEncode">To encode.</param>
        /// <returns>System.String.</returns>
        static public string EncodePassword(string toEncode)
        {
            return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(toEncode));

        }
        /// <summary>
        /// Decodes the from64.
        /// </summary>
        /// <param name="encodedData">The encoded data.</param>
        /// <returns>System.String.</returns>
        static public string DecodePassword(string encodedData)
        {
              return ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(encodedData));
        }
        /// <summary>
        /// Sends the email with byte array.
        /// </summary>
        /// <param name="emailFrom">The email from.</param>
        /// <param name="emailTo">The email to.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="result">The result.</param>
        /// <param name="attachFileFormat">The attach file format.</param>
        /// <param name="emailCC">The email cc.</param>
        /// <param name="emailBCC">The email BCC.</param>
        public static void SendEmailwithByteArray(string emailFrom, string emailTo, string subject, string body,  byte[] result, string attachFileFormat,string emailCC = null, string emailBCC = null)
        {
            Attachment attachment = null;
            using (MailMessage mail = new MailMessage())
            {
                //Setting up general email parameters.
                mail.From = new MailAddress(emailFrom);

                if (!string.IsNullOrEmpty(emailTo))
                    mail.To.Add(emailTo.Replace(";", ","));

                if (!string.IsNullOrEmpty(emailCC))
                    mail.CC.Add(emailCC.Replace(";", ","));

                if (!string.IsNullOrEmpty(emailBCC))
                    mail.Bcc.Add(emailBCC.Replace(";", ","));

                mail.Subject = subject;
                mail.Body = body;

                try
                {


                    if (result != null && !string.IsNullOrEmpty(attachFileFormat))
                    {
                        //Add attachment from byte stream.
                        using (MemoryStream stream = new MemoryStream(result))
                        {
                            if (stream.Length > 0)
                            {
                                attachment = new Attachment(stream, attachFileFormat);
                                if (attachment != null)
                                    mail.Attachments.Add(attachment);
                            }
                            //Sent out email.
                            using (SmtpClient smtp = new SmtpClient(ConfigValues.SMTP))
                            {
                                smtp.Send(mail);
                            }
                        }
                    }
                    else
                    {
                        using (SmtpClient smtp = new SmtpClient(ConfigValues.SMTP))
                        {
                            smtp.Send(mail);
                        }
                    }
                }
                catch (SmtpException)
                {

                    throw;
                }

            }            
                                       
            }               
        #endregion
    }
}
