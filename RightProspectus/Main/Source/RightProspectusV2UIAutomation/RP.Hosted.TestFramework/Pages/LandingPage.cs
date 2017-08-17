using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP.Hosted.TestFramework
{
    public class LandingPage : PDFjs,IPage
    {
        public new string Title
        {
            get { return "title"; }
        }
        
        public By Fundlinks { get { return By.ClassName("fundLinks"); } }
    }
}
