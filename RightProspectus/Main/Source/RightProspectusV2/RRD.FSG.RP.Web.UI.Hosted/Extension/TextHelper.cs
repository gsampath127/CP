using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Generic calss for setting the properties for Site Text and Page Text
    /// </summary>
    /// <typeparam name="T">View Model</typeparam>
    public  class TextHelper
    {        
        /// <summary>
        /// Sets the values for Site Text by Object Reference
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cachedSiteText"></param>
        public static void GetSiteText(BaseViewModel model, List<HostedSiteText> cachedSiteText)
        {
            if (cachedSiteText.Exists(p => p.ResourceKey == "LogoText"))
            {
                model.LogoText = cachedSiteText.Find(p => p.ResourceKey == "LogoText").Text;
            }
            if (cachedSiteText.Exists(p => p.ResourceKey == "BrowserAlertText"))
            {
                model.BrowserAlertText = cachedSiteText.Find(p => p.ResourceKey == "BrowserAlertText").Text;
            }
            if (cachedSiteText.Exists(p => p.ResourceKey == "DocumentNotAvailableText"))
            {
                model.DocumentNotAvailableText = cachedSiteText.Find(p => p.ResourceKey == "DocumentNotAvailableText").Text;
            }
        }

        /// <summary>
        /// Gets all Page Text by Page
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cachedPageText"></param>
        /// <param name="pageName"></param>
        public static void GetPageText(BaseViewModel model, List<HostedPageText> cachedPageText, string pageName)
        {
            
            switch (pageName.ToUpper())
            {
                case "TADF":
                    GetTADFPageText(model, cachedPageText);
                    break; 
                case "TAGD":
                    GetTAGDPageText(model, cachedPageText);
                    break;                
            }
        }
        
        /// <summary>
        /// Gets Page Text for TAGD
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cachedPageText"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private static void GetTAGDPageText(BaseViewModel model, List<HostedPageText> cachedPageText)
        {            
            var tagdModel = model as TaxonomyAssociationGroupViewModel;

            if (cachedPageText.Exists(p => p.ResourceKey == "TAGD_DocumentNotAvailableText"))
            {
                tagdModel.DocumentNotAvailableText = cachedPageText.Find(p => p.ResourceKey == "TAGD_DocumentNotAvailableText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAGD_ClientCustomHeader"))
            {
                tagdModel.ClientCustomHeader = cachedPageText.Find(p => p.ResourceKey == "TAGD_ClientCustomHeader").Text;
            }

            if (cachedPageText.Exists(p => p.ResourceKey == "TAGD_FootnotesHeaderText") && model.GetType().GetProperty("FootnotesHeaderText") != null)
            {
                tagdModel.FootnotesHeaderText = cachedPageText.Find(p => p.ResourceKey == "TAGD_FootnotesHeaderText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAGD_Glossary"))
            {
                tagdModel.Glossary = cachedPageText.Find(p => p.ResourceKey == "TAGD_Glossary").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAGD_UnderlayingFundGridFundNameColumnText"))
            {
                tagdModel.UnderlayingFundGridFundNameColumnText = cachedPageText.Find(p => p.ResourceKey == "TAGD_UnderlayingFundGridFundNameColumnText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAGD_GroupHeaderText"))
            {
                tagdModel.GroupHeaderText = cachedPageText.Find(p => p.ResourceKey == "TAGD_GroupHeaderText").Text;
            }
            if (cachedPageText.Exists(p => p.ResourceKey == "TAGD_LogoText"))
            {
               
                tagdModel.TAGDLogoText = HttpUtility.HtmlDecode(cachedPageText.Find(p => p.ResourceKey == "TAGD_LogoText").Text);
            }
            else
            {
                tagdModel.TAGDLogoText = tagdModel.LogoText;
            }
        }

        /// <summary>
        /// Gets Page Text for TADF
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cachedPageText"></param>
        private static void GetTADFPageText(BaseViewModel model, List<HostedPageText> cachedPageText)
        {
            var tagdModel = model as TaxonomySpecificDocumentFrameViewModel;


            if (cachedPageText.Exists(p => p.ResourceKey == "TADF_LogoText"))
            {

                tagdModel.TADFLogoText = cachedPageText.Find(p => p.ResourceKey == "TADF_LogoText").Text;
            }
            else
            {
                tagdModel.TADFLogoText = tagdModel.LogoText;
            }
        }
    }
}