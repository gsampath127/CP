// ***********************************************************************
// Assembly         : RRD.FSG.RP.Utilities
// Author           : 
// Created          : 11-12-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015

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
        public static bool SecureFTPFile(string dirPath, string fileName, string ftp, string ftpUsername, string ftpPassword)
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
    }
}
