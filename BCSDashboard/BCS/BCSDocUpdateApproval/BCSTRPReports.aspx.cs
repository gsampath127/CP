using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


namespace BCSDocUpdateApproval
{
    public partial class BCSTRPReports : System.Web.UI.Page
    {
        private const string BCSTRPFLTArchiveDropPath = "BCSTRPFLTArchiveDropPath";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divExceptionLog.Visible = false;
                ((Label)Master.FindControl("lblMasterHeader")).Text = "InLine - First Dollar (FD) Dashboard";

                GridCustomerFLT.Visible = false;
                GridCustomerDocs.Visible = false;
            }
            
        }

        #region FLTFTPInfoData region

        protected void GridFLTFTPInfoDataEntries_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                int startIndex = GridFLTFTPInfoDataEntries.CurrentPageIndex * GridFLTFTPInfoDataEntries.PageSize;
                int endIndex = startIndex + GridFLTFTPInfoDataEntries.PageSize;
                BindToGridFLTFTPInfoDataEntries(false, startIndex, endIndex);
            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }

        private void BindToGridFLTFTPInfoDataEntries(bool isReBindGrid, int startIndex, int endIndex)
        {
            bool? isPDFReceived = null;
            if (comboPDFStatus.SelectedValue == "1")
            {
                isPDFReceived = true;
            }
            else if (comboPDFStatus.SelectedValue == "2")
            {
                isPDFReceived = false;
            }

            BCSTRPReportData bCSTRPReportData = new BCSDocUpdateApprovalFactory().GetBCSTRPReportFLTFTPInfoData(string.IsNullOrWhiteSpace(txtCUSIP.Text) ? null : txtCUSIP.Text.Trim(), isPDFReceived, startIndex, endIndex);
            GridFLTFTPInfoDataEntries.DataSource = bCSTRPReportData.BCSTRPReportFLTFTPInfoData;
            GridFLTFTPInfoDataEntries.VirtualItemCount = bCSTRPReportData.BCSTRPReportFLTFTPInfoDataVirtualCount;

            if (isReBindGrid)
            {
                GridFLTFTPInfoDataEntries.CurrentPageIndex = 0;
                GridFLTFTPInfoDataEntries.DataBind();
            }
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                GridFLTFTPInfoDataEntries.PageSize = 10;
                GridFLTFTPInfoDataEntries.MasterTableView.PageSize = 10;
                int GridFLTFTPInfoDataStartIndex = 0;
                int GridFLTFTPInfoDataEndIndex = GridFLTFTPInfoDataStartIndex + GridFLTFTPInfoDataEntries.PageSize;
                BindToGridFLTFTPInfoDataEntries(true, GridFLTFTPInfoDataStartIndex, GridFLTFTPInfoDataEndIndex);

            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtCUSIP.Text = "";
                comboPDFStatus.Text = "";
                comboPDFStatus.ClearSelection();
                GridFLTFTPInfoDataEntries.PageSize = 10;
                GridFLTFTPInfoDataEntries.MasterTableView.PageSize = 10;
                int GridFLTFTPInfoDataStartIndex = 0;
                int GridFLTFTPInfoDataEndIndex = GridFLTFTPInfoDataStartIndex + GridFLTFTPInfoDataEntries.PageSize;
                BindToGridFLTFTPInfoDataEntries(true, GridFLTFTPInfoDataStartIndex, GridFLTFTPInfoDataEndIndex);

            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }

        #endregion

        protected void BCSTRPReportTabStrip_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.Index == 0)
            {
                GridFLTFTPInfoDataEntries.PageSize = 10;
                GridFLTFTPInfoDataEntries.MasterTableView.PageSize = 10;
                int GridFLTFTPInfoDataStartIndex = 0;
                int GridFLTFTPInfoDataEndIndex = GridFLTFTPInfoDataStartIndex + GridFLTFTPInfoDataEntries.PageSize;
                BindToGridFLTFTPInfoDataEntries(true, GridFLTFTPInfoDataStartIndex, GridFLTFTPInfoDataEndIndex);
            }
            else if (e.Tab.Index == 1)
            {
                GridFLTMissingDataEntries.PageSize = 10;
                GridFLTMissingDataEntries.MasterTableView.PageSize = 10;
                int GridFLTMissingDataStartIndex = 0;
                int GridFLTMissingDataEndIndex = GridFLTMissingDataStartIndex + GridFLTFTPInfoDataEntries.PageSize;
                BindToGridFLTMissingDataEntries(true, GridFLTMissingDataStartIndex, GridFLTMissingDataEndIndex);
            }
            else if (e.Tab.Index == 2)
            {
                GridRPMissingCUSIPDataEntries.PageSize = 10;
                GridRPMissingCUSIPDataEntries.MasterTableView.PageSize = 10;
                int GridRPMissingCUSIPDataStartIndex = 0;
                int GridRPMissingCUSIPDataEndIndex = GridRPMissingCUSIPDataStartIndex + GridRPMissingCUSIPDataEntries.PageSize;
                BindToGridFLTMissingDataEntries(true, GridRPMissingCUSIPDataStartIndex, GridRPMissingCUSIPDataEndIndex);
            }
            else if (e.Tab.Index == 3) // Customer FLT
            {
                btnClearFLT_Click(btnClearFLT, null);
                GridCustomerFLT.Visible = false;
            }
            else if (e.Tab.Index == 4) // Customer Docs
            {
                btnClearREportCustomerDocs_Click(btnClearREportCustomerDocs, null);
                GridCustomerDocs.Visible = false;
            }
            else if (e.Tab.Index == 5) // Blank FLT Cusip details
            {
                GridBlankFltCusipDetails.PageSize = 10;
                GridBlankFltCusipDetails.MasterTableView.PageSize = 10;
                int GridBlankFltCusipDetailsStartIndex = 0;
                int GridBlankFltCusipDetailsEndIndex = GridBlankFltCusipDetailsStartIndex + GridBlankFltCusipDetails.PageSize;
                BindToGridBlankFltCusipDetails(true, GridBlankFltCusipDetailsStartIndex, GridBlankFltCusipDetailsEndIndex);
            }
        }

        #region FLT Missing region


        protected void GridFLTMissingDataEntries_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                int startIndex = GridFLTMissingDataEntries.CurrentPageIndex * GridFLTMissingDataEntries.PageSize;
                int endIndex = startIndex + GridFLTMissingDataEntries.PageSize;

                BindToGridFLTMissingDataEntries(false, startIndex, endIndex);
            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }

        private void BindToGridFLTMissingDataEntries(bool isReBindGrid, int startIndex, int endIndex)
        {
            BCSTRPReportData bCSTRPReportData = new BCSDocUpdateApprovalFactory().GetBCSTRPReportFLTMissingData(startIndex, endIndex);
            GridFLTMissingDataEntries.DataSource = bCSTRPReportData.BCSTRPReportFLTMissingData;
            GridFLTMissingDataEntries.VirtualItemCount = bCSTRPReportData.BCSTRPReportFLTMissingDataVirtualCount;

            if (isReBindGrid)
            {
                GridFLTMissingDataEntries.CurrentPageIndex = 0;
                GridFLTMissingDataEntries.DataBind();
            }
        }
        #endregion


        #region RP CUSIP Missing region


        protected void GridRPMissingCUSIPDataEntries_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                int startIndex = GridRPMissingCUSIPDataEntries.CurrentPageIndex * GridRPMissingCUSIPDataEntries.PageSize;
                int endIndex = startIndex + GridRPMissingCUSIPDataEntries.PageSize;

                BindToGridRPMissingCUSIPDataEntries(false, startIndex, endIndex);
            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }

        private void BindToGridRPMissingCUSIPDataEntries(bool isReBindGrid, int startIndex, int endIndex)
        {
            BCSTRPReportData bCSTRPReportData = new BCSDocUpdateApprovalFactory().GetBCSTRPReportRPMissingCUSIPData(startIndex, endIndex);
            GridRPMissingCUSIPDataEntries.DataSource = bCSTRPReportData.BCSTRPReportRPCUSIPMissingData;
            GridRPMissingCUSIPDataEntries.VirtualItemCount = bCSTRPReportData.BCSTRPReportRPCUSIPMissingDataVirtualCount;

            if (isReBindGrid)
            {
                GridRPMissingCUSIPDataEntries.CurrentPageIndex = 0;
                GridRPMissingCUSIPDataEntries.DataBind();
            }
        }
        #endregion

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("BCSReportDashBoard.aspx");
        }

        #region Customer_FLT
        protected void btnClearFLT_Click(object sender, EventArgs e)
        {
            dateFLT.Clear();
            GridCustomerFLT.Visible = false;
        }

        protected void btnGenerateReportFLT_Click(object sender, EventArgs e)
        {
            BindToGridCustomerFLT(true);
        }

        private void BindToGridCustomerFLT(bool isReBindGrid)
        {
            List<SANFileDetails> FLTFileInfo = new BCSDocUpdateApprovalFactory().GetSANFileDetails(ConfigValues.BCSTRPFLTArchiveDropPath, dateFLT.SelectedDate, "*.txt");
            GridCustomerFLT.DataSource = FLTFileInfo;

            if (isReBindGrid)
            {
                GridCustomerFLT.DataBind();
            }
            GridCustomerFLT.Visible = true;
        }


        protected void GridCustomerFLT_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "download_file")
            {
                DownloadFile(ConfigurationManager.AppSettings.Get(BCSTRPFLTArchiveDropPath) + e.CommandArgument.ToString(), "txt");
            }
        }

        private void DownloadFile(string fileName, string type)
        {
            
            if (fileName != "")
            {
                //string downloadPath = ConfigValues.RPDestinationSANReplace + fileName.Substring(fileName.LastIndexOf("WebDocuments\\")).Replace("WebDocuments\\", string.Empty).Replace(@"\", "/");
                if (type == "txt" && !fileName.Contains(".txt"))
                    fileName = fileName + ".txt";

                string attachment = "attachment; filename=" + Path.GetFileName(fileName);
                Response.Clear();
                Response.AddHeader("content-disposition", attachment);
                if (type == "txt")
                    Response.ContentType = "text/plain";
                Response.WriteFile(fileName);
                Response.End();
            }
        }
               
        #endregion

        #region Customer_Docs
        protected void btnGenerateReportCustomerDoc_Click(object sender, EventArgs e)
        {
            BindToGridCustomerDocs();
        }

        private void BindToGridCustomerDocs()
        {
            List<BCSTRPFLTFileInfo> listData = new List<BCSTRPFLTFileInfo>();
            DateTime selecteddate = Convert.ToDateTime(dateDoc.SelectedDate);
            var month = selecteddate.Month;
            var year = selecteddate.Year;
            var date = selecteddate.Day;
            string doumentPath = ConfigValues.BCSTRPFLTDocArchiveDropPath + year + @"/" + month + @"/" + date + @"/";
            string hrefPath = doumentPath.Replace(ConfigValues.RPSourceURLReplace, ConfigValues.RPDestinationSANReplace).Replace(@"\", "/");
            if (Directory.Exists(doumentPath))
            {
                var filePaths = Directory.EnumerateFiles(doumentPath);
                foreach (string file in filePaths)
                {

                    BCSTRPFLTFileInfo fltDatafiles = new BCSTRPFLTFileInfo();                   
                    fltDatafiles.DirectoryName = hrefPath + Path.GetFileName(file);
                    fltDatafiles.FileName = Path.GetFileNameWithoutExtension(file);
                    FileInfo f = new FileInfo(file);
                    fltDatafiles.DateReceived = f.LastWriteTime.ToString();

                    listData.Add(fltDatafiles);
                }
            }
            GridCustomerDocs.DataSource = listData;
            GridCustomerDocs.DataBind();
            GridCustomerDocs.Visible = true;
        }

        protected void btnClearREportCustomerDocs_Click(object sender, EventArgs e)
        {
            dateDoc.Clear();
            GridCustomerDocs.Visible = false;
        }

        protected void GridCustomerDocs_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "download_file")
            {
                DownloadFile(e.CommandArgument.ToString(), "pdf");
            }

        }
        #endregion


        #region Blank FLT Cusip Details

        protected void GridBlankFltCusipDetails_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                int startIndex = GridBlankFltCusipDetails.CurrentPageIndex * GridBlankFltCusipDetails.PageSize;
                int endIndex = startIndex + GridBlankFltCusipDetails.PageSize;

                BindToGridBlankFltCusipDetails(false, startIndex, endIndex);
            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }

        private void BindToGridBlankFltCusipDetails(bool isReBindGrid, int startIndex, int endIndex)
        {
            BCSTRPReportData bCSTRPReportData = new BCSDocUpdateApprovalFactory().GetBCSTRPReportBlankFLTCUSIPData(startIndex, endIndex);
            GridBlankFltCusipDetails.DataSource = bCSTRPReportData.BCSTRPReportBlankFLTCUSIPData;
            GridBlankFltCusipDetails.VirtualItemCount = bCSTRPReportData.BCSTRPReportBlankFLTCUSIPDataVirtualCount;

            if (isReBindGrid)
            {
                GridBlankFltCusipDetails.CurrentPageIndex = 0;
                GridBlankFltCusipDetails.DataBind();
            }
        }

        #endregion


    }
}