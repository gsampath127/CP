using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP.Hosted.TestFramework
{
    public class TAD:PDFjs,IPage
    {
        public string Title
        {
            get { return ""; }
        }
        public By cssLinks { get { return By.ClassName("cssLinks"); } }
        public By pdfJsFrame { get { return By.Id("docFrame"); } }
        public By PDFJsDiv { get { return By.XPath("//div[@id='viewerContainer']"); } }
        public By Tabs { get { return By.TagName("li"); } }
    }
}
