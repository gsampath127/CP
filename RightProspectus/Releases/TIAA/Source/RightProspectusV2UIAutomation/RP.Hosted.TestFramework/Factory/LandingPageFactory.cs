using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RP.Hosted.TestFramework
{
    public class LandingPageFactory<TPage, TBrowser> : IFactory<TPage, TBrowser>
        where TPage : LandingPage, new()
        where TBrowser : Browser, new()
    {
        private TPage landingPage = new TPage();
        private TBrowser browser = new TBrowser();
        
      

        public bool GoTo(string url, string browserName)
        {
            browser.BrowserName = browserName.ToString();
            browser.Initialize(url);
            return browser.Driver.Title.Contains(landingPage.Title);
        }

        public string GetDocuments()
        {
            var docs = browser.Driver.FindElements(landingPage.Fundlinks).Take(1); //TODO : Remove Take
            string docNotAvailableLinks = string.Empty;
            foreach (var item in docs)
            {
                if(item.Displayed)
                {
                    item.Click();
                    System.Threading.Thread.Sleep(new TimeSpan(0,1,10)); // wait till page loads completely
                    var tabFrame = browser.Driver.SwitchTo().Window(browser.Driver.WindowHandles[1]);

                    HttpWebResponse response = null;
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(tabFrame.Url);
                    request.Method = "GET";

                    response = (HttpWebResponse)request.GetResponse();
                    int status = (int)response.StatusCode;
                    if (status != 200)
                    {
                        docNotAvailableLinks = tabFrame.Url + ",";
                    }

                }
                browser.Driver.Close(); //Closes the current tab or window
                browser.Driver.SwitchTo().Window(browser.Driver.WindowHandles[0]); //Switch to lannding page
            }
            return docNotAvailableLinks;
        }
    }
}
