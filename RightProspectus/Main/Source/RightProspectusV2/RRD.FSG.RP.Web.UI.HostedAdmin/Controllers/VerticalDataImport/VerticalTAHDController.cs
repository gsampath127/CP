using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RP.Utilities;
using System.IO;
using System.Data;
using System.Threading.Tasks;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Keys;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.Enumerations;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    public class VerticalTAHDController : BaseController
    {
        /// <summary>
        /// The Taxonomy Association  factory
        /// </summary>
        private IFactory<TaxonomyAssociationObjectModel, int> taxonomyAssociationFactory;

        /// <summary>
        /// The VerticalDataImportFactory
        /// </summary>
        private IVerticalDataImportFactory verticalDataImportFactory;


        public VerticalTAHDController()
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
        [HttpPost]
        public JsonResult GetTaxonomyAssociationHierarchy(int siteId, int parentTaxonomyAssociationId)
        {
            var data = verticalDataImportFactory.GetTaxonomyAssociationProduct(siteId).Where(x => x.ParentTaxonomyAssociationId == parentTaxonomyAssociationId);

            var jsonResult = Json(new { data = data, total = data.Count() });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost, ValidateInput(false)]
        public string SaveTaxonomyAssociationHierarchy(List<TaxonomyAssociationProductObjectModel> updated, List<TaxonomyAssociationProductObjectModel> deleted, List<TaxonomyAssociationProductObjectModel> added)
        {
            return verticalDataImportFactory.SaveTaxonomyHierarchy(added, updated, deleted);

        }
        public FileResult ExportTAHD(int siteId, string taxonomyOrderFeatureMode)
        {

            string file = Path.Combine(ConfigurationManager.AppSettings["Import"], "ProductToFundAssociation.xlsx");
            var data = verticalDataImportFactory.GetTaxonomyAssociationProduct(siteId);
            DataTable dt = CreateExcelFile.ListToDataTable<TaxonomyAssociationProductObjectModel>(data);

            //Rename the column names to match Excel Format
            dt.Columns["ParentMarketId"].ColumnName = "Product CUSIPs";
            dt.Columns["ChildMarketId"].ColumnName = "Underlying Fund CUSIPs";
            DataTable filteredData = new DataTable();
            if (taxonomyOrderFeatureMode == "CustomizeOrder")
            {
                filteredData = dt.DefaultView.ToTable(false, "Product CUSIPs", "Underlying Fund CUSIPs", "Order");
            }
            else
            {
                filteredData = dt.DefaultView.ToTable(false, "Product CUSIPs", "Underlying Fund CUSIPs");
            }


            DataSet ds = new DataSet();
            ds.Tables.Add(filteredData);
            CreateExcelFile.CreateExcelDocument(ds, file);
            return File(file, "application/vnd.ms-excel", "ProductToFundAssociation.xlsx");

        }

        public async Task<JsonResult> TAHDImport(HttpPostedFileBase file, int siteId, string taxonomyOrderFeatureMode)
        {

            List<TaxonomyAssociationProductObjectModel> importData = new List<TaxonomyAssociationProductObjectModel>();
            List<TaxonomyAssociationProductObjectModel> validData = new List<TaxonomyAssociationProductObjectModel>();
            List<TaxonomyAssociationProductObjectModel> invalidData = new List<TaxonomyAssociationProductObjectModel>();
            List<TaxonomyAssociationProductObjectModel> invalidDataWithoutOrder = new List<TaxonomyAssociationProductObjectModel>();

            string status = string.Empty;
            var taxonomyData = taxonomyAssociationFactory.GetAllEntities().Where(x => x.SiteId == siteId || x.SiteId == null);

            if (file != null && file.ContentLength > 0)
            {

                var uploadDir = ConfigurationManager.AppSettings["Import"];

                //Save the file in Application folder
                var uploadPath = Path.Combine(uploadDir, Path.GetFileName(file.FileName));
                file.SaveAs(uploadPath);
                string contactsPath = ExcelReader.CheckPath(uploadPath);
                var data = await ExcelReader.GetDataToListAsync(contactsPath, ImportTaxonomyAssociationHierarchy);

                if (data.Count < 15000)
                {
                    importData = data.ToList();

                    IEnumerable<TaxonomyAssociationProductObjectModel> filteredData = new List<TaxonomyAssociationProductObjectModel>();

                    filteredData = (from ta in importData
                                    join cta in taxonomyData on ta.ChildMarketId equals cta.MarketId
                                    join pta in taxonomyData on ta.ParentMarketId equals pta.MarketId
                                    where pta.SiteId == siteId && cta.SiteId == null // Validation for Mandatory fields
                                    && cta.IsProofing == true && pta.IsProofing == true
                                    select new TaxonomyAssociationProductObjectModel()
                                    {
                                        ParentMarketId = pta.MarketId,
                                        ChildMarketId = cta.MarketId,
                                        ParentNameOverride = pta.NameOverride,
                                        ChildNameOverride = cta.NameOverride,
                                        ParentTaxonmyId = pta.TaxonomyId,
                                        ChildTaxonomyId = cta.TaxonomyId,
                                        ParentTaxonomyAssociationId = pta.TaxonomyAssociationId,
                                        ChildTaxonomyAssociationId = cta.TaxonomyAssociationId,
                                        Order = ta.Order
                                    });


                    if (taxonomyOrderFeatureMode == "CustomizeOrder") // Order is mandatory for CustomizeOrder feature mode
                    {
                        invalidDataWithoutOrder = filteredData.Where(x => x.Order == null).ToList();

                        //filteredData = filteredData.Where(x => x.Order != null);

                        validData = filteredData.Where(x => x.Order != null).GroupBy(test => new { test.ChildTaxonomyAssociationId, test.ParentTaxonomyAssociationId })
                                     .Select(grp => grp.LastOrDefault())
                                     .ToList();
                    }
                    else
                    {
                        validData = filteredData.GroupBy(test => new { test.ChildTaxonomyAssociationId, test.ParentTaxonomyAssociationId })
                                     .Select(grp => grp.LastOrDefault())
                                     .ToList();
                    }
                    if (validData.Count > 0)
                    {
                        status = verticalDataImportFactory.SaveTaxonomyHierarchyExcelImport(validData);
                    }

                    invalidData = importData.Where(x => !validData.Exists(p => p.ParentMarketId == x.ParentMarketId && p.ChildMarketId == x.ChildMarketId)).ToList();

                    invalidDataWithoutOrder.Where(x => !invalidData.Exists(p => p.ChildMarketId == x.ChildMarketId && p.ParentMarketId == x.ParentMarketId))
                                           .ToList().ForEach(p => { invalidData.Add(p); });

                    FileInfo info = new FileInfo(uploadPath);
                    info.Delete();

                }
                else
                {
                    status = "countgreaterthan15000";
                }
            }
            
            return Json(new { status = status, invalidData = invalidData });

        }

        /// <summary>
        /// Maps excel coumns to object model
        /// </summary>
        /// <param name="rowData"></param>
        /// <param name="columnNames"></param>
        /// <returns>TaxonomyAssociationProductObjectModel</returns>
        private TaxonomyAssociationProductObjectModel ImportTaxonomyAssociationHierarchy(IList<string> rowData, IList<string> columnNames)
        {
            TaxonomyAssociationProductObjectModel taxonomyAssocaition = new TaxonomyAssociationProductObjectModel();


            if (!string.IsNullOrEmpty((rowData[columnNames.IndexFor("ProductCUSIPs")]).ToString().Trim()))
            {
                taxonomyAssocaition.ParentMarketId = (rowData[columnNames.IndexFor("ProductCUSIPs")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                taxonomyAssocaition.ParentMarketId = null;
            }

            if (!string.IsNullOrEmpty((rowData[columnNames.IndexFor("UnderlyingFundCUSIPs")]).ToString().Trim()))
            {
                taxonomyAssocaition.ChildMarketId = (rowData[columnNames.IndexFor("UnderlyingFundCUSIPs")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                taxonomyAssocaition.ChildMarketId = null;
            }


            if (columnNames.Contains("order") && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("order")]).ToString().Trim()))
            {
                string order = (rowData[columnNames.IndexFor("order")]).ToString().Trim().Replace("\n", string.Empty);
                int x;
                bool isParsed = int.TryParse(order, out x);
                if (isParsed)
                {

                    taxonomyAssocaition.Order = x;
                }
                else taxonomyAssocaition.Order = null;

             }
                else taxonomyAssocaition.Order = null;

            return taxonomyAssocaition;

        }

    }
}