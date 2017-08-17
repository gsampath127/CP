using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace RP.Hosted.TestFramework
{
    public class PDFjsFactory<TPage, TBrowser> : IFactory<TPage, TBrowser> where TPage : PDFjs, new() where TBrowser : Browser, new()
    {
        private TPage page = new TPage();
        private TBrowser browser = new TBrowser();

        public bool GoTo(string url, string browserName)
        {
            try
            {
                browser.BrowserName = browserName;
                browser.Initialize(url);
            }
            catch (Exception ex)
            {
                Log.WriteLog("Error in PDFjs GoTo. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                return false;
            }
            return browser.Driver.Title.Contains(page.Title);
        }
        /// <summary>
        /// Verifies PDF Loaded
        /// </summary>
        /// <returns></returns>
        public bool IsPDFLoaded()
        {
           
            bool pdfLoaded = false;
            while (!pdfLoaded)
            {
                try
                {
                    if (browser.Driver.FindElement(By.ClassName("textLayer")).Displayed)
                    {
                        pdfLoaded = true;
                    }

                }
                catch
                {
                    pdfLoaded = false;
                    //System.Threading.Thread.Sleep(new TimeSpan(0, 0, 5));
                }
            }
            return pdfLoaded;
        }
        
        /// <summary>
        /// Clicks Next Page
        /// </summary>
        /// <returns></returns>
        public bool NextButtonClick()
        {
            try
            {
                int pageNum = Convert.ToInt32(browser.Driver.FindElement(page.PageNumber).GetAttribute("value"));
                var nextPage = browser.Driver.FindElement(page.NextPage);
                nextPage.Click();
                int newPageNum = Convert.ToInt32(browser.Driver.FindElement(page.PageNumber).GetAttribute("value"));

                if (newPageNum > pageNum)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.WriteLog("Error in PDFjs NextButtonClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                return false; }
        }

        /// <summary>
        /// Clicks Previous Page
        /// </summary>
        /// <returns></returns>
        public bool PrevButtonClick()
        {
            try
            {
                int pageNum = Convert.ToInt32(browser.Driver.FindElement(page.PageNumber).GetAttribute("value"));
                browser.Driver.FindElement(page.PreviousPage).Click();
                int newPageNum = Convert.ToInt32(browser.Driver.FindElement(page.PageNumber).GetAttribute("value"));

                if (newPageNum < pageNum)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.WriteLog("Error in PDFjs PrevButtonClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                return false;
            }
        }

        /// <summary>
        /// Gets all failed bookmarks
        /// </summary>
        /// <returns></returns>
        public string Bookmark()
        {
            try {
                string failedBookmarks = string.Empty;

                var bookmarkView = browser.Driver.FindElement(page.BookmarkView);
                if (bookmarkView.Displayed)
                {
                    var bookmarkItems = bookmarkView.FindElements(page.BookmarkItem);
                    foreach (var item in bookmarkItems)
                    {
                        try
                        {
                            item.Click();
                            System.Threading.Thread.Sleep(1000);
                            string currentPageNum = browser.Driver.FindElement(page.PageNumber).GetAttribute("value").ToString();
                            string fragment = new System.Uri(item.GetAttribute("href")).Fragment;
                            string bookmarkPageNum = fragment.Split('&')[0].Replace("#page=", "").Trim();
                            if (currentPageNum != bookmarkPageNum)
                            {
                                failedBookmarks += item.Text + ", ";
                            }
                        }
                        catch
                        {
                            failedBookmarks += item.Text + ", ";
                        }
                    }
                }
                return failedBookmarks;
            }
            catch(Exception ex)
            {
                return "Error validating bookmarks. " + ex.Message;
                Log.WriteLog("Error in PDFjs Bookmark. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
            }
        }



        /// <summary>
        /// Clicks Last Page from secondary tool bar
        /// </summary>
        /// <returns></returns>
        public bool LastPageClick()
        {
            try
            {
                //Clicking the secondary tool bar to make last page button visible
                browser.Driver.FindElement(page.SecondaryToolBar).Click();
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 2));
                browser.Driver.FindElement(page.LastPage).Click();

                string maxnum = browser.Driver.FindElement(page.PageNumber).GetAttribute("max");
                string pagenum = browser.Driver.FindElement(page.PageNumber).GetAttribute("value");
                if (pagenum == maxnum)
                {
                   
                    return true;
                   
                }
                else
                {
                   
                    return false;
                    
                }
                
            }
            catch (Exception ex) {
                Log.WriteLog("Error in PDFjs LastPageClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                return false;
            }
        }

        /// <summary>
        /// Clicks First Page from secondary tool bar
        /// </summary>
        /// <returns></returns>
        public bool FirstPageClick()
         {
            try {
                //Clicking the secondary tool bar to make first page button visible
                browser.Driver.FindElement(page.SecondaryToolBar).Click();
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 2));
                browser.Driver.FindElement(page.FirstPage).Click();
                string minnum = browser.Driver.FindElement(page.PageNumber).GetAttribute("min");
                string pagenum = browser.Driver.FindElement(page.PageNumber).GetAttribute("value");
                if (pagenum == minnum)
                {
                    
                    return true;
                }
                else
                {
                    
                    return false;
                }
               
            }
            catch (Exception ex) {
                Log.WriteLog("Error in PDFjs FirstPageClick. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                return false; }
        }

        /// <summary>
        /// Clicks Document Properties from secondary tool bar 
        /// </summary>
        public bool DocumentProperties()
        {
            try {
                //Clicking the secondary tool bar to make last page button visible
                browser.Driver.FindElement(page.SecondaryToolBar).Click();
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 2));
                browser.Driver.FindElement(page.DocumentProperties).Click();
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 2));

                // TODO : Validation
                var docProperty = browser.Driver.FindElement(page.DocumentPropertiesDiv);
               
                
                return docProperty.Displayed;
            }
            catch (Exception ex) {
                Log.WriteLog("Error in PDFjs DocumentProperties. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                return false; }
        }/// <summary>
         /// Clicks Document Properties from secondary tool bar 
         /// </summary>
        public bool CloseDocumentProperties()
        {
            try {
                //Clicking close button in Document Properties
                browser.Driver.FindElement(page.DocumentPropertiesClose).Click();
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 3));
                // TODO : Validation
                var docProperty = browser.Driver.FindElement(page.DocumentPropertiesDiv);
                return !docProperty.Displayed;
            }
            catch (Exception ex) {
                Log.WriteLog("Error in PDFjs CloseDocumentProperties. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                return false; }
        }

        /// <summary>
        /// Finds the word for single occurance
        /// </summary>
        /// <returns></returns>
        public bool FindSingle(string searchVal = "the")
        {
            try {
                //Clicks find button to open the find input(text box)
                if (!browser.Driver.FindElement(page.FindBar).Displayed)
                    browser.Driver.FindElement(page.ViewFind).Click();

                // Search the page giving some input in the text box ex:the
                browser.Driver.FindElement(page.FindInput).Clear();
                browser.Driver.FindElement(page.FindInput).SendKeys(searchVal.ToLower());

                //TODO : Sleep the thread for 1 second 

                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 5));
                var textElement = browser.Driver.FindElement(page.HighlightSelected);
                if (searchVal.ToLower().Contains(textElement.Text.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) {
                Log.WriteLog("Error in PDFjs FindSingle. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                return false; }
        }

        /// <summary>
        /// Finds the word for all occurances
        /// </summary>
        /// <returns></returns>
        public bool FindAll(string searchVal = "the")
        {
            try {
                bool result = false;
                //Clicks find button to open the find input(text box)
                if (!browser.Driver.FindElement(page.FindBar).Displayed)
                    browser.Driver.FindElement(page.ViewFind).Click();

                // Search the page giving some input in the text box ex:the
                browser.Driver.FindElement(page.FindInput).Clear();
                browser.Driver.FindElement(page.FindInput).SendKeys(searchVal.ToLower());
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 5));
                //Click Hightlight All 
                browser.Driver.FindElement(page.FindHighlightAll).Click();
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 5));

                // TODO :Sleep the thread for 1 sec
                var highlightedElements = browser.Driver.FindElements(page.HighlightedElements);

                foreach (var item in highlightedElements)
                {
                    if (searchVal.ToLower().Contains(item.Text.ToLower()))
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return result;
            }
            catch (Exception ex) {
                Log.WriteLog("Error in PDFjs FindAll. Url " + browser.Driver.Url + " for browser " + browser.BrowserName + ", Error: " + ex.Message + "," + DateTime.Now.ToString());
                return false; }
        }

        public void CloseBrowser()
        {
            browser.Close();
        }
    }
}

  