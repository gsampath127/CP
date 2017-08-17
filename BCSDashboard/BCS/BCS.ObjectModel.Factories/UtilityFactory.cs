using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net.Cache;
using System.Net.Mime;
using HiQPdf;
using System.Text.RegularExpressions;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.Configuration;


namespace BCS.ObjectModel.Factories
{
    public static class UtilityFactory
    {
        private static BCSFLTandFTPLogicFactory fltandftpLogicfactory = new BCSFLTandFTPLogicFactory();

        public static string GetMD5HashFromFile(string fileName)
        {
            
            using (FileStream file = new FileStream(fileName, FileMode.Open))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                
                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }
                    return sb.ToString();                
            }

        }

        public static string ConvertDocumentType(string _documentType)
        {
            string strShortDocType = _documentType;
            
            //PROSPECTUS SUPPLEMENT
            if (_documentType.ToUpper().Contains("PROSPECTUS") && _documentType.ToUpper().Contains("SUMMARY") && !_documentType.ToUpper().Contains("SUPPLEMENT") && !_documentType.ToUpper().Contains("REVISED"))
            {
                strShortDocType = "SP";
            }
            if (_documentType.ToUpper().Contains("PROSPECTUS") && _documentType.ToUpper().Contains("SUMMARY") && !_documentType.ToUpper().Contains("SUPPLEMENT") && _documentType.ToUpper().Contains("REVISED"))
            {
                strShortDocType = "RSP";
            }
            if (_documentType.ToUpper().Contains("PROSPECTUS") && _documentType.ToUpper().Contains("SUMMARY") && _documentType.ToUpper().Contains("SUPPLEMENT"))
            {
                strShortDocType = "SPS";
            }
            if (_documentType.ToUpper().Contains("PROSPECTUS") && !_documentType.ToUpper().Contains("SUMMARY") && !_documentType.ToUpper().Contains("SUPPLEMENT") && !_documentType.ToUpper().Contains("REVISED"))
            {
                strShortDocType = "P";
            }
            if (_documentType.ToUpper().Contains("PROSPECTUS") && !_documentType.ToUpper().Contains("SUMMARY") && !_documentType.ToUpper().Contains("SUPPLEMENT") && _documentType.ToUpper().Contains("REVISED"))
            {
                strShortDocType = "RP";
            }
            if (_documentType.ToUpper().Contains("PROSPECTUS") && !_documentType.ToUpper().Contains("SUMMARY") && _documentType.ToUpper().Contains("SUPPLEMENT"))
            {
                strShortDocType = "PS";
            }  

            //ANNUAL and Semi ANNUAL
            if (_documentType.ToUpper().Contains("ANNUAL"))
            {
                if (!_documentType.ToUpper().Contains("SEMI"))
                {
                    strShortDocType = _documentType.ToUpper().Contains("REVISED") ? "RAR" : "AR";
                }
                else
                {
                    strShortDocType = _documentType.ToUpper().Contains("REVISED") ? "RSAR" : "SAR";
                }
            }

            //Quarterly Report
            if (_documentType.ToUpper().Contains("QUARTERLY"))
            {
                strShortDocType = _documentType.ToUpper().Contains("REVISED") ? "RQR" : "QR";                
            }
            
            return strShortDocType;
        }

        public static string ExtractFilename(string filepath)
        {
            // If path ends with a "\", it's a path only so return String.Empty.
            if (filepath.Trim().EndsWith(@"\"))
                return String.Empty;

            // Determine where last backslash is. 
            int position = filepath.LastIndexOf('\\');
            // If there is no backslash, assume that this is a filename. 

            // Determine whether file exists using filepath. 
            if (File.Exists(filepath))
                // Return filename without file path. 
                return filepath.Substring(position + 1);
            else
                return String.Empty;

        }

        public static bool Upload(string filename, string _ftpServerIP, string _ftpUser, string _ftpPw)
        {

            bool rslt = false;
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + _ftpServerIP + "/" + fileInf.Name;


            if (!WebRequestMethods.Ftp.ListDirectoryDetails.Contains("ftp://" + _ftpServerIP + "/" + fileInf.Name))
            {
                FtpWebRequest reqFTP;

                // Create FtpWebRequest object from the Uri provided
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + _ftpServerIP + "/" + fileInf.Name));

                // Provide the WebPermission Credintials
                reqFTP.Credentials = new NetworkCredential(_ftpUser, _ftpPw);
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
                catch (Exception ex)
                {
                    rslt = false;
                }
            }

            return rslt;

        }

        public static string GetExceptionMessageContent(int exceptionID)
        {
            return "An exception has occurred on this page. ExceptionID = " + exceptionID + ". Please contact your system administrator with ExceptionID.";
        }
        
        public static void DownloadFileFromFTP(Uri uri, string dropPathWithFileName, string FtpUserID, string FtpPassword, bool archiveExistingFileWithSameName = false)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);


            request.KeepAlive = false;
            request.Credentials = new NetworkCredential(FtpUserID, FtpPassword);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Encoding objEncoding = Encoding.Default;
                StreamReader reader = new StreamReader(response.GetResponseStream(), objEncoding);
                string tmp = reader.ReadToEnd();
                response.Close();


                if (archiveExistingFileWithSameName && File.Exists(dropPathWithFileName))
                {
                    //Rename the duplicate if any in local path
                    string path = Path.GetDirectoryName(dropPathWithFileName) + @"\";
                    string fileExtension = Path.GetExtension(dropPathWithFileName);

                    File.Copy(dropPathWithFileName, path + Path.GetFileNameWithoutExtension(dropPathWithFileName) + DateTime.Now.ToString("_yyyymmddhhmmss") + fileExtension);
                    File.Delete(dropPathWithFileName);
                }

                using (StreamWriter tws = new StreamWriter(dropPathWithFileName, false, objEncoding))
                {
                    tws.Write(tmp);
                    tws.Close();
                }
            }

        }

        public static void DeleteFTPFileRRDInt(Uri uri, string FtpUserID, string FtpPassword)
        {
            try
            {
                if (uri.Scheme == Uri.UriSchemeFtp)
                {

                    FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);
                    request.KeepAlive = false;
                    request.Credentials = new NetworkCredential(FtpUserID, FtpPassword);
                    request.Method = WebRequestMethods.Ftp.DeleteFile;

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {

                    }

                }

            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Function UtilityFactory.DeleteFTPFileRRDInt - uri : " + uri.ToString() + "  - " + expt.Message;
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                
                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }

        }

        public static List<string> GetFileList(string PickFTP, string FtpUserID, string FtpPassword)
        {
            List<string> downloadFiles = new List<string>();
            StringBuilder result = new StringBuilder();
            FtpWebRequest request;
            try
            {

                request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + PickFTP + "/"));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(FtpUserID, FtpPassword);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.KeepAlive = false;

                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                bool bFilesExists = false;

                while (line != null)
                {
                    bFilesExists = true;
                    result.Append(line);
                    result.Append("\n");
                    downloadFiles.Add(line);

                    line = reader.ReadLine();
                }
                if (bFilesExists)
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return downloadFiles;
            }
            catch (Exception ex)
            {                
                string ErrorEmailBody = "Function UtilityFactory.GetFileList - PickFTP : " + PickFTP + ex.Message;
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);

                downloadFiles = null;
                return downloadFiles;
            }
        }
        

        public static void SendEmail(string emailFrom, string emailTo, string emailCC, string emailBCC, string subject, string body, string fromDisplayName, string toDisplayName)
        {
            subject = subject + " - " + ConfigValues.AppEnvironment;

            MailMessage message = new MailMessage();
            //string strToEmail = emailTo;
            string strFromEmail = emailFrom;
            string strCCEmail = emailCC;
            string strBCCEmail = emailBCC;
            
            message.From = new MailAddress(strFromEmail, fromDisplayName);

            foreach (string toEmailAddr in emailTo.Split(';'))
            {
                message.To.Add(new MailAddress(toEmailAddr, toDisplayName));
            }

            if (!string.IsNullOrWhiteSpace(emailCC))
            {
                message.CC.Add(emailCC);
            }
            if (!string.IsNullOrWhiteSpace(emailBCC))
            {
                message.Bcc.Add(emailBCC);
            }
            
            message.Subject = subject;
            message.Body = null;
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
                try
                {
                    SmtpClient client = new SmtpClient(ConfigValues.SMTP);
                    client.UseDefaultCredentials = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;

                    client.Send(message);
                }
                catch (Exception excm)
                {

                    throw;
                }
            }
        }




        public static bool DownloadFileWithHeaders(string originalUrl, string pathWithFileName)
        {
            bool FileDownloaded = false;
            try
            {
                using (WebClient request = new WebClient())
                {


                    HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    request.CachePolicy = noCachePolicy;

                    request.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
                    request.Headers.Add("Accept-Encoding", "gzip, deflate");
                    request.Headers.Add("Accept-Language", "en-US");
                    request.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");

                    request.DownloadFile(originalUrl, pathWithFileName);

                    request.Dispose();

                }
                FileDownloaded = true;
            }
            catch (Exception excp)
            {
                //Logging.LogToDB("download failed " + originalUrl + " from DownloadFileWithHeaders at path" + pathWithFileName + "\n" + excp.Message + "\n" + excp.StackTrace, 3);
            }

            return FileDownloaded;


        }

        public static bool DownloadFileWithoutHeaders(string originalUrl, string pathWithFileName)
        {
            bool FileDownloaded = false;

            try
            {

                using (WebClient request = new WebClient())
                {

                    HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    request.CachePolicy = noCachePolicy;

                    request.DownloadFile(originalUrl, pathWithFileName);

                    request.Dispose();
                }
                FileDownloaded = true;

            }
            catch (Exception excp)
            {
                //Logging.LogToDB("download failed " + originalUrl + " from DownloadFileWithoutHeaders at path" + pathWithFileName + "\n" + excp.Message + "\n" + excp.StackTrace, 3);

            }

            return FileDownloaded;
        }


        private static string CorrectPDFUrl(string ProsDocURL)
        {
            string NewProsDocURL = ProsDocURL;

            if (ProsDocURL.Contains(@"http://individual.troweprice.com/public/Retail/xStaticFiles"))
            {
                NewProsDocURL = ProsDocURL.Substring(ProsDocURL.LastIndexOf(@"/") + 1, ProsDocURL.Length - ProsDocURL.LastIndexOf(@"/") - 1);

                NewProsDocURL = "http://individual.troweprice.com/staticFiles/gcFiles/pdf/" + NewProsDocURL;

            }
            else
                if (ProsDocURL.Contains(@"http://www.nuveen.com/Home/Documents/Default.aspx"))
                {
                    NewProsDocURL = ProsDocURL.Replace("Default.aspx", "Viewer.aspx");
                }

            return NewProsDocURL;

        }

        public static bool TryExecute<T>(Func<T> func, int timeout, out T result)
        {
            var t = default(T);
            var thread = new Thread(() => t = func());
            thread.Start();
            var completed = thread.Join(timeout);
            if (!completed) thread.Abort();
            result = t;
            return completed;
        }

        public static bool CheckPDFIsValid(string FileNameWithPath)
        {
            bool IsPDFValidated = true;            

            try
            {

                PdfDocument document = null;

                var func = new Func<bool>(() =>
                {
                    try
                    {
                        document = PdfDocument.FromFile(FileNameWithPath);
                        document.SerialNumber = ConfigValues.HiQPDFSerialNumber;
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                });


                bool WasPDFRead = false;

                bool ThreadTimeoutPassed = false;

                ThreadTimeoutPassed = TryExecute(func, ConfigValues.PDFWorkflowTimeOutDuration, out WasPDFRead);

                if (!WasPDFRead || !ThreadTimeoutPassed)
                {
                    IsPDFValidated = false;

                }

                document.Close(); // important to close the file.
            }
            catch
            {

            }

            return IsPDFValidated;
        }

        public static bool DownloadAndValidateFile(string originalUrl, string pathWithFileName, string pathToDownloadFile, string fileName,string ThreadNumber)
        {


            bool DownloadWithHeaderStatus = true;

            bool DownloadWithoutHeaderStatus = true;

            string TempWithHeaderPDF = pathToDownloadFile + @"\" + "_Head_" + ThreadNumber + fileName;

            string TempWithoutHeaderPDF = pathToDownloadFile + @"\" + "_WOHead_" + ThreadNumber + fileName;

            bool IsPDFValidated = true;



            DownloadWithoutHeaderStatus = DownloadFileWithoutHeaders(originalUrl, TempWithoutHeaderPDF); // download file without sending any headers.     

            if (DownloadWithoutHeaderStatus)
            {
                IsPDFValidated = CheckPDFIsValid(TempWithoutHeaderPDF); // check if this downloaded file is valid

                if (!IsPDFValidated) //if pdf validation failed,see if url needs to be corrected.
                {
                    string newurl = CorrectPDFUrl(originalUrl);
                    if (newurl != originalUrl)
                    {
                        if (File.Exists(TempWithoutHeaderPDF))
                            File.Delete(TempWithoutHeaderPDF);
                        DownloadWithoutHeaderStatus = DownloadFileWithoutHeaders(newurl, TempWithoutHeaderPDF); // download file with headers

                        if (DownloadWithoutHeaderStatus)
                        {
                            IsPDFValidated = CheckPDFIsValid(TempWithoutHeaderPDF); // check if this downloaded file is valid
                        }
                    }
                }
            }
            else //if download failed correct the url in some cases this needs to be done.
            {


                string newurl = CorrectPDFUrl(originalUrl);

                if (newurl != originalUrl) //we do have a corrected url.lets try to download this url.
                {
                    if (File.Exists(TempWithoutHeaderPDF))
                        File.Delete(TempWithoutHeaderPDF);

                    DownloadWithoutHeaderStatus = DownloadFileWithoutHeaders(newurl, TempWithoutHeaderPDF); // try downloading again with a corrected url

                    if (DownloadWithoutHeaderStatus)
                    {
                        IsPDFValidated = CheckPDFIsValid(TempWithoutHeaderPDF); // check if this downloaded file is valid
                    }
                }
            }

            if (!IsPDFValidated) // if pdf downloaded without header was not validated
            {
                if (File.Exists(TempWithHeaderPDF))
                    File.Delete(TempWithHeaderPDF);

                DownloadWithHeaderStatus = DownloadFileWithHeaders(originalUrl, TempWithHeaderPDF); // download file with headers

                if (DownloadWithHeaderStatus)
                {
                    IsPDFValidated = CheckPDFIsValid(TempWithHeaderPDF); //check if the downloaded file is valid

                    if (!IsPDFValidated)
                    {
                        string newurl = CorrectPDFUrl(originalUrl);
                        if (newurl != originalUrl)
                        {
                            if (File.Exists(TempWithHeaderPDF))
                                File.Delete(TempWithHeaderPDF);
                            DownloadWithHeaderStatus = DownloadFileWithHeaders(newurl, TempWithHeaderPDF); // download file with headers
                        }
                    }
                }
                else
                {
                    string newurl = CorrectPDFUrl(originalUrl);

                    if (newurl != originalUrl) //we do have a corrected url.lets try to download this url.
                    {
                        if (File.Exists(TempWithHeaderPDF))
                            File.Delete(TempWithHeaderPDF);

                        DownloadWithHeaderStatus = DownloadFileWithHeaders(newurl, TempWithHeaderPDF); // try downloading again with a corrected url

                        if (DownloadWithHeaderStatus)
                        {
                            IsPDFValidated = CheckPDFIsValid(TempWithHeaderPDF); // check if this downloaded file is valid
                        }
                    }
                }

                if (IsPDFValidated)  //if pdf downloaded with header is valid              
                {
                    File.Copy(TempWithHeaderPDF, pathWithFileName, true);
                }

            } //if pdf downloaded without header was validated copy this file to destination
            else
            {
                File.Copy(TempWithoutHeaderPDF, pathWithFileName, true);
            }

            if (!IsPDFValidated) // if the pdf was not validated at all use atleast one of the downloaded pdfs if the download was successfull
            {
                if (DownloadWithoutHeaderStatus)
                    File.Copy(TempWithoutHeaderPDF, pathWithFileName, true);
                else
                    if (DownloadWithHeaderStatus)
                        File.Copy(TempWithHeaderPDF, pathWithFileName, true);
            }

            try
            {
                if (File.Exists(TempWithoutHeaderPDF))
                    File.Delete(TempWithoutHeaderPDF);

                if (File.Exists(TempWithHeaderPDF))
                    File.Delete(TempWithHeaderPDF);
            }
            catch
            {

            }

            return IsPDFValidated;

        }


        public static string GetFailureNotificationEmailTemplate(FailureNotificationEmailTemplate template)
        {
            string emailTemplate = string.Empty;
            switch (template)
            {
                case FailureNotificationEmailTemplate.WatchlistFailureNotification:
                    emailTemplate = "<html><body><br/>System did not receive the expected Watchlist file from <<CustomerName>> on <<Date>>. Please contact, shareholdersolutions_imp@dfsco.com for any questions or concerns.<br/><br/>Thanks,<br/>RightProspectus</body></html>";
                    break;
                case FailureNotificationEmailTemplate.CustDocUPDTFailureNotification:
                    emailTemplate = "<html><body><br/>System did not submit the expected Doc Update file to <<CustomerName>> on <<Date>>. Please contact, shareholdersolutions_imp@dfsco.com for any questions or concerns.<br/><br/>Thanks,<br/>RightProspectus</body></html>";
                    break;               
            }

            return emailTemplate;
        }

        #region FTP and FLT Logic using chilkat 

        

        public static List<string> GetOnlyFTPFileList(BCSClient bcsclient)
        {
            List<string> downloadFiles = new List<string>();
            StringBuilder result = new StringBuilder();
            FtpWebRequest request;
            try
            {

                request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + bcsclient.ClientDocsFTPPath + "/"));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(bcsclient.ClientDocsFTPUserName, bcsclient.ClientDocsFTPPassword);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.KeepAlive = false;

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {

                        string Datastring = reader.ReadToEnd();
                        FileStruct[] list = GetList(Datastring, bcsclient);


                        bool hasFiles = false;
                        foreach (FileStruct thisstruct in list)
                        {
                            if (!thisstruct.IsDirectory)
                            {
                                result.Append(thisstruct.Name);
                                result.Append("\n");
                                downloadFiles.Add(thisstruct.Name);
                                hasFiles = true;
                            }
                        }


                        if (hasFiles)
                            result.Remove(result.ToString().LastIndexOf('\n'), 1);
                        reader.Close();
                    }
                    response.Close();
                }
                return downloadFiles;
            }
            catch (Exception ex)
            {
                string ErrorEmailBody = "Error in GetOnlyFileList for " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + ex.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                downloadFiles = null;
                return downloadFiles;
            }
        }

    

        public static void DeleteFTPFileFromFTPLocation(string strFile, BCSClient bcsclient)
        {
            try
            {
                //Uri uri = new Uri("ftp://" + bcsclient.ClientDocsFTPPath + "/" + strFile);

                //Remove this once you get the correct FTP location and use the above one
                Uri uri = new Uri("ftp://Admin-RP:fXN79nGv@ftp_docs.rightprospectus.com/%2F/Adhoc-RP/TestTRPFTP/" + strFile);


                if (uri.Scheme == Uri.UriSchemeFtp)
                {

                    FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);
                    request.KeepAlive = false;
                    //request.Credentials = new NetworkCredential(bcsclient.ClientDocsFTPUserName, bcsclient.ClientDocsFTPPassword);
                    request.Credentials = new NetworkCredential("Admin-RP", "fXN79nGv");
                    request.Method = WebRequestMethods.Ftp.DeleteFile;

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                    }

                }
            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in DeleteFTPFileFromFTPLocation for " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }
            }
        }

        

        public static FileStruct[] GetList(string datastring, BCSClient bcsclient)
        {
            List<FileStruct> myListArray = new List<FileStruct>();


            try
            {
                string[] dataRecords = datastring.Split('\n');
                FileListStyle _directoryListStyle = GuessFileListStyle(dataRecords);
                foreach (string s in dataRecords)
                {
                    if (_directoryListStyle != FileListStyle.Unknown && s != "")
                    {
                        FileStruct f = new FileStruct();
                        f.Name = "..";
                        switch (_directoryListStyle)
                        {
                            case FileListStyle.UnixStyle:
                                f = ParseFileStructFromUnixStyleRecord(s, bcsclient);
                                break;
                            case FileListStyle.WindowsStyle:
                                f = ParseFileStructFromWindowsStyleRecord(s, bcsclient);
                                break;
                        }
                        if (!(f.Name == "." || f.Name == ".."))
                        {
                            myListArray.Add(f);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string ErrorEmailBody = "Error in GetList for " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + ex.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

            }

            return myListArray.ToArray(); ;
        }

        public static FileListStyle GuessFileListStyle(string[] recordList)
        {
            foreach (string s in recordList)
            {
                if (s.Length > 10
                 && Regex.IsMatch(s.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
                {
                    return FileListStyle.UnixStyle;
                }
                else if (s.Length > 8
                 && Regex.IsMatch(s.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
                {
                    return FileListStyle.WindowsStyle;
                }
            }
            return FileListStyle.Unknown;
        }

        public static FileStruct ParseFileStructFromUnixStyleRecord(string Record, BCSClient bcsclient)
        {
            ///Assuming record style as
            /// dr-xr-xr-x   1 owner    group               0 Nov 25  2002 bussys
            FileStruct f = new FileStruct();
            string temp = string.Empty;
            try
            {
                string processstr = Record.Trim();
                f.Flags = processstr.Substring(0, 9);
                f.IsDirectory = (f.Flags[0] == 'd');
                processstr = (processstr.Substring(11)).Trim();
                _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);   //skip one part
                f.Owner = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
                f.Group = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
                _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);   //skip one part
                temp = _cutSubstringFromStringWithTrim(ref processstr, ' ', 8);
                f.CreateTime = DateTime.Now;
                //f.CreateTime = DateTime.Parse(_cutSubstringFromStringWithTrim(ref processstr, ' ', 8), CultureInfo.GetCultureInfo("en-US"));
                f.Name = processstr;   //Rest of the part is name
            }
            catch (Exception ex)
            {

                string ErrorEmailBody = "Error in ParseFileStructFromUnixStyleRecord for " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + ex.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

            }
            return f;
        }

        public static FileStruct ParseFileStructFromWindowsStyleRecord(string Record, BCSClient bcsclient)
        {

            ///Assuming the record style as
            /// 02-03-04  07:46PM       <DIR>          Append
            FileStruct f = new FileStruct();
            try
            {
                string processstr = Record.Trim();
                string dateStr = processstr.Substring(0, 8);
                processstr = (processstr.Substring(8, processstr.Length - 8)).Trim();
                string timeStr = processstr.Substring(0, 7);
                processstr = (processstr.Substring(7, processstr.Length - 7)).Trim();
                //f.CreateTime = DateTime.Parse(dateStr + " " + timeStr);
                f.CreateTime = DateTime.Now;
                if (processstr.Substring(0, 5) == "<DIR>")
                {
                    f.IsDirectory = true;
                    processstr = (processstr.Substring(5, processstr.Length - 5)).Trim();
                }
                else
                {
                    string[] strs = processstr.Split(new char[] { ' ' });
                    processstr = strs[1].Trim();
                    f.IsDirectory = false;
                }
                f.Name = processstr;  //Rest is name   

            }
            catch (Exception ex)
            {

                string ErrorEmailBody = "Error in ParseFileStructFromWindowsStyleRecord for " + bcsclient.ClientName;
                Logging.LogToFile(ErrorEmailBody + ex.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

            }

            return f;
        }

        public static string _cutSubstringFromStringWithTrim(ref string s, char c, int startIndex)
        {
            int pos1 = s.IndexOf(c, startIndex);
            string retString = s.Substring(0, pos1);
            s = (s.Substring(pos1)).Trim();
            return retString;
        }

        public static string GetEnumDescription(Enum value)
        {

            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            DescriptionAttribute[] attributes =
               (DescriptionAttribute[])
             fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }
       
        public static List<string> ExtractZipFile(string archiveFilenameIn, string password, string outFolder)
        {
            List<string> extractedFilenames = new List<string>();

            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);
                if (!String.IsNullOrEmpty(password))
                {
                    zf.Password = password;     // AES encrypted entries are handled automatically
                }
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }
                    String entryFileName = zipEntry.Name;
                    if (File.Exists(outFolder + entryFileName))
                    {
                        File.Delete(outFolder + entryFileName);
                    }
                    extractedFilenames.Add(outFolder + entryFileName);

                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    String fullZipToPath = Path.Combine(outFolder, entryFileName);

                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }

            return extractedFilenames;
        }
       

        public static List<string> GetEnumDescriptionList(Type type)
        {


            List<string> list = new List<string>();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(GetEnumDescription(value));
            }

            return list;
        }

        public static string GenerateCUSIPWithCheckDigit(string cusip)
        {
            string retCUSIP = string.Empty;
            try
            {
                int sum = 0;
                char[] digits = cusip.ToUpper().ToCharArray();
                string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ*@#";

                for (int i = 0; i < digits.Length; i++)
                {
                    int val;
                    if (!int.TryParse(digits[i].ToString(), out val))
                        val = alphabet.IndexOf(digits[i]) + 10;

                    if ((i % 2) != 0)
                        val *= 2;

                    val = (val % 10) + (val / 10);

                    sum += val;
                }

                int check = (10 - (sum % 10)) % 10;

                retCUSIP = cusip + check.ToString();
            }
            catch (Exception ex)
            {
                retCUSIP = string.Empty;
            }

            return retCUSIP;
        }

        public struct FileStruct
        {
            public string Flags;
            public string Owner;
            public string Group;
            public bool IsDirectory;
            public DateTime CreateTime;
            public string Name;
        }

        public enum FileListStyle
        {
            UnixStyle,
            WindowsStyle,
            Unknown
        }
        

        #endregion


        public static string EncryptString(string planeText)
        {
            string returnVal = "";
            try
            {
                //AES Starts here 
                Chilkat.Crypt2 crypt = new Chilkat.Crypt2();

                bool success;
                success = crypt.UnlockComponent(ConfigurationManager.AppSettings.Get("ChilkatUnlockKey"));
                if (success != true)
                {
                    //MessageBox.Show(crypt.LastErrorText);
                    return null;
                }

                //  AES is also known as Rijndael.
                crypt.CryptAlgorithm = ConfigurationManager.AppSettings.Get("SSOCryptAlgorithm");

                //  CipherMode may be "ecb" or "cbc"
                crypt.CipherMode = ConfigurationManager.AppSettings.Get("SSOCipherMode");

                //  KeyLength may be 128, 192, 256
                crypt.KeyLength = 128;

                //  The padding scheme determines the contents of the bytes
                //  that are added to pad the result to a multiple of the
                //  encryption algorithm's block size.  AES has a block
                //  size of 16 bytes, so encrypted output is always
                //  a multiple of 16.
                crypt.PaddingScheme = 0;

                //  EncodingMode specifies the encoding of the output for
                //  encryption, and the input for decryption.
                //  It may be "hex", "url", "base64", or "quoted-printable".
                crypt.EncodingMode = ConfigurationManager.AppSettings.Get("SSOEncodingMode");

                //  An initialization vector is required if using CBC mode.
                //  ECB mode does not use an IV.
                //  The length of the IV is equal to the algorithm's block size.
                //  It is NOT equal to the length of the key.
                //string ivHex;
                //ivHex = "000102030405060708090A0B0C0D0E0F";

                ////crypt.SetEncodedIV(ConfigValues.SSOIV, "hex");

                //  The secret key must equal the size of the key.  For
                //  256-bit encryption, the binary secret key is 32 bytes.
                //  For 128-bit encryption, the binary secret key is 16 bytes.
                //string keyHex;
                //keyHex = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";

                crypt.SetEncodedKey(ConfigurationManager.AppSettings.Get("SSOEncodedSecretKey"), "ascii");

                //  Encrypt a string...
                //  The input string is 44 ANSI characters (i.e. 44 bytes), so
                //  the output should be 48 bytes (a multiple of 16).
                //  Because the output is a hex string, it should
                //  be 96 characters long (2 chars per byte).           
                returnVal = crypt.EncryptStringENC(planeText);
            }
            catch (Exception ex)
            {

            }
            return returnVal;

        }

        public static string DecryptString(string encryptedText)
        {
            string returnVal = "";

            try
            {
                Chilkat.Crypt2 crypt = new Chilkat.Crypt2();

                bool success;
                success = crypt.UnlockComponent(ConfigurationManager.AppSettings.Get("ChilkatUnlockKey"));
                if (success != true)
                {
                    //MessageBox.Show(crypt.LastErrorText);
                    return null;
                }

                //  AES is also known as Rijndael.
                crypt.CryptAlgorithm = ConfigurationManager.AppSettings.Get("SSOCryptAlgorithm");

                //  CipherMode may be "ecb" or "cbc"
                crypt.CipherMode = ConfigurationManager.AppSettings.Get("SSOCipherMode");

                //  KeyLength may be 128, 192, 256
                crypt.KeyLength = 128;

                //  The padding scheme determines the contents of the bytes
                //  that are added to pad the result to a multiple of the
                //  encryption algorithm's block size.  AES has a block
                //  size of 16 bytes, so encrypted output is always
                //  a multiple of 16.
                crypt.PaddingScheme = 0;

                //  EncodingMode specifies the encoding of the output for
                //  encryption, and the input for decryption.
                //  It may be "hex", "url", "base64", or "quoted-printable".
                crypt.EncodingMode = ConfigurationManager.AppSettings.Get("SSOEncodingMode");

                //  An initialization vector is required if using CBC mode.
                //  ECB mode does not use an IV.
                //  The length of the IV is equal to the algorithm's block size.
                //  It is NOT equal to the length of the key.
                //string ivHex;
                //ivHex = "000102030405060708090A0B0C0D0E0F";
                ///crypt.SetEncodedIV(ConfigValues.SSOIV, "hex");

                //  The secret key must equal the size of the key.  For
                //  256-bit encryption, the binary secret key is 32 bytes.
                //  For 128-bit encryption, the binary secret key is 16 bytes.
                //string keyHex;
                //keyHex = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F";

                crypt.SetEncodedKey(ConfigurationManager.AppSettings.Get("SSOEncodedSecretKey"), "ascii");

                //  Decrypt           

                returnVal = crypt.DecryptStringENC(encryptedText);
            }
            catch (Exception ex)
            {

            }

            return returnVal;
        }
    }
}
