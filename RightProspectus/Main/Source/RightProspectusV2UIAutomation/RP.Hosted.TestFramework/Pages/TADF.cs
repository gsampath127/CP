using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace RP.Hosted.TestFramework
{
    public class TADF : IPage
    {
        public string Title { get { return "GetDocument"; } }
        public By Tabs { get { return By.XPath("//ul[@id='NavMenu']/li"); } }
        public By IFrames { get { return By.TagName("iframe"); } }
        public By PDFDocument { get { return By.ClassName("pdfViewer"); } }
        public By PDFErrorWrapper { get { return By.XPath("//div[@id='errorWrapper']"); } }
    }
}
