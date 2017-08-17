using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Utilities
{
    /// <summary>
    /// FileStruct
    /// </summary>
    public class FileStruct
    {
        /// <summary>
        /// Flags
        /// </summary>
        public string Flags { get; set; }
        /// <summary>
        /// Owner
        /// </summary>
        public string Owner { get; set; }        
        /// <summary>
        /// Group
        /// </summary>
        public string Group { get; set; }         
        /// <summary>
        /// IsDirectory
        /// </summary>
        public bool IsDirectory { get; set; }        
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }        
    }
    /// <summary>
    /// enum FileListStyle
    /// </summary>
    public enum FileListStyle
    {   
        /// <summary>
        /// UnixStyle
        /// </summary>
        UnixStyle,
        /// <summary>
        /// WindowsStyle
        /// </summary>
        WindowsStyle,
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown
    }

    /// <summary>
    /// Class FTPHelper.
    /// </summary>
    public static class FTPHelper
    {
        /// <summary>
        /// GuessFileListStyle
        /// </summary>
        /// <param name="recordList"></param>
        /// <returns></returns>
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

        /// <summary>
        /// ParseFileStructFromUnixStyleRecord
        /// </summary>
        /// <param name="Record"></param>
        /// <returns></returns>
        public static FileStruct ParseFileStructFromUnixStyleRecord(string Record)
        {
            ///Assuming record style as
            /// dr-xr-xr-x   1 owner    group               0 Nov 25  2002 bussys
            FileStruct f = new FileStruct();
            string processstr = Record.Trim();
            f.Flags = processstr.Substring(0, 9);
            f.IsDirectory = (f.Flags[0] == 'd');
            processstr = (processstr.Substring(11)).Trim();
            _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);   //skip one part
            f.Owner = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
            f.Group = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
            _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);   //skip one part            
            f.CreateTime = DateTime.Now;
            //f.CreateTime = DateTime.Parse(_cutSubstringFromStringWithTrim(ref processstr, ' ', 8));
            f.Name = processstr;   //Rest of the part is name
            return f;
        }

        
        /// <summary>
        /// ParseFileStructFromWindowsStyleRecord
        /// </summary>
        /// <param name="Record"></param>
        /// <returns></returns>
        public static FileStruct ParseFileStructFromWindowsStyleRecord(string Record)
        {
            ///Assuming the record style as
            /// 02-03-04  07:46PM       <DIR>          Append
            FileStruct f = new FileStruct();
            string processstr = Record.Trim();            
            processstr = (processstr.Substring(8, processstr.Length - 8)).Trim();
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
            return f;
        }

        /// <summary>
        /// _cutSubstringFromStringWithTrim
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string _cutSubstringFromStringWithTrim(ref string s, char c, int startIndex)
        {
            int pos1 = s.IndexOf(c, startIndex);
            string retString = s.Substring(0, pos1);
            s = (s.Substring(pos1)).Trim();
            return retString;
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="datastring"></param>
        /// <returns></returns>
        public static List<FileStruct> GetList(string datastring)
        {
            List<FileStruct> myList = new List<FileStruct>();
            string[] dataRecords = datastring.Split('\n');
            FileListStyle _directoryListStyle = GuessFileListStyle(dataRecords);
            foreach (string s in dataRecords)
            {
                if (_directoryListStyle != FileListStyle.Unknown && string.IsNullOrEmpty(s))
                {
                    FileStruct f = new FileStruct();
                    f.Name = "..";
                    switch (_directoryListStyle)
                    {
                        case FileListStyle.UnixStyle:
                            f = ParseFileStructFromUnixStyleRecord(s);
                            break;
                        case FileListStyle.WindowsStyle:
                            f = ParseFileStructFromWindowsStyleRecord(s);
                            break;
                    }
                    if (!(f.Name == "." || f.Name == ".."))
                    {
                        myList.Add(f);
                    }
                }
            }
            return myList; ;
        }

        /// <summary>
        /// DownloadFileFromFTP
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static byte[] DownloadFileFromFTP(string ftp, string userid, string password, string filename)
        {

            Uri uri = new Uri("ftp://" + ftp + "/" + filename);

            var bytes = default(byte[]);

            if (uri.Scheme == Uri.UriSchemeFtp)
            {

                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);
                request.KeepAlive = false;
                request.Credentials = new NetworkCredential(userid, password);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Encoding objEncoding = Encoding.Default;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), objEncoding))
                    {
                        using (var memstream = new MemoryStream())
                        {
                            reader.BaseStream.CopyTo(memstream);
                            bytes = memstream.ToArray();
                        }
                    }
                }

            }

            return bytes;           
        }
        /// <summary>
        /// RemoveWhiteSpaces
        /// </summary>
        /// <param name="processString"></param>
        /// <returns></returns>
        public static string RemoveWhiteSpaces(string processString)
        {
            string resultString = string.Empty;
            char[] arrayOfCharacters = processString.ToCharArray();
            foreach (char singleCharacter in arrayOfCharacters)
            {
                if (singleCharacter != ' ')
                {
                    resultString = resultString + singleCharacter.ToString();
                }
            }
            return resultString;
        }

        /// <summary>
        /// GetFileListFromFTP
        /// </summary>
        /// <param name="_ftp"></param>
        /// <param name="_user"></param>
        /// <param name="_password"></param>
        /// <returns></returns>
        public static List<string> GetFileListFromFTP(string _ftp, string _user, string _password)
        {
            List<string> downloadFiles = new List<string>();
            
            //StringBuilder result = new StringBuilder();
            FtpWebRequest request;


                request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + _ftp + "/"));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(_user, _password);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.KeepAlive = false;

                using (WebResponse response = request.GetResponse())
                {
                    //List<string> entries = new List<string>();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {

                        string processString;
                        string dirname = string.Empty , dirdate = string.Empty;
                        processString = reader.ReadLine();
                        while (processString != null)
                        {
                            if (processString.IndexOf("<DIR>", 0) == -1)
                            {
                                string processedString = RemoveWhiteSpaces(processString);
                                dirdate = processedString.Substring(0, processedString.IndexOf("<DIR>", 0));
                                dirname = processedString.Substring(processedString.IndexOf("<DIR>", 0) + 5, processedString.Length - (processedString.IndexOf("<DIR>", 0) + 5));
                                

                            }
                            
                            processString = reader.ReadLine();

                        }

                        if (string.IsNullOrEmpty(dirname) || string.IsNullOrEmpty(dirdate)) { }
                    }
                    



                    //Loop and add details of each File to the DataTable.

                    
                }
                return downloadFiles;

            

        }

        /// <summary>
        /// DeleteFileFromFTP
        /// </summary>
        /// <param name="ftpserverwithpath"></param>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <param name="strFile"></param>
        public static void DeleteFileFromFTP(string ftpserverwithpath, string userid, string password, string strFile)
        {


            Uri uri = new Uri("ftp://" + userid + ":" + password + "@" + ftpserverwithpath + "/" + strFile);
            if (uri.Scheme == Uri.UriSchemeFtp)
            {

                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);

                request.KeepAlive = false;
                request.Credentials = new NetworkCredential(userid, password);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {

                }
            }
        }

    }
}
