// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-17-2015
// ***********************************************************************

using RP.Extensions;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.Common;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Model.SortDetail.System;
using System.Data.SqlClient;
using RRD.FSG.RP.Web.UI.HostedAdmin.Common;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.SearchEntities.System;
using System.IO;
using RRD.FSG.RP.Model.Factories.System;
using RP.Extensions;
using RRD.FSG.RP.Model.Interfaces;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    /// <summary>
    /// Class ReportScheduleController.
    /// </summary>
    public class ReportScheduleController : BaseController
    {
        #region Properties
        /// <summary>
        /// The report schedule cache factory
        /// </summary>
        private IFactory<ReportScheduleObjectModel, int> reportScheduleFactory;
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;
        #endregion 

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportScheduleController"/> class.
        /// </summary>
        public ReportScheduleController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {
                reportScheduleFactory = RPV2Resolver.Resolve<IFactory<ReportScheduleObjectModel, int>>("ReportSchedule");

                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;
            }

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportScheduleController"/> class.
        /// </summary>
        /// <param name="reportScheduleCacheFactoryMock">The report schedule cache factory mock.</param>
        /// <param name="userCacheFactoryMock">The user cache factory mock.</param>
        /// 
        public ReportScheduleController(IFactory<ReportScheduleObjectModel, int> reportScheduleCacheFactoryMock, IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactoryMock)
        {
            reportScheduleFactory = reportScheduleCacheFactoryMock;
            userCacheFactory = userCacheFactoryMock;
            userCacheFactory.Mode = FactoryCacheMode.All;
        }
        //public ReportScheduleController(IFactory<ReportScheduleFactory, int> reportScheduleCacheFactoryMock, IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactoryMock)
        //{
        //    reportScheduleFactory = reportScheduleCacheFactoryMock;
        //    userCacheFactory = userCacheFactoryMock;
        //    userCacheFactory.Mode = FactoryCacheMode.All;
            
        //}

        #endregion

        #region List
        // GET: ReportSchedule
        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult List()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }
        #endregion

        #region Edit
        /// <summary>
        /// Edits the Report Scheduling.
        /// </summary>
        /// <param name="id1">The id1.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [IsPopUp]
        public ActionResult EditReportSchedule(int? id1)
        {
            EditReportScheduleViewModel viewModel = new EditReportScheduleViewModel();
            viewModel.FrequencyType = new List<DisplayValuePair>();
            
             viewModel.FrequencyType.Add(new DisplayValuePair { Value = "-1", Display = "--Please select Frequency Type-" });
            foreach (int key in Enum.GetValues(typeof(FrequencyType)))
            {
                viewModel.FrequencyType.Add(new DisplayValuePair { Display = Enum.GetName(typeof(FrequencyType), key), Value = key.ToString() });

            }
            viewModel.TransferType = new List<DisplayValuePair>();
            viewModel.TransferType.Add(new DisplayValuePair { Value = "-1", Display = "--Please select Transfer type--" });
            viewModel.TransferType.Add(new DisplayValuePair { Value = "0", Display = "Email" });
            viewModel.TransferType.Add(new DisplayValuePair { Value = "1", Display = "SFTP" });

            viewModel.ReportName = new List<DisplayValuePair>();
            viewModel.ReportName.Add(new DisplayValuePair { Value = "-1", Display = "--Please select Report--" });
            foreach (var report in reportScheduleFactory.GetEntitiesBySearch(0, -1, null))
            {
                viewModel.ReportName.Add(new DisplayValuePair { Value = report.ReportId.ToString(), Display = report.ReportName});
            }
        

            if (id1 > 0)
            {                
                ReportScheduleObjectModel objObjectModel = reportScheduleFactory.GetEntityByKey(id1.Value);
               
                viewModel.ReportScheduleId = objObjectModel.ReportScheduleId;
                viewModel.SelectedReportId = objObjectModel.ReportId;
                viewModel.SelectedFrequencyType = objObjectModel.FrequencyType;
                viewModel.FrequencyInterval = objObjectModel.FrequencyInterval;
                viewModel.FrequencyDescription = objObjectModel.FrequencyDescription;
                viewModel.UtcFirstScheduledRunDate = objObjectModel.UtcFirstScheduledRunDate.Value.ToString();
                //viewModel.UtcLastScheduledRunDate = RPFormatDate(objObjectModel.UtcLastScheduledRunDate);
                viewModel.UTCLastModifiedDate = objObjectModel.LastModified;
                viewModel.ModifiedBy = objObjectModel.ModifiedBy;
                viewModel.IsEnabled = objObjectModel.IsEnabled;
                viewModel.Email = objObjectModel.Email;
                viewModel.FTPServerIP = objObjectModel.FTPServerIP;
                viewModel.FTPFilePath = objObjectModel.FTPFilePath;
                viewModel.FTPUsername = objObjectModel.FTPUsername;
                viewModel.FTPPassword = objObjectModel.FTPPassword;
                if(!string.IsNullOrEmpty(viewModel.Email))
                {
                    viewModel.SelectedTransferType = 0;

                }
                else
                {
                    viewModel.SelectedTransferType = 1;
                }
                if (objObjectModel.ModifiedBy != 0)
                {
                    viewModel.ModifiedByName = objObjectModel.ModifiedBy.ToString();
                    if (userCacheFactory.GetEntitiesBySearch(0, 0, new UserSearchDetail { UserID = objObjectModel.ModifiedBy }).Count() > 0)
                    {
                        viewModel.ModifiedByName = userCacheFactory.GetEntitiesBySearch(0, 0, new UserSearchDetail { UserID = objObjectModel.ModifiedBy }).FirstOrDefault().UserName;
                    }
                }
                else
                {

                    viewModel.ModifiedByName = string.Empty;
                }
            }

            else
            {
                viewModel.SelectedReportId = -1;
                viewModel.SelectedFrequencyType = -1;
                viewModel.SelectedTransferType = -1;

            }


            ViewData["Success"] = "In Progress";
            return View(viewModel);
           
        }
       
        /// <summary>
        /// Edits the report schedule.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult EditReportSchedule(EditReportScheduleViewModel viewModel)
        {
            try
            {
              
                ReportScheduleObjectModel objObjectModel = new ReportScheduleObjectModel();
                objObjectModel.ReportScheduleId = viewModel.ReportScheduleId;
                objObjectModel.ReportId = viewModel.SelectedReportId;
                objObjectModel.FrequencyType = viewModel.SelectedFrequencyType;                
                objObjectModel.FrequencyDescription = viewModel.FrequencyDescription;
                objObjectModel.IsEnabled = viewModel.IsEnabled;
                objObjectModel.UtcLastScheduledRunDate = string.IsNullOrEmpty(viewModel.UtcLastScheduledRunDate)? (DateTime?) null : Convert.ToDateTime(viewModel.UtcLastScheduledRunDate);
                TimeSpan interval = TimeSpan.FromMinutes(Convert.ToInt32(Request.Form["offsetTime"]));
                objObjectModel.UtcFirstScheduledRunDate = Convert.ToDateTime(viewModel.UtcFirstScheduledRunDate) + interval;
                objObjectModel.UtcNextScheduledRunDate = string.IsNullOrEmpty(viewModel.UtcNextScheduledRunDate) ? (DateTime?)null : Convert.ToDateTime(viewModel.UtcNextScheduledRunDate);
                objObjectModel.ClientId = SessionClientID;
                objObjectModel.Email = viewModel.Email;
                objObjectModel.FTPServerIP = viewModel.FTPServerIP;
                objObjectModel.FTPFilePath = viewModel.FTPFilePath;
                objObjectModel.FTPUsername = viewModel.FTPUsername;
                objObjectModel.FTPPassword = viewModel.FTPPassword;
                
                objObjectModel.FrequencyInterval = CalculateFrequencyInterval(objObjectModel.UtcFirstScheduledRunDate, (FrequencyType)objObjectModel.FrequencyType, viewModel.FrequencyInterval);                

                reportScheduleFactory.SaveEntity(objObjectModel, SessionUserID);

                viewModel.FrequencyType = new List<DisplayValuePair>();

                viewModel.FrequencyType.Add(new DisplayValuePair { Value = "-1", Display = "--Please select Frequency Type-" });
                foreach (int key in Enum.GetValues(typeof(FrequencyType)))
                {
                    viewModel.FrequencyType.Add(new DisplayValuePair { Display = Enum.GetName(typeof(FrequencyType), key), Value = key.ToString() });
                }

                viewModel.TransferType = new List<DisplayValuePair>();
                viewModel.TransferType.Add(new DisplayValuePair { Value = "-1", Display = "--Please select Transfer type--" });
                viewModel.TransferType.Add(new DisplayValuePair { Value = "0", Display = "Email" });
                viewModel.TransferType.Add(new DisplayValuePair { Value = "1", Display = "FTP" });
                viewModel.ReportName = new List<DisplayValuePair>();
                viewModel.ReportName.Add(new DisplayValuePair { Value = "-1", Display = "--Please select Report--" });
                foreach (var report in reportScheduleFactory.GetEntitiesBySearch(0, -1, null))
                {
                    viewModel.ReportName.Add(new DisplayValuePair { Value = report.ReportId.ToString(), Display = report.ReportName });
                }

                ViewData["Success"] = "OK";
            }
            catch (Exception exception)
            {
                viewModel.SuccessOrFailedMessage = exception.Message;
            }
            return View(viewModel);
        }


        /// <summary>
        /// CalculateFrequencyInterval based on frequencyType and  utcFirstScheduledRunDate.
        /// </summary>
        /// <param name="utcFirstScheduledRunDate">The first schedule rundate.</param>
        /// <param name="frequencyType">Type of the frequency.</param>
        /// <param name="frequencyInterval">The frequency interval.</param>        
        /// <returns>frequencyInterval</returns>
        private int CalculateFrequencyInterval(DateTime? utcFirstScheduledRunDate, FrequencyType frequencyType, int frequencyInterval)
        {
            switch (frequencyType)
            {                
                case FrequencyType.Weekly:
                    frequencyInterval = (int)utcFirstScheduledRunDate.Value.DayOfWeek + 1;
                    break;
                case FrequencyType.Monthly:
                    frequencyInterval = utcFirstScheduledRunDate.Value.Day;
                    break;
            }

            return frequencyInterval;
        }
        #endregion

        #region GetAllReportScheduleDetails
        /// <summary>
        /// Gets all report schedule details.
        /// </summary>
        /// <param name="reportName">Name of the report.</param>
        /// <param name="frequencyType">Type of the frequency.</param>
        /// <param name="isEnabled">The is enabled.</param>
        /// <param name="firstScheduleRundate">The first schedule rundate.</param>
        /// <param name="nextScheduleRunDate">The next schedule run date.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        [IsPopUp]
        public JsonResult GetAllReportScheduleDetails(string reportName, string  frequencyType, string isEnabled, string firstScheduleRundate, string nextScheduleRunDate,int offset)
        {
            int i;

            //For invalid Search in Isenabled Drop Down
            bool? isInvalidSearch = null;
            if (!string.IsNullOrEmpty(isEnabled))
            {
                if (isEnabled == "True" || isEnabled == "False")
                {
                    isInvalidSearch = false;
                }
                else // if invalid string is passed
                {
                    isInvalidSearch = true;
                    isEnabled = string.Empty;
                }
            }
            else
            {
                isInvalidSearch = false;
            }

        
            ReportScheduleSearchDetail objReportScheduleSearchDetail = new ReportScheduleSearchDetail()
            {

                ReportName = string.IsNullOrWhiteSpace(reportName) ? null : reportName
                ,
                FrequencyType = !(string.IsNullOrEmpty(frequencyType)) ? (int.TryParse(frequencyType, out i) ? i : 0) : (int?)null
                ,

                IsEnabled = string.IsNullOrEmpty(isEnabled) ? (bool?)null : Convert.ToBoolean(isEnabled)
                ,
                FirstScheduleRunDate = string.IsNullOrEmpty(firstScheduleRundate) ? (DateTime?)null : Convert.ToDateTime(firstScheduleRundate)
                ,
                NextScheduleRunDate = string.IsNullOrEmpty(nextScheduleRunDate) ? (DateTime?)null : Convert.ToDateTime(nextScheduleRunDate)
                ,
                ClientId = SessionClientID

            };

            
            
            
            KendoGridPost kendoGridPost = new KendoGridPost();            

            ReportScheduleSortColumn SortColumn = ReportScheduleSortColumn.ReportName;
            switch (kendoGridPost.SortColumn)
            {
                case "FrequencyType":
                    SortColumn = ReportScheduleSortColumn.FrequencyType;
                    break;              
                    break;
                case "UtcFirstScheduledRunDate":
                    SortColumn = ReportScheduleSortColumn.UtcFirstScheduledRunDate;
                    break;
                case "UtcLastActualRunDate":
                    SortColumn = ReportScheduleSortColumn.UtcLastActualRunDate;
                    break;
                case "UtcNextScheduledRunDate":
                    SortColumn = ReportScheduleSortColumn.UtcNextScheduledRunDate;
                    break;
                case "IsEnabled":
                    SortColumn = ReportScheduleSortColumn.IsEnabled;
                    break;
            }


            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }
            if( isInvalidSearch.Equals(false))
            {
                int totalRecords = 0;
                IEnumerable<ReportScheduleObjectModel> reportScheduleDetails = reportScheduleFactory.GetEntitiesBySearch(kendoGridPost.Page, kendoGridPost.PageSize,
                                                                                                           objReportScheduleSearchDetail,
                                                                                                            new ReportScheduleSortDetail() { Column = SortColumn, Order = sortOrder }, out totalRecords);


                List<ReportScheduleViewModel> lstViewModel = new List<ReportScheduleViewModel>();
                foreach (ReportScheduleObjectModel objectModel in reportScheduleDetails)
                {
                    ReportScheduleViewModel objviewModel = new ReportScheduleViewModel();
                    objviewModel.ReportScheduleId = objectModel.ReportScheduleId;
                    objviewModel.ReportName = objectModel.ReportName;
                    objviewModel.ReportId = objectModel.ReportId;
                    objviewModel.FrequencyType = Enum.GetName(typeof(FrequencyType), objectModel.FrequencyType);
                    objviewModel.FrequencyInterval = objectModel.FrequencyInterval;
                    objviewModel.UtcFirstScheduledRunDate = GetLocaltime(objectModel.UtcFirstScheduledRunDate.Value, offset);
                    objviewModel.UtcLastActualRunDate = string.IsNullOrEmpty(objectModel.UtcLastActualRunDate.ToString()) ? null : GetLocaltime(objectModel.UtcLastActualRunDate.Value,offset);
                    objviewModel.UtcNextScheduledRunDate = string.IsNullOrEmpty(objectModel.UtcNextScheduledRunDate.ToString()) ? null : GetLocaltime(objectModel.UtcNextScheduledRunDate.Value,offset);
                    objviewModel.IsEnabled = objectModel.IsEnabled.ToString();
                    lstViewModel.Add(objviewModel);
                }
                return Json(new { total = totalRecords, data = lstViewModel });
            }
            else
            {
                return Json(new { total = 0, data = Enumerable.Empty<ReportScheduleObjectModel>() });
            }
           
            
        }
        #endregion

        #region Load Search Creteria Methods.

        /// <summary>
        /// Gets the type of the frequency.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetFrequencyType()
        {

            List<DisplayValuePair> lstDisplayValuePair = new List<DisplayValuePair>();

            foreach (int key in Enum.GetValues(typeof(FrequencyType)))
            {
                lstDisplayValuePair.Add(new DisplayValuePair { Display = Enum.GetName(typeof(FrequencyType), key), Value = key.ToString() });
            }
            return Json(lstDisplayValuePair, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Gets the name of the report.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetReportName()
        {
            return Json((

                (from obj in reportScheduleFactory.GetEntitiesBySearch(new ReportScheduleSearchDetail { ClientId = SessionClientID })
                 select new { Display = obj.ReportName, Value = obj.ReportName }).Distinct()
                
                ), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the frequency interval.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetFrequencyInterval()
        {
            return Json((

               (from obj in reportScheduleFactory.GetEntitiesBySearch(new ReportScheduleSearchDetail { ClientId = SessionClientID })
                select new { Display = obj.FrequencyInterval, Value = obj.FrequencyInterval }).Distinct()

               ), JsonRequestBehavior.AllowGet);
        }
        

        /// <summary>
        /// Gets the is enabled.
        /// </summary>
        /// <returns>JsonResult.</returns>
         [HttpGet]
        public JsonResult GetIsEnabled()
        {
            return Json((

               (from obj in reportScheduleFactory.GetEntitiesBySearch(new ReportScheduleSearchDetail { ClientId = SessionClientID })
                select new { Display = obj.IsEnabled.ToString(), Value = obj.IsEnabled.ToString() }).Distinct()

               ), JsonRequestBehavior.AllowGet);
        }
       

         /// <summary>
         /// Gets the first schedule run date.
         /// </summary>
         /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetFirstScheduleRunDate()
        {
          List<DisplayValuePair> lstDisplayValuePair = new List<DisplayValuePair>();
            
             foreach (var item in reportScheduleFactory.GetEntitiesBySearch(new ReportScheduleSearchDetail { ClientId = SessionClientID }))
             {
                 if (item.UtcFirstScheduledRunDate != null)
                 {
                     string data = RPFormatDate(item.UtcFirstScheduledRunDate);
                     if (!lstDisplayValuePair.Select(x => x.Value).Contains(data))
                     {
                         lstDisplayValuePair.Add(new DisplayValuePair { Display = data, Value = data });
                     }
                 }
             }
            
            return Json(lstDisplayValuePair, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the next schedule run date.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetNextScheduleRunDate()
        {
          List<DisplayValuePair> lstDisplayValuePair = new List<DisplayValuePair>();
            foreach(var item in reportScheduleFactory.GetEntitiesBySearch(new ReportScheduleSearchDetail { ClientId = SessionClientID }))
            {
                if (item.UtcNextScheduledRunDate != null)
                {
                    string data = RPFormatDate(item.UtcNextScheduledRunDate);
                    if (!lstDisplayValuePair.Select(x => x.Value).Contains(data))
                    {
                        lstDisplayValuePair.Add(new DisplayValuePair { Display = data, Value = data });
                    }
                }
            }

          
            return Json(lstDisplayValuePair, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Disable Report Schedule
        /// <summary>
        /// Deletes the Report Schedule.
        /// </summary>
        /// <param name="reportScheduleId">The report schedule identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DeleteReportSchedule(int reportScheduleId)
        {
            int? userId = userCacheFactory.GetEntitiesBySearch(0, 0, new UserSearchDetail { UserName = HttpContext.User.Identity.Name }).First().UserId;
            reportScheduleFactory.DeleteEntity(reportScheduleId, userId == null ? 0 : userId.Value);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Caluculates Local Time
        /// <summary>
        /// Caluculates Local Time
        /// </summary>
        /// <param name="utcDate">The UTC date.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>System.String.</returns>
        public string GetLocaltime(DateTime utcDate , int offset)
        {
            //Note:  The time-zone offset is the difference, in minutes, between UTC and local time.   i.e  offset = utc - localtime
            TimeSpan interval = TimeSpan.FromMinutes(Convert.ToInt32(offset));
            return (utcDate - interval).ToString();
        }
        #endregion

    }
}