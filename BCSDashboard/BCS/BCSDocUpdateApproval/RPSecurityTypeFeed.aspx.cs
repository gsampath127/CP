using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BCSDocUpdateApproval
{
    public partial class RPSecurityTypeFeed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "OnCloseWindow")
            {
                int startIndex = 0;
                GridMissingReports.CurrentPageIndex = 0;
                int endIndex = startIndex + GridMissingReports.PageSize;
                BindToGridMissingReports(true, startIndex, endIndex);
            }
            else
            {
                if (!IsPostBack)
                {
                    RPSecurityTypeFeedTabStrip.Tabs.FindTabByValue("MissingReport").Selected = true;
                    pvMissingReport.Selected = true;
                    ClearSelection();
                }

                dvReports.Visible = true;
                ((Label)Master.FindControl("lblMasterHeader")).Text = "Internal Ops Process";
                lnkHome.Visible = true;



                switch (RPSecurityTypeFeedTabStrip.SelectedTab.Value)
                {
                    case "DailyUpdateReport":
                        GridDailyUpdate.Visible = false;
                        GridMissingReports.Visible = false;
                        GridSecurityType.Visible = false;
                        GridEdgarData.Visible = false;
                        break;
                    case "MissingReport":
                        GridDailyUpdate.Visible = false;
                        GridMissingReports.Visible = false;
                        GridSecurityType.Visible = false;
                        GridEdgarData.Visible = false;
                        break;
                    case "SecurityTypes":
                        GridDailyUpdate.Visible = false;
                        GridMissingReports.Visible = false;
                        GridSecurityType.Visible = true;
                        divCUSIPCount.Visible = true;
                        GridEdgarData.Visible = false;
                        break;
                    case "EdgarOnline":
                        GridDailyUpdate.Visible = false;
                        GridMissingReports.Visible = false;
                        GridSecurityType.Visible = false;
                        GridEdgarOnline.Visible = false;
                        GridEdgarData.Visible = false;
                        break;
                    case "EdgarOnlineData":
                        GridDailyUpdate.Visible = false;
                        GridMissingReports.Visible = false;
                        GridSecurityType.Visible = false;
                        GridEdgarOnline.Visible = false;
                        GridEdgarData.Visible = true;
                        break;
                }
            }
        }


        protected void RPSecurityTypeFeedTabStrip_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Index)
            {

                case 0:
                    {

                        dailyUpdateDate.Clear();
                        GridDailyUpdate.Visible = false;

                        break;
                    }
                case 1:
                    {
                        ClearSelection();
                        break;
                    }
                case 3:
                    {

                        //int sIndex = GridSecurityType.CurrentPageIndex * GridSecurityType.PageSize;
                        //int eIndex = sIndex + GridSecurityType.PageSize;
                        LoadSecurityTypeCombo();
                        LoadSummmarizedSecuritYType();
                        //BindToGridSecurityType(true, sIndex, eIndex);
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("BCSReportDashBoard.aspx");
        }



        #region Daily Update Report
        protected void btnGenerateDailyUpdate_Click(object sender, EventArgs e)
        {
            // GridDailyUpdate.Visible = true;
            int startIndex = GridSecurityType.CurrentPageIndex * GridSecurityType.PageSize;
            int endIndex = startIndex + GridSecurityType.PageSize;
            BindToGridDailyUpdateReport(true, startIndex, endIndex);

        }

        private void BindToGridDailyUpdateReport(bool isReBindGrid, int startIndex, int endIndex, string sortField = null, string sortOrder = null)
        {
            GridDailyUpdate.Visible = false;
            List<BCSDailyUpdateReport> dailyUpdateData = new List<BCSDailyUpdateReport>();
            int virtualcount = 0;
            if (sortField == null && sortOrder == null)
            {
                sortField = "CUSIP";
                sortOrder = "Asc";
            }
            if (dailyUpdateDate.SelectedDate != null)
            {
                GridColumnCollection dailyUpdateReportColumns = GridDailyUpdate.Columns;
                dailyUpdateReportColumns.FindByUniqueName("CUSIP").HeaderText = "rpCUSIP";
                dailyUpdateReportColumns.FindByUniqueName("CompanyName").HeaderText = "rpCompanyName";
                dailyUpdateReportColumns.FindByUniqueName("FundName").HeaderText = "rpFundName";
                dailyUpdateReportColumns.FindByUniqueName("CompanyCIK").HeaderText = "rpCIK";
                dailyUpdateReportColumns.FindByUniqueName("SeriesID").HeaderText = "rpSeries";
                dailyUpdateReportColumns.FindByUniqueName("Class").HeaderText = "rpClassId";
                dailyUpdateReportColumns.FindByUniqueName("Ticker").HeaderText = "rpTicker";
                dailyUpdateReportColumns.FindByUniqueName("OldSecurityType").HeaderText = "OldSecurityType";
                dailyUpdateReportColumns.FindByUniqueName("SecurityType").HeaderText = "SecurityType";
                dailyUpdateData = new RPSecurityTypeFeedFactory().GetDailyUpdateReport(Convert.ToDateTime(dailyUpdateDate.SelectedDate), sortField, sortOrder, startIndex, endIndex, out virtualcount);
                GridDailyUpdate.DataSource = dailyUpdateData;
                GridDailyUpdate.VirtualItemCount = virtualcount;
                GridDailyUpdate.Visible = true;
            }
            else
            {
                GridDailyUpdate.DataSource = null;
                GridDailyUpdate.Visible = false;
            }
            if (isReBindGrid)
            {
                GridDailyUpdate.CurrentPageIndex = 0;
                GridDailyUpdate.DataBind();
            }
        }

        protected void btnDailyUpdateClear_Click(object sender, EventArgs e)
        {
            dailyUpdateDate.Clear();
            GridDailyUpdate.Visible = false;
        }

        protected void GridDailyUpdate_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var sort = GridDailyUpdate.MasterTableView.SortExpressions;
            int startIndex = GridDailyUpdate.CurrentPageIndex * GridDailyUpdate.PageSize;
            int endIndex = startIndex + GridDailyUpdate.PageSize;
            if (sort.Count > 0)
            {
                string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "ASC";
                sortOrder = sortOrder.Contains("Descending") ? "DESC" : "ASC";
                BindToGridDailyUpdateReport(false, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CUSIP", sortOrder);
            }
            else
                BindToGridDailyUpdateReport(false, startIndex, endIndex);

        }

        protected void GridDailyUpdate_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            var sort = GridDailyUpdate.MasterTableView.SortExpressions;
            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "ASC";
            sortOrder = sortOrder.Contains("Descending") ? "DESC" : "ASC";
            int pageIndex = e.NewPageIndex;
            int startIndex = pageIndex * GridDailyUpdate.PageSize;
            int endIndex = startIndex + GridDailyUpdate.PageSize;
            BindToGridDailyUpdateReport(false, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CUSIP", sortOrder);
        }

        protected void GridDailyUpdate_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            var sort = GridDailyUpdate.MasterTableView.SortExpressions;
            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "ASC";
            sortOrder = sortOrder.Contains("Descending") ? "DESC" : "ASC";
            int pageSize = e.NewPageSize;
            int startIndex = GridDailyUpdate.CurrentPageIndex * pageSize;
            int endIndex = startIndex + pageSize;
            BindToGridDailyUpdateReport(false, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CUSIP", sortOrder);

        }

        #endregion

        #region Missing Reports

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            int startIndex = 0;
            GridMissingReports.CurrentPageIndex = 0;
            int endIndex = startIndex + GridMissingReports.PageSize;
            BindToGridMissingReports(true, startIndex, endIndex);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }

        protected void GridMissingReports_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var sort = GridMissingReports.MasterTableView.SortExpressions;
            if (sort.Count > 0)
            {
                string sortOrder = sort[0].SortOrder.ToString();
                sortOrder = sortOrder.Contains("Descending") ? "Desc" : "Asc";
                int startIndex = GridMissingReports.CurrentPageIndex * GridMissingReports.PageSize;
                int endIndex = startIndex + GridMissingReports.PageSize;
                BindToGridMissingReports(false, startIndex, endIndex, sort[0].FieldName, sortOrder);
            }
        }

        protected void GridMissingReports_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            var sort = GridMissingReports.MasterTableView.SortExpressions;
            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "Asc";
            sortOrder = sortOrder.Contains("Descending") ? "Desc" : "Asc";
            int pageIndex = e.NewPageIndex;
            int startIndex = pageIndex * GridMissingReports.PageSize;// *GridMissingReports.PageSize;
            int endIndex = startIndex + GridMissingReports.PageSize;
            BindToGridMissingReports(false, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CUSIP", sortOrder);
        }

        protected void GridMissingReports_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            var sort = GridMissingReports.MasterTableView.SortExpressions;
            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "Asc";
            sortOrder = sortOrder.Contains("Descending") ? "Desc" : "Asc";
            int pageSize = e.NewPageSize;
            int startIndex = GridMissingReports.CurrentPageIndex * pageSize;
            int endIndex = startIndex + pageSize;
            BindToGridMissingReports(false, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CUSIP", sortOrder);

        }

        protected void GridMissingReports_ItemDataBound(object sender, GridItemEventArgs e)
        {
            GridColumnCollection missingReportColumns = GridMissingReports.Columns;
            GridTemplateColumn templateColumn = new GridTemplateColumn();
        }

        private void BindToGridMissingReports(bool isReBindGrid, int startindex, int endindex, string sortField = null, string sortOrder = null)
        {
            GridMissingReports.Visible = true;
            List<BCSMissingReports> bCSMissingReports = new List<BCSMissingReports>();
            int virtualcount = 0;
            var reportType = cmbReportType.SelectedValue;
            if (sortField == null && sortOrder == null)
            {
                sortField = "CUSIP";
                sortOrder = "Asc";
            }
            if (!string.IsNullOrEmpty(reportType))
            {
                GridColumnCollection missingReportColumns = GridMissingReports.Columns;
                if (cmbReportType.SelectedValue == "CUSIP")
                {
                    missingReportColumns.FindByUniqueName("CUSIP").HeaderText = "eoCUSIP";
                    missingReportColumns.FindByUniqueName("CompanyName").HeaderText = "eoCompanyName";
                    missingReportColumns.FindByUniqueName("FundName").HeaderText = "eoFundName";
                    missingReportColumns.FindByUniqueName("CIK").HeaderText = "eoCIK";
                    missingReportColumns.FindByUniqueName("SeriesID").HeaderText = "eoSeries#";
                    missingReportColumns.FindByUniqueName("ClassContractID").HeaderText = "eoClass#";
                    missingReportColumns.FindByUniqueName("Ticker").HeaderText = "eoTicker";

                    MissingCUSIPNote.InnerText = "Note: This page displays missing CUSIP(s) in RightProspectus.";
                }
                else if (cmbReportType.SelectedValue == "Security Types")
                {
                    missingReportColumns.FindByUniqueName("CUSIP").HeaderText = "rpCUSIP";
                    missingReportColumns.FindByUniqueName("CompanyName").HeaderText = "rpCompanyName";
                    missingReportColumns.FindByUniqueName("FundName").HeaderText = "rpFundName";
                    missingReportColumns.FindByUniqueName("CIK").HeaderText = "rpCIK";
                    missingReportColumns.FindByUniqueName("SeriesID").HeaderText = "rpSeriesId";
                    missingReportColumns.FindByUniqueName("ClassContractID").HeaderText = "rpClassId";
                    missingReportColumns.FindByUniqueName("Ticker").HeaderText = "rpTicker";

                    MissingCUSIPNote.InnerText = "Note: This page displays missing Security Type(s) in RightProspectus.";
                }
                bCSMissingReports = new RPSecurityTypeDashboardFactory().GetMissingReports(reportType, startindex, endindex, sortField, sortOrder, out virtualcount);
                GridMissingReports.DataSource = bCSMissingReports;
                GridMissingReports.VirtualItemCount = virtualcount;
                GridMissingReports.Visible = true;
            }
            else
            {
                GridMissingReports.DataSource = null;
                GridMissingReports.Visible = false;
            }
            if (isReBindGrid)
            {
                GridMissingReports.CurrentPageIndex = 0;
                GridMissingReports.DataBind();
            }

        }

        private void ClearSelection()
        {
            cmbReportType.ClearSelection();
            GridMissingReports.DataSource = null;
            GridMissingReports.Visible = false;
        }

        #endregion

        #region Security Types

        public void LoadSecurityTypeCombo()
        {

            var details = new RPSecurityTypeDashboardFactory().GetCompanyandSecurityType();

            cmbCompany.DataSource = details.Tables[0];
            cmbCompany.DataValueField = "CompanyID";
            cmbCompany.DataTextField = "CompanyName";
            cmbCompany.DataBind();

            cmbSecurityType.DataSource = details.Tables[1];
            cmbSecurityType.DataValueField = "SecurityTypeID";
            cmbSecurityType.DataTextField = "SecurityTypeCode";
            cmbSecurityType.DataBind();
        }

        protected void btnGenerateSecurityType_Click(object sender, EventArgs e)
        {
            int startIndex = GridSecurityType.CurrentPageIndex * GridSecurityType.PageSize;
            int endIndex = startIndex + GridSecurityType.PageSize;
            BindToGridSecurityType(true, startIndex, endIndex);

        }

        protected void btnSecurityTypeClear_Click(object sender, EventArgs e)
        {
            cmbCompany.Text = "";
            cmbCompany.ClearSelection();
            cmbSecurityType.Text = "";
            cmbSecurityType.ClearSelection();
            GridSecurityType.Visible = false;
            divCUSIPCount.Visible = false;

        }

        protected void GridSecurityType_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int sIndex = GridSecurityType.CurrentPageIndex * GridSecurityType.PageSize;
            int eIndex = sIndex + GridSecurityType.PageSize;
            var sort = GridSecurityType.MasterTableView.SortExpressions;

            if (sort.Count > 0)
            {
                string sortOrder = sort[0].SortOrder.ToString();
                sortOrder = sortOrder.Contains("Descending") ? "DESC" : "ASC";
                BindToGridSecurityType(false, sIndex, eIndex, sort[0].FieldName, sortOrder);
            }
            else
                BindToGridSecurityType(false, sIndex, eIndex);


        }

        protected void GridSecurityType_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            var sort = GridSecurityType.MasterTableView.SortExpressions;
            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "ASC";
            sortOrder = sortOrder.Contains("Descending") ? "DESC" : "ASC";

            int pageIndex = e.NewPageIndex;
            int startIndex = pageIndex * GridSecurityType.PageSize;
            int endIndex = startIndex + GridSecurityType.PageSize;

            BindToGridSecurityType(false, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CUSIP", sortOrder);

        }

        protected void GridSecurityType_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            var sort = GridSecurityType.MasterTableView.SortExpressions;
            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "ASC";
            sortOrder = sortOrder.Contains("Descending") ? "DESC" : "ASC";

            int pageSize = e.NewPageSize;
            int startIndex = GridSecurityType.CurrentPageIndex * pageSize;
            int endIndex = startIndex + pageSize;

            BindToGridSecurityType(false, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CUSIP", sortOrder);
        }

        private void BindToGridSecurityType(bool isReBindGrid, int startIndex, int endIndex, string sortColumn = null, string sortDirection = null)
        {
            GridSecurityType.Visible = true;
            int? selectedCompanyId = 0, selectedSecurityType = 0; int total = 0;
            List<BCSecurityType> securityTypeDetails = new List<BCSecurityType>();
            if (string.IsNullOrEmpty(cmbCompany.SelectedValue))
                selectedCompanyId = null;
            else
                selectedCompanyId = Convert.ToInt32(cmbCompany.SelectedValue);
            if (string.IsNullOrEmpty(cmbSecurityType.SelectedValue))
                selectedSecurityType = null;
            else
                selectedSecurityType = Convert.ToInt32(cmbSecurityType.SelectedValue);


            if (sortColumn == null || sortDirection == null)
                securityTypeDetails = new RPSecurityTypeDashboardFactory().GetSecurityTypeDetails(txtCusip.Text.ToString().Trim(), selectedCompanyId, selectedSecurityType, "CUSIP", "ASC", startIndex, endIndex, out total);
            else
                securityTypeDetails = new RPSecurityTypeDashboardFactory().GetSecurityTypeDetails(txtCusip.Text.ToString().Trim(), selectedCompanyId, selectedSecurityType, sortColumn, sortDirection, startIndex, endIndex, out total);



            GridSecurityType.DataSource = securityTypeDetails;
            GridSecurityType.VirtualItemCount = total;

            if (isReBindGrid)
            {
                GridSecurityType.CurrentPageIndex = 0;
                GridSecurityType.DataBind();
            }



        }

        private void LoadSummmarizedSecuritYType()
        {
            GridSummarizedSecurityType.DataSource = new RPSecurityTypeDashboardFactory().GetSummarizedData();
            GridSummarizedSecurityType.DataBind();

        }
        #endregion

        #region EdgarOnline

        protected void btnGenerateEdgarOnline_Click(object sender, EventArgs e)
        {
            BindToGridEdgarOnlineReport();
        }

        private void BindToGridEdgarOnlineReport()
        {
            GridEdgarOnline.Visible = false;

            if (EdgarOnlineDatePicker.SelectedDate != null)
            {
                RPSecurityTypeDashboardFactory obj = new RPSecurityTypeDashboardFactory();

                GridEdgarOnline.DataSource = obj.GetEdgarOnlineFeedHistory(Convert.ToDateTime(EdgarOnlineDatePicker.SelectedDate));

                GridEdgarOnline.Visible = true;
                GridEdgarOnline.DataBind();
            }
            else
            {
                GridEdgarOnline.DataSource = null;
                GridEdgarOnline.Visible = false;
            }

        }



        protected void btnEdgarOnline_Click(object sender, EventArgs e)
        {
            EdgarOnlineDatePicker.Clear();
            GridEdgarOnline.Visible = false;
        }

        protected void GridEdgarOnline_ItemCommand(object sender, GridCommandEventArgs e)
        {

            if (e.CommandName == "download_file")
            {
                string directory = ConfigValues.EdgarOnlineFilePath;
                string filename = e.CommandArgument.ToString();
                DownloadFile(Path.Combine(directory, filename));
            }
        }
        private void DownloadFile(string fileLocation)
        {

            string attachment = "attachment; filename=" + Path.GetFileName(fileLocation);
            Response.Clear();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/x-zip-compressed";

            Response.WriteFile(fileLocation);
            Response.End();
        }

        protected void btnClearEdgarOnlineData_Click(object sender, EventArgs e)
        {
            txtEdgarCUSIP.Text = "";
            txtEdgarCIK.Text = "";
            txtEdgarSeriesID.Text = "";
            txtEdgarClass.Text = "";
            txtEdgarTicker.Text = "";

            GridEdgarData.Visible = false;

        }

        protected void btnGenerateEdgarOnlineData_Click(object sender, EventArgs e)
        {
            int sIndex = GridEdgarData.CurrentPageIndex * GridSecurityType.PageSize;
            int eIndex = sIndex + GridEdgarData.PageSize;
           
            BindToGridEdgarDataReport(true, sIndex, eIndex,txtEdgarCUSIP.Text.ToString(), txtEdgarCIK.Text.ToString(), txtEdgarSeriesID.Text.ToString(), txtEdgarClass.Text.ToString(), txtEdgarTicker.Text.ToString());
        }

        private void BindToGridEdgarDataReport(bool isReBindGrid, int sIndex,int eIndex,string CUSIP=null,string CIK=null,string SeriesId=null,string Class=null,string Ticker=null)
        {

            sIndex = GridEdgarData.CurrentPageIndex * GridSecurityType.PageSize;
            eIndex = sIndex + GridEdgarData.PageSize;
            int total = 0;
            RPSecurityTypeDashboardFactory obj = new RPSecurityTypeDashboardFactory();
            GridEdgarData.DataSource = obj.GetBCSEdgarOnlineData(txtEdgarCUSIP.Text.ToString(), txtEdgarCIK.Text.ToString(), txtEdgarSeriesID.Text.ToString(), txtEdgarClass.Text.ToString(), txtEdgarTicker.Text.ToString(), sIndex, eIndex, out total);

            GridEdgarData.Visible = true;
            GridEdgarData.VirtualItemCount = total;            

            if (isReBindGrid)
            {
                GridEdgarData.CurrentPageIndex = 0;
                GridEdgarData.DataBind();
            }
            
        }
        #endregion

        protected void GridEdgarData_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {


            int pageIndex = e.NewPageIndex;
            int sIndex = pageIndex * GridEdgarData.PageSize;
            int eIndex = sIndex + GridEdgarData.PageSize;

            BindToGridEdgarDataReport(false, sIndex, eIndex, txtEdgarCUSIP.Text.ToString(), txtEdgarCIK.Text.ToString(), txtEdgarSeriesID.Text.ToString(), txtEdgarClass.Text.ToString(), txtEdgarTicker.Text.ToString());

        }

        protected void GridEdgarData_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {


            int pageSize = e.NewPageSize;
            int sIndex = GridEdgarData.CurrentPageIndex * pageSize;
            int eIndex = sIndex + pageSize;

            BindToGridEdgarDataReport(false, sIndex, eIndex, txtEdgarCUSIP.Text.ToString(), txtEdgarCIK.Text.ToString(), txtEdgarSeriesID.Text.ToString(), txtEdgarClass.Text.ToString(), txtEdgarTicker.Text.ToString());
        }

        protected void GridEdgarData_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int sIndex = GridEdgarData.CurrentPageIndex * GridSecurityType.PageSize;
            int eIndex = sIndex + GridEdgarData.PageSize;

            BindToGridEdgarDataReport(false, sIndex, eIndex, txtEdgarCUSIP.Text.ToString(), txtEdgarCIK.Text.ToString(), txtEdgarSeriesID.Text.ToString(), txtEdgarClass.Text.ToString(), txtEdgarTicker.Text.ToString());
        }

    }

}






