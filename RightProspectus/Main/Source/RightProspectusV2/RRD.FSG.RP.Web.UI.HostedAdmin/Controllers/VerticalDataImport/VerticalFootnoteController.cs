using RP.Utilities;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
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
    public class VerticalFootnoteController : BaseController
    {

        /// <summary>
        /// The Footnote factory
        /// </summary>

        private IFactory<FootnoteObjectModel, int> footNoteFactory;
        /// <summary>
        /// The VerticalDataImportFactory
        /// </summary>
        private IVerticalDataImportFactory verticalDataImportFactory;
        /// <summary>
        /// The Taxonomy Association  factory
        /// </summary>
        private IFactory<TaxonomyAssociationObjectModel, int> taxonomyAssociationFactory;
        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;

         /// <summary>
        /// Constructor to initialize Document Type Association
        /// </summary>
        public VerticalFootnoteController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                footNoteFactory = RPV2Resolver.Resolve<IFactory<FootnoteObjectModel, int>>("FootnoteFactory");
                footNoteFactory.ClientName = SessionClientName;

                verticalDataImportFactory = RPV2Resolver.Resolve<IVerticalDataImportFactory>();
                verticalDataImportFactory.ClientName = SessionClientName;
                verticalDataImportFactory.UserId = SessionUserID;

                taxonomyAssociationFactory = RPV2Resolver.Resolve<IFactory<TaxonomyAssociationObjectModel, int>>("TaxonomyAssociationFactory");
                taxonomyAssociationFactory.ClientName = SessionClientName;

                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;
            }

        }
        // GET: VerticalFootnote
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Save Footnotes
        /// </summary>
        /// <param name="updated"></param>
        /// <param name="deleted"></param>
        /// <param name="added"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public string SaveFootnote(List<FootnoteObjectModel> updated, List<FootnoteObjectModel> deleted, List<FootnoteObjectModel> added)
        {
            string retVal = "Success";
            if (!(updated == null && deleted == null && added == null))
            {
                retVal = verticalDataImportFactory.SaveFootNotes(added, updated, deleted);
            }
            return retVal;
        }

        /// <summary>
        /// Gets all footnotes
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFootnotes(int siteId)
        {
            string defaultPageName = siteCacheFactory.GetEntityByKey(siteId).DefaultPageName;
            List<TaxonomyAssociationObjectModel> taxonomyDetails = null;
            switch (defaultPageName)
            {
                case "TAL":
                case "TAHD":
                case "TAGD":
                    taxonomyDetails = taxonomyAssociationFactory.GetAllEntities().Where(x => (x.SiteId == null || x.SiteId == siteId) && x.IsProofing).ToList();
                    break;                
                case "TAD":
                    taxonomyDetails = taxonomyAssociationFactory.GetAllEntities().Where(x => x.SiteId == siteId && x.IsProofing).ToList();
                    break;               
                default:
                    break;
            }
             //var taxonomyDetails = verticalDataImportFactory.GetTaxonomyAssociationUsingSiteId(SessionClientName, siteId, true).Select(y=>y.TaxonomyAssociationId).ToList();             
             var data = footNoteFactory.GetAllEntities().Where(x=> x.TaxonomyAssociationId!=null && taxonomyDetails.Exists(p=>p.TaxonomyAssociationId == x.TaxonomyAssociationId));
             
            var jsonResult = Json(new { data = data, total = data.Count() });
             jsonResult.MaxJsonLength = int.MaxValue;
             return jsonResult;

        }

        [HttpPost]
        public JsonResult GetTaxonomyByTemplateForFootnote(int? siteId)
        {

            string defaultPageName = siteCacheFactory.GetEntityByKey(siteId.Value).DefaultPageName;
            List<TaxonomyAssociationObjectModel> taxonomyDetails = null;
            switch (defaultPageName)
            {
                case "TAL":
                case "TAHD":
                case "TAGD":
                    taxonomyDetails = taxonomyAssociationFactory.GetAllEntities().Where(x => (x.SiteId == null || x.SiteId == siteId) && x.IsProofing).ToList();
                    break;
                case "TAD":
                    taxonomyDetails = taxonomyAssociationFactory.GetAllEntities().Where(x => x.SiteId == siteId && x.IsProofing).ToList();
                    break;
                default:
                    break;
            }
                        
            var jsonResult = Json(new { data = taxonomyDetails, total = taxonomyDetails.Count() }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        /// <summary>
        /// Imports the file and parse to model
        /// </summary>
        /// <param name="file"></param>
        /// <returns>List of Footnote</returns>
        [HttpPost]
        public async Task<JsonResult> FootnoteImport(HttpPostedFileBase file,int siteId)
        {
            List<FootnoteObjectModel> importData = new List<FootnoteObjectModel>();
            List<FootnoteObjectModel> lstFootnotes = new List<FootnoteObjectModel>();
            List<FootnoteObjectModel> invalidData = new List<FootnoteObjectModel>();
            string status = string.Empty;
            if (file != null && file.ContentLength > 0)
            {

                var uploadDir = ConfigurationManager.AppSettings["Import"];

                //Save the file in Application folder
                var uploadPath = Path.Combine(uploadDir, Path.GetFileName(file.FileName));
                file.SaveAs(uploadPath);
                string contactsPath = ExcelReader.CheckPath(uploadPath);
                var data = await ExcelReader.GetDataToListAsync(contactsPath, ImportFootnotes);

                if (data.Count < 5000)
                {

                    List<TaxonomyAssociationObjectModel> taxonomyDetails = null;
                    switch (siteCacheFactory.GetEntityByKey(siteId).DefaultPageName)
                    {
                        case "TAL":
                        case "TAHD":
                        case "TAGD":
                            taxonomyDetails = taxonomyAssociationFactory.GetAllEntities().Where(x => (x.SiteId == null || x.SiteId == siteId) && x.IsProofing).ToList();
                            break;
                        case "TAD":
                            taxonomyDetails = taxonomyAssociationFactory.GetAllEntities().Where(x => x.SiteId == siteId && x.IsProofing).ToList();
                            break;
                        default:
                            break;
                    }
                    importData = data.ToList();

                    var validData = from f in importData
                                    join ta in taxonomyDetails on f.TaxonomyMarketId equals ta.MarketId
                                    where f.Text != null && f.Order != null // Validation for Mandatory fields
                                    select new
                                    {
                                        data = new FootnoteObjectModel()
                                        {
                                            Text = f.Text,
                                            TaxonomyMarketId = ta.MarketId,
                                            TaxonomyAssociationId = ta.TaxonomyAssociationId,
                                            Order = f.Order

                                        }
                                    };
                    lstFootnotes = validData.Select(x => x.data).
                                                 GroupBy(y => new { y.TaxonomyAssociationId }).
                                                 Select(z => z.LastOrDefault()).ToList();

                    status = verticalDataImportFactory.SaveFootnoteExcelImport(lstFootnotes);

                    invalidData = importData.Where(x => string.IsNullOrWhiteSpace(x.Text) || !lstFootnotes.Exists(p=> p.TaxonomyMarketId == x.TaxonomyMarketId)).ToList();


                    FileInfo info = new FileInfo(uploadPath);
                    info.Delete();
                }
                else
                {
                    status = "countgreaterthan5000";
                }

            }
            return Json(new { status = status, invalidData = invalidData });
        }

        /// <summary>
        /// Maps excel coumns to object model
        /// </summary>
        /// <param name="rowData"></param>
        /// <param name="columnNames"></param>
        /// <returns>FootnoteViewModel</returns>
        private FootnoteObjectModel ImportFootnotes(IList<string> rowData, IList<string> columnNames)
        {
            FootnoteObjectModel footnote = new FootnoteObjectModel();

            if (columnNames.IndexOf("text") >= 0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("text")]).ToString().Trim()))
            {
                footnote.Text = (rowData[columnNames.IndexFor("text")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                footnote.Text = null;
            }


            if (!string.IsNullOrEmpty((rowData[columnNames.IndexFor("order")]).ToString().Trim()))
            {
                string order = (rowData[columnNames.IndexFor("order")]).ToString().Trim().Replace("\n", string.Empty);
                int x;
                bool isParsed = int.TryParse(order, out x);
                if (isParsed)
                {
                    footnote.Order = x;

                }
                else
                    footnote.Order = null;
            }
            else
            {
                footnote.Order = null;
            }
            if (columnNames.IndexOf("cusips") >= 0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("cusips")]).ToString().Trim()))
            {
                string taxonomyMarketId = (rowData[columnNames.IndexFor("cusips")]).ToString().Trim().Replace("\n", string.Empty);
                footnote.TaxonomyMarketId = taxonomyMarketId;
            }
            else
            {
                footnote.TaxonomyMarketId = null;
            }

            return footnote;

        }
    }
}