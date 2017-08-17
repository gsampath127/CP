// ***********************************************************************
// Assembly         : RRD.FSG.RP.Utilities
// Author           : 
// Created          : 11-12-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015

using System;
using System.IO;
using System.Net;
namespace RRD.FSG.RP.Utilities
{
    /// <summary>
    /// Class SFTPHelper.
    /// </summary>
    public static class SFTPHelper
    {
        #region Secure FTP

        /// <summary>
        /// SecureFTPFile
        /// </summary>
        /// <param name="dirPath">Directory for file to be uploaded to SFTP</param>
        /// <param name="fileName">File Name for file to be uploaded to SFTP</param>
        /// <param name="ftp">ftp address</param>
        /// <param name="ftpUsername">The FTP username.</param>
        /// <param name="ftpPassword">ftp Password</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool SFTPFileUpload(string dirPath, string fileName, string ftp, string ftpUsername, string ftpPassword)
        {
            //  Important: It is helpful to send the contents of the
            //  sftp.LastErrorText property when requesting support.

            Chilkat.SFtp sftp = new Chilkat.SFtp();

            //  Any string automatically begins a fully-functional 30-day trial.
            bool success;
            success = sftp.UnlockComponent("RRDONNSSH_f2gUUAaQ3CnU");
            if (success != true)
            { 
                return success;
            }

            //  Connect to the SSH server.
            //  The standard SSH port = 22
            //  The hostname may be a host name or IP address.
            int port;
            string hostname;
            hostname = ftp;
            port = 22;
            success = sftp.Connect(hostname, port);
            if (success != true)
            {
                return success;
            }

            //  Authenticate with the SSH server.  Chilkat SFTP supports
            //  both password-based authentication as well as public-key
            //  authentication.  This example uses password authentication.
            success = sftp.AuthenticatePw(ftpUsername, ftpPassword);
            if (success != true)
            {
                return success;
            }

            //  After authenticating, the SFTP subsystem must be initialized:
            success = sftp.InitializeSftp();
            if (success != true)
            {
                return success;
            }


            //  Upload from the local file to the SSH server.
            success = sftp.UploadFileByName(fileName, dirPath + fileName);
            if (success != true)
            {
                return success;
            }

            //  Close the file.
            if (success != true)
            {
                return success;
            }

            return success;


        }

        #endregion


        #region FTP

        /// <summary>
        /// FTPFileUpload
        /// </summary>       
        /// <param name="dirPath">Directory for file to be uploaded to SFTP</param>
        /// <param name="fileName">File Name for file to be uploaded to SFTP</param>
        /// <param name="ftpServerIP">ftp address</param>
        /// <param name="ftpUsername">The FTP username.</param>
        /// <param name="ftpPassword">ftp Password</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool FTPFileUpload(string dirPath, string fileName, string ftpServerIP, string ftpUsername, string ftpPassword)
        {

            bool rslt = false;
            FileInfo fileInf = new FileInfo(dirPath + fileName);
            string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;


            if (!WebRequestMethods.Ftp.ListDirectoryDetails.Contains(uri))
            {
                FtpWebRequest reqFTP;

                // Create FtpWebRequest object from the Uri provided
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                // Provide the WebPermission Credintials
                reqFTP.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                reqFTP.Timeout = -1;
                // By default KeepAlive is true, where the control connection is not closed
                // after a command is executed.
                reqFTP.KeepAlive = false;

                //reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;

                // Specify the command to be executed.
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

                // Specify the data transfer type.
                reqFTP.UseBinary = true;

                // Notify the server about the size of the uploaded file
                reqFTP.ContentLength = fileInf.Length;

                // The buffer size is set to 2kb
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;

                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                FileStream fs = fileInf.OpenRead();

                try
                {
                    // Stream to which the file to be upload is written
                    Stream strm = reqFTP.GetRequestStream();

                    // Read from the file stream 2kb at a time
                    contentLen = fs.Read(buff, 0, buffLength);

                    // Till Stream content ends
                    while (contentLen != 0)
                    {
                        // Write Content from the file stream to the FTP Upload Stream
                        strm.Write(buff, 0, contentLen);
                        contentLen = fs.Read(buff, 0, buffLength);
                    }

                    // Close the file stream and the Request Stream
                    strm.Close();
                    fs.Close();


                    rslt = true;
                }
                catch
                {
                    rslt = false;
                }
            }

            return rslt;

        }

        #endregion
    }
}
