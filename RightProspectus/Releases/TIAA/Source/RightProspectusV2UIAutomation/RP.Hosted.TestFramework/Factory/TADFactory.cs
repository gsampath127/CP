using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace RP.Hosted.TestFramework
{
    public class TADFactory<TPage, TBrowser> : IFactory<TPage, TBrowser>
        where TPage : TAD, new()
        where TBrowser : Browser, new()
    {
        private TPage page = new TPage();
        private TBrowser browser = new TBrowser();


        public bool GoTo(string url, string browserName)
        {
            try
            {
                browser.BrowserName = browserName;
                System.Threading.Thread.Sleep(4000);
                browser.Initialize(url);
                System.Threading.Thread.Sleep(4000);
                return true;
            }
            catch(Exception ex)
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

        public string FundLinkClick()
        {
            string docNotAvailableLinks = string.Empty;
            try
            {
                var docs = browser.Driver.FindElements(page.cssLinks); //TODO : Remove Take
                if (docs != null && docs.Count() > 0)
                    foreach (var item in docs)
                    {
                        if (item.Displayed)
                        {
                            item.Click();
                            var tabFrame = browser.Driver.SwitchTo().Window(browser.Driver.WindowHandles[1]);
                            System.Threading.Thread.Sleep(2000);
                            if (tabFrame.FindElements(page.Tabs).Count() <= 0)
                            {
                                docNotAvailableLinks = item.Text + "[" + tabFrame.Url + "], \n";
                            }
                        }
                        browser.Driver.Close(); //Closes the current tab or window
                        browser.Driver.SwitchTo().Window(browser.Driver.WindowHandles[0]); //Switch to landing page
                    }
                else
                    Log.WriteLog("Couldn't access element cssLinks. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + "," + DateTime.Now.ToString());
            }
            catch(Exception ex)
            {
                Log.WriteLog("Error in TAD FundLinkClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                CloseBrowser();
            }
            return docNotAvailableLinks;
        }

        public void CloseBrowser()
        {
            try
            {
                browser.Close();
            }
            catch (Exception ex)
            {
                Log.WriteLog("Error closing browser, " + DateTime.Now.ToString());
            }
        }
    }
}
