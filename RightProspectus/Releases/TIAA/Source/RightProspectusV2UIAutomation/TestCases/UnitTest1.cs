using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RP.Hosted.TestFramework;

namespace TestCases
{
    [TestClass]
    public class UnitTest1
    {
        private string Url = "https://tools-stage.rightprospectus.com/Viewer/Viewer.aspx?file=%2fViewer%2fHosted%2fGetDocument.ashx%3ffile%3dhttp%3a%2f%2fwww.rightprospectus.com%2fdocuments%2fTransamerica%2fSUM_AssetAllConservative.pdf";

        /// <summary>
        /// Validates whether its navigating to document page, using the page title. Test will fail if document not found
        /// </summary>
        [TestMethod]
        public void PDFjsChromeTest()
        {
            //Assert.IsTrue(Pages.PDFjs.GoTo(Url, HostedEnum.BrowserEnum.Chrome), "Page Open failed");
            //System.Threading.Thread.Sleep(4000);
            //Assert.IsTrue(Pages.PDFjs.NextButtonClick(browser.Driver), "Next Button click failed");
            //System.Threading.Thread.Sleep(4000);
            //Assert.IsTrue(Pages.PDFjs.PrevButtonClick(), "Previous Button click failed");
            //System.Threading.Thread.Sleep(4000);
            //string bookMarkValidationResult = Pages.PDFjs.Bookmark();
            //Assert.IsTrue(string.IsNullOrEmpty(bookMarkValidationResult), "Failed Bookmarks: " + bookMarkValidationResult);
            //Assert.IsTrue(Pages.PDFjs.LastPageClick(), "Last Page click failed");
            //Assert.IsTrue(Pages.PDFjs.FirstPageClick(), "First Page click failed");
            //Assert.IsTrue(Pages.PDFjs.DocumentProperties());
            //Assert.IsTrue(Pages.PDFjs.CloseDocumentProperties());
            //Assert.IsTrue(Pages.PDFjs.FindSingle("Summary Prospectus"), "Search failed");
            //Assert.IsTrue(Pages.PDFjs.FindAll("Summary Prospectus"), "Find All failed");
        }
       

        [TestMethod]
        public void LandingPageTest()
        {
            //Pages.LandingPage.GoTo("http://hosted-stage.rightprospectus.com/artisan/",HostedEnum.BrowserEnum.Chrome);
            //Pages.LandingPage.GetDocuments();
        }

        [TestMethod]
        public void TADTest()
        {
            //Pages.TAD.GoTo("http://connect-stage.rightprospectus.com/transAmericaFunds/",HostedEnum.BrowserEnum.Chrome);
            //Pages.TAD.FundLinkClick();
        }

        [TestMethod]
        public void TADFTest()
        {

            //Pages.TADF.GoTo("http://connect-stage.rightprospectus.com/transAmericaFunds/TADF/523/18?isInternalTAID=True", HostedEnum.BrowserEnum.Chrome);
            //var result = Pages.TADF.DocumentLoad();
            //Assert.AreEqual(result.Item1, 0);

            //Pages.TADF.SwitchTabs();
        }

        [TestMethod]
        public void TabbedNavigationTest()
        {
            //string myUrl = "http://connect-stage.rightprospectus.com/transAmericaFunds/TADF/523/18?isInternalTAID=True";
            //Pages.TADF.GoTo(myUrl,HostedEnum.BrowserEnum.Chrome);
            //System.Threading.Thread.Sleep(new TimeSpan(0, 0, 3));
            //Pages.TADF.SwitchTabs();

        }

        [TestMethod]
        public void PDFJsTest()
        {
            var pdfUrl = "https://tools-stage.rightprospectus.com/Viewer/Viewer.aspx?file=%2fViewer%2fHosted%2fGetDocument.ashx%3ffile%3dhttp%3a%2f%2fwww.rightprospectus.com%2fdocuments%2fTransamerica%2fSUM_AssetAllConservative.pdf";
            //Execute PDF.js test cases
            var pdfJsPAge = new PDFjsFactory<PDFjs, Browser>();
            pdfJsPAge.GoTo(pdfUrl, "Chrome");
            bool documentLoaded = pdfJsPAge.IsPDFLoaded();
            if (documentLoaded)
            {
                Assert.IsTrue(pdfJsPAge.DocumentProperties());
            }
        }
    }
}
