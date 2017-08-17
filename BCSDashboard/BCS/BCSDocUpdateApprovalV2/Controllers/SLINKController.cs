using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using BCSDocUpdateApprovalV2.Filters;
using BCSDocUpdateApprovalV2.Formats;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BCSDocUpdateApprovalV2.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorizationFilter(Roles = "*")]
    [CustomExceptionFilter]
    public class SLINKController : ApiController
    {
        [Route("api/SLINK/GetReportStatus")]
        [HttpGet]
        public List<string> GetReportStatus()
        {
            List<string> lstStatus = new List<string>();

            UtilityFactory.GetEnumDescriptionList(typeof(SlinkStatus)).ForEach(x =>
                {
                    lstStatus.Add(x);
                });
            return lstStatus;
        }

        [Route("api/SLINK/Report")]
        [HttpPost]
        public object Report([FromBody]JObject data)
        {
            dynamic gridData = data;
            KendoGridPost postData = new KendoGridPost(gridData);

            //int startIndex = postData.Page * postData.PageSize;
            //int endIndex = startIndex + postData.PageSize, totalCount = 0;
            int startIndex = (postData.Page - 1) * postData.PageSize + 1;
            int endIndex = postData.Page * postData.PageSize;
            int  totalCount = 0;
            string sortField = string.IsNullOrEmpty(postData.SortColumn) ? "SLINKFileName" : postData.SortColumn,
                   sortOrder = string.IsNullOrEmpty(postData.SortOrder) ? "ASC" : postData.SortOrder.ToUpper();

            string ReportStatus = gridData.slinkData.Status;
            string clientName = gridData.slinkData.clientName;
            string DocId = gridData.slinkData.DocId;
            DateTime? ReportDate = gridData.slinkData.reportDate;
            int hdnDateOffSet = Convert.ToInt32(gridData.slinkData.hdnDateOffSet);

            List<SLINKReportDetails> linkReports = new List<SLINKReportDetails>();
            int virtualcount = 0;
            string status = null;
            if (ReportStatus != "")
                status = ReportStatus;
            var selectedStatus = string.IsNullOrEmpty(ReportStatus) ? null : ReportStatus;
            string docID = string.IsNullOrEmpty(DocId) ? null : DocId;

            object countDetails = null;

            linkReports = new BCSDocUpdateApprovalFactory().GetSLinkReportDetails(clientName, selectedStatus, ReportDate, docID, sortField, sortOrder, startIndex, endIndex, out virtualcount, out countDetails);

            linkReports.ForEach(p =>
            {
                p.ReceivedDate = GetLocaltime(p.ReceivedDate.ToUniversalTime(), hdnDateOffSet);
            });

            List<SlinkHeaderData> lstHeader = new List<SlinkHeaderData>();
            SlinkHeaderData objHeader = new SlinkHeaderData();
            System.Reflection.PropertyInfo TC = countDetails.GetType().GetProperty("TotalCount");
            objHeader.TotalCount = (Int32)(TC.GetValue(countDetails, null));

            System.Reflection.PropertyInfo EC = countDetails.GetType().GetProperty("ExCount");
            objHeader.ExCount = (Int32)(EC.GetValue(countDetails, null));

            System.Reflection.PropertyInfo AP = countDetails.GetType().GetProperty("APCount");
            objHeader.APCount = (Int32)(AP.GetValue(countDetails, null));

            System.Reflection.PropertyInfo OP = countDetails.GetType().GetProperty("OPCount");
            objHeader.OPCount = (Int32)(OP.GetValue(countDetails, null));

            System.Reflection.PropertyInfo APC = countDetails.GetType().GetProperty("APCCount");
            objHeader.APCCount = (Int32)(APC.GetValue(countDetails, null));

            System.Reflection.PropertyInfo OPC = countDetails.GetType().GetProperty("OPCCount");
            objHeader.OPCCount = (Int32)(OPC.GetValue(countDetails, null));
            lstHeader.Add(objHeader);


            //List<object> LstobjData = new List<object>();
            //LstobjData.Add(lstHeader);
            //LstobjData.Add(linkReports);
            //return Json(new { total = virtualcount, data = LstobjData });

            return Json(new { total = virtualcount, HeaderData = lstHeader, data = linkReports });
        }

        private object BindSlinkGrid(int endIndex, string ReportStatus, string clientName, string DocId, DateTime? ReportDate, int hdnDateOffSet, string sortField = null, string sortOrder = null, int startIndex = 1)
        {
            List<SLINKReportDetails> linkReports = new List<SLINKReportDetails>();
            int virtualcount = 0;
            string status = null;
            if (ReportStatus != null)
                status = ReportStatus;
            var selectedStatus = string.IsNullOrEmpty(ReportStatus) ? null : ReportStatus;
            string docID = string.IsNullOrEmpty(DocId) ? null : DocId;

            object countDetails = null;

            linkReports = new BCSDocUpdateApprovalFactory().GetSLinkReportDetails(clientName, selectedStatus, ReportDate, docID, sortField, sortOrder, startIndex, endIndex, out virtualcount, out countDetails);
           
            linkReports.ForEach(p =>
            {
                p.ReceivedDate = GetLocaltime(p.ReceivedDate.ToUniversalTime(), hdnDateOffSet);
            });

            SlinkHeaderData objHeader = new SlinkHeaderData();
            System.Reflection.PropertyInfo TC = countDetails.GetType().GetProperty("TotalCount");
            objHeader.TotalCount = (Int32)(TC.GetValue(countDetails, null));

            System.Reflection.PropertyInfo EC = countDetails.GetType().GetProperty("ExCount");
            objHeader.ExCount = (Int32)(EC.GetValue(countDetails, null));

            System.Reflection.PropertyInfo AP = countDetails.GetType().GetProperty("APCount");
            objHeader.APCount = (Int32)(AP.GetValue(countDetails, null));

            System.Reflection.PropertyInfo OP = countDetails.GetType().GetProperty("OPCount");
            objHeader.OPCount = (Int32)(OP.GetValue(countDetails, null));

            System.Reflection.PropertyInfo APC = countDetails.GetType().GetProperty("APCCount");
            objHeader.APCCount = (Int32)(APC.GetValue(countDetails, null));

            System.Reflection.PropertyInfo OPC = countDetails.GetType().GetProperty("OPCCount");
            objHeader.OPCCount = (Int32)(OPC.GetValue(countDetails, null));

            return Json(new { total = virtualcount, HeaderData = objHeader, DetailData = linkReports });
        }

        public DateTime GetLocaltime(DateTime utcDate, int offset)
        {
            //Note:  The time-zone offset is the difference, in minutes, between UTC and local time.   i.e  offset = utc - localtime
            TimeSpan interval = TimeSpan.FromMinutes(Convert.ToInt32(offset));
            return (utcDate - interval);
        }
    }
}
