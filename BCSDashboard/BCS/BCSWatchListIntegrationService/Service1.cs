using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using System.Timers;
using System.Configuration.Install;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;

namespace BCSWatchListIntegrationService
{
    public partial class Service1 : ServiceBase
    {

        private static BCSFLTandFTPLogicFactory fltandftpLogicfactory = new BCSFLTandFTPLogicFactory();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            
            System.Threading.ThreadStart RunBCSPickUPWatchListFileAndProcessJob = new System.Threading.ThreadStart(RunBCSPickUPWatchListFileAndProcess);
            System.Threading.Thread RunBCSPickUPWatchListFileAndProcessJobThread = new System.Threading.Thread(RunBCSPickUPWatchListFileAndProcessJob);
            RunBCSPickUPWatchListFileAndProcessJobThread.Start();


            Logging.LogToFile("BCSWatchListIntegration Service STARTED At " + DateTime.Now);


            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
            {
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, Process.GetCurrentProcess().ProcessName, "BCS.BCSWatchListIntegrationService Service Started " + ConfigValues.AppEnvironment, "support", null);
            }
        }

        protected override void OnStop()
        {
            Logging.LogToFile("BCSWatchListIntegrationService STOPPED At " + DateTime.Now);


            foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
            {
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, Process.GetCurrentProcess().ProcessName, "BCS.BCSWatchListIntegrationService Service Stopped " + ConfigValues.AppEnvironment, "support", null);
            }
        }


        public void RunBCSPickUPWatchListFileAndProcess()
        {
            while (true)
            {
                try
                {
                    ServiceFactory servicefactory = new ServiceFactory();

                    List<BCSClient> bcsclients = servicefactory.GetWatchListClientConfigs();

                    foreach (BCSClient bcsclient in bcsclients)
                    {
                        // Do this only if FLT is enabled for this client.

                        // Pick Up , Process and Archive the FLT File
                        BCSPickUpStoreAndDeleteWatchListFile(bcsclient);

                        fltandftpLogicfactory.ProcessWatchListFile(bcsclient);                        

                    }


                    Thread.Sleep(Convert.ToInt32(ConfigValues.TimerInterval));
                }
                catch (Exception expt)
                {
                    string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function RunBCSPickUPWatchListFileAndProcess : " + expt.Message;

                    Logging.LogToFile(ErrorEmailBody + expt.Message);

                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                    }

                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }
            }

        }


        private void BCSPickUpStoreAndDeleteWatchListFile(BCSClient bcsclient)
        {
            List<string> WatchListFileName = new List<string>();

            string strDropDirectory = string.Empty;
            bool doesFileExists = false;

            
            BCSWatchListFileDownload(bcsclient, out WatchListFileName, out strDropDirectory);


            if (WatchListFileName.Count > 0)
            {


                string WatchListFileNames = string.Empty;

                foreach (string f in WatchListFileName)
                {
                    WatchListFileNames = WatchListFileNames + "<br />" + f;
                }

                string emailString = "<html><head><title></title></head><body>";
                emailString = emailString + @"<br /><font size=""5"" color=""#666666"" face=""Arial, Helvetica, sans-serif"">" + "BCS " + bcsclient.ClientName + " WatchList File FTP Alert</font><br /><br />";
                emailString = emailString + "List of Document(s) received on " + DateTime.Now.ToString() + ":<hr />" + WatchListFileNames + "<hr/>";

                emailString = emailString + "</body></html>";

                Logging.LogToFile(emailString);

                foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, "BCS " + bcsclient.ClientName + " WatchList File Alert", emailString, "Support", null);
                }


                foreach (string DailyFile in WatchListFileName)
                {
                    DeleteWatchListFile(DailyFile, bcsclient);
                }

                foreach (string DailyFile in WatchListFileName)
                {
                    fltandftpLogicfactory.InsertWatchListHistory(DailyFile, bcsclient);
                }

            }


            //Thread.Sleep(Convert.ToInt32(ConfigValues.TimerInterval));


        }

        private void DeleteWatchListFile(string FLTFile, BCSClient bcsclient)
        {
            try
            {
                Uri uri = new Uri(bcsclient.CUSIPWatchlistPickupFTPLocation + "/" + FLTFile);
                if (uri.Scheme == Uri.UriSchemeFtp)
                {

                    FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);
                    request.KeepAlive = false;
                    request.Credentials = new NetworkCredential(bcsclient.CUSIPWatchlistPickupFTPUserName, bcsclient.CUSIPWatchlistPickupFTPPassword);
                    request.Method = WebRequestMethods.Ftp.DeleteFile;

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {

                    }



                }
            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function DeleteWatchListFile : " + expt.Message;

                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }
        }


        # region "BCSWatchListFileDownload"

        private void BCSWatchListFileDownload(BCSClient bcsclient, out List<string> DailyWatchListFiles, out string WatchListDownloadPath)
        {
            List<string> WatchListFileList = GetOnlyFileList(bcsclient);
            WatchListDownloadPath = bcsclient.CUSIPWatchListArchiveDropPath;
            List<string> erroredFiles = new List<string>();
            
            

            if (WatchListFileList != null)
            {
                try
                {
                    foreach (string FLTFile in WatchListFileList)
                    {
                        Uri uri = new Uri(bcsclient.CUSIPWatchlistPickupFTPLocation  + "/" + FLTFile);



                        if (uri.Scheme == Uri.UriSchemeFtp)
                        {
                            try
                            {
                                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);
                                request.KeepAlive = false;
                                request.Credentials = new NetworkCredential(bcsclient.CUSIPWatchlistPickupFTPUserName, bcsclient.CUSIPWatchlistPickupFTPPassword);
                                request.Method = WebRequestMethods.Ftp.DownloadFile;

                                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                                {

                                    Encoding objEncoding = Encoding.Default;

                                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), objEncoding))
                                    {



                                        if (File.Exists(bcsclient.CUSIPWatchListArchiveDropPath + FLTFile))
                                        {
                                            
                                            //Rename the duplicate if any in local path
                                            File.Copy(bcsclient.CUSIPWatchListArchiveDropPath + FLTFile, bcsclient.CUSIPWatchListArchiveDropPath + Path.GetFileNameWithoutExtension(FLTFile) + DateTime.Now.ToString("_yyyymmddhhmmss") + ".txt");
                                            File.Delete(bcsclient.CUSIPWatchListArchiveDropPath + FLTFile);
                                        }


                                        using (StreamWriter tws = new StreamWriter(bcsclient.CUSIPWatchListArchiveDropPath + FLTFile, false, objEncoding))
                                        {
                                            tws.Write(reader.ReadToEnd());
                                            tws.Close();
                                        }
                                        

                                        reader.Close();
                                    }
                                    response.Close();
                                }
                            }
                            catch (Exception expt)
                            {

                                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function BCSWatchListFileDownload : " + expt.Message;

                                Logging.LogToFile(ErrorEmailBody + expt.Message);

                                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                                {
                                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                                }

                                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                            }
                        }
                    }
                    foreach (string f in erroredFiles)
                    {
                        WatchListFileList.Remove(f);
                    }

                }
                catch (Exception expt)
                {
                    string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function BCSWatchListFileDownload : " + expt.Message;

                    Logging.LogToFile(ErrorEmailBody + expt.Message);

                    foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                    {
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                    }

                    System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                }

            }

            DailyWatchListFiles = WatchListFileList;

        }

        public List<string> GetOnlyFileList(BCSClient bcsclient)
        {
            List<string> downloadFiles = new List<string>();
            StringBuilder result = new StringBuilder();
            FtpWebRequest request;
            try
            {

                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(bcsclient.CUSIPWatchlistPickupFTPLocation));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(bcsclient.CUSIPWatchlistPickupFTPUserName, bcsclient.CUSIPWatchlistPickupFTPPassword);
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
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function GetOnlyFileList : " + expt.Message;

                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);

                downloadFiles = null;
                return downloadFiles;
            }
        }

        private FileStruct[] GetList(string datastring, BCSClient bcsclient)
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
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function GetList : " + expt.Message;

                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }

            return myListArray.ToArray(); ;
        }

        public FileListStyle GuessFileListStyle(string[] recordList)
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

        private FileStruct ParseFileStructFromUnixStyleRecord(string Record, BCSClient bcsclient)
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
            catch (Exception expt)
            {

                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function ParseFileStructFromUnixStyleRecord : " + expt.Message;

                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }
            return f;
        }

        private FileStruct ParseFileStructFromWindowsStyleRecord(string Record, BCSClient bcsclient)
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
            catch (Exception expt)
            {

                string ErrorEmailBody = "Error in BCSWatchListIntegrationService in function ParseFileStructFromWindowsStyleRecord : " + expt.Message;

                Logging.LogToFile(ErrorEmailBody + expt.Message);

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);
                }

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }

            return f;
        }

        private string _cutSubstringFromStringWithTrim(ref string s, char c, int startIndex)
        {
            int pos1 = s.IndexOf(c, startIndex);
            string retString = s.Substring(0, pos1);
            s = (s.Substring(pos1)).Trim();
            return retString;
        }

        # endregion

        



        #region File Struct

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


    }


    [RunInstallerAttribute(true)]
    public partial class Installer1 : Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller processInstaller;

        public Installer1()
        {

            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            // Service will run under system account
            processInstaller.Account = ServiceAccount.LocalSystem;

            // Service will have Start Type of Manual
            serviceInstaller.StartType = ServiceStartMode.Manual;

            serviceInstaller.ServiceName = "BCS.BCSWatchListIntegrationService";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);

        }

    }
}
