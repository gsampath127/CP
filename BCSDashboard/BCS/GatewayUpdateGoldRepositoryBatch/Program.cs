using BCS.ObjectModel.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GatewayUpdateGoldRepositoryBatch
{
    public class Program
    {
        private static string environment = ConfigurationManager.AppSettings["AppEnvironment"];
        private static string ftpUserName = ConfigurationManager.AppSettings["FTPUserID"];
        private static string ftpPword = ConfigurationManager.AppSettings["FTPUserPassword"];
        private static string gatewayFtpDir = ConfigurationManager.AppSettings["GatewayFTPPath"];
        private static string workingDirectory = ConfigurationManager.AppSettings["WorkingDirectory"];
        private static string aPfPdfFtpDir = gatewayFtpDir + environment + @"/AP/";
        private static string oPfPdfFtpDir = gatewayFtpDir + environment + @"/OP/";
        private static string oPfPdfWorkDir = workingDirectory + @"oFpfPDFWork\";
        private static string aPfPdfWorkDir = workingDirectory + @"aFpfPDFWork\";
        private static string goldPdfRepositoryDir = workingDirectory + @"GoldPdfRepository\";
        private static string destLogDir = ConfigurationManager.AppSettings["FTPLogPath"];
        public static string loggingDir = workingDirectory + @"Log\";

        private static StreamWriter swPreProcessLogFile;
        private static string preProcessLogFilename;

        static void Main(string[] args)
        {
            UpdateGoldRepository();
        }

        private static void UpdateGoldRepository()
        {
            string logFilesDateTime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
            preProcessLogFilename = "PreProcess_UpdateGoldRepository_log_" + logFilesDateTime + ".txt";
            swPreProcessLogFile = new StreamWriter(loggingDir + preProcessLogFilename, true);

            List<string> oPFPDFFiles = GetFTPList(oPfPdfFtpDir);
            List<string> aPFPDFFiles = GetFTPList(aPfPdfFtpDir);

            DownloadFilesFromIntFtp(oPfPdfWorkDir, oPfPdfFtpDir, oPFPDFFiles);
            DownloadFilesFromIntFtp(aPfPdfWorkDir, aPfPdfFtpDir, aPFPDFFiles);

            MovePdfFiles();

            swPreProcessLogFile.Close();            
        }

        private static List<string> GetFTPList(string uri)
        {
            List<string> directories = new List<string>();
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(uri);
                ftpRequest.Credentials = new NetworkCredential(ftpUserName, ftpPword);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());

                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    directories.Add(line);
                    line = streamReader.ReadLine();
                }

                streamReader.Close();
            }
            catch (Exception ex)
            {
                LogActivity("GetFTPList failed: URI: " + uri + "|" + ex.ToString(), true, true);                
            }
            return directories;
        }

        private static void DownloadFilesFromIntFtp(string localDir, string ftpDir, List<string> FilesToGet)
        {
            try
            {
                foreach (string FileToGet in FilesToGet)
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpDir + FileToGet);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    request.Credentials = new NetworkCredential(ftpUserName, ftpPword);
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                    Stream responseStream = response.GetResponseStream();

                    FileStream fs = new FileStream(localDir + FileToGet, FileMode.Create);
                    CopyStream(fs, responseStream);
                    fs.Close();

                    //Delete file
                    FtpWebRequest requestDelete = (FtpWebRequest)WebRequest.Create(ftpDir + FileToGet);
                    requestDelete.Credentials = new NetworkCredential(ftpUserName, ftpPword);
                    requestDelete.Method = WebRequestMethods.Ftp.DeleteFile;
                    FtpWebResponse responseDelete = (FtpWebResponse)requestDelete.GetResponse();
                    LogActivity("DownloadFilesFromIntFtp succeeded: File: " + localDir + FileToGet + "; From FTPDir: " + ftpDir, false, false);
                }
            }
            catch (Exception ex)
            {
                LogActivity("DownloadFilesFromIntFtp failed: LocalDir: " + localDir + " FTPDir: " + ftpDir + "|" + ex.ToString(), true, true);
            }            
        }

        private static void CopyStream(Stream destination, Stream source)
        {
            int count;
            byte[] buffer = new byte[4096];
            while ((count = source.Read(buffer, 0, buffer.Length)) > 0)
                destination.Write(buffer, 0, count);
        }


        private static void MovePdfFiles()
        {
            foreach (string oWorkDirPDF in Directory.GetFiles(oPfPdfWorkDir, "*.pdf"))
            {
                string oPdfCopyToFileName = Path.GetFileName(oWorkDirPDF).Substring(Path.GetFileName(oWorkDirPDF).IndexOf("_") + 1);

                if (File.Exists(oWorkDirPDF))
                {
                    try
                    {                        
                        File.Copy(oWorkDirPDF, goldPdfRepositoryDir + oPdfCopyToFileName, true);
                        File.Delete(oWorkDirPDF);
                    }
                    catch (Exception ex)
                    {
                        LogActivity("Copy failed: " + oWorkDirPDF + " To " + goldPdfRepositoryDir + oPdfCopyToFileName + " | " + ex.ToString(), true, false);
                    }

                    if (File.Exists(goldPdfRepositoryDir + oPdfCopyToFileName))
                    {                        
                        LogActivity("Copy succeeded: " + oWorkDirPDF + " To " + goldPdfRepositoryDir + oPdfCopyToFileName, false, false);                        
                    }
                    else
                    {
                        LogActivity("Copy failed: " + oWorkDirPDF + " To " + goldPdfRepositoryDir + oPdfCopyToFileName, true, false);                        
                    }
                }

            }

            foreach (string aWorkDirPDF in Directory.GetFiles(aPfPdfWorkDir, "*.pdf"))
            {
                string aPdfCopyToFileName = Path.GetFileName(aWorkDirPDF).Substring(Path.GetFileName(aWorkDirPDF).IndexOf("_") + 1);

                if (File.Exists(aWorkDirPDF))
                {
                    try
                    {                       
                        File.Copy(aWorkDirPDF, goldPdfRepositoryDir + aPdfCopyToFileName, true);
                        File.Delete(aWorkDirPDF);
                    }
                    catch (Exception ex)
                    {
                        LogActivity("Copy failed: " + aWorkDirPDF + " To " + goldPdfRepositoryDir + aPdfCopyToFileName + " | " + ex.ToString(), true, false);
                    }
                }

                //Verify that file copied successfully
                if (File.Exists(goldPdfRepositoryDir + aPdfCopyToFileName))
                {
                    LogActivity("Copy succeeded: " + aWorkDirPDF + " To " + goldPdfRepositoryDir + aPdfCopyToFileName, false, false);                   
                }
                else
                {
                    LogActivity("Copy failed: " + aWorkDirPDF + "To " + goldPdfRepositoryDir + aPdfCopyToFileName, true, false);                    
                }
            }           
        }


        static void LogActivity(string logText, bool isError, bool stopProcessing)
        {
            string statusText = "";

            if (isError)
            {
                if (stopProcessing)
                {
                    statusText = "PROCESS TERMINATED|";
                }
                else
                {
                    statusText = "ERROR|";
                }
            }
            else
            {
                statusText = "VALID|";
            }
            swPreProcessLogFile.WriteLine(statusText + DateTime.Now.ToString() + "|" + logText);

            if (stopProcessing)
            {
                swPreProcessLogFile.Close();                

                foreach (string eml in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null, ConfigValues.BCSExceptionEmailSub, statusText + DateTime.Now.ToString() + "|" + logText, "support", null);
                }

                //Stop the process altogether 
                Environment.Exit(999);                
            }
        }       
    }
}
