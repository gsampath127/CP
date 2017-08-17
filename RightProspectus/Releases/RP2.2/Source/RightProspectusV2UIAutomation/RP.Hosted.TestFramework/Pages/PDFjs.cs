using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace RP.Hosted.TestFramework
{
    public class PDFjs : IPage
    {
        public string Title { get { return "GetDocument"; } }        
        public By NextPage { get { return By.XPath("//button[@id='next']"); } }
        public By PreviousPage { get { return By.XPath("//button[@id='previous']"); } }
        public By PageNumber { get { return By.XPath("//input[@id='pageNumber']"); } }
        public By BookmarkView { get { return By.XPath("//div[@id='outlineView']"); } }
        public By BookmarkItem { get { return By.XPath("//div[@class='outlineItem']/a"); } }
        public By AnchorTag { get { return By.XPath("a"); } }
        public By SecondaryToolBar { get { return By.XPath("//button[@id='secondaryToolbarToggle']"); } }
        public By FirstPage { get { return By.XPath("//button[@id='firstPage']"); } }
        public By LastPage { get { return By.XPath("//button[@id='lastPage']"); } }
        public By ViewFind { get { return By.XPath("//button[@id='viewFind']"); } }
        public By FindInput { get { return By.XPath("//input[@id='findInput']"); } }
        public By FindBar { get { return By.XPath("//div[@id='findbar']"); } }
        public By HighlightSelected { get { return By.XPath("//span[@class='highlight selected']"); } }
        public By FindHighlightAll { get { return By.XPath("//input[@id='findHighlightAll']"); } }
        public By HighlightedElements { get { return By.ClassName("highlight"); } }
        public By DocumentProperties { get { return By.XPath("//button[@id='documentProperties']"); } }
        public By DocumentPropertiesDiv { get { return By.XPath("//div[@id='documentPropertiesOverlay']"); } }
        public By DocumentPropertiesClose { get { return By.XPath("//button[@id='documentPropertiesClose']"); } }
    }
}
