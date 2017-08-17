using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using RP.Hosted.TestFramework;
using Utilities;

namespace TestAutomationService
{
    public partial class ExecuteTest : ServiceBase
    {
        Timer _timer;
        private static string sourcePath = ConfigurationManager.AppSettings["SourceFolder"].ToString();
        private static int timeInterval = Convert.ToInt32(ConfigurationManager.AppSettings["timeInterval"].ToString());
        private static int timeDelay = Convert.ToInt32(ConfigurationManager.AppSettings["timeDelay"].ToString());
        public ExecuteTest()
        {
            InitializeComponent();
            _timer = new Timer(MonitorFolder, null, Timeout.Infinite, Timeout.Infinite);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                CreateRequiredFolders();
                _timer.Change(timeDelay, timeInterval);
                
            }
            catch
            {

            }
        }
        protected override void OnStop()
        {
            Log.WriteLog("Service stopped, " + DateTime.Now.ToString());
        }

        private void MonitorFolder(object state)
        {
            DirectoryInfo dInfo = new DirectoryInfo(sourcePath);
            string[] extensions = new string[] { ".xls", ".xlsx" };
            foreach (var e in dInfo.GetFiles())
            {
                if (extensions.Contains(e.Extension))
                {
                    //pause timer during processing so it
                    //wont be run twice if the processing takes longer
                    //than the interval for some reason
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                    try
                    {
                        Log.WriteLog("Reading file " + e.Name + ", " + DateTime.Now.ToString());
                        var configData = ConfigDataCollection.GetData(Path.Combine(sourcePath, e.Name), ConfigurationManager.AppSettings["ConfigSheetName"].ToString());
                        var fileData = FileDataCollection.GetData(Path.Combine(sourcePath, e.Name), ConfigurationManager.AppSettings["URLSheetName"].ToString());

                        Log.WriteLog("Running testcase started ," + DateTime.Now.ToString());
                        var results = RunTest(configData, fileData);
                        Log.WriteLog("Running testcase completed ," + DateTime.Now.ToString());

                        string resultFile = CreateExcel.ExportExcel(results, sourcePath, e.Name);
                        Log.WriteLog("Results saved ," + DateTime.Now.ToString());
                        if (!string.IsNullOrWhiteSpace(resultFile))
                        {
                            string emailBody = string.Empty;
                            bool emailResult = false;
                            string From = ConfigurationManager.AppSettings["From"];
                            string To = ConfigDataCollection.GetValue(configData, "Email");

                            if (results.Count() > 0)
                            {
                                emailBody = "Test completed.There are " + results.Count() + " errors. Please find attached the test result.";
                                emailResult = EmailUtility.SendMail(From, To, string.Empty, emailBody, "RP Test Automation", resultFile, string.Empty, string.Empty);
                            }
                            else
                            {
                                emailBody = "Test completed.There are " + results.Count() + " errors.";
                                emailResult = EmailUtility.SendMail(From, To, string.Empty, emailBody, "RP Test Automation", string.Empty, string.Empty, string.Empty);
                            }

                            if (emailResult)
                                Log.WriteLog("Email sent to " + To + " ," + DateTime.Now.ToString());
                            else
                                Log.WriteLog("Email failed ," + DateTime.Now.ToString());

                        }
                        else
                        {
                            Log.WriteLog("Coundn't Save Results," + DateTime.Now.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLog("Error in MonitorFolder, Error: " + ex.Message + ". " + ex.InnerException + "," + DateTime.Now.ToString());
                    }

                    //reset timer to monitor the folder
                    _timer.Change(timeDelay, timeInterval);
                }
            }
        }

        private List<TestResult> RunTest(IList<ConfigData> configData, IList<FileData> fileData)
        {
            List<TestResult> testResult = new List<TestResult>();
            string[] browsers = ConfigDataCollection.GetValue(configData, "Browsers").Split(',');
            try
            {
                foreach (var row in fileData)
                {
                    try
                    {
                        switch (row.Page.ToUpper())
                        {
                            case "TAD": // LandingPage / ProductPage with direct link to FundPage
                                foreach (var browser in browsers /*Enum.GetValues(typeof(HostedEnum.BrowserEnum)).Cast<HostedEnum.BrowserEnum>()*/)
                                //Parallel.ForEach<HostedEnum.BrowserEnum>(Enum.GetValues(typeof(HostedEnum.BrowserEnum)).Cast<HostedEnum.BrowserEnum>(), (browser) =>
                                {
                                    var tadPage = new TADFactory<TAD, Browser>();
                                    try
                                    {
                                        //Log.WriteLog("Testing Goto. Url " + row.Url + " for browser " + browser + "," + DateTime.Now.ToString());
                                        if (!tadPage.GoTo(row.Url, browser))
                                            testResult.Add(new TestResult() { Url = row.Url, Browser = browser, Page = row.Page, TestCase = "GoTo", Description = "Navigation failed", Type = TestResultEnum.Failure });

                                        // Click on each fund link in the page
                                        //Log.WriteLog("Testing FundLinkClick. Url " + row.Url + " for browser " + browser + "," + DateTime.Now.ToString());
                                        string failedLinks = tadPage.FundLinkClick();
                                        if (!string.IsNullOrWhiteSpace(failedLinks))
                                            testResult.Add(new TestResult() { Url = row.Url, Browser = browser, Page = row.Page, TestCase = "FundLinkClick", Description = "Non working links: " + failedLinks, Type = TestResultEnum.Failure });
                                        
                                        Log.WriteLog("Completed Page " + row.Url + " for browser " + browser + "," + DateTime.Now.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.WriteLog("Error in TAD Page " + row.Url + " for browser " + browser + ", Error: " + ex.Message + ". " + ex.InnerException.Message + "," + DateTime.Now.ToString());
                                    }
                                    finally
                                    {
                                        tadPage.CloseBrowser();
                                    }
                                //});
                                }
                                break;

                            case "TADF": // FundPage / TabbedView Page
                                //Parallel.ForEach<HostedEnum.BrowserEnum>(Enum.GetValues(typeof(HostedEnum.BrowserEnum)).Cast<HostedEnum.BrowserEnum>(), (browser) =>
                                foreach (var browser in browsers /*Enum.GetValues(typeof(HostedEnum.BrowserEnum)).Cast<HostedEnum.BrowserEnum>()*/)
                                {
                                    var tadfPage = new TADFFactory<TADF, Browser>();
                                    try
                                    {
                                        //Log.WriteLog("Testing Goto. Url " + row.Url + " for browser " + browser + "," + DateTime.Now.ToString());
                                        if (!tadfPage.GoTo(row.Url, browser))
                                            testResult.Add(new TestResult() { Url = row.Url, Browser = browser, Page = row.Page, TestCase = "GoTo", Description = "Navigation failed", Type = TestResultEnum.Failure });

                                        //Log.WriteLog("Testing DocumentLoad. Url " + row.Url + " for browser " + browser + "," + DateTime.Now.ToString());
                                        //var result = tadfPage.DocumentLoad();
                                        //if (result.Item1 > 0)
                                        //    testResult.Add(new TestResult() { Url = row.Url, Browser = browser, Page = row.Page, TestCase = "DocumentLoad", Description = result.Item2, Type = result.Item3 });
                                        //else
                                        //{
                                            //Log.WriteLog("Testing SwitchTabs. Url " + row.Url + " for browser " + browser + "," + DateTime.Now.ToString());
                                            var results = tadfPage.SwitchTabs(configData);
                                            foreach (var res in results)
                                            {
                                                testResult.Add(new TestResult() { Url = row.Url, Browser = browser, Page = row.Page, TestCase = "PDF.js Validations", Description = res.Item1, Type = res.Item2 });
                                            }

                                            Log.WriteLog("Completed Page " + row.Url + " for browser " + browser + "," + DateTime.Now.ToString());
                                        //}
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.WriteLog("Error in TADF Page " + row.Url + " for browser " + browser + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                                    }
                                    finally
                                    {
                                        tadfPage.CloseBrowser();
                                    }
                                //});
                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLog("Error in RunTest. Page " + row.Url + "  Error: " + ex.Message + ". " + ex.InnerException + "," + DateTime.Now.ToString());
                    }

                }
            }
            catch(Exception ex)
            {
                Log.WriteLog("Error in RunTest. Error: " + ex.Message + "," + DateTime.Now.ToString());
            }
            return testResult;
        }

        

        private bool ValidateInputFile(string name)
        {
            string[] extensions = new string[] { ".xls", ".xlsx" };
            bool isFileExist = false;
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            if (!dir.Exists)
                isFileExist = false;
            else
            {
                string fileFullPath = Path.Combine(sourcePath, name);
                FileInfo fInfo = new FileInfo(fileFullPath);
                if (fInfo.Exists)
                {
                    if (extensions.Contains(fInfo.Extension))
                        isFileExist = true;
                }
            }
            return isFileExist;
        }

        private void CreateRequiredFolders()
        {
            string resultsFolder = Path.Combine(sourcePath, "results");
            string logFolder = Path.Combine(sourcePath, "log");

            if (!Directory.Exists(resultsFolder))
                Directory.CreateDirectory(resultsFolder);
            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);

        }
    }
}
