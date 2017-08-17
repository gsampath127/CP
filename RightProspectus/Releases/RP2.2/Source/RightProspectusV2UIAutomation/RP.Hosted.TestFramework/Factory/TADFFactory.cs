using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using Utilities;
using OpenQA.Selenium.Chrome;

namespace RP.Hosted.TestFramework
{
    public class TADFFactory<TPage, TBrowser> : IFactory<TPage, TBrowser>
        where TPage : TADF, new()
        where TBrowser : Browser, new()
    {

        private TPage page = new TPage();
        private TBrowser browser = new TBrowser();

        public bool GoTo(string url, string browserName)
        {
            try
            {
                browser.BrowserName = browserName;
                browser.Initialize(url);
                return true;
            }
            catch (Exception ex)
            {
                if (browser.Driver != null)
                {
                    Log.WriteLog("Error in TADF GoTo. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                    CloseBrowser();
                }
                else
                    Log.WriteLog("Error in TADF GoTo. couldn't initialize Url., Error: " + ex.Message + "," + DateTime.Now.ToString());

                return false;
            }
        }

        public Tuple<int, string, Utilities.TestResultEnum> DocumentLoad()
        {
            var result = new Tuple<int, string, Utilities.TestResultEnum>(0, "Valid document loaded", Utilities.TestResultEnum.Success);
            try
            {
                browser.Driver.SwitchTo().Frame(0);
                var pdfContainer = browser.Driver.FindElement(page.PDFDocument);
                var errorWrapper = browser.Driver.FindElement(page.PDFErrorWrapper);
                if (errorWrapper.Displayed)
                    result = new Tuple<int, string, Utilities.TestResultEnum>(1, "Error loading document " + browser.Driver.Url, Utilities.TestResultEnum.Failure);

                browser.Driver.SwitchTo().DefaultContent();
            }
            catch (Exception ex)
            {
                Log.WriteLog("Error in TADF DocumentLoad. couldn't initialize Url., Error: " + ex.Message + "," + DateTime.Now.ToString());
                result = new Tuple<int, string, Utilities.TestResultEnum>(2, "Error in TADF DocumentLoad. couldn't initialize Url.", Utilities.TestResultEnum.Failure);
                CloseBrowser();
            }
            return result;
        }

        /// <summary>
        /// Navigates between the tabs
        /// </summary>
        /// <returns></returns>
        public List<Tuple<string, Utilities.TestResultEnum>> SwitchTabs(IList<ConfigData> configData)
        {
            var resultSet = new List<Tuple<string, Utilities.TestResultEnum>>();
            string failedDocs = string.Empty;
            browser.Driver.SwitchTo().DefaultContent();
            var Tabs = browser.Driver.FindElements(page.Tabs);
            foreach (var defaultTab in Tabs)
            {
                var tabNames = ConfigDataCollection.GetValue(configData, "TADF Tabs");

                if (tabNames.Contains(defaultTab.Text))
                {
                    defaultTab.Click();
                    System.Threading.Thread.Sleep(2000);

                    var pdfUrl = browser.Driver.FindElement(page.IFrames).GetAttribute("src").ToString();

                    //Execute PDF.js test cases
                    var pdfJsPAge = new PDFjsFactory<PDFjs, Browser>();

                    pdfJsPAge.GoTo(pdfUrl, browser.BrowserName);
                    bool documentLoaded = pdfJsPAge.IsPDFLoaded();
                    if (documentLoaded)
                    {
                        try
                        {
                            //Log.WriteLog("Testing DocumentProperties. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + "," + DateTime.Now.ToString());
                            if (!pdfJsPAge.DocumentProperties())
                                resultSet.Add(new Tuple<string, Utilities.TestResultEnum>("PDF.js Show Document Properties failed " + browser.Driver.Url, Utilities.TestResultEnum.Failure));

                            System.Threading.Thread.Sleep(2000);

                            if (!pdfJsPAge.CloseDocumentProperties())
                                resultSet.Add(new Tuple<string, Utilities.TestResultEnum>("PDF.js Close Document Properties failed " + browser.Driver.Url, Utilities.TestResultEnum.Failure));
                            System.Threading.Thread.Sleep(2000);
                        }
                        catch (Exception ex) { Log.WriteLog("Error in TADF pdfJsPAge.DocumentProperties. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString()); }

                        try
                        {
                            //Log.WriteLog("Testing NextButtonClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + "," + DateTime.Now.ToString());
                            if (!pdfJsPAge.NextButtonClick())
                                resultSet.Add(new Tuple<string, Utilities.TestResultEnum>("PDF.js Next button click failed " + browser.Driver.Url, Utilities.TestResultEnum.Failure));
                            System.Threading.Thread.Sleep(4000);
                        }
                        catch (Exception ex) {
                            Log.WriteLog("Error in TADF pdfJsPAge.NextButtonClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());

                        }

                        try
                        {
                            //Log.WriteLog("Testing PrevButtonClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + "," + DateTime.Now.ToString());
                            if (!pdfJsPAge.PrevButtonClick())
                                resultSet.Add(new Tuple<string, Utilities.TestResultEnum>("PDF.js Prev button click failed " + browser.Driver.Url, Utilities.TestResultEnum.Failure));
                            System.Threading.Thread.Sleep(4000);
                        }
                        catch (Exception ex) { Log.WriteLog("Error in TADF pdfJsPAge.PrevButtonClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString()); }


                        try
                        {
                            //Log.WriteLog("Testing Bookmarks. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + "," + DateTime.Now.ToString());
                            string bookMarkValidationResult = pdfJsPAge.Bookmark();
                            if (!string.IsNullOrEmpty(bookMarkValidationResult))
                                resultSet.Add(new Tuple<string, Utilities.TestResultEnum>("PDF.js Clicking Bookmarks " + bookMarkValidationResult + " failed for " + browser.Driver.Url, Utilities.TestResultEnum.Failure));
                            System.Threading.Thread.Sleep(4000);
                        }
                        catch (Exception ex) { Log.WriteLog("Error in TADF pdfJsPAge.Bookmark. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString()); }

                        try
                        {
                            //Log.WriteLog("Testing FirstPageClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + "," + DateTime.Now.ToString());
                            if (!pdfJsPAge.FirstPageClick())
                                resultSet.Add(new Tuple<string, Utilities.TestResultEnum>("PDF.js First Page click failed " + browser.Driver.Url, Utilities.TestResultEnum.Failure));
                        }
                        catch (Exception ex) { Log.WriteLog("Error in TADF pdfJsPAge.FirstPageClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString()); }
                        System.Threading.Thread.Sleep(2000);

                        try
                        {
                            //Log.WriteLog("Testing FindSingle. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + "," + DateTime.Now.ToString());
                            string searchKey = ConfigDataCollection.GetValue(configData, "Search Key");
                            if (!pdfJsPAge.FindSingle(searchKey))
                                resultSet.Add(new Tuple<string, Utilities.TestResultEnum>("PDF.js Find text failed " + browser.Driver.Url, Utilities.TestResultEnum.Failure));
                        }
                        catch (Exception ex) { Log.WriteLog("Error in TADF pdfJsPAge.FindSingle. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString()); }
                        System.Threading.Thread.Sleep(2000);

                        try
                        {
                            //Log.WriteLog("Testing LastPageClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + "," + DateTime.Now.ToString());
                            if (!pdfJsPAge.LastPageClick())
                                //if (!pdfJsPAge.FirstPageClick())
                                    resultSet.Add(new Tuple<string, Utilities.TestResultEnum>("PDF.js LastPageClick failed " + browser.Driver.Url, Utilities.TestResultEnum.Failure));
                        }
                        catch (Exception ex) { Log.WriteLog("Error in TADF pdfJsPAge.LastPageClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString()); }
                        System.Threading.Thread.Sleep(2000);

                        pdfJsPAge.CloseBrowser();
                    }
                }
                else
                {
                    if (!defaultTab.Text.ToLower().Contains("xbrl"))
                        Log.WriteLog("Tab Name " + defaultTab.Text + " not in the configuration TADF Tabs," + DateTime.Now.ToString());
                }
            }
            return resultSet;
        }

        public void CloseBrowser()
        {
            try
            {
                browser.Close();
            }
            catch(Exception ex)
            {
                Log.WriteLog("Error closing browser, " + DateTime.Now.ToString());
            }
        }


    }
}
