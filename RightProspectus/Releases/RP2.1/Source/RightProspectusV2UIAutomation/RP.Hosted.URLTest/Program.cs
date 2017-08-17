using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using RP.Hosted.TestFramework;
using Utilities;

namespace ConsoleApplication1
{
    class Program
    {
        private static string sourcePath = ConfigurationManager.AppSettings["SourceFolder"].ToString();
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Process Started!!!");
                CreateRequiredFolders();
                MonitorFolder();
                Console.WriteLine("Process Completed!!!");

            }
            catch(Exception ex)
            {
                Log.WriteLog("Process failed " + ex.Message + ", " + DateTime.Now.ToString());                
            }
        }

        private static void MonitorFolder()
        {
            DirectoryInfo dInfo = new DirectoryInfo(sourcePath);
            string[] extensions = new string[] { ".xls", ".xlsx" };
            foreach (var e in dInfo.GetFiles())
            {
                if (extensions.Contains(e.Extension))
                {
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
                }
            }
        }

        private static List<TestResult> RunTest(IList<ConfigData> configData, IList<FileData> fileData)
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
                                foreach (var browser in browsers)
                                //Parallel.ForEach(browsers, (browser) =>
                                {
                                    var tadPage = new TADFactory<TAD, Browser>();
                                    try
                                    {
                                        if (!tadPage.GoTo(row.Url, browser))
                                            testResult.Add(new TestResult() { Url = row.Url, Browser = browser, Page = row.Page, TestCase = "GoTo", Description = "Navigation failed", Type = TestResultEnum.Failure });

                                        // Click on each fund link in the page
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
                                //Parallel.ForEach(browsers, (browser) =>
                                foreach (var browser in browsers)
                                {
                                    var tadfPage = new TADFFactory<TADF, Browser>();
                                    try
                                    {
                                        if (!tadfPage.GoTo(row.Url, browser))
                                            testResult.Add(new TestResult() { Url = row.Url, Browser = browser, Page = row.Page, TestCase = "GoTo", Description = "Navigation failed", Type = TestResultEnum.Failure });

                                        var results = tadfPage.SwitchTabs(configData);
                                        foreach (var res in results)
                                        {
                                            testResult.Add(new TestResult() { Url = row.Url, Browser = browser, Page = row.Page, TestCase = "PDF.js Validations", Description = res.Item1, Type = res.Item2 });
                                        }

                                        Log.WriteLog("Completed Page " + row.Url + " for browser " + browser + "," + DateTime.Now.ToString());

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
            catch (Exception ex)
            {
                Log.WriteLog("Error in RunTest. Error: " + ex.Message + "," + DateTime.Now.ToString());
            }
            return testResult;
        }

        private static void CreateRequiredFolders()
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
