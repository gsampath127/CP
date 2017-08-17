using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP.Hosted.TestFramework
{
    public interface IBrowser
    {
        void Initialize(string url);
        string Title { get; }
        IWebDriver Driver { get; }
        void Close();
        string BrowserName { get; set; }

    }
}
