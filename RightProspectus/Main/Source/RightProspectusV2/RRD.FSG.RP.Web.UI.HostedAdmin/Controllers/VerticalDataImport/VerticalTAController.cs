using Newtonsoft.Json.Linq;
using RP.Utilities;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    public class VerticalTAController : BaseController
    {
        // GET: VerticalTA
        public ActionResult Index()
        {
            return View();
        }
         
        /// <summary>
        /// The Taxonomy Association  factory
        /// </summary>
        private IFactory<TaxonomyAssociationObjectModel, int> taxonomyAssociationFactory;
       
        /// <summary>
        /// The VerticalDataImportFactory
        /// </summary>
        private IVerticalDataImportFactory verticalDataImportFactory;
        /// <summary>
        /// SelectedSiteId
        /// </summary>
        private int? SelectedSiteId { get; set; }
        /// <summary>
        /// Constructor to initialize Taxonomy Type Association
        /// </summary>
        public VerticalTAController()
        {    
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                taxonomyAssociationFactory = RPV2Resolver.Resolve<IFactory<TaxonomyAssociationObjectModel, int>>("TaxonomyAssociationFactory");
                taxonomyAssociationFactory.ClientName = SessionClientName;

                verticalDataImportFactory = RPV2Resolver.Resolve<IVerticalDataImportFactory>();
                verticalDataImportFactory.ClientName = SessionClientName;
                verticalDataImportFactory.UserId = SessionUserID;
            }
        }
        /// <summary>
        /// Saves all taxonomy association changes 
        /// </summary>
        /// <param name="updated"></param>
        /// <param name="deleted"></param>
        /// <param name="added"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public string SaveTaxonomyAssociation(List<TaxonomyAssociationObjectModel> updated, List<TaxonomyAssociationObjectModel> deleted, List<TaxonomyAssociationObjectModel> added)
        {
            string retVal = "Success";
            if(!(updated == null && deleted == null && added == null))
            {
                retVal =  verticalDataImportFactory.SaveTaxonomyAssociation(added, updated, deleted);
            }
            return retVal;
        }

       
       /// <summary>
        /// Get GetTaxonomyAssociationUsingSiteId
       /// </summary>
        /// <param name="file"></param>
        /// <returns>List of Taxonomy Association</returns>   
        [HttpPost]
        public JsonResult GetTaxonomy(int? siteId)
        {
            var data = taxonomyAssociationFactory.GetAllEntities().Where(x => x.SiteId == siteId && x.IsProofing == true).ToList();
            var filteredData = verticalDataImportFactory.VerifyTaxonomywithVerticalData(data);

            var jsonResult = Json(new { data = filteredData, total = filteredData.Count() });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult GetTaxonomyByTemplate(int? siteId)
        {
           var data =  verticalDataImportFactory.GetTaxonomyAssociationUsingSiteId(SessionClientName, siteId.Value, true);

           var jsonResult = Json(new { data = data, total = data.Count() });
           jsonResult.MaxJsonLength = int.MaxValue;
           return jsonResult;    
        }
       

        /// <summary>
        /// Imports the file and parse to model
        /// </summary>
        /// <param name="file"></param>
        /// <returns>List of Taxonomy Association</returns>
        [HttpPost]
        public async Task<JsonResult> TaxonomyAssocaitionImport(HttpPostedFileBase file, int? siteId, string taxonomyOrderFeatureMode)
        {
            List<TaxonomyAssociationObjectModel> importData = new List<TaxonomyAssociationObjectModel>();
            List<TaxonomyAssociationObjectModel> lstTaxonomyAssocaition = new List<TaxonomyAssociationObjectModel>();
            List<TaxonomyAssociationObjectModel> invalidData = new List<TaxonomyAssociationObjectModel>();
            List<TaxonomyAssociationObjectModel> invalidDataWithoutOrder = new List<TaxonomyAssociationObjectModel>();
            string status = string.Empty;
            this.SelectedSiteId = siteId;
            if (file != null && file.ContentLength > 0)
            {

                var uploadDir = ConfigurationManager.AppSettings["Import"];

                //Save the file in Application folder
                var uploadPath = Path.Combine(uploadDir, Path.GetFileName(file.FileName));
                file.SaveAs(uploadPath);
                string contactsPath = ExcelReader.CheckPath(uploadPath);
                var data = await ExcelReader.GetDataToListAsync(contactsPath, ImportTaxonomyAssociation);
                if (data.Count < 5000)
                {
                    if (siteId.HasValue && taxonomyOrderFeatureMode == "CustomizeOrder") // Order is mandatory for CustomizeOrder feature mode
                    {
                        importData = data.Where(x => x.Order != null).ToList();
                        invalidDataWithoutOrder = data.Where(x => x.Order == null).ToList();

                        importData = importData.Where(x => x.MarketId != null).GroupBy(x => x.MarketId).Select(y => y.LastOrDefault()).ToList(); // distinct records
                    }
                    else
                    {
                        importData = data.Where(x => x.MarketId != null).GroupBy(x => x.MarketId).Select(y => y.LastOrDefault()).ToList(); // distinct records
                    }


                    invalidData = verticalDataImportFactory.SaveTaxonomyAssocaitionExcelImport(siteId, importData, out status);

                    invalidDataWithoutOrder.ForEach(p => { invalidData.Add(p); });

                    FileInfo info = new FileInfo(uploadPath);
                    info.Delete();
                }
                else
                {
                    status = "countgreaterthan5000";
                }

            }
            return Json(new { status = status ,invalidData = invalidData });
        }
        /// <summary>
        /// Maps excel coumns to object model
        /// </summary>
        /// <param name="rowData"></param>
        /// <param name="columnNames"></param>
        /// <returns>TaxonomyAssociationObjectModel</returns>
        private TaxonomyAssociationObjectModel ImportTaxonomyAssociation(IList<string> rowData, IList<string> columnNames)
        {
            TaxonomyAssociationObjectModel taxonomyAssocaition = new TaxonomyAssociationObjectModel();


            if (!string.IsNullOrEmpty((rowData[columnNames.IndexFor("cusips")]).ToString().Trim()))
            {
                taxonomyAssocaition.MarketId = (rowData[columnNames.IndexFor("cusips")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                taxonomyAssocaition.MarketId = null;
            }
           

            if (!string.IsNullOrEmpty((rowData[columnNames.IndexFor("nameoverride")]).ToString().Trim()))
            {
                taxonomyAssocaition.NameOverride = (rowData[columnNames.IndexFor("nameoverride")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                taxonomyAssocaition.NameOverride = null;
            }
            if (columnNames.IndexOf("tabbedpagenameoverride") > 0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("tabbedpagenameoverride")]).ToString().Trim()))
            {
                taxonomyAssocaition.TabbedPageNameOverride = (rowData[columnNames.IndexFor("tabbedpagenameoverride")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                taxonomyAssocaition.TabbedPageNameOverride = null;
            }
            if (columnNames.IndexOf("tooltipinformation") >0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("tooltipinformation")]).ToString().Trim()))
            {
                taxonomyAssocaition.DescriptionOverride = (rowData[columnNames.IndexFor("tooltipinformation")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                taxonomyAssocaition.DescriptionOverride = null;
            }
            if ( columnNames.IndexOf("cssclass") >0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("cssclass")]).ToString().Trim()))
            {
                taxonomyAssocaition.CssClass = (rowData[columnNames.IndexFor("cssclass")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                taxonomyAssocaition.CssClass = null;
            }
            if (columnNames.IndexOf("order") >= 0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("order")]).ToString().Trim()))
            {
                int x;                
                string order = (rowData[columnNames.IndexFor("order")]).ToString().Trim().Replace("\n", string.Empty);              
                bool isParsed = int.TryParse(order, out x);
                if (isParsed)
                {
                    taxonomyAssocaition.Order = x;
                }
                else
                    taxonomyAssocaition.Order = null;
            }
            else
            {
                taxonomyAssocaition.Order = null;
            }
                taxonomyAssocaition.SiteId = this.SelectedSiteId;
            
                return taxonomyAssocaition;

        }
    }
}