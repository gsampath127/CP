using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;

namespace RP.Hosted.TestFramework
{
    public class Browser : IBrowser
    {
        private IWebDriver webDriver = null;

        public string BrowserName
        {
            get { return webDriver.GetType().ToString(); }
            set
            {
                try
                {
                    if (value.ToUpper().Contains("FIREFOX"))
                    {
                        //FirefoxBinary binary = new FirefoxBinary();
                        //FirefoxProfile profile = new FirefoxProfile(@"C:\Users\rr215388\AppData\Roaming\Mozilla\Firefox\Profiles\ce3h1lr5.SeleniumUser");
                        //profile.SetPreference("webdriver.firefox.profile", "SeleniumUser");
                        webDriver = new FirefoxDriver(/*profile*/);
                    }
                    else if (value.ToUpper().Contains("CHROME"))
                    {
                        var option = new ChromeOptions();
                        option.AddArgument("no-sandbox");
                        webDriver = new ChromeDriver(option);
                    }
                    else
                    {
                        InternetExplorerOptions options = new InternetExplorerOptions();
                        options.ToCapabilities();
                        options.EnableNativeEvents = false;
                        options.EnablePersistentHover = true;
                        webDriver = new InternetExplorerDriver(options);
                    }
                }
                catch(Exception ex)
                {
                    if (webDriver != null)
                        Close();
                    throw ex;
                }

            }
        }
        public IWebDriver Driver
        {
            get
            {
                return webDriver;
            }
        }

        public string Title
        {
            get
            {
                return webDriver.Title;
            }
        }

        public void Close()
        {
            webDriver.Quit();
        }

        public void Initialize(string url)
        {
            webDriver.Url = url;
        }
    }
}
