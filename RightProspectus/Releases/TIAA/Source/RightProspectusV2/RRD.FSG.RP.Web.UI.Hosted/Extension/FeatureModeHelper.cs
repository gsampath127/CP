using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRD.FSG.RP.Web.UI.Hosted
{
    public class FeatureModeHelper<T> where T : class
    {
        // <summary>
        /// The hosted client page scenarios
        /// </summary>
        private IHostedClientPageScenariosFactory hostedClientPageScenarios;

        public FeatureModeHelper()
        {
            this.hostedClientPageScenarios = RPV2Resolver.Resolve<IHostedClientPageScenariosFactory>();
        }

        public void GetFeatureModes(ref T model, List<Type> features)
        {
            var viewModel = model as BaseViewModel;
            if (typeof(T) == typeof(TaxonomyAssociationGroupViewModel))
            {
                viewModel = model as TaxonomyAssociationGroupViewModel;
            }

            foreach (Type e in features)
            {
                string key = e.Name;
                switch (key)
                {
                    case "XBRL":
                        int xbrlFeatureMode = hostedClientPageScenarios.GetSiteFeatureModeFromCache(viewModel.ClientName, viewModel.SiteName, key);
                        List<FeatureEnums.XBRL> featureModes = FeatureHelper.GetFeatureModesXBRL(xbrlFeatureMode);
                        if (featureModes.Contains(FeatureEnums.XBRL.Enabled) && featureModes.Contains(FeatureEnums.XBRL.ShowXBRLInLandingPage))
                        {
                            viewModel.ShowXBRLInLandingPage = true;
                            if (featureModes.Contains(FeatureEnums.XBRL.ShowXBRLInNewTab))
                            {
                                viewModel.DisplayXBRLInNewTAB = true;
                            }
                        }
                        break;
                    case "SARValidation":
                        int sarValidationFeatureMode = hostedClientPageScenarios.GetSiteFeatureModeFromCache(viewModel.ClientName, viewModel.SiteName, key);
                        List<FeatureEnums.SARValidation> featureModesSARValidation = FeatureHelper.GetFeatureModesSARValidation(sarValidationFeatureMode);
                        if (featureModesSARValidation.Contains(FeatureEnums.SARValidation.Enabled))
                        {
                            viewModel.IsSARValidationEnabled = true;
                        }
                        break;
                    case "ShowDocumentDate":
                        int showDocumentDateMode = hostedClientPageScenarios.GetPageFeatureModeFromCache(viewModel.ClientName, viewModel.SiteName, viewModel.PageId, key);
                        List<FeatureEnums.ShowDocumentDate> featureModeShowDocumentDate = FeatureHelper.GetFeatureModesShowDocumentDate(showDocumentDateMode);

                        if (featureModeShowDocumentDate.Contains(FeatureEnums.ShowDocumentDate.Enabled))
                        {
                            viewModel.ShowDocumentDate = true;
                        }
                        break;
                    case "AllCategories":
                        int groupSearchMode = hostedClientPageScenarios.GetPageFeatureModeFromCache(viewModel.ClientName, viewModel.SiteName, viewModel.PageId, key);
                        List<FeatureEnums.AllCategories> featureModeGroupSearch = FeatureHelper.GetFeatureModesAllCategories(groupSearchMode);

                        if (featureModeGroupSearch.Contains(FeatureEnums.AllCategories.Enabled) && model.GetType().GetProperty("EnableGroupSearch") != null)
                        {
                            model.GetType().GetProperty("EnableGroupSearch").SetValue(model, true);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}