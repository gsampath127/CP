using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP.Hosted.TestFramework
{
    public static class Pages
    {
        public static PDFjsFactory<PDFjs, Browser> PDFjs
        {
            get
            {
                var page = new PDFjsFactory<PDFjs, Browser>();
                return page;
            }
        }
       

        //Landing Page
        public static LandingPageFactory<LandingPage, Browser> LandingPage
        {
            get
            {
                var page = new LandingPageFactory<LandingPage, Browser>();
                return page;
            }
        }

        //TAD Page
        public static TADFactory<TAD, Browser> TAD
        {
            get
            {
                var page = new TADFactory<TAD, Browser>();
                return page;
            }
        }

     
        public static TADFFactory<TADF, Browser> TADF
        {
            get
            {
                var page = new TADFFactory<TADF, Browser>();
                return page;
            }
        }
    }
}
