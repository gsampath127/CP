using Newtonsoft.Json.Linq;
using RP.Utilities;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    public class VerticalTAGDController : BaseController
    {
        /// <summary>
        /// SelectedSiteId
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private int? SelectedSiteId { get; set; }
        /// <summary>
        /// The Taxonomy Association  factory
        /// </summary>
        private IFactory<TaxonomyAssociationObjectModel, int> taxonomyAssociationFactory;
        // GET: VerticalTAGD
        public ActionResult Index()
        {
            return View();
        }       
        private IVerticalDataImportFactory verticalDataImportFactory;
    
        public VerticalTAGDController()
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
        public JsonResult GetTaxonomyGroup(int? siteId , bool isFunds=false)
        {
            var data = verticalDataImportFactory.GetTaxonomyAssociationGroups(siteId);
            var dataTAGTA = verticalDataImportFactory.GetTaxonomyAssociationGroupFunds(siteId);

            data.ForEach(p =>
            {
                p.HasFundData = dataTAGTA.Exists(t => t.TaxonomyAssociationGroupId == p.TaxonomyAssociationGroupId);
            });

            if(isFunds)
            {
                var parentGroupIds = data.Select(x => x.ParentTaxonomyAssociationGroupId).ToList();
                data = data.Where(x => !parentGroupIds.Contains(x.TaxonomyAssociationGroupId)).ToList();
            }
             
            
            var jsonResult = Json(new { data = data, total = data.Count() });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost, ValidateInput(false)]
        public string SaveTaxonomyGroups(List<TaxonomyGroupObjectModel> updated, List<TaxonomyGroupObjectModel> deleted, List<TaxonomyGroupObjectModel> added)
         {
            string retVal = "Success";
            if(!(updated == null && deleted == null && added == null))
            {
                retVal = verticalDataImportFactory.SaveTaxonomyAssociationGroup(added, updated, deleted);
            }
            return retVal;
        }

        [HttpGet]
        public FileResult ExportTaxonomyGroup(int siteId)
        {
            string file = Path.Combine(ConfigurationManager.AppSettings["Import"], "TaxonomyGroups.xlsx");
            var data = verticalDataImportFactory.GetTaxonomyAssociationGroups(siteId);

            var groupData = from gr in data
                            join pr in data on gr.TaxonomyAssociationGroupId equals pr.ParentTaxonomyAssociationGroupId into grpData
                            from r in grpData.DefaultIfEmpty()
                            select new TaxonomyGroupObjectModel
                            {
                                Name = gr.Name,
                                ParentName = r==null?"":r.Name,
                                CssClass = gr.CssClass,
                                Description = gr.Description,
                                Level = gr.Level
                            };


            DataTable dt = CreateExcelFile.ListToDataTable<TaxonomyGroupObjectModel>(groupData.ToList());

            //Rename the column names to match Excel Format
            dt.Columns["ParentName"].ColumnName = "Parent Group";
           
            DataTable filteredData = new DataTable();
            filteredData = dt.DefaultView.ToTable(false, "Name", "Parent Group","Description","CssClass","Level");
           

            DataSet ds = new DataSet();
            ds.Tables.Add(filteredData);
            CreateExcelFile.CreateExcelDocument(ds, file);
            return File(file, "application/vnd.ms-excel", "TaxonomyGroups.xlsx");

        }

        [HttpPost]
        public JsonResult GetTaxonomyAssociationGroupTaxonomyAssociation(int? siteID, int TaxonomyGroupId)
        {
            var data = verticalDataImportFactory.GetTaxonomyAssociationGroupFunds(siteID).Where(x => x.TaxonomyAssociationGroupId == TaxonomyGroupId);
            
            var jsonResult = Json(new { data = data, total = data.Count() });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        
        }

        [HttpPost]
        public string SaveGroupHierarchy(List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> updated, List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> deleted, List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> added)
        {
            string retVal="Success";
            if (!(updated == null && deleted == null && added == null))
            {
                retVal = verticalDataImportFactory.SaveTaxonomyGroupFundMapping(added, updated, deleted);
            }
            return retVal;
        }

        [HttpGet]
        public FileResult ExportTaxonomyGroupFunds(int siteId, string taxonomyOrderFeatureMode)
        {
            string file = Path.Combine(ConfigurationManager.AppSettings["Import"], "GroupToFundAssociation.xlsx");
            var data = verticalDataImportFactory.GetTaxonomyAssociationGroupFunds(siteId);

            var groupData = from gr in data
                            select new TaxonomyAssociationGroupTaxonomyAssociationObjectModel
                            {
                                Name = gr.Name,
                                MarketId=gr.MarketId,
                                Order=gr.Order
                               
                            };


            DataTable dt = CreateExcelFile.ListToDataTable<TaxonomyAssociationGroupTaxonomyAssociationObjectModel>(groupData.ToList());
            //Rename the column names to match Excel Format
            dt.Columns["Name"].ColumnName = "Group Name";
            dt.Columns["MarketId"].ColumnName = "CUSIP";
            DataTable filteredData = new DataTable();
            if (taxonomyOrderFeatureMode == "CustomizeOrder")
            {
                filteredData = dt.DefaultView.ToTable(false, "Group Name", "CUSIP", "Order");
            }
            else
            {
                filteredData = dt.DefaultView.ToTable(false, "Group Name", "CUSIP");
            }
            
            DataSet ds = new DataSet();
            ds.Tables.Add(filteredData);
            CreateExcelFile.CreateExcelDocument(ds, file);
            return File(file, "application/vnd.ms-excel", "GroupToFundAssociation.xlsx");

        }
        /// <summary>
        /// Imports the file and parse to model
        /// </summary>
        /// <param name="file"></param>
        /// <returns>List of Taxonomy Association</returns>
        [HttpPost]
        public async Task<JsonResult> ImportTaxonomyGroupFunds(HttpPostedFileBase file, int? siteId, string taxonomyOrderFeatureMode)
        {
            List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> importData = new List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel>();
            List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> validData = new List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel>();
            List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> invalidData = new List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel>();
            List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> invalidDataWithoutOrder = new List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel>();
            string status = string.Empty;
            this.SelectedSiteId = siteId;
            var taxonomyData = taxonomyAssociationFactory.GetAllEntities().Where(x => (x.SiteId == siteId || x.SiteId == null) && x.IsProofing);

            var taxonomyGroupData = GetTaxonomyGroupMappingData(siteId);
            List<TaxonomyGroupObjectModel> returnGroupData = new List<TaxonomyGroupObjectModel>();
            GetTaxonomyGroupsFromHierarchy(taxonomyGroupData, returnGroupData, siteId, null);

            taxonomyGroupData = returnGroupData.FindAll(p => !p.HasChildren);

            if (file != null && file.ContentLength > 0)
            {

                var uploadDir = ConfigurationManager.AppSettings["Import"];

                //Save the file in Application folder
                var uploadPath = Path.Combine(uploadDir, Path.GetFileName(file.FileName));
                file.SaveAs(uploadPath);
                string contactsPath = ExcelReader.CheckPath(uploadPath);
                var data = await ExcelReader.GetDataToListAsync(contactsPath, ImportTaxonomyAssociationGroupFunds);
                if (data.Count < 15000)
                {
                    importData = data.ToList();
                    IEnumerable<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> filteredData = new List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel>();

                    filteredData = (from import in importData
                                    join td in taxonomyData on import.MarketId equals td.MarketId
                                    join tgd in taxonomyGroupData on import.Name equals tgd.Name

                                    select new TaxonomyAssociationGroupTaxonomyAssociationObjectModel()
                                    {
                                        Name = tgd.Name,
                                        MarketId = td.MarketId,
                                        TaxonomyAssociationGroupId = tgd.TaxonomyAssociationGroupId,
                                        TaxonomyAssociationId = td.TaxonomyAssociationId,
                                        Order = import.Order
                                    });


                    if (taxonomyOrderFeatureMode == "CustomizeOrder") // Order is mandatory for CustomizeOrder feature mode
                    {
                        invalidDataWithoutOrder = filteredData.Where(x => x.Order == null).ToList();

                        validData = filteredData.Where(x => x.Order != null).GroupBy(test => new { test.TaxonomyAssociationGroupId, test.TaxonomyAssociationId })
                                 .Select(grp => grp.LastOrDefault())
                                 .ToList();
                    }
                    else
                    {
                        validData = filteredData.GroupBy(test => new { test.TaxonomyAssociationGroupId, test.TaxonomyAssociationId })
                                 .Select(grp => grp.LastOrDefault())
                                 .ToList();
                    }

                    if (validData.Count > 0)
                    {
                        status = verticalDataImportFactory.SaveTaxonomyGroupFundsExcelImport(validData);
                    }

                    invalidData = importData.Where(x => !validData.Exists(p => p.Name == x.Name && p.MarketId == x.MarketId)).ToList();

                    invalidDataWithoutOrder.Where(x => !invalidData.Exists(p => p.Name == x.Name && p.MarketId == x.MarketId))
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
        /// <returns>TaxonomyAssociationObjectModel</returns>
        private TaxonomyAssociationGroupTaxonomyAssociationObjectModel ImportTaxonomyAssociationGroupFunds(IList<string> rowData, IList<string> columnNames)
        {
            TaxonomyAssociationGroupTaxonomyAssociationObjectModel taxonomyAssocaitionGTA = new TaxonomyAssociationGroupTaxonomyAssociationObjectModel();


            if (!string.IsNullOrEmpty((rowData[columnNames.IndexFor("groupname")]).ToString().Trim()))
            {
                taxonomyAssocaitionGTA.Name = (rowData[columnNames.IndexFor("groupname")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                taxonomyAssocaitionGTA.Name = null;
            }


            if (!string.IsNullOrEmpty((rowData[columnNames.IndexFor("cusip")]).ToString().Trim()))
            {
                taxonomyAssocaitionGTA.MarketId = (rowData[columnNames.IndexFor("cusip")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                taxonomyAssocaitionGTA.MarketId = null;
            }

            if (columnNames.Contains("order") && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("order")]).ToString().Trim()))
            {

                string order = (rowData[columnNames.IndexFor("order")]).ToString().Trim().Replace("\n", string.Empty);
                int n;
                bool isNumeric = int.TryParse(order, out n);
                if (isNumeric)
                    taxonomyAssocaitionGTA.Order = n;
                else
                    taxonomyAssocaitionGTA.Order = null;
            }
            else
            {
                taxonomyAssocaitionGTA.Order = null;
            
            }
            return taxonomyAssocaitionGTA;

        }

        [HttpPost]
        public JsonResult GetTaxonomyGroupMapping(int? siteId)
        {
            var jsonResult = Json(new { data = GetTaxonomyGroupMappingData(siteId) });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        private List<TaxonomyGroupObjectModel> GetTaxonomyGroupMappingData(int? siteId)
        {
            var dataTAG = verticalDataImportFactory.GetTaxonomyAssociationGroups(siteId);

            var dataTAGTA = verticalDataImportFactory.GetTaxonomyAssociationGroupFunds(siteId);

            dataTAG.ForEach(p =>
            {
                p.HasFundData = dataTAGTA.Exists(t => t.TaxonomyAssociationGroupId == p.TaxonomyAssociationGroupId);
            });

            int previousGroupLevel = -1;

            List<TaxonomyGroupObjectModel> ParentTAGDataList = new List<TaxonomyGroupObjectModel>();
            List<TaxonomyGroupObjectModel> ChildTAGDataList = new List<TaxonomyGroupObjectModel>();

            foreach (var item in dataTAG.OrderByDescending(p => p.Level).ThenBy(p => p.Order))
            {
                if (previousGroupLevel != item.Level)
                {
                    ChildTAGDataList = ParentTAGDataList;
                    ParentTAGDataList = new List<TaxonomyGroupObjectModel>();

                    previousGroupLevel = item.Level;
                }

                item.ChildTAGData = ChildTAGDataList.FindAll(p => p.ParentTaxonomyAssociationGroupId.Value == item.TaxonomyAssociationGroupId);
                item.HasChildren = item.ChildTAGData != null && item.ChildTAGData.Count > 0;
                ParentTAGDataList.Add(item);
            }

            return ParentTAGDataList;
        }

        [HttpPost]
        public string SaveTaxonomyGroupMapping(int? siteId, List<TaxonomyGroupObjectModel> groupData)
        {
            string retVal = "Success";

            List<TaxonomyGroupObjectModel> updated = new List<TaxonomyGroupObjectModel>();
            List<TaxonomyGroupObjectModel> deleted = new List<TaxonomyGroupObjectModel>();

            List<TaxonomyGroupObjectModel> TAGData = verticalDataImportFactory.GetTaxonomyAssociationGroups(siteId);
            List<int> tagids = new List<int>();

            if (groupData != null && groupData.Count > 0)
            {
                GetUpdatedTaxonomyGroupsFromHierarchy(groupData, updated, siteId, null, tagids);
            }

            TAGData.FindAll(p => !tagids.Contains(p.TaxonomyAssociationGroupId.Value)).ForEach(p =>
            {
                deleted.Add(p);
            });

            if (updated.Count > 0 || deleted.Count > 0)
            {
                retVal = verticalDataImportFactory.SaveTaxonomyAssociationGroup(null, updated, deleted);
            }
            return retVal;
        }

        private void GetUpdatedTaxonomyGroupsFromHierarchy(List<TaxonomyGroupObjectModel> groupData, List<TaxonomyGroupObjectModel> returnGroupData,
                                                    int? siteId, int? newParentTaxonomyAssociationGroupId, List<int> tagids)
        {
            int newOrder = 1;
            foreach (var item in groupData)
            {
                tagids.Add(item.TaxonomyAssociationGroupId.Value);

                if (item.ParentTaxonomyAssociationGroupId != newParentTaxonomyAssociationGroupId || item.Order != newOrder)
                {
                    item.ParentTaxonomyAssociationGroupId = newParentTaxonomyAssociationGroupId;
                    item.Order = newOrder;
                    item.SiteId = siteId;

                    returnGroupData.Add(item);                    
                }

                if (item.ChildTAGData != null && item.ChildTAGData.Count > 0)
                {
                    GetUpdatedTaxonomyGroupsFromHierarchy(item.ChildTAGData, returnGroupData, null, item.TaxonomyAssociationGroupId, tagids);
                }

                newOrder++;
            }
        }

        private void GetTaxonomyGroupsFromHierarchy(List<TaxonomyGroupObjectModel> groupData, List<TaxonomyGroupObjectModel> returnGroupData,
                                                    int? siteId, int? newParentTaxonomyAssociationGroupId)
        {
            foreach (var item in groupData)
            {
                returnGroupData.Add(item);

                if (item.ChildTAGData != null && item.ChildTAGData.Count > 0)
                {
                    GetTaxonomyGroupsFromHierarchy(item.ChildTAGData, returnGroupData, null, item.TaxonomyAssociationGroupId);
                }
            }
        }
        public async Task<JsonResult> TAGDImport(HttpPostedFileBase file, int siteId)
        {

            List<TaxonomyGroupObjectModel> importData = new List<TaxonomyGroupObjectModel>();
            List<TaxonomyGroupObjectModel> validData = new List<TaxonomyGroupObjectModel>();
            List<TaxonomyGroupObjectModel> invalidData = new List<TaxonomyGroupObjectModel>();

            string status = string.Empty;
            var taxonomyGroupData = verticalDataImportFactory.GetTaxonomyAssociationGroups(siteId);
            if (file != null && file.ContentLength > 0)
            {

                var uploadDir = ConfigurationManager.AppSettings["Import"];

                //Save the file in Application folder
                var uploadPath = Path.Combine(uploadDir, Path.GetFileName(file.FileName));
                file.SaveAs(uploadPath);
                string contactsPath = ExcelReader.CheckPath(uploadPath);
                var data = await ExcelReader.GetDataToListAsync(contactsPath, importTaxonomygroups);
                importData = data.ToList();
                IEnumerable<TaxonomyGroupObjectModel> filteredData = new List<TaxonomyGroupObjectModel>();


                filteredData = (from id in importData
                                join tg in taxonomyGroupData on id.Name equals tg.Name 
                                into grp
                                from g in grp.DefaultIfEmpty()
                                where !string.IsNullOrWhiteSpace(id.Name) 
                                
                                select new TaxonomyGroupObjectModel()
                                {
                                    TaxonomyAssociationGroupId = g == null ? null : g.TaxonomyAssociationGroupId,
                                    Name = id.Name,
                                    Description = id.Description,
                                    CssClass = id.CssClass,
                                    SiteId = siteId
                                    

                                });

                validData = filteredData.ToList();

                validData = validData.GroupBy(x => x.Name).Select(y => y.LastOrDefault()).ToList();
                invalidData = importData.FindAll(p => string.IsNullOrWhiteSpace(p.Name));

                status = verticalDataImportFactory.SaveTaxonomyGroupExcelImport(validData);

                FileInfo info = new FileInfo(uploadPath);
                info.Delete();
            }
            return Json(new { status = status, invalidData = invalidData });
        }

        /// <summary>
        /// Maps excel coumns to object model
        /// </summary>
        /// <param name="rowData"></param>
        /// <param name="columnNames"></param>
        /// <returns> TaxonomyGroupObjectModel</returns>
        private TaxonomyGroupObjectModel importTaxonomygroups(IList<string> rowData, IList<string> columnNames)
        {
            TaxonomyGroupObjectModel taxonomyGroup = new TaxonomyGroupObjectModel();


            if (!string.IsNullOrEmpty((rowData[columnNames.IndexFor("name")]).ToString().Trim()))
            {
                taxonomyGroup.Name = (rowData[columnNames.IndexFor("name")]).ToString().Trim().Replace("\n", string.Empty);
            }
            else
            {
                taxonomyGroup.Name = null;
            }

            //if (!string.IsNullOrEmpty((rowData[columnNames.IndexFor("parentgroup")]).ToString().Trim()))
            //{
            //    taxonomyGroup.ParentName = (rowData[columnNames.IndexFor("parentgroup")]).ToString().Trim().Replace("\n", string.Empty);
            //}
            //else
            //{
            //    taxonomyGroup.ParentName = null;
            //}


            if (columnNames.Contains("tooltipinformation") && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("tooltipinformation")]).ToString().Trim()))
            {
                taxonomyGroup.Description = (rowData[columnNames.IndexFor("tooltipinformation")]).ToString().Trim().Replace("\n", string.Empty);

            }
            if (columnNames.Contains("cssclass") && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("cssclass")]).ToString().Trim()))
            {
                taxonomyGroup.CssClass = (rowData[columnNames.IndexFor("cssclass")]).ToString().Trim().Replace("\n", string.Empty);

            }
            //if (columnNames.Contains("level") && !string.IsNullOrEmpty((rowData[columnNames.IndexFor("level")]).ToString().Trim()))
            //{
            //    taxonomyGroup.Level = Convert.ToInt32((rowData[columnNames.IndexFor("level")]).ToString());

            //}

            return taxonomyGroup;

        }

    }
}