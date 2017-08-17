using RP.Utilities;
using RRD.DSA.Core.DAL;
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
    public class VerticalSDTAController : BaseController
    {
        /// <summary>
        /// The document type cache factory
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public IFactoryCache<DocumentTypeFactory, DocumentTypeObjectModel, int> documentTypeCacheFactory;
        /// <summary>
        /// The document type association factory
        /// </summary>
        private IFactory<DocumentTypeAssociationObjectModel, int> documentTypeAssociationFactory;

        /// <summary>
        /// The VerticalDataImportFactory
        /// </summary>
        private IVerticalDataImportFactory verticalDataImportFactory;
        /// <summary>
        /// The site cache factory
        /// </summary>
        private IFactoryCache<SiteFactory, SiteObjectModel, int> siteCacheFactory;
        /// <summary>
        /// The Taxonomy Association  factory
        /// </summary>
        private IFactory<TaxonomyAssociationObjectModel, int> taxonomyAssociationFactory;
        /// <summary>
        /// Constructor to initialize Document Type Association
        /// </summary>
        public VerticalSDTAController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                documentTypeCacheFactory = RPV2Resolver.Resolve<IFactoryCache<DocumentTypeFactory, DocumentTypeObjectModel, int>>("DocumentType");
                documentTypeCacheFactory.ClientName = SessionClientName;
                documentTypeCacheFactory.Mode = FactoryCacheMode.All;

                documentTypeAssociationFactory = RPV2Resolver.Resolve<IFactory<DocumentTypeAssociationObjectModel, int>>("DocumentTypeAssociationFactory");
                documentTypeAssociationFactory.ClientName = SessionClientName;

                verticalDataImportFactory = RPV2Resolver.Resolve<IVerticalDataImportFactory>();
                verticalDataImportFactory.ClientName = SessionClientName;
                verticalDataImportFactory.UserId = SessionUserID;

                siteCacheFactory = RPV2Resolver.Resolve<IFactoryCache<SiteFactory, SiteObjectModel, int>>("Site");
                siteCacheFactory.ClientName = SessionClientName;
                siteCacheFactory.Mode = FactoryCacheMode.All;

                taxonomyAssociationFactory = RPV2Resolver.Resolve<IFactory<TaxonomyAssociationObjectModel, int>>("TaxonomyAssociationFactory");
                taxonomyAssociationFactory.ClientName = SessionClientName;
            }
        }
        // GET: VerticalSDTA
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets all document types present in vertical market
        /// </summary>
        /// <returns>JsonResult</returns>
        public JsonResult GetDocumentTypes()
        {

            IEnumerable<DocumentTypeObjectModel> documentTypeDetails = documentTypeCacheFactory.GetAllEntities();

            return Json(documentTypeDetails, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Saves all document type association changes 
        /// </summary>
        /// <param name="updated"></param>
        /// <param name="deleted"></param>
        /// <param name="added"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaveDocumentTypeAssociation(List<DocumentTypeAssociationObjectModel> updated, List<DocumentTypeAssociationObjectModel> deleted, List<DocumentTypeAssociationObjectModel> added)
        {
            string retVal = "Success";
            if (!(updated == null && deleted == null && added == null))
            {
                retVal = verticalDataImportFactory.SaveDocumentTypeAssociation(added, updated, deleted);
            }
            return retVal;
        }
        /// <summary>
        /// Gets all Document Type Association at site level
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDocumentTypeAssociation(int? siteId)
        {
            var data = documentTypeAssociationFactory.GetAllEntities().Where(x => x.IsProofing == true && x.SiteId == siteId && x.SiteId != null).OrderBy(x => x.Order);
            
            var jsonResult = Json(new { data = data, total = data.Count() });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        /// <summary>
        /// Imports the file and parse to model
        /// </summary>
        /// <param name="file"></param>
        /// <returns>List of Document Type Association</returns>
        [HttpPost]
        public async Task<JsonResult> DocumentTypeImport(HttpPostedFileBase file, int? siteId)
        {
            List<DocumentTypeAssociationViewModel> importData = new List<DocumentTypeAssociationViewModel>();
            List<DocumentTypeAssociationObjectModel> lstDocumentTypeAssocaition = new List<DocumentTypeAssociationObjectModel>();
            List<DocumentTypeAssociationViewModel> invalidData = new List<DocumentTypeAssociationViewModel>();
            string status = string.Empty;
            if (file != null && file.ContentLength > 0)
            {

                var uploadDir = ConfigurationManager.AppSettings["Import"];

                //Save the file in Application folder
                var uploadPath = Path.Combine(uploadDir, Path.GetFileName(file.FileName));
                file.SaveAs(uploadPath);
                string contactsPath = ExcelReader.CheckPath(uploadPath);
                var data = await ExcelReader.GetDataToListAsync(contactsPath, ImportDocumentTypeAssociation);
                IEnumerable<DocumentTypeObjectModel> documentTypeDetails = documentTypeCacheFactory.GetAllEntities();
                importData = data.ToList();
                var validData = from m in importData
                                join dt in documentTypeDetails on m.MarketId equals dt.MarketId
                                where m.MarketId != null && m.HeaderText != null && m.Order != null // Validation for Mandatory fields
                                select new
                                {
                                    data = new DocumentTypeAssociationObjectModel()
                                    {
                                        DocumentTypeId = dt.DocumentTypeId,
                                        MarketId = dt.MarketId,
                                        HeaderText = m.HeaderText,
                                        LinkText = m.LinkText,
                                        CssClass = m.CssClass,
                                        DescriptionOverride = m.DescriptionOverride,
                                        Order = m.Order


                                    }
                                };
                lstDocumentTypeAssocaition = validData.Select(x => x.data)
                                             .GroupBy(y => y.MarketId).
                                             Select(z => z.LastOrDefault()).ToList();
                status = verticalDataImportFactory.SaveDocumentTypeAssociationExcelImport(siteId, lstDocumentTypeAssocaition);
                invalidData = importData.Where(x => !lstDocumentTypeAssocaition.Exists(p=> p.MarketId == x.MarketId)).ToList();
                FileInfo info = new FileInfo(uploadPath);
                info.Delete();

            }
            return Json(new { status = status, invalidData = invalidData });
        }

        /// <summary>
        /// Imports the file and parse to model
        /// </summary>
        /// <param name="file"></param>
        /// <returns>List of Document Type Association</returns>
        [HttpPost]
        public async Task<JsonResult> MarketLevelDocumentTypeImport(HttpPostedFileBase file, int siteId)
        {
            List<DocumentTypeAssociationViewModel> importData = new List<DocumentTypeAssociationViewModel>();
            List<DocumentTypeAssociationObjectModel> lstDocumentTypeAssocaition = new List<DocumentTypeAssociationObjectModel>();
            List<DocumentTypeAssociationViewModel> invalidData = new List<DocumentTypeAssociationViewModel>();
            string status = string.Empty;
            if (file != null && file.ContentLength > 0)
            {

                var uploadDir = ConfigurationManager.AppSettings["Import"];

                //Save the file in Application folder
                var uploadPath = Path.Combine(uploadDir, Path.GetFileName(file.FileName));
                file.SaveAs(uploadPath);
                string contactsPath = ExcelReader.CheckPath(uploadPath);
                var data = await ExcelReader.GetDataToListAsync(contactsPath, ImportDocumentTypeAssociation);
                if (data.Count < 5000)
                {
                    var documentTypeDetails = documentTypeCacheFactory.GetAllEntities().ToList();
                    //var taxonomyDetails = verticalDataImportFactory.GetTaxonomyAssociationUsingSiteId(SessionClientName, siteId, true); 
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

                    var validData = from m in importData
                                    join dt in documentTypeDetails on m.MarketId equals dt.MarketId
                                    join ta in taxonomyDetails on m.TaxonomyMarketId equals ta.MarketId
                                    where m.HeaderText != null && m.Order != null // Validation for Mandatory fields
                                    select new
                                    {
                                        data = new DocumentTypeAssociationObjectModel()
                                        {
                                            DocumentTypeId = dt.DocumentTypeId,
                                            MarketId = dt.MarketId,
                                            TaxonomyAssociationId = ta.TaxonomyAssociationId,
                                            HeaderText = m.HeaderText,
                                            LinkText = m.LinkText,
                                            CssClass = m.LinkText,
                                            DescriptionOverride = m.DescriptionOverride,
                                            Order = m.Order,
                                            TaxonomyMarketId = ta.MarketId
                                        }
                                    };
                    lstDocumentTypeAssocaition = validData.Select(x => x.data).
                                                 GroupBy(y => new { y.MarketId, y.TaxonomyAssociationId }).
                                                 Select(z => z.LastOrDefault()).ToList();

                    status = verticalDataImportFactory.SaveDocumentTypeAssociationExcelImport(null, lstDocumentTypeAssocaition);

                    invalidData = importData.Where(x => !lstDocumentTypeAssocaition.Exists(p=> p.MarketId == x.MarketId && p.TaxonomyMarketId == x.TaxonomyMarketId)).ToList();


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
        /// <returns>DocumentTypeAssociationViewModel</returns>
        private DocumentTypeAssociationViewModel ImportDocumentTypeAssociation(IList<string> rowData, IList<string> columnNames)
        {
            DocumentTypeAssociationViewModel documentTypeAssocaition = new DocumentTypeAssociationViewModel();

            if (columnNames.IndexOf("documenttype") >= 0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("documenttype")]).ToString().Trim()))
            {
                documentTypeAssocaition.MarketId = (rowData[columnNames.IndexFor("documenttype")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                documentTypeAssocaition.MarketId = null;
            }


            if (columnNames.IndexOf("headertext") >= 0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("headertext")]).ToString().Trim()))
            {
                documentTypeAssocaition.HeaderText = (rowData[columnNames.IndexFor("headertext")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                documentTypeAssocaition.HeaderText = null;
            }

            if (columnNames.IndexOf("linktext") >= 0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("linktext")]).ToString().Trim()))
            {
                documentTypeAssocaition.LinkText = (rowData[columnNames.IndexFor("linktext")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                documentTypeAssocaition.LinkText = null;
            }
            if (columnNames.IndexOf("tooltipinformation") >= 0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("tooltipinformation")]).ToString().Trim()))
            {
                documentTypeAssocaition.DescriptionOverride = (rowData[columnNames.IndexFor("tooltipinformation")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                documentTypeAssocaition.DescriptionOverride = null;
            }
            if (columnNames.IndexOf("cssclass") >= 0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("cssclass")]).ToString().Trim()))
            {
                documentTypeAssocaition.CssClass = (rowData[columnNames.IndexFor("cssclass")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                documentTypeAssocaition.CssClass = null;
            }
            if (columnNames.IndexOf("order") >= 0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("order")]).ToString().Trim()))
            {
                string order = (rowData[columnNames.IndexFor("order")]).ToString().Trim().Replace("\n", string.Empty);
                int x;
                bool isParsed = int.TryParse(order, out x);
                if (isParsed)
                {
                    documentTypeAssocaition.Order = x;
                }
                else documentTypeAssocaition.Order = null;
            }
            else
            {
                documentTypeAssocaition.Order = null;
            }
            if (columnNames.IndexOf("cusips") >= 0 && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("cusips")]).ToString().Trim()))
            {
                string taxonomyMarketId = (rowData[columnNames.IndexFor("cusips")]).ToString().Trim().Replace("\n", string.Empty);
                documentTypeAssocaition.TaxonomyMarketId = taxonomyMarketId;
            }
            else
            {
                documentTypeAssocaition.TaxonomyMarketId = null;
            }

            return documentTypeAssocaition;

        }
        /// <summary>
        /// Gets all Document Type Association at site level
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMarketLevelDocumentTypeAssociation(int? siteId)
        {

            string defaultPageName = siteCacheFactory.GetEntityByKey(siteId.Value).DefaultPageName;
            List<TaxonomyAssociationObjectModel> taxonomyDetails = null;
            switch (defaultPageName)
            {
                case "TAL":
                case "TAHD":
                case "TAGD":
                    taxonomyDetails = taxonomyAssociationFactory.GetAllEntities().Where(x => (x.SiteId == null || x.SiteId == siteId) && x.IsProofing == true).ToList();
                    break;
                case "TAD":
                    taxonomyDetails = taxonomyAssociationFactory.GetAllEntities().Where(x => x.SiteId == siteId && x.IsProofing == true).ToList();
                    break;
                default:
                    break;
            }

            var data = documentTypeAssociationFactory.GetAllEntities().Where(x => x.SiteId == null && x.IsProofing == true).OrderBy(x => x.Order);

            var finalData = data.Where(p => taxonomyDetails.Exists(ta => ta.TaxonomyAssociationId == p.TaxonomyAssociationId));

            var jsonResult = Json(new { data = finalData, total = finalData.Count() }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetTaxonomyByTemplateForMLDTA(int? siteId)
        {

            string defaultPageName = siteCacheFactory.GetEntityByKey(siteId.Value).DefaultPageName;
            List<TaxonomyAssociationObjectModel> taxonomyDetails = null;
            switch (defaultPageName)
            {
                case "TAL":
                case "TAHD":
                case "TAGD":
                    taxonomyDetails = taxonomyAssociationFactory.GetAllEntities().Where(x => (x.SiteId == null || x.SiteId == siteId) && x.IsProofing == true).ToList();
                    break;
                case "TAD":
                    taxonomyDetails = taxonomyAssociationFactory.GetAllEntities().Where(x => x.SiteId == siteId && x.IsProofing == true).ToList();
                    break;
                default:
                    break;
            }

            
            var jsonResult = Json(new { data = taxonomyDetails, total = taxonomyDetails.Count() }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}