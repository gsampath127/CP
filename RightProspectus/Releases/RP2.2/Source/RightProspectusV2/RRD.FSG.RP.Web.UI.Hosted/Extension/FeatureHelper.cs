// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 04-29-2016
//
// Last Modified By : 
// Last Modified On : 04-29-2016
// ***********************************************************************

using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class FeatureHelper.
    /// </summary>
    public static class FeatureHelper
    {
        /// <summary>
        /// Gets the feature modes XBRL.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;FeatureEnums.XBRL&gt;.</returns>
        public static List<FeatureEnums.XBRL> GetFeatureModesXBRL(int mode)
        {
            FeatureEnums.XBRL featureMode = (FeatureEnums.XBRL)mode;
            List<FeatureEnums.XBRL> featureModes = new List<FeatureEnums.XBRL>();

            if (featureMode.HasFlag(FeatureEnums.XBRL.Disabled))
            {
                featureModes.Add(FeatureEnums.XBRL.Disabled);
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.Enabled))
            {
                featureModes.Add(FeatureEnums.XBRL.Enabled);
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.ShowXBRLInNewTab))
            {
                featureModes.Add(FeatureEnums.XBRL.ShowXBRLInNewTab);
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.ShowXBRLInTabbedView))
            {
                featureModes.Add(FeatureEnums.XBRL.ShowXBRLInTabbedView);
            }
            if (featureMode.HasFlag(FeatureEnums.XBRL.ShowXBRLInLandingPage))
            {
                featureModes.Add(FeatureEnums.XBRL.ShowXBRLInLandingPage);
            }
            return featureModes;
        }

        /// <summary>
        /// Gets the feature modes request material.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;FeatureEnums.RequestMaterial&gt;.</returns>
        public static List<FeatureEnums.RequestMaterial> GetFeatureModesRequestMaterial(int mode)
        {
            FeatureEnums.RequestMaterial featureMode = (FeatureEnums.RequestMaterial)mode;
            List<FeatureEnums.RequestMaterial> featureModes = new List<FeatureEnums.RequestMaterial>();

            if (featureMode.HasFlag(FeatureEnums.RequestMaterial.Disabled))
            {
                featureModes.Add(FeatureEnums.RequestMaterial.Disabled);
            }
            if (featureMode.HasFlag(FeatureEnums.RequestMaterial.Enabled))
            {
                featureModes.Add(FeatureEnums.RequestMaterial.Enabled);
            }

            return featureModes;
        }
        /// <summary>
        /// GetFeatureModesSinglePdfView
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;eatureEnums.SinglePdfView&gt;.</returns>
        public static List<FeatureEnums.SinglePdfView> GetFeatureModesSinglePdfView(int mode)
        {
            FeatureEnums.SinglePdfView featureMode = (FeatureEnums.SinglePdfView)mode;
            List<FeatureEnums.SinglePdfView> featureModes = new List<FeatureEnums.SinglePdfView>();

            if (featureMode.HasFlag(FeatureEnums.SinglePdfView.Disabled))
            {
                featureModes.Add(FeatureEnums.SinglePdfView.Disabled);
            }
            if (featureMode.HasFlag(FeatureEnums.SinglePdfView.Enabled))
            {
                featureModes.Add(FeatureEnums.SinglePdfView.Enabled);
            }
            if (featureMode.HasFlag(FeatureEnums.SinglePdfView.ShowClientLogoFrame))
            {
                featureModes.Add(FeatureEnums.SinglePdfView.ShowClientLogoFrame);
            }

            return featureModes;
        }


        /// <summary>
        /// Gets the feature modes FormN_MFP.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;MultiSelectDropDownViewModel&gt;.</returns>
        public static List<FeatureEnums.FormN_MFP> GetFeatureModesFormN_MFP(int mode)
        {
            FeatureEnums.FormN_MFP featureMode = (FeatureEnums.FormN_MFP)mode;
            List<FeatureEnums.FormN_MFP> featureModes = new List<FeatureEnums.FormN_MFP>();

            if (featureMode.HasFlag(FeatureEnums.FormN_MFP.Disabled))
            {
                featureModes.Add(FeatureEnums.FormN_MFP.Disabled);
            }
            if (featureMode.HasFlag(FeatureEnums.FormN_MFP.Enabled))
            {
                featureModes.Add(FeatureEnums.FormN_MFP.Enabled);
            }
            return featureModes;
        }
        /// <summary>
        /// Gets the feature modes Daily Money Market Disclosure.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;DailyMoneyMarketDisclosure&gt;.</returns>
        public static List<FeatureEnums.DailyMoneyMarketDisclosure> GetFeatureModesDailyMoneyMarketDisclosure(int mode)
        {
            FeatureEnums.DailyMoneyMarketDisclosure featureMode = (FeatureEnums.DailyMoneyMarketDisclosure)mode;
            List<FeatureEnums.DailyMoneyMarketDisclosure> featureModes = new List<FeatureEnums.DailyMoneyMarketDisclosure>();

            if (featureMode.HasFlag(FeatureEnums.DailyMoneyMarketDisclosure.Disabled))
            {
                featureModes.Add(FeatureEnums.DailyMoneyMarketDisclosure.Disabled);
            }
            if (featureMode.HasFlag(FeatureEnums.DailyMoneyMarketDisclosure.Enabled))
            {
                featureModes.Add(FeatureEnums.DailyMoneyMarketDisclosure.Enabled);
            }
            return featureModes;
        }
        /// <summary>
        /// Gets the feature modes N-CR.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;MultiSelectDropDownViewModel&gt;.</returns>
        public static List<FeatureEnums.NCR> GetFeatureModesNCR(int mode)
        {
            FeatureEnums.NCR featureMode = (FeatureEnums.NCR)mode;
            List<FeatureEnums.NCR> featureModes = new List<FeatureEnums.NCR>();

            if (featureMode.HasFlag(FeatureEnums.NCR.Disabled))
            {
                featureModes.Add(FeatureEnums.NCR.Disabled);
            }
            if (featureMode.HasFlag(FeatureEnums.NCR.Enabled))
            {
                featureModes.Add(FeatureEnums.NCR.Enabled);
            }
            return featureModes;
        }

        public static BrowserVersionObjectModel ValidateLatestBrowser(BrowserVersionObjectModel browserVersionObjectModel, System.Web.HttpBrowserCapabilitiesBase browser)
        {
            browserVersionObjectModel.IsLatest = browser.MajorVersion >= browserVersionObjectModel.MinimumVersion ? true : false;
            return browserVersionObjectModel;
        }

        /// <summary>
        /// Gets the feature modes All Categories
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;MultiSelectDropDownViewModel&gt;.</returns>
        public static List<FeatureEnums.AllCategories> GetFeatureModesAllCategories(int mode)
        {
            FeatureEnums.AllCategories featureMode = (FeatureEnums.AllCategories)mode;
            List<FeatureEnums.AllCategories> featureModes = new List<FeatureEnums.AllCategories>();

            if (featureMode.HasFlag(FeatureEnums.AllCategories.Disabled))
            {

                featureModes.Add(FeatureEnums.AllCategories.Disabled);
            }
            if (featureMode.HasFlag(FeatureEnums.AllCategories.Enabled))
            {

                featureModes.Add(FeatureEnums.AllCategories.Enabled);
            }

            return featureModes;
        }

        /// <summary>
        /// Gets the feature modes browser alert.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;FeatureEnums.BrowserAlert&gt;.</returns>
        public static List<FeatureEnums.BrowserAlert> GetFeatureModesBrowserAlert(int mode)
        {
            FeatureEnums.BrowserAlert featureMode = (FeatureEnums.BrowserAlert)mode;
            List<FeatureEnums.BrowserAlert> featureModes = new List<FeatureEnums.BrowserAlert>();

            if (featureMode.HasFlag(FeatureEnums.BrowserAlert.Disabled))
            {
                featureModes.Add(FeatureEnums.BrowserAlert.Disabled);
            }
            if (featureMode.HasFlag(FeatureEnums.BrowserAlert.Enabled))
            {
                featureModes.Add(FeatureEnums.BrowserAlert.Enabled);
            }

            return featureModes;
        }
        /// <summary>
        /// Get All Feature Modes for SARValidation
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>List&lt;MultiSelectDropDownViewModel&gt;.</returns>
        public static List<FeatureEnums.SARValidation> GetFeatureModesSARValidation(int mode)
        {
            FeatureEnums.SARValidation featureMode = (FeatureEnums.SARValidation)mode;
            List<FeatureEnums.SARValidation> featureModes = new List<FeatureEnums.SARValidation>();

            if (featureMode.HasFlag(FeatureEnums.SARValidation.Disabled))
            {
                featureModes.Add(FeatureEnums.SARValidation.Disabled);
            }
            if (featureMode.HasFlag(FeatureEnums.SARValidation.Enabled))
            {
                featureModes.Add(FeatureEnums.SARValidation.Enabled);
            }

            return featureModes;
        }
        /// <summary>
        /// Get All Feature Modes for ShowDocumentDate
        /// </summary>
        /// <param name="mode">The mode.</param>
        
        public static List<FeatureEnums.ShowDocumentDate> GetFeatureModesShowDocumentDate(int mode)
        {
            FeatureEnums.ShowDocumentDate featureMode = (FeatureEnums.ShowDocumentDate)mode;
            List<FeatureEnums.ShowDocumentDate> featureModes = new List<FeatureEnums.ShowDocumentDate>();

            if (featureMode.HasFlag(FeatureEnums.ShowDocumentDate.Disabled))
            {
                featureModes.Add(FeatureEnums.ShowDocumentDate.Disabled);
            }
            if (featureMode.HasFlag(FeatureEnums.ShowDocumentDate.Enabled))
            {
                featureModes.Add(FeatureEnums.ShowDocumentDate.Enabled);
            }

            return featureModes;
        }

     


    }
}