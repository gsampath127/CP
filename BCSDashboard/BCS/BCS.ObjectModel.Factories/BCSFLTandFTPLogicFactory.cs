using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;
using BCS.ObjectModel;
using BCS.Core.DAL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Net;
using System.Text.RegularExpressions;
using BCS.ObjectModel.Factories.DocumentBoardingService;
using System.IO;
using Chilkat;
using System.Data.SqlClient;

namespace BCS.ObjectModel.Factories
{
    public class BCSFLTandFTPLogicFactory
    {

        private const string SPGetFileNameForProsID = "OnGoing_GetProsDocURL";


        private const string DBPath = "Path";
        private const string DBCompany = "Company";
        private const string DBOriginalFileName = "OriginalFileName";
        private const string DBIsDuplicate = "IsDuplicate";
        private const string DBIsDocUploadReady = "IsDocUploadReady";
        private const string DBDate = "Date";
        private const string DBFUNDDocName = "FUNDDocName";
        private const string DBDatePDFReceivedonFTP = "DatePDFReceivedonFTP";
        private const string DBBCSTRPFLTID = "BCSTRPFLTID";
        private const string DBID = "ID";
        private const string DBFUNDCUSIPNUMBER = "FUNDCUSIPNUMBER";
        private const string DBBCSFLTTableDetails = "BCSFLTTableDetails";
        private const string DBFLTFileName = "FLTFileName";
        private const string DBFLTDateReceived = "FLTDateReceived";
        private const string DBProsDocTypeId = "ProsDocTypeId";
        private const string DBProsID = "ProsID";
        private const string DBProsIDs = "ProsIDs";
        private const string DBSetDocRelation = "SetDocRelation";
        private const string DBIsPDFWorkDone = "IsPDFWorkDone";
        private const string DBCompanyID = "CompanyID";
        private const string DBBookMark = "BookMark";        
        private const string DBIsFTPDocument = "IsFTPDocument";
        private const string DBPageCount = "PageCount";
        private const string DBPageHeight = "PageHeight";
        private const string DBPageWidth = "PageWidth";
        private const string DBRenamedFileName = "RenamedFileName";
        

        private readonly string DB1029ConnectionString;
        private readonly string ReadOnlyDB1029ConnectionString;
        private readonly string HostedAdminConnectionString;    
        private readonly IDataAccess dataAccess;



        public BCSFLTandFTPLogicFactory()
        {
            this.DB1029ConnectionString = DBConnectionString.DB1029ConnectionString();
            this.ReadOnlyDB1029ConnectionString = DBConnectionString.ReadOnlyDB1029Connection();
            this.HostedAdminConnectionString = DBConnectionString.HostedAdminConnectionString();
            this.dataAccess = new DataAccess();
        }


        #region FLT SFTP Logic

        public bool GetFLTFileDownloadArchiveAndDelete(BCSClient bcsclient)
        {

            Chilkat.SFtp sftp = new Chilkat.SFtp();

            //  Original Chilkat Key
            bool success;
            success = sftp.UnlockComponent("BOWNECSSH_R0FULnWLmZnG");
            if (success != true)
            {
                Console.WriteLine(sftp.LastErrorText);
                return success;
            }

            //  Set some timeouts, in milliseconds:
            sftp.ConnectTimeoutMs = 5000;
            sftp.IdleTimeoutMs = 10000;

            //  Connect to the SSH server.
            //  The standard SSH port = 22
            //  The hostname may be a hostname or IP address.
            int port;
            string hostname;
            hostname = bcsclient.FLTPickupFTPPath;
            port = 22;
            success = sftp.Connect(hostname, port);
            if (success != true)
            {

                return success;
            }

            //  Authenticate with the SSH server.  Chilkat SFTP supports
            //  both password-based authenication as well as public-key
            //  authentication.  This example uses password authenication.
            success = sftp.AuthenticatePw(bcsclient.FLTPickupFTPUserName, bcsclient.FLTPickupFTPPassword);
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

            //  Open a directory on the server...
            //  Paths starting with a slash are "absolute", and are relative
            //  to the root of the file system. Names starting with any other
            //  character are relative to the user's default directory (home directory).
            //  A path component of ".." refers to the parent directory,
            //  and "." refers to the current directory.
            string handle;
            handle = sftp.OpenDir(ConfigValues.FLTFilePath);
            if (handle == null)
            {

                return success;
            }

            //  Download the directory listing:
            Chilkat.SFtpDir dirListing = null;
            dirListing = sftp.ReadDir(handle);
            if (dirListing == null)
            {

                return success;
            }

            //  Iterate over the files.
            int i;
            int n = dirListing.NumFilesAndDirs;
            if (n == 0)
            {
                //  Close the directory
                success = sftp.CloseHandle(handle);
                if (success != true)
                {
                    Console.WriteLine(sftp.LastErrorText);
                    return success;
                }

                sftp.Disconnect();

                return success;
            }
            else
            {
                for (i = 0; i <= n - 1; i++)
                {
                    try
                    {
                        Chilkat.SFtpFile fileObj = null;
                        fileObj = dirListing.GetFileObject(i);                      
                        

                        //success = DownloadFLTFilesUsingSFTP(bcsclient, fileObj.Filename);

                        //DownloadFLTFilesUsingSFTP

                        handle = sftp.OpenFile("/" + ConfigValues.FLTFilePath + "/" + fileObj.Filename, "readOnly", "openExisting");
                        if (handle == null)
                        {

                            return success;
                        }

                        //Rename the duplicate if any in local path
                        if (File.Exists(bcsclient.FLTArchiveDropPath + fileObj.Filename))
                        {

                            File.Copy(bcsclient.FLTArchiveDropPath + fileObj.Filename, bcsclient.FLTArchiveDropPath + Path.GetFileNameWithoutExtension(fileObj.Filename) + DateTime.Now.ToString("_yyyymmddhhmmss") + ".txt");
                            File.Delete(bcsclient.FLTArchiveDropPath + fileObj.Filename);
                        }

                        //  Download the file:
                        try
                        {
                            success = sftp.DownloadFile(handle, bcsclient.FLTArchiveDropPath + fileObj.Filename);
                        }
                        catch (Exception expt)
                        {

                            string ErrorEmailBody = "Download of FTP file " + fileObj.Filename + "failed for " + bcsclient.ClientName;
                            Logging.LogToFile(ErrorEmailBody + expt.Message);

                            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                            {
                                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                            }
                            return false;
                        }
                      

                        //  Close the directory
                        success = sftp.CloseHandle(handle);
                        if (success != true)
                        {
                            Console.WriteLine(sftp.LastErrorText);
                            return success;
                        }

                        //DownloadFLTFilesUsingSFTP
                        DateTime DateReceived = System.DateTime.Now.Date;

                        if (success == true)
                        {
                            //process the FLT file
                            ProcessFLTFile(bcsclient, bcsclient.FLTArchiveDropPath + fileObj.Filename);

                            InsertintoFLTFileTrackingTable(fileObj.Filename, bcsclient, DateReceived);

                            //delete the file right here
                            //success = DeleteFLTFileUsingSFTP(fileObj.Filename, bcsclient);

                            //DeleteFLTFileUsingSFTP

                            //  Delete a file on the server:

                            success = sftp.RemoveFile("/" + ConfigValues.FLTFilePath + "/" + fileObj.Filename);

                            //DeleteFLTFileUsingSFTP

                            if (success != true)
                            {

                                string ErrorEmailBody = "Could not delete FLT file from FTP Host" + fileObj.Filename;
                                Logging.LogToFile(ErrorEmailBody);

                                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                                {
                                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                                }
                            }

                            
                            //Check if the FLT file was valid or not
                            //If valid sent email saying receival
                            //If invalid sent email saying rejected.
                            //Using a boolean value for this condition.

                            string emailString = "<html><head><title></title></head><body>";
                            emailString = emailString + @"<br /><font size=""5"" color=""#666666"" face=""Arial, Helvetica, sans-serif"">" + "BCS " + bcsclient.ClientName + " Funds Literal Table (FLT) file FTP Alert</font><br /><br />";
                            emailString = emailString + "List of Document(s) received on " + DateTime.Now.ToString() + ":<hr />" + fileObj.Filename + "<hr/>";

                            emailString = emailString + "</body></html>";

                            Logging.LogToFile(emailString);

                            foreach (string eml in ConfigValues.FLTConfirmationEmailListTo.Split(';'))
                            {
                                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, "BCS " + bcsclient.ClientName + " Funds Literal Table (FLT) File Alert", emailString, "Support", null);
                            }
                        }                        

                        
                    }
                    catch (Exception expt)
                    {

                        string ErrorEmailBody = "Error in GetFLTFileDownloadArchiveAndDelete for " + bcsclient.ClientName;
                        Logging.LogToFile(ErrorEmailBody + expt.Message);

                        foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                        }
                    }

                }

            }            

            sftp.Disconnect();

            return success;
        }

        static bool DownloadFLTFilesUsingSFTP(BCSClient bcsclient,  string FLTFile)
        {
            //  Important: It is helpful to send the contents of the
            //  sftp.LastErrorText property when requesting support.

            Chilkat.SFtp sftp = new Chilkat.SFtp();

            //  Original Chilkat Key
            bool success;
            try
            {
                success = sftp.UnlockComponent("BOWNECSSH_R0FULnWLmZnG");
                if (success != true)
                {

                    return success;
                }

                //  Set some timeouts, in milliseconds:
                sftp.ConnectTimeoutMs = 5000;
                sftp.IdleTimeoutMs = 10000;

                //  Connect to the SSH server.
                //  The standard SSH port = 22
                //  The hostname may be a hostname or IP address.
                int port;
                string hostname;
                hostname = bcsclient.FLTPickupFTPPath;
                port = 22;
                success = sftp.Connect(hostname, port);
                if (success != true)
                {

                    return success;
                }

                //  Authenticate with the SSH server.  Chilkat SFTP supports
                //  both password-based authenication as well as public-key
                //  authentication.  This example us;es password authenication.
                success = sftp.AuthenticatePw(bcsclient.FLTPickupFTPUserName, bcsclient.FLTPickupFTPPassword);
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

                //  Open a file on the server:
                string handle;
                handle = sftp.OpenFile("/" + ConfigValues.FLTFilePath + "/" + FLTFile, "readOnly", "openExisting");
                if (handle == null)
                {

                    return success;
                }

                //Rename the duplicate if any in local path
                if (File.Exists(bcsclient.FLTArchiveDropPath + FLTFile))
                {

                    File.Copy(bcsclient.FLTArchiveDropPath + FLTFile, bcsclient.FLTArchiveDropPath + Path.GetFileNameWithoutExtension(FLTFile) + DateTime.Now.ToString("_yyyymmddhhmmss") + ".txt");
                    File.Delete(bcsclient.FLTArchiveDropPath + FLTFile);
                }

                //  Download the file:
                try
                {
                    success = sftp.DownloadFile(handle, bcsclient.FLTArchiveDropPath + FLTFile);
                }
                catch (Exception expt)
                {

                    string ErrorEmailBody = "Download of FTP file " + FLTFile + "failed for " + bcsclient.ClientName;
                    Logging.LogToFile(ErrorEmailBody + expt.Message);

                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                    }
                    return false;
                }

                //  Close the file.
                success = sftp.CloseHandle(handle);
                
            }
            catch (Exception expt)
            {

                string ErrorEmailBody = "Download of FTP file " + FLTFile + "failed for " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
                return false;
            }

            return success;    
        }

        public static bool DeleteFLTFileUsingSFTP(string File, BCSClient bcsclient)
        {
            //  Important: It is helpful to send the contents of the
            //  sftp.LastErrorText property when requesting support.

            Chilkat.SFtp sftp = new Chilkat.SFtp();

            //  Any string automatically begins a fully-functional 30-day trial.
            bool success;
            try
            {
                success = sftp.UnlockComponent("BOWNECSSH_R0FULnWLmZnG");
                if (success != true)
                {

                    return success;
                }

                //  Set some timeouts, in milliseconds:
                sftp.ConnectTimeoutMs = 5000;
                sftp.IdleTimeoutMs = 10000;

                //  Connect to the SSH server.
                //  The standard SSH port = 22
                //  The hostname may be a hostname or IP address.
                int port;
                string hostname;
                hostname = bcsclient.FLTPickupFTPPath;
                port = 22;
                success = sftp.Connect(hostname, port);
                if (success != true)
                {

                    return success;
                }

                //  Authenticate with the SSH server.  Chilkat SFTP supports
                //  both password-based authenication as well as public-key
                //  authentication.  This example uses password authenication.
                success = sftp.AuthenticatePw(bcsclient.FLTPickupFTPUserName, bcsclient.FLTPickupFTPPassword);
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

                //  Delete a file on the server:

                success = sftp.RemoveFile("/" + ConfigValues.FLTFilePath + "/" + File);
            }
            catch (Exception expt)
            {

                string ErrorEmailBody = "Delete of FLT file " + File + "failed for " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
                return false;
            }

            return success;
        }

        #endregion

        #region FTP SFTP Logic

        public bool GetFTPFileDownloadArchiveAndDelete(BCSClient bcsclient)
        {

            Chilkat.SFtp sftp = new Chilkat.SFtp();
            string attachmentFilesMessage = string.Empty;
            int attachmentcount = 0;
            string DestinationFTPFileName = String.Empty;
            UploadDocument uploadDocument = new UploadDocument();

            //  Original Chilkat Key
            bool success;
            success = sftp.UnlockComponent("BOWNECSSH_R0FULnWLmZnG");
            if (success != true)
            {
                Console.WriteLine(sftp.LastErrorText);
                return success;
            }

            //  Set some timeouts, in milliseconds:
            sftp.ConnectTimeoutMs = 15000;
            sftp.IdleTimeoutMs = 15000;

            //  Connect to the SSH server.
            //  The standard SSH port = 22
            //  The hostname may be a hostname or IP address.
            int port;
            string hostname;
            hostname = bcsclient.ClientDocsFTPPath;
            port = 22;
            success = sftp.Connect(hostname, port);
            if (success != true)
            {

                return success;
            }

            //  Authenticate with the SSH server.  Chilkat SFTP supports
            //  both password-based authenication as well as public-key
            //  authentication.  This example uses password authenication.
            success = sftp.AuthenticatePw(bcsclient.ClientDocsFTPUserName, bcsclient.ClientDocsFTPPassword);
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

            //  Open a directory on the server...
            //  Paths starting with a slash are "absolute", and are relative
            //  to the root of the file system. Names starting with any other
            //  character are relative to the user's default directory (home directory).
            //  A path component of ".." refers to the parent directory,
            //  and "." refers to the current directory.
            string handle;
            handle = sftp.OpenDir(ConfigValues.FTPFilePath);
            if (handle == null)
            {

                return success;
            }

            //  Download the directory listing:
            Chilkat.SFtpDir dirListing = null;
            dirListing = sftp.ReadDir(handle);
            if (dirListing == null)
            {

                return success;
            }

            //  Iterate over the files.
            int i;
            int n = dirListing.NumFilesAndDirs;
            if (n == 0)
            {
                //Close handle when the count is 0

                //  Close the directory
                success = sftp.CloseHandle(handle);
                if (success != true)
                {
                    Console.WriteLine(sftp.LastErrorText);
                    return success;
                }

                sftp.Disconnect();

                return success;
            }
            else
            {
                //Create archive path 
                string FTPDropDirectory = String.Empty;
                FTPDropDirectory = bcsclient.FTPDocArchiveDropPath;                


                if (!Directory.Exists(FTPDropDirectory + DateTime.Now.Year.ToString() + "\\"))
                    Directory.CreateDirectory(FTPDropDirectory + DateTime.Now.Year.ToString() + "\\");

                if (!Directory.Exists(FTPDropDirectory + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\"))
                    Directory.CreateDirectory(FTPDropDirectory + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\");

                if (!Directory.Exists(FTPDropDirectory + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString() + "\\"))
                    Directory.CreateDirectory(FTPDropDirectory + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString() + "\\");

                string FTPDropArchivePath = FTPDropDirectory + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString() + "\\";


                for (i = 0; i <= n - 1; i++)
                {
                    try
                    {
                        Chilkat.SFtpFile fileObj = null;
                        fileObj = dirListing.GetFileObject(i);

                        attachmentFilesMessage = attachmentFilesMessage + "<br />" + fileObj.Filename;
                        attachmentcount++;


                        //success = DownloadFTPFilesUsingSFTP(bcsclient, fileObj.Filename, FTPDropArchivePath); 
                        //DownloadFTPFilesUsingSFTP

                        handle = sftp.OpenFile("/" + ConfigValues.FTPFilePath + "/" + fileObj.Filename, "readOnly", "openExisting");
                        if (handle == null)
                        {

                            return success;
                        }

                        //Archive all files coming in inlcuding pdf files and other extensions
                        //If duplicate file exists then rename and store the copy in the list

                        if (File.Exists(FTPDropArchivePath + fileObj.Filename))
                        {
                            DestinationFTPFileName = Path.GetFileNameWithoutExtension(fileObj.Filename) + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                            uploadDocument.IsDuplicate = true;
                        }
                        else
                        {
                            DestinationFTPFileName = fileObj.Filename;
                            uploadDocument.IsDuplicate = false;
                        }
                        uploadDocument.Path = FTPDropArchivePath + DestinationFTPFileName;
                        uploadDocument.OriginalFileName = DestinationFTPFileName;
                        uploadDocument.IsDocUploadReady = false;
                        uploadDocument.Date = DateTime.Now;

                        try
                        {

                            //  Download the file:
                            success = sftp.DownloadFile(handle, uploadDocument.Path);
                        }
                        catch (Exception expt)
                        {

                            string ErrorEmailBody = "Download of FTP file " + fileObj.Filename + "failed for " + bcsclient.ClientName;
                            Logging.LogToFile(ErrorEmailBody + expt.Message);

                            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                            {
                                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                            }
                        }

                        //  Close the directory
                        success = sftp.CloseHandle(handle);
                        if (success != true)
                        {
                            Console.WriteLine(sftp.LastErrorText);
                            return success;
                        }                      


                        //DownloadFTPFilesUsingSFTP

                        if (success == true)
                        {
                            //Update the downloaded file data into database here.
                            InsertFTPDocumentInfoInBCSWorkingTable(uploadDocument, bcsclient);
                            
                           
                            //delete the file right here
                            //success = DeleteFTPFileUsingSFTP(fileObj.Filename, bcsclient);

                            //  Delete a file on the server:
                            // DeleteFTPFileUsingSFTP

                            success = sftp.RemoveFile("/" + ConfigValues.FTPFilePath + "/" + fileObj.Filename);

                            //DeleteFTPFileUsingSFTP

                            if (success != true)
                            {

                                string ErrorEmailBody = "Could not delete FTP file from FTP Host" + fileObj.Filename;
                                Logging.LogToFile(ErrorEmailBody);

                                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                                {
                                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                                }
                            }
                            
                        }


                    }
                    catch (Exception expt)
                    {

                        string ErrorEmailBody = "Error in GetFLTFileDownloadArchiveAndDelete for " + bcsclient.ClientName;
                        Logging.LogToFile(ErrorEmailBody + expt.Message);

                        foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                        }
                    }

                }
                string EmailSubject = String.Empty;
                string EmailHeader = String.Empty;

                if (bcsclient.ClientName == "TRP")
                {
                    EmailSubject = ConfigValues.TRPCompany + " - " + ConfigValues.BCSExceptionEmailSub + " - " + ConfigValues.AppEnvironment;
                    EmailHeader = ConfigValues.TRPCompany + " FTP Receipt";
                }

                string emailString = "<html><head><title></title></head><body>";
                emailString = emailString + @"<br /><font size=""5"" color=""#666666"" face=""Arial, Helvetica, sans-serif"">" + EmailHeader + "</font><br /><br />";
                emailString = emailString + "List of Document(s) received on " + GetNextRun(DateTime.Now, "EST") + " EST :<hr />" + "PDFs Count:" + attachmentcount + "<br /><br />" + attachmentFilesMessage + "<hr/><br />";
                emailString = emailString + @"<font size=""2"" color=""#0000FF"" face=""Arial, Helvetica, sans-serif"">  RR Donnelley </font><br />";
                emailString = emailString + @"<font size=""2"" color=""#0000FF"" face=""Arial, Helvetica, sans-serif"">  I/T RightProspectus  </font><br /><br />";
                emailString = emailString + @"<font size=""2"" color=""#0000FF"" face=""Arial, Helvetica, sans-serif"">  THIS MESSAGE HAS BEEN SENT USING AN AUTOMATED EMAIL SYSTEM.  </font><br />";
                emailString = emailString + @"<font size=""2"" color=""#0000FF"" face=""Arial, Helvetica, sans-serif""> PLEASE DO NOT REPLY TO THIS EMAIL. </font><br /><br />";
                emailString = emailString + @"<font size=""2"" color=""#FF0000"" face=""Arial, Helvetica, sans-serif""> (This electronic message transmission contains information which may be confidential. The information is intended to be for the use of the individual or entity named above. We do not waive   </font><br />" +
                                            @"<font size=""2"" color=""#FF0000"" face=""Arial, Helvetica, sans-serif""> confidentiality by any transmission in error. If you are not the intended recipient, be aware that any disclosure, copying, distribution or use of the contents of this information is prohibited.)  </font><br />";                                            
                                            
                emailString = emailString + "</body></html>";                

                Logging.LogToFile(emailString);

                foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, EmailSubject, emailString, "RightPro_DonotReply", null);
                }

            }


            sftp.Disconnect();
            return success;
        }

        public bool DownloadFTPFilesUsingSFTP(BCSClient bcsclient, string FTPFile, string FTPDropArchivePath)
        {
            
                //  Important: It is helpful to send the contents of the
                //  sftp.LastErrorText property when requesting support.

                string DestinationFTPFileName = String.Empty;
                UploadDocument uploadDocument = new UploadDocument();
                bool success;

                try
                {

                    Chilkat.SFtp sftp = new Chilkat.SFtp();

                    //  Original Chilkat Key
                   
                    success = sftp.UnlockComponent("BOWNECSSH_R0FULnWLmZnG");
                    if (success != true)
                    {

                        return success;
                    }

                    //  Set some timeouts, in milliseconds:
                    sftp.ConnectTimeoutMs = 5000;
                    sftp.IdleTimeoutMs = 10000;

                    //  Connect to the SSH server.
                    //  The standard SSH port = 22
                    //  The hostname may be a hostname or IP address.
                    int port;
                    string hostname;
                    hostname = bcsclient.ClientDocsFTPPath;
                    port = 22;
                    success = sftp.Connect(hostname, port);
                    if (success != true)
                    {

                        return success;
                    }

                    //  Authenticate with the SSH server.  Chilkat SFTP supports
                    //  both password-based authenication as well as public-key
                    //  authentication.  This example us;es password authenication.
                    success = sftp.AuthenticatePw(bcsclient.ClientDocsFTPUserName, bcsclient.ClientDocsFTPPassword);
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

                    //  Open a file on the server:
                    string handle;
                    handle = sftp.OpenFile("/" + ConfigValues.FTPFilePath + "/" + FTPFile, "readOnly", "openExisting");
                    if (handle == null)
                    {

                        return success;
                    }

                    //Archive all files coming in inlcuding pdf files and other extensions
                    //If duplicate file exists then rename and store the copy in the list

                    if (File.Exists(FTPDropArchivePath + FTPFile))
                    {
                        DestinationFTPFileName = Path.GetFileNameWithoutExtension(FTPFile) + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                        uploadDocument.IsDuplicate = true;
                    }
                    else
                    {
                        DestinationFTPFileName = FTPFile;
                        uploadDocument.IsDuplicate = false;
                    }
                    uploadDocument.Path = FTPDropArchivePath + DestinationFTPFileName;
                    uploadDocument.OriginalFileName = DestinationFTPFileName;
                    uploadDocument.IsDocUploadReady = false;
                    uploadDocument.Date = DateTime.Now;

                    try
                    {

                        //  Download the file:
                        success = sftp.DownloadFile(handle, uploadDocument.Path);
                    }
                    catch (Exception expt)
                    {

                        string ErrorEmailBody = "Download of FTP file " + FTPFile + "failed for " + bcsclient.ClientName;
                        Logging.LogToFile(ErrorEmailBody + expt.Message);

                        foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                        }
                    }

                    //  Close the file.
                    success = sftp.CloseHandle(handle);

                    //Update the downloaded file data into database here.
                    InsertFTPDocumentInfoInBCSWorkingTable(uploadDocument, bcsclient);
                }
                catch (Exception expt)
                {

                    string ErrorEmailBody = "Download of FTP file " + FTPFile + "failed for " + bcsclient.ClientName;
                    Logging.LogToFile(ErrorEmailBody + expt.Message);

                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                    }
                    return false;
                }

            return success;
        }

        public void InsertFTPDocumentInfoInBCSWorkingTable(UploadDocument uploadDocument, BCSClient bcsclient)
        {
            try
            {

                string SP_InsertFTPDocumentInfoInWorkingTable = "BCS_InsertFTPDocumentInfoIn" + bcsclient.ClientPrefix + "WorkingTable";

                DbParameterCollection parametercollection = this.dataAccess.ExecuteNonQueryReturnOutputParams
                    (
                        this.HostedAdminConnectionString,
                        SP_InsertFTPDocumentInfoInWorkingTable,
                         this.dataAccess.CreateParameter(DBPath, SqlDbType.VarChar, uploadDocument.Path),
                         this.dataAccess.CreateParameter(DBOriginalFileName, SqlDbType.VarChar, uploadDocument.OriginalFileName),
                         this.dataAccess.CreateParameter(DBIsDuplicate, SqlDbType.VarChar, uploadDocument.IsDuplicate),
                         this.dataAccess.CreateParameter(DBIsDocUploadReady, SqlDbType.VarChar, uploadDocument.IsDocUploadReady),
                         this.dataAccess.CreateParameter(DBDate, SqlDbType.DateTime, uploadDocument.Date)
                    );
            }

            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSFLTIntegrationService in function InsertFTPDocumentInfoInBCSWorkingTable for client " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
            }


        }

        public static bool DeleteFTPFileUsingSFTP(string File, BCSClient bcsclient)
        {
            //  Important: It is helpful to send the contents of the
            //  sftp.LastErrorText property when requesting support.

            Chilkat.SFtp sftp = new Chilkat.SFtp();

            //  Any string automatically begins a fully-functional 30-day trial.
            bool success;
            try
            {
                success = sftp.UnlockComponent("BOWNECSSH_R0FULnWLmZnG");
                if (success != true)
                {

                    return success;
                }

                //  Set some timeouts, in milliseconds:
                sftp.ConnectTimeoutMs = 5000;
                sftp.IdleTimeoutMs = 10000;

                //  Connect to the SSH server.
                //  The standard SSH port = 22
                //  The hostname may be a hostname or IP address.
                int port;
                string hostname;
                hostname = bcsclient.ClientDocsFTPPath;
                port = 22;
                success = sftp.Connect(hostname, port);
                if (success != true)
                {

                    return success;
                }

                //  Authenticate with the SSH server.  Chilkat SFTP supports
                //  both password-based authenication as well as public-key
                //  authentication.  This example uses password authenication.
                success = sftp.AuthenticatePw(bcsclient.ClientDocsFTPUserName, bcsclient.ClientDocsFTPPassword);
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

                //  Delete a file on the server:

                success = sftp.RemoveFile("/" + ConfigValues.FTPFilePath + "/" + File);
            }
            catch (Exception expt)
            {

                string ErrorEmailBody = "Delete of FTP file " + File + "failed for " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
                return false;
            }

            return success;
        }

        #endregion


        


        public void PickUPDocumentsAndMakeitDocUploadReady(BCSClient bcsclient)
        {
            try
            {
                
                string SPSweepFTPDocumentReadyForDocUpload = "BCS_SelectFTPDocumentReadyFor" + bcsclient.ClientPrefix + "DocUpload";
                //Select all the documents which are ready for DocUpload

                using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.HostedAdminConnectionString,
                    SPSweepFTPDocumentReadyForDocUpload,
                    this.dataAccess.CreateParameter(DBIsDocUploadReady, SqlDbType.Bit, false)
                ))
                {
                    while (reader.Read())
                    {
                        int ID = Convert.ToInt32(reader["ID"]);
                        //To get the filename without file extension
                        string FTPDocumentName = Path.GetFileNameWithoutExtension(reader["OriginalFileName"].ToString());
                        string DatePDFReceivedonFTP = reader["Date"].ToString();
                        bool IsDuplicate = Convert.ToBoolean(reader["IsDuplicate"]);
                        //consider only the six length filename from now onwards to avoid the duplicates
                        if (IsDuplicate)
                        {
                            FTPDocumentName = FTPDocumentName.Substring(0, FTPDocumentName.Length - 14);
                        }

                        string SPGetFLTDocumentDataUsingFTPDoc = "BCS_GetFLTDocumentDataUsingFTPDocFor" + bcsclient.ClientPrefix;

                        using (IDataReader secondreader = this.dataAccess.ExecuteReader
                          (
                               this.DB1029ConnectionString,
                               SPGetFLTDocumentDataUsingFTPDoc,
                               this.dataAccess.CreateParameter(DBFUNDDocName, SqlDbType.VarChar, FTPDocumentName),
                               this.dataAccess.CreateParameter(DBDatePDFReceivedonFTP, SqlDbType.DateTime, Convert.ToDateTime(DatePDFReceivedonFTP))
                           ))
                        {
                            while (secondreader.Read())
                            {

                                FLTDocument fltDoc = new FLTDocument();

                                fltDoc.FUNDCODE = secondreader["FUNDCODE"].ToString();
                                fltDoc.FUNDNAME = secondreader["FUNDNAME"].ToString();
                                fltDoc.FUNDTYPE = secondreader["FUNDTYPE"].ToString();
                                fltDoc.FUNDTELEACCESSCODE = secondreader["FUNDTELEACCESSCODE"].ToString();
                                fltDoc.FUNDCUSIPNUMBER = secondreader["FUNDCUSIPNUMBER"].ToString();
                                fltDoc.FUNDCHKHEADINGCODE = secondreader["FUNDCHKHEADINGCODE"].ToString();
                                fltDoc.FUNDGROUPNUMBER = secondreader["FUNDGROUPNUMBER"].ToString();                                
                                fltDoc.FUNDPROSPECTUSINSERT = secondreader["FUNDPROSPECTUSINSERT"].ToString();
                                fltDoc.FUNDPROSPECTUSINSERT2 = secondreader["FUNDPROSPECTUSINSERT2"].ToString();
                                fltDoc.FUNDTICKERSYMBOL = secondreader["FUNDTICKERSYMBOL"].ToString();
                                fltDoc.FUNDDocName = secondreader["FUNDDocName"].ToString();                                       


                                //Update the FLT Data for the Document in Staging FTP Table. Get the base table data using CUSIP like in the SP. Make it Doc Upload ready


                                string SP_UpdateFLTDataInFTPStagingTable = "BCS_UpdateFLTDataIn" + bcsclient.ClientPrefix + "FTPStagingTable";
                                this.dataAccess.ExecuteNonQueryReturnOutputParams
                                  (
                                      this.HostedAdminConnectionString,
                                      SP_UpdateFLTDataInFTPStagingTable,
                                      this.dataAccess.CreateParameter(DBID, SqlDbType.VarChar, ID),                                      
                                      this.dataAccess.CreateParameter(DBFUNDCUSIPNUMBER, SqlDbType.VarChar, fltDoc.FUNDCUSIPNUMBER)                                      
                                      
                                  );

                            }
                            

                        }


                    }

                }
            }

            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSFLTIntegrationService in function PickUPDocumentsAndMakeitDocUploadReady for client " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
            }         


        }


        public void ProcessFLTFile(BCSClient bcsclient, string FLTFilePath)
        {
            try
            {
                DataTable BCSFLTTableDetails = new DataTable();
                BCSFLTTableDetails.Columns.Add("FUNDCODE", typeof(string));
                BCSFLTTableDetails.Columns.Add("FUNDNAME", typeof(string));
                BCSFLTTableDetails.Columns.Add("FUNDTYPE", typeof(string));
                BCSFLTTableDetails.Columns.Add("FUNDTELEACCESSCODE", typeof(string));
                BCSFLTTableDetails.Columns.Add("FUNDCUSIPNUMBER", typeof(string));
                BCSFLTTableDetails.Columns.Add("FUNDCHKHEADINGCODE", typeof(string));
                BCSFLTTableDetails.Columns.Add("FUNDGROUPNUMBER", typeof(string));                
                BCSFLTTableDetails.Columns.Add("FUNDPROSPECTUSINSERT", typeof(string));
                BCSFLTTableDetails.Columns.Add("FUNDPROSPECTUSINSERT2", typeof(string));
                BCSFLTTableDetails.Columns.Add("FUNDTICKERSYMBOL", typeof(string));
                BCSFLTTableDetails.Columns.Add("FUNDDocName", typeof(string));

                //Read the file 
                using (StreamReader sr = new StreamReader(FLTFilePath))
                {

                    string input;
                    DataRow BCSFLTTableRow;
                    bool RejectFLT = false;
                    bool CUSIPErrors = false;
                    bool ShortNameErrors = false;
                    string strCUSIPRowErrors = String.Empty;
                    string ShortNameErrorRows = String.Empty;
                    try
                    {
                        while ((input = sr.ReadLine()) != null)
                        {
                            input = input.PadRight(100);
                            string[] rowLength = new string[150];
                            BCSFLTTableRow = BCSFLTTableDetails.NewRow();

                            rowLength[0] = input.Substring(0, 7);
                            rowLength[1] = input.Substring(8, 40);
                            rowLength[2] = input.Substring(49, 7);
                            rowLength[3] = input.Substring(57, 3);
                            rowLength[4] = input.Substring(61, 8);
                            rowLength[5] = input.Substring(70, 2);
                            rowLength[6] = input.Substring(73, 11);
                            rowLength[7] = input.Substring(85, 1);
                            rowLength[8] = input.Substring(87, 1);
                            rowLength[9] = input.Substring(89, 5);
                            rowLength[10] = input.Substring(94, 6);

                            //Add values to table row.
                            BCSFLTTableRow["FUNDCODE"] = rowLength[0].ToString().TrimStart().TrimEnd();
                            BCSFLTTableRow["FUNDNAME"] = rowLength[1].ToString().TrimStart().TrimEnd();
                            BCSFLTTableRow["FUNDTYPE"] = rowLength[2].ToString().TrimStart().TrimEnd();
                            BCSFLTTableRow["FUNDTELEACCESSCODE"] = rowLength[3].ToString().TrimStart().TrimEnd();
                            BCSFLTTableRow["FUNDCUSIPNUMBER"] = rowLength[4].ToString().TrimStart().TrimEnd();

                            // Adding CUSIP Validation logic here 
                            if (BCSFLTTableRow["FUNDCUSIPNUMBER"].ToString() == null || BCSFLTTableRow["FUNDCUSIPNUMBER"].ToString() == String.Empty || BCSFLTTableRow["FUNDCUSIPNUMBER"].ToString().Length != 8)
                            {
                                RejectFLT = true;
                                CUSIPErrors = true;
                                strCUSIPRowErrors = strCUSIPRowErrors + "<br/>" + input;
                            }

                            BCSFLTTableRow["FUNDCHKHEADINGCODE"] = rowLength[5].ToString().TrimStart().TrimEnd();
                            BCSFLTTableRow["FUNDGROUPNUMBER"] = rowLength[6].ToString().TrimStart().TrimEnd();
                            BCSFLTTableRow["FUNDPROSPECTUSINSERT"] = rowLength[7].ToString().TrimStart().TrimEnd();
                            BCSFLTTableRow["FUNDPROSPECTUSINSERT2"] = rowLength[8].ToString().TrimStart().TrimEnd();
                            BCSFLTTableRow["FUNDTICKERSYMBOL"] = rowLength[9].ToString().TrimStart().TrimEnd();
                            BCSFLTTableRow["FUNDDocName"] = rowLength[10].ToString().TrimStart().TrimEnd();
                            if (BCSFLTTableRow["FUNDDocName"].ToString() == null || BCSFLTTableRow["FUNDDocName"].ToString() == String.Empty || BCSFLTTableRow["FUNDDocName"].ToString().Length != 3)
                            {
                                RejectFLT = true;
                                ShortNameErrors = true;
                                ShortNameErrorRows = ShortNameErrorRows + "<br/>" + input;
                            }

                            BCSFLTTableDetails.Rows.Add(BCSFLTTableRow);

                        }
                    }
                    catch (Exception expt)
                    {
                        RejectFLT = true;
                        BCSFLTTableDetails = new DataTable();

                        string emailString = "<html><head><title></title></head><body>";
                        emailString = emailString + @"<br /><font size=""5"" color=""#666666"" face=""Arial, Helvetica, sans-serif"">" + "BCS " + bcsclient.ClientName + " Funds Literal Table (FLT) File Rejected</font><br /><br />";
                        emailString = emailString + "File reading error for  Funds Literal Table (FLT) File : " + Path.GetFileName(FLTFilePath) + " :<hr />" + expt.Message + "<hr/><br/><br/>";
                        emailString = emailString + "</body></html>";

                        Logging.LogToFile(emailString);

                        foreach (string eml in ConfigValues.FLTErrorEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, "BCS " + bcsclient.ClientName + " Funds Literal Table (FLT) File Rejected", emailString, "Support", null);
                        }

                        
                    }

                    if (RejectFLT)
                    {
                        BCSFLTTableDetails = new DataTable();

                        string emailString = "<html><head><title></title></head><body>";
                        emailString = emailString + @"<br /><font size=""5"" color=""#666666"" face=""Arial, Helvetica, sans-serif"">" + "BCS " + bcsclient.ClientName + " Funds Literal Table (FLT) File Rejected</font><br /><br />";
                        if (CUSIPErrors)
                        {
                            emailString = emailString + "Following rows have CUSIP errors in this Funds Literal Table (FLT) File " + Path.GetFileName(FLTFilePath) + " :<hr />" + strCUSIPRowErrors + "<hr/><br/><br/>";
                        }
                        if (ShortNameErrors)
                        {
                            emailString = emailString + "Following rows have SHORT-NAME errors in this Funds Literal Table (FLT) File " + Path.GetFileName(FLTFilePath) + " :<hr />" + ShortNameErrorRows + "<hr/><br/><br/>";
                        }

                        emailString = emailString + "</body></html>";

                        Logging.LogToFile(emailString);

                        if (CUSIPErrors == true || ShortNameErrors == true)
                        {
                            foreach (string eml in ConfigValues.FLTErrorEmailListTo.Split(';'))
                            {
                                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, "BCS " + bcsclient.ClientName + " Funds Literal Table (FLT) File Rejected", emailString, "Support", null);
                            }
                        }

                    }
                    else
                    {

                        string SP_UpdateFLTTableWithData = "BCS_Update" + bcsclient.ClientPrefix + "FLTTable";

                        this.dataAccess.ExecuteNonQuery
                                                             (
                                                                  this.DB1029ConnectionString,
                                                                  SP_UpdateFLTTableWithData,
                                                                  this.dataAccess.CreateParameter(DBBCSFLTTableDetails, SqlDbType.Structured, BCSFLTTableDetails));
                    }
                    
                }

            }

            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSFLTIntegrationService in function PickUPDocumentsAndMakeitDocUploadReady for client " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
            }


        }

        public void InsertintoFLTFileTrackingTable(string strFLTFile, BCSClient bcsclient, DateTime DateReceived)
        {
            try
            {

                string SP_InsertFTPDocumentInfoInWorkingTable = "BCS_InsertFLTFileInfoIn" + bcsclient.ClientPrefix + "TrackingTable";

                DbParameterCollection parametercollection = this.dataAccess.ExecuteNonQueryReturnOutputParams
                    (
                        this.DB1029ConnectionString,
                        SP_InsertFTPDocumentInfoInWorkingTable,
                         this.dataAccess.CreateParameter(DBFLTFileName, SqlDbType.VarChar, strFLTFile),
                         this.dataAccess.CreateParameter(DBFLTDateReceived, SqlDbType.DateTime, DateReceived) 
                    );
            }

            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSFLTIntegrationService in function InsertintoFLTFileTrackingTable for client " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
            }


        }

        public void PrepareDocAndPushEachOnetoDocUploadUI(BCSClient bcsclient)
        {
            try
            {

                string SPSweepFTPDocumentReadyForDocUpload = "BCS_SelectFTPDocumentReadyFor" + bcsclient.ClientPrefix + "DocUpload";
                //Select all the documents which are ready for DocUpload

                using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.HostedAdminConnectionString,
                    SPSweepFTPDocumentReadyForDocUpload,
                    this.dataAccess.CreateParameter(DBIsDocUploadReady, SqlDbType.Bit, true)
                ))
                {
                    while (reader.Read())
                    {
                        UploadDocument uploadDocument = new UploadDocument();

                        int ID = Convert.ToInt32(reader["ID"]);
                        string Path = reader["Path"].ToString();
                        string Company = reader["Company"].ToString();
                        string OriginalFileName = reader["OriginalFileName"].ToString();
                        DateTime Date = Convert.ToDateTime(reader["Date"]);
                        string ProsDocTypeId = reader["ProsDocTypeId"].ToString();
                        string ProsID = reader["ProsID"].ToString();
                        string CompanyID = reader["CompanyID"].ToString();
                        bool IsDuplicate = Convert.ToBoolean(reader["IsDuplicate"]);
                        int IsPDFWorkDone = 0;
                        string FundName = reader["ProsName"].ToString();

                        //Get the value of current RP Doc Name 
                        string GetProDocName = DocUpload_GetFileNameForProsID(ProsID, ProsDocTypeId);
                        string SuggestedDocumentName = GetProDocName;
                        if (GetProDocName == "") SuggestedDocumentName = TranslateFileName(ProsDocTypeId, FundName) + ".pdf";  


                        uploadDocument.ID = ID;
                        uploadDocument.Path = Path;
                        uploadDocument.Company = Company;
                        uploadDocument.OriginalFileName = OriginalFileName;
                        uploadDocument.Date = Date;
                        uploadDocument.ProsDocTypeId = ProsDocTypeId;
                        uploadDocument.ProsID = ProsID;
                        uploadDocument.CompanyID = CompanyID;
                        uploadDocument.IsDuplicate = IsDuplicate;
                        uploadDocument.SetDocRelation = true;
                        uploadDocument.IsFTPDocument = true;
                        uploadDocument.RenamedFileName = SuggestedDocumentName;


                        //Do PDF Work on this document and get the PDF work related values.
                        PDFWork pdfwork = new PDFWork();
                        DocumentOnboardContractClient client = new DocumentOnboardContractClient();
                        PDFWorkflowStatus workstatus = client.CheckPDFAndUpdateBookMarksView(uploadDocument.Path, uploadDocument.ProsDocTypeId);
                        pdfwork.IsPDFValidatedField = workstatus.IsPDFValidated;
                        pdfwork.IsPDFBookmarkedField = workstatus.IsPDFBookmarked;

                        if (pdfwork.IsPDFValidatedField == false)
                        {
                            IsPDFWorkDone = 2;
                        }
                        else if (pdfwork.IsPDFValidatedField == true)
                        {
                            if (pdfwork.IsPDFBookmarkedField == true)
                            {
                                uploadDocument.BookMark = 1;
                            }

                            if (workstatus.PageCount.HasValue)
                            {
                                uploadDocument.PageCount = (int?)workstatus.PageCount;
                            }
                            if (workstatus.PageHeight.HasValue)
                            {
                                uploadDocument.PageHeight = (double?)workstatus.PageHeight;
                            }
                            if (workstatus.PageWidth.HasValue)
                            {
                                uploadDocument.PageWidth = (double?)workstatus.PageWidth;
                            }

                            IsPDFWorkDone = 1;

                        }

                        //Insert document information in doc upload working table.
                        string SPInsertFTPDocintoDocUploadWorkingTable = "BCS_InsertFTPDocFor" + bcsclient.ClientPrefix + "intoDocUploadWorkingTable";

                        DbParameterCollection parametercollection = this.dataAccess.ExecuteNonQueryReturnOutputParams
                        (
                            this.HostedAdminConnectionString,
                            SPInsertFTPDocintoDocUploadWorkingTable,
                            this.dataAccess.CreateParameter(DBID, SqlDbType.Int, uploadDocument.ID),
                            this.dataAccess.CreateParameter(DBPath, SqlDbType.VarChar, uploadDocument.Path),
                            this.dataAccess.CreateParameter(DBOriginalFileName, SqlDbType.VarChar, uploadDocument.OriginalFileName),
                            this.dataAccess.CreateParameter(DBCompany, SqlDbType.VarChar, uploadDocument.Company),
                            this.dataAccess.CreateParameter(DBDate, SqlDbType.DateTime, uploadDocument.Date),
                            this.dataAccess.CreateParameter(DBRenamedFileName, SqlDbType.VarChar, uploadDocument.RenamedFileName),
                            this.dataAccess.CreateParameter(DBProsDocTypeId, SqlDbType.VarChar, uploadDocument.ProsDocTypeId),
                            this.dataAccess.CreateParameter(DBProsID, SqlDbType.VarChar, uploadDocument.ProsID),
                            this.dataAccess.CreateParameter(DBSetDocRelation, SqlDbType.Bit, uploadDocument.SetDocRelation),
                            this.dataAccess.CreateParameter(DBIsPDFWorkDone, SqlDbType.Int, IsPDFWorkDone),
                            this.dataAccess.CreateParameter(DBCompanyID, SqlDbType.VarChar, uploadDocument.CompanyID),
                            this.dataAccess.CreateParameter(DBBookMark, SqlDbType.Int, uploadDocument.BookMark),
                            this.dataAccess.CreateParameter(DBIsDuplicate, SqlDbType.Bit, uploadDocument.IsDuplicate),
                            this.dataAccess.CreateParameter(DBIsFTPDocument, SqlDbType.Bit, uploadDocument.IsFTPDocument),
                            this.dataAccess.CreateParameter(DBPageCount, SqlDbType.Int, uploadDocument.PageCount),
                            this.dataAccess.CreateParameter(DBPageHeight, SqlDbType.Decimal, uploadDocument.PageHeight),
                            this.dataAccess.CreateParameter(DBPageWidth, SqlDbType.Decimal, uploadDocument.PageWidth)
                            
                        );

                    }

                }
            }

            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSFLTIntegrationService in function PrepareDocAndPushEachOnetoDocUploadUI for client " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
            }


        }

        public string DocUpload_GetFileNameForProsID(string prosID, string doctypeid)
        {

            string ProsDocName = "";
            string ProsDocUrl = "";
            using (IDataReader reader = this.dataAccess.ExecuteReader
                                        (
                                          DB1029ConnectionString,
                                          SPGetFileNameForProsID,
                                          this.dataAccess.CreateParameter(DBProsIDs, SqlDbType.VarChar, prosID),
                                          this.dataAccess.CreateParameter(DBProsDocTypeId, SqlDbType.VarChar, doctypeid)
                                        ))
            {

                while (reader.Read())
                {
                    ProsDocUrl = reader["ProsDocUrl"].ToString();
                    int lastindex = ProsDocUrl.LastIndexOf(@"/");

                    lastindex++;

                    ProsDocName = ProsDocUrl.Substring(lastindex, ProsDocUrl.Length - lastindex);
                }

            }

            return ProsDocName;
        }

        #region TranslateFileName

        private static string TranslateFileName(string ProsDocTypeID, string FundName)
        {
            string filename = string.Empty;

            string prosnamewithoutspace = string.Empty;

            prosnamewithoutspace = Regex.Replace(FundName, "[^0-9a-zA-Z]+", "");


            switch (ProsDocTypeID)
            {
                case "P": filename = "PRO_" + prosnamewithoutspace;
                    break;
                case "PS": filename = "PROSup_" + prosnamewithoutspace;
                    break;
                case "SP": filename = "SUM_" + prosnamewithoutspace;
                    break;
                case "SPS": filename = "SUMSup_" + prosnamewithoutspace;
                    break;
                case "S": filename = "SAI_" + prosnamewithoutspace;
                    break;
                case "SS": filename = "SAISup_" + prosnamewithoutspace;
                    break;
                case "AR": filename = "ANN_" + prosnamewithoutspace;
                    break;
                case "SAR": filename = "SEMI_" + prosnamewithoutspace;
                    break;
                case "PH": filename = "PH_" + prosnamewithoutspace;
                    break;
                case "PVR": filename = "PVR_" + prosnamewithoutspace;
                    break;
                case "GWF": filename = "GWF_" + prosnamewithoutspace;
                    break;
                case "CAR": filename = "CAR_" + prosnamewithoutspace;
                    break;
                case "COM": filename = "COM_" + prosnamewithoutspace;
                    break;
                case "CS": filename = "CS_" + prosnamewithoutspace;
                    break;
                case "CSR": filename = "CSR_" + prosnamewithoutspace;
                    break;
                case "FS": filename = "FS_" + prosnamewithoutspace;
                    break;
                case "MMD": filename = "MMD_" + prosnamewithoutspace;
                    break;
                case "SC": filename = "SC_" + prosnamewithoutspace;
                    break;
                case "CP": filename = "CP_" + prosnamewithoutspace;
                    break;

            }

            return filename;
        }


        #endregion 
 
        #region Convert to EST Time Zone

        protected DateTime GetNextRun(DateTime Date, string TimeZone)
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
           
            //Set the time zone information to US Mountain Standard Time 
            switch (TimeZone)
            {
                case "CST":
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                    break;
                case "EST":
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    break;
                case "MST":
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("US Mountain Standard Time");
                    break;
                case "PST":
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                    break;
                default:
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                    break;
            }


            //Get date and time in US Mountain Standard Time 
            dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);           


            return dateTime;


        }

        #endregion

        #region ProcessWatchListFile

        public void ProcessWatchListFile(BCSClient bcsclient)
        {


            bool boolSendEmail = false;

            string ReportLocationFirstDollar = ConfigValues.WatchListReportLocation;

            string strSANDirectory = bcsclient.CUSIPWatchListArchiveDropPath;



            try
            {
                string GetAllUnprocessedFilesSql = "SELECT fileName FROM BCS"
                                                    + bcsclient.ClientName + "FTP WHERE IsProcessed = 0 Order By ID";

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = DBConnectionString.DB1029ConnectionString();

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(GetAllUnprocessedFilesSql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string strFile = reader["filename"].ToString();

                                WatchListdataBulkInsert(strSANDirectory + strFile, bcsclient, out boolSendEmail);                                

                                SetProcessed(strFile, bcsclient);
                            }

                            reader.Close();
                        }
                    }
                    connection.Close();
                }


                UpdateBCSCUSIPWatchListTable(bcsclient);

                if (boolSendEmail)
                {

                    string emailString = "<html><head><title></title></head><body>";
                    emailString = emailString + @"<br /><font size=""5"" color=""#666666"" face=""Arial, Helvetica, sans-serif"">" + "BCS " + bcsclient.ClientName + " WatchList Difference Report</font><br /><br />";
                    emailString = emailString + "Report Link :<hr />";
                    emailString = emailString + "<br /><a href='" + ReportLocationFirstDollar + "'>" + ReportLocationFirstDollar + "</a><hr/>";
                    emailString = emailString + "</body></html>";

                    Logging.LogToFile(emailString);

                    foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, "BCS " + bcsclient.ClientName + "FTP Alert", emailString, "Support", null);
                    }
                }
            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function ProcessWatchListFile : " + expt.Message;

                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }

        }


        private void WatchListdataBulkInsert(string strFileName, BCSClient bcsclient, out bool boolSendEmail)
        {
            boolSendEmail = false;
            try
            {
                
                
                using (SqlConnection cn = new SqlConnection(DBConnectionString.DB1029ConnectionString()))
                {

                    cn.Open();
                    using (SqlBulkCopy copy = new SqlBulkCopy(cn))
                    {
                        copy.DestinationTableName = "dbo.BCS" + bcsclient.ClientName + "_FTP_RT1";
                        DataTable Data = new DataTable();
                        Data = CreateDataTableFromWatchListFile(strFileName, bcsclient);
                        if (Data.Rows.Count > 0)
                        {
                            boolSendEmail = true;
                            DeleteOld_CUSIPWatchListFileData(bcsclient);
                            copy.WriteToServer(Data);
                        }
                    }
                    cn.Close();
                }

            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function WatchListdataBulkInsert : " + expt.Message;

                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }
        }


        private bool SetProcessed(string file, BCSClient bcsclient)
        {
            int iId = -1;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = DBConnectionString.DB1029ConnectionString();
                conn.Open();


                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.BCS" + bcsclient.ClientName + "FTP SET isProcessed = 1 WHERE fileName='" + file + "' ", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    iId = cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            return iId != -1 ? true : false;

        }


        private void UpdateBCSCUSIPWatchListTable(BCSClient bcsclient)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = DBConnectionString.DB1029ConnectionString();
                    conn.Open();


                     // Replacing first dollar logic with a new one. New columns are added to master table and history. 
                    string cmdString = @"DELETE  FROM dbo.BCS" + bcsclient.ClientName +
                                       @"WatchListCUSIPs WHERE CUSIP NOT IN(SELECT clmn5 FROM dbo.BCS" + bcsclient.ClientName + "_FTP_RT1) " +

                                       @"INSERT INTO  dbo.BCS" + bcsclient.ClientName + "WatchListCUSIPs (CUSIP, FundName, CIK, SeriesID, ClassContractID, TickerSymbol, Class) " +
                                       @"SELECT ProsTicker.CUSIP, dbo.BCS" + bcsclient.ClientName + "_FTP_RT1.clmn6, ProsTicker.CIK, ProsTicker.SeriesID, ProsTicker.ClassContractID, ProsTicker.TickerSymbol, dbo.BCS" + bcsclient.ClientName + "_FTP_RT1.clmn7 " +
                                       @"FROM ProsTicker INNER JOIN dbo.BCS" + bcsclient.ClientName + "_FTP_RT1 ON ProsTicker.CUSIP =  dbo.BCS" + bcsclient.ClientName + "_FTP_RT1.clmn5  " +
                                       @"LEFT JOIN dbo.BCS" + bcsclient.ClientName + "WatchListCUSIPs ON dbo.BCS" + bcsclient.ClientName + "WatchListCUSIPs.CUSIP =  dbo.BCS" + bcsclient.ClientName + "_FTP_RT1.clmn5  " +
                                       @"WHERE dbo.BCS" + bcsclient.ClientName + "WatchListCUSIPs.CUSIP IS NULL  AND ProsTicker.CUSIP != ''";



                    using (SqlCommand cmd = new SqlCommand(cmdString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        int dr = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function UpdateBCSCUSIPWatchListTable : " + expt.Message;

                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);

            }
        }


        private DataTable CreateDataTableFromWatchListFile(string strUNCFileName, BCSClient bcsclient)
        {
            
            try
            {
                
                DataTable dt = new DataTable();
                DataColumn dc;
                DataRow dr;

                int NumberOfRecords = 0;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "clmn1";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "clmn2";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "clmn3";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "clmn4";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "clmn5";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "clmn6";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "clmn7";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "clmn8";
                dc.Unique = false;
                dt.Columns.Add(dc);
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "clmn9";
                dc.Unique = false;
                dt.Columns.Add(dc);
                StreamReader sr = new StreamReader(strUNCFileName);
                string input;
                bool isFormatError = false;
                bool isCUSIPError = false;
                string strErrorMessage = string.Empty;
                string strCUSIPErrors = String.Empty;

                while ((input = sr.ReadLine()) != null)
                {
                    string[] s = input.Split(new char[] { '|' });
                    if (s.Length == 9)
                    {
                        dr = dt.NewRow();
                        dr["clmn1"] = s[0].ToString();
                        dr["clmn2"] = s[1].ToString();
                        dr["clmn3"] = s[2].ToString();
                        dr["clmn4"] = s[3].ToString();
                        dr["clmn5"] = s[4].ToString();
                        if (dr["clmn5"].ToString() == String.Empty)
                        {
                            isCUSIPError = true;
                            strCUSIPErrors = strCUSIPErrors + "<br/>" + input;
                        }
                        dr["clmn6"] = s[5].ToString();
                        dr["clmn7"] = s[6].ToString();
                        dr["clmn8"] = s[7].ToString();
                        dr["clmn9"] = s[8].ToString();

                        dt.Rows.Add(dr);
                    }
                    else if (s.Length == 4)
                    {
                        NumberOfRecords = Convert.ToInt32(s[3]);
                    }
                    else if (s[1].ToString() != "H" && s.Length < 9)
                    {
                        isFormatError = true;
                        strErrorMessage = strErrorMessage + "<br/>" + input;
                        break;
                        
                    }
                }

                sr.Close();                

                if (isFormatError)
                {
                    string emailString = "<html><head><title></title></head><body>";
                    emailString = emailString + @"<br /><font size=""5"" color=""#666666"" face=""Arial, Helvetica, sans-serif"">" + "BCS " + bcsclient.ClientName + " WatchList File Rejected</font><br /><br />";
                    emailString = emailString + "Format error in watch list file :<hr />" + Path.GetFileName(strUNCFileName) + strErrorMessage + "<hr/>";

                    emailString = emailString + "</body></html>";

                    Logging.LogToFile(emailString);

                    foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, "BCS " + bcsclient.ClientName + " WatchList File Rejected", emailString, "Support", null);
                    }

                    //Set the file as processed to prevent duplicate processing 
                    SetProcessed(Path.GetFileName(strUNCFileName), bcsclient);

                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                    return new DataTable();
                }
                else
                {

                    if (NumberOfRecords == dt.Rows.Count)
                    {
                        if (isCUSIPError)
                        {
                            string emailString = "<html><head><title></title></head><body>";
                            emailString = emailString + @"<br /><font size=""5"" color=""#666666"" face=""Arial, Helvetica, sans-serif"">" + "BCS " + bcsclient.ClientName + " WatchList File CUSIP Errors</font><br /><br />";
                            emailString = emailString + "Following rows have empty CUSIPs in this WatchList File " + Path.GetFileName(strUNCFileName) + " :<hr />" + strCUSIPErrors + "<hr/>";

                            emailString = emailString + "</body></html>";

                            Logging.LogToFile(emailString);

                            foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                            {
                                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, "BCS " + bcsclient.ClientName + " WatchList File CUSIP Errors", emailString, "Support", null);
                            }
                        }

                        return dt;
                    }
                    else
                    {
                        
                        string emailString = "<html><head><title></title></head><body>";
                        emailString = emailString + @"<br /><font size=""5"" color=""#666666"" face=""Arial, Helvetica, sans-serif"">" + "BCS " + bcsclient.ClientName + " WatchList File Rejected</font><br /><br />";
                        emailString = emailString + "Header count does not match the number of records in this watch list file :<hr />" + Path.GetFileName(strUNCFileName) + "<hr/>";

                        emailString = emailString + "</body></html>";

                        Logging.LogToFile(emailString);

                        foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                        {
                            UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, "BCS " + bcsclient.ClientName + " WatchList File Rejected", emailString, "Support", null);
                        }

                        //Set the file as processed to prevent duplicate processing 
                        SetProcessed(Path.GetFileName(strUNCFileName), bcsclient);


                        System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                        return new DataTable();
                    }
                }               


            }
            catch (Exception expt)
            {

                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function CreateDataTableFromWatchListFile : " + expt.Message;

                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
               
                return new DataTable();
            }
        }


        public void InsertWatchListHistory(string fileName, BCSClient bcsclient)
        {
            try
            {

                //string sqlcommandstring = string.Format("INSERT INTO {0}FTP (dateReceived, fileName, isProcessed) VALUES(GETDATE(),'{1}', 0)"
                //                                        , "BCS" + bcsclient.ClientName, fileName);

                string sqlcommandstring = "BCS_" + bcsclient.ClientName + "InsertWatchListHistory";

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = DBConnectionString.DB1029ConnectionString();
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlcommandstring, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@FileName", fileName));
                        int rslt = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            catch (Exception expt)
            {

                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function InsertWatchListHistory : " + expt.Message;

                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }
        }


        private void DeleteOld_CUSIPWatchListFileData(BCSClient bcsclient)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = DBConnectionString.DB1029ConnectionString();
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"delete from dbo.BCS" + bcsclient.ClientName + "_FTP_RT1" + @"", conn);
                    cmd.CommandType = CommandType.Text;
                    int dr = cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function DeleteOld_CUSIPWatchListFileData : " + expt.Message;

                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }
        }

        

        #endregion




    }
}
