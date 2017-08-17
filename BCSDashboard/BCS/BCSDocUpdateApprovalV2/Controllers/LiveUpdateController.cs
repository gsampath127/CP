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
    [RoutePrefix("api/LiveUpdate")]
    public class LiveUpdateController : ApiController
    {
        [Route("GetReportStatus")]
        [HttpGet]
        public List<string> GetReportStatus()
        {
            List<string> lstStatus = new List<string>();
            UtilityFactory.GetEnumDescriptionList(typeof(LiveUpdateStatus)).ForEach(x =>
            {
                lstStatus.Add(x);
            });
            return lstStatus;
        }

        [Route("Report")]
        [HttpPost]
        public Object Report([FromBody]JObject data)
        {
            dynamic gridData = data;
            string clientName = gridData.liveUpdate.clientName;
            string ReportStatus = gridData.liveUpdate.Status;
            string DocId = gridData.liveUpdate.DocId;
            string Cusip = gridData.liveUpdate.CUSIP;
            string Accnt = gridData.liveUpdate.Accnt;
            int hdnDateOffSet = Convert.ToInt32(gridData.liveUpdate.hdnDateOffSet);

            KendoGridPost postData = new KendoGridPost(gridData);

            int startIndex = (postData.Page - 1) * postData.PageSize;
            int endIndex = startIndex + postData.PageSize;

            switch (clientName)
            {
                case "Transamerica":
                    {
                        LiveUpdateReport LiveUpdateData = BindToGridLiveUpdateForCustomer(startIndex, endIndex, ReportStatus, clientName, Cusip, Accnt, DocId, hdnDateOffSet);
                        return Json(new
                        {
                            
                            total = LiveUpdateData.TotalCount,LiveUpdateData = LiveUpdateData,
                            data = LiveUpdateData.BCSDocUpdateApprovalCUSIPData.AllCUSIPDetails
                        });
                    }
                case "AllianceBernstein":
                    {
                        LiveUpdateReport LiveUpdateData = BindToGridLiveUpdateForCustomer(startIndex, endIndex, ReportStatus, clientName, Cusip, Accnt, DocId, hdnDateOffSet);
                        return Json(new
                        {
                            total = LiveUpdateData.TotalCount,
                            LiveUpdateData = LiveUpdateData,
                            data = LiveUpdateData.BCSDocUpdateApprovalCUSIPData.AllCUSIPDetails
                        });
                    }
                case "GMS":
                    {
                        LiveUpdateReport LiveUpdateData = BindToGridLiveUpdateForGIM(startIndex, endIndex, ReportStatus, clientName, Cusip, Accnt, DocId, hdnDateOffSet);

                        return Json(new
                        {
                            total = LiveUpdateData.TotalCount,
                            LiveUpdateData = LiveUpdateData,
                            data = LiveUpdateData.BCSDocUpdateApprovalCUSIPData.AllCUSIPDetails
                        });
                    }
                case "GIM":
                    {
                        LiveUpdateReport LiveUpdateData = BindToGridLiveUpdateForGIM(startIndex, endIndex, ReportStatus, clientName, Cusip, Accnt, DocId, hdnDateOffSet);

                        return Json(new
                        {
                            total = LiveUpdateData.TotalCount,
                            LiveUpdateData = LiveUpdateData,
                            data = LiveUpdateData.BCSDocUpdateApprovalCUSIPData.AllCUSIPDetails
                        });
                    }
                default:
                    return new object();
            }
        }

        private LiveUpdateReport BindToGridLiveUpdateForCustomer(int startIndex, int endIndex, string ReportStatus, string clientName, string Cusip, string Accnt, string DocId, int hdnDateOffSet)
        {
            LiveUpdateReport objLiveUpdateReport = new LiveUpdateReport();

            objLiveUpdateReport.Show_GridLiveUpdate = false;
            objLiveUpdateReport.Show_divGridLiveUpdateInvalidCUSIPs = false;
            objLiveUpdateReport.Show_divCUSIPCount = false;
            objLiveUpdateReport.Show_divGridLiveUpdateInvalidDocIds = false;
            objLiveUpdateReport.Show_divDocIdsCount = false;

            List<string> lstInvalidCUSIPs = new List<string>();
            List<string> lstInvalidDocIds = new List<string>();
            string status = null;
            if (ReportStatus != null)
            {
                status = ReportStatus;
            }
            BCSDocUpdateApprovalCUSIPData bCSTransamericaCustomerDocUpdateAllCUSIPData = new BCSDocUpdateApprovalFactory().GetBCSLiveUpdateCustomerDocUpdateAllCUSIPData(clientName, string.IsNullOrWhiteSpace(Cusip) ? null : Cusip.Trim(), string.IsNullOrWhiteSpace(Accnt) ? null : Accnt.Trim(), startIndex, endIndex, status, string.IsNullOrWhiteSpace(DocId) ? null : DocId.Trim(), lstInvalidCUSIPs, lstInvalidDocIds);
            bCSTransamericaCustomerDocUpdateAllCUSIPData.AllCUSIPDetails.ForEach(p =>
            {
                p.StatusDate = GetLocaltimeString(DateTime.Parse(p.StatusDate).ToUniversalTime(), hdnDateOffSet);
            });
            // 1
            objLiveUpdateReport.BCSDocUpdateApprovalCUSIPData = bCSTransamericaCustomerDocUpdateAllCUSIPData;
            objLiveUpdateReport.Show_GridLiveUpdate = true;

            if (lstInvalidCUSIPs.Count > 0)
            {
                List<object> obj = new List<object>();
                lstInvalidCUSIPs.ForEach(p =>
                {
                    obj.Add(new { CUSIP = p });
                });

                List<Cusips> lstInvalidCusip = new List<Cusips>();
                lstInvalidCUSIPs.ForEach(item =>
                {
                    Cusips objCS = new Cusips();
                    objCS.CUSIP = item;
                    lstInvalidCusip.Add(objCS);
                });
                // 2
                objLiveUpdateReport.lstInvalidCusips = lstInvalidCusip;
                objLiveUpdateReport.Show_divGridLiveUpdateInvalidCUSIPs = true;
            }

            if (!string.IsNullOrWhiteSpace(Cusip))
            {
                string[] cusipDeliminator = new string[] { "\n" };
                string[] lstCUSIP = Cusip.Replace("\r", "").Replace("\t", "").Split(cusipDeliminator, StringSplitOptions.RemoveEmptyEntries);

                IList<object> objList = new List<object>();
                objList.Add(new
                {
                    TotalCUSIPs = lstCUSIP.Length,
                    CUSIPsFound = lstCUSIP.Length - lstInvalidCUSIPs.Count,
                    MissingCUSIPs = lstInvalidCUSIPs.Count
                });

                //LiveUpdateCUSIPDetails
                List<LiveUpdateCUSIPDetails> lstCusipDetails = new List<LiveUpdateCUSIPDetails>();

                LiveUpdateCUSIPDetails objCD = new LiveUpdateCUSIPDetails();
                System.Reflection.PropertyInfo TC = objList[0].GetType().GetProperty("TotalCUSIPs");
                objCD.TotalCUSIPs = (Int32)(TC.GetValue(objList[0], null));

                System.Reflection.PropertyInfo CF = objList[0].GetType().GetProperty("CUSIPsFound");
                objCD.CUSIPsFound = (Int32)(CF.GetValue(objList[0], null));

                System.Reflection.PropertyInfo MC = objList[0].GetType().GetProperty("MissingCUSIPs");
                objCD.MissingCUSIPs = (Int32)(MC.GetValue(objList[0], null));
                lstCusipDetails.Add(objCD);

                // 3
                objLiveUpdateReport.LiveUpdateCUSIPDetails = lstCusipDetails;
                objLiveUpdateReport.Show_divCUSIPCount = true;
            }
            //Invalid DocIds
            if (lstInvalidDocIds.Count > 0)
            {
                List<object> obj = new List<object>();
                lstInvalidDocIds.ForEach(p =>
                {
                    obj.Add(new { DocId = p });
                });

                List<DocIds> lstInvalidDocumentIds = new List<DocIds>();
                lstInvalidDocIds.ForEach(item =>
                {
                    DocIds objDoc = new DocIds();
                    objDoc.DocId = item;
                    lstInvalidDocumentIds.Add(objDoc);
                });

                // 4
                objLiveUpdateReport.lstInvalidDocumentIds = lstInvalidDocumentIds;
                objLiveUpdateReport.Show_divGridLiveUpdateInvalidDocIds = true;
            }

            if (!string.IsNullOrWhiteSpace(DocId))
            {
                string[] docIdDeliminator = new string[] { "\n" };
                string[] lstDocId = DocId.Replace("\r", "").Replace("\t", "").Split(docIdDeliminator, StringSplitOptions.RemoveEmptyEntries);

                IList<object> objList = new List<object>();
                objList.Add(new
                {
                    TotalDocIds = lstDocId.Length,
                    DocIdsFound = lstDocId.Length - lstInvalidDocIds.Count,
                    MissingDocIds = lstInvalidDocIds.Count
                });

                // LiveUpdateDocumentIdDetails
                List<LiveUpdateDocumentIdDetails> lstDocIdDetails = new List<LiveUpdateDocumentIdDetails>();
                LiveUpdateDocumentIdDetails objDD = new LiveUpdateDocumentIdDetails();
                System.Reflection.PropertyInfo TD = objList[0].GetType().GetProperty("TotalDocIds");
                objDD.TotalDocIds = (Int32)(TD.GetValue(objList[0], null));

                System.Reflection.PropertyInfo DF = objList[0].GetType().GetProperty("DocIdsFound");
                objDD.DocIdsFound = (Int32)(DF.GetValue(objList[0], null));

                System.Reflection.PropertyInfo MD = objList[0].GetType().GetProperty("MissingDocIds");
                objDD.MissingDocIds = (Int32)(MD.GetValue(objList[0], null));
                lstDocIdDetails.Add(objDD);

                // 5
                objLiveUpdateReport.LiveUpdateDocumentIdDetails = lstDocIdDetails;
                objLiveUpdateReport.Show_divDocIdsCount = true;
            }
            objLiveUpdateReport.TotalCount = bCSTransamericaCustomerDocUpdateAllCUSIPData.AllCUSIPDetailsTotalCount;
            return objLiveUpdateReport;
        }

        private LiveUpdateReport BindToGridLiveUpdateForGIM(int startIndex, int endIndex, string ReportStatus, string clientName, string Cusip, string Accnt, string DocId, int hdnDateOffSet)
        {
            LiveUpdateReport objLiveUpdateReport = new LiveUpdateReport();

            objLiveUpdateReport.Show_GridLiveUpdate = false;
            objLiveUpdateReport.Show_divGridLiveUpdateInvalidCUSIPs = false;
            objLiveUpdateReport.Show_divCUSIPCount = false;
            objLiveUpdateReport.Show_divGridLiveUpdateInvalidDocIds = false;
            objLiveUpdateReport.Show_divDocIdsCount = false;

            List<string> lstInvalidCUSIPs = new List<string>();
            List<string> lstInvalidDocIds = new List<string>();
            string status = null;
            if (ReportStatus!=null)
            {
                status = ReportStatus;
            }
           // GridLiveUpdate.Visible = true;
            BCSDocUpdateApprovalCUSIPData bCSDocUpdateApprovalCUSIPData = new BCSDocUpdateApprovalFactory().GetBCSDocUpdateApprovalAllCUSIPData(string.IsNullOrWhiteSpace(Cusip) ? null : Cusip.Trim(), string.IsNullOrWhiteSpace(Accnt) ? null : Accnt.Trim(), startIndex, endIndex, status, string.IsNullOrWhiteSpace(DocId) ? null :DocId.Trim(), lstInvalidCUSIPs, lstInvalidDocIds);

            bCSDocUpdateApprovalCUSIPData.AllCUSIPDetails.ForEach(p =>
            {
                p.StatusDate = GetLocaltimeString(DateTime.Parse(p.StatusDate).ToUniversalTime(), hdnDateOffSet);
            });

            // 1
            objLiveUpdateReport.BCSDocUpdateApprovalCUSIPData = bCSDocUpdateApprovalCUSIPData;
            objLiveUpdateReport.Show_GridLiveUpdate = true;

            if (lstInvalidCUSIPs.Count > 0)
            {
                List<object> obj = new List<object>();
                lstInvalidCUSIPs.ForEach(p =>
                {
                    obj.Add(new { CUSIP = p });
                });

                List<Cusips> lstInvalidCusip = new List<Cusips>();
                lstInvalidCUSIPs.ForEach(item =>
                {
                    Cusips objCS = new Cusips();
                    objCS.CUSIP = item;
                    lstInvalidCusip.Add(objCS);
                });
                // 2
                objLiveUpdateReport.lstInvalidCusips = lstInvalidCusip;
                objLiveUpdateReport.Show_divGridLiveUpdateInvalidCUSIPs = true;
            }

            if (!string.IsNullOrWhiteSpace(Cusip))
            {
                string[] cusipDeliminator = new string[] { "\n" };
                string[] lstCUSIP = Cusip.Replace("\r", "").Replace("\t", "").Split(cusipDeliminator, StringSplitOptions.RemoveEmptyEntries);

                IList<object> objList = new List<object>();
                objList.Add(new
                {
                    TotalCUSIPs = lstCUSIP.Length,
                    CUSIPsFound = lstCUSIP.Length - lstInvalidCUSIPs.Count,
                    MissingCUSIPs = lstInvalidCUSIPs.Count
                });
                //LiveUpdateCUSIPDetails
                List<LiveUpdateCUSIPDetails> lstCusipDetails = new List<LiveUpdateCUSIPDetails>();

                LiveUpdateCUSIPDetails objCD = new LiveUpdateCUSIPDetails();
                System.Reflection.PropertyInfo TC = objList[0].GetType().GetProperty("TotalCUSIPs");
                objCD.TotalCUSIPs = (Int32)(TC.GetValue(objList[0], null));

                System.Reflection.PropertyInfo CF = objList[0].GetType().GetProperty("CUSIPsFound");
                objCD.CUSIPsFound = (Int32)(CF.GetValue(objList[0], null));

                System.Reflection.PropertyInfo MC = objList[0].GetType().GetProperty("MissingCUSIPs");
                objCD.MissingCUSIPs = (Int32)(MC.GetValue(objList[0], null));
                lstCusipDetails.Add(objCD);

                // 3
                objLiveUpdateReport.LiveUpdateCUSIPDetails = lstCusipDetails;
                objLiveUpdateReport.Show_divCUSIPCount = true;
            }

            //Invalid DocIds
            if (lstInvalidDocIds.Count > 0)
            {
                List<object> obj = new List<object>();
                lstInvalidDocIds.ForEach(p =>
                {
                    obj.Add(new { DocId = p });
                });

                List<DocIds> lstInvalidDocumentIds = new List<DocIds>();
                lstInvalidDocIds.ForEach(item =>
                {
                    DocIds objDoc = new DocIds();
                    objDoc.DocId = item;
                    lstInvalidDocumentIds.Add(objDoc);
                });

                // 4
                objLiveUpdateReport.lstInvalidDocumentIds = lstInvalidDocumentIds;
                objLiveUpdateReport.Show_divGridLiveUpdateInvalidDocIds = true;
            }

            if (!string.IsNullOrWhiteSpace(DocId))
            {
                string[] docIdDeliminator = new string[] { "\n" };
                string[] lstDocId = DocId.Replace("\r", "").Replace("\t", "").Split(docIdDeliminator, StringSplitOptions.RemoveEmptyEntries);

                IList<object> objList = new List<object>();
                objList.Add(new
                {
                    TotalDocIds = lstDocId.Length,
                    DocIdsFound = lstDocId.Length - lstInvalidDocIds.Count,
                    MissingDocIds = lstInvalidDocIds.Count
                });
                // LiveUpdateDocumentIdDetails
                List<LiveUpdateDocumentIdDetails> lstDocIdDetails = new List<LiveUpdateDocumentIdDetails>();
                LiveUpdateDocumentIdDetails objDD = new LiveUpdateDocumentIdDetails();
                System.Reflection.PropertyInfo TD = objList[0].GetType().GetProperty("TotalDocIds");
                objDD.TotalDocIds = (Int32)(TD.GetValue(objList[0], null));

                System.Reflection.PropertyInfo DF = objList[0].GetType().GetProperty("DocIdsFound");
                objDD.DocIdsFound = (Int32)(DF.GetValue(objList[0], null));

                System.Reflection.PropertyInfo MD = objList[0].GetType().GetProperty("MissingDocIds");
                objDD.MissingDocIds = (Int32)(MD.GetValue(objList[0], null));
                lstDocIdDetails.Add(objDD);

                // 5
                objLiveUpdateReport.LiveUpdateDocumentIdDetails = lstDocIdDetails;
                objLiveUpdateReport.Show_divDocIdsCount = true;
            }
            objLiveUpdateReport.TotalCount = bCSDocUpdateApprovalCUSIPData.AllCUSIPDetailsTotalCount;
            return objLiveUpdateReport;
        }

        public string GetLocaltimeString(DateTime utcDate, int offset)
        {
            //Note:  The time-zone offset is the difference, in minutes, between UTC and local time.   i.e  offset = utc - localtime
            TimeSpan interval = TimeSpan.FromMinutes(Convert.ToInt32(offset));
            return (utcDate - interval).ToString();
        }
    }
}
