using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using System.Configuration;
using Telerik.Web.UI;
using System.Xml.Linq;

namespace BCSDocUpdateApproval
{
    public partial class BCSReportDashBoard : System.Web.UI.Page
    {
        private const string watchListTransamericaDocumentPath = "watchListTransamericaDocumentPath";
        private const string filteredIpTransamericaDocumentPath = "filteredIpTransamericaDocumentPath";
        private const string watchListAllianceBernsteinDocumentPath = "watchListAllianceBernsteinDocumentPath";
        private const string filteredIpAllianceBernsteinDocumentPath = "filteredIpAllianceBernsteinDocumentPath";
        private const string docUpdateDocumentPath = "docUpdateDocumentPath";        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                dvReports.Visible = false;
                dvClientSelection.Visible = true;
                ClearClientSelection();
                BindClients();
                BindReports();
                ((Label)Master.FindControl("lblMasterHeader")).Text = "RightProspectus - DASHBOARD";
                lnkHome.Visible = false;

                GridCUSIPReports.Visible = false;
                GridWatchListEntries.Visible = false;
                GridfilteredIPEntries.Visible = false;
                GriddocUpdateDownStreamEntries.Visible = false;
                GriddocUpdateDownStreamEntriesHeader.Visible = false;
                GridDailyUpdateHeader.Visible = false;
                GridDailyUpdateDetail.Visible = false;
                GridSlinkReports.Visible = false;
                GridSlinkReportsCount.Visible = false;
                GridFullfillmentInfo.Visible = false;
                GridFullfillmentInfoCount.Visible = false;
            }
        }

        public void ClearClientSelection()
        {
            comboClient.ClearSelection();
            comboClient.Text = string.Empty;
        }

        public void BindClients()
        {
            List<BCSClient> bcsClient = new ServiceFactory().GetALLClientConfig().FindAll(x => x.ShowClientInDashboard);
            comboClient.DataSource = bcsClient;
            comboClient.DataTextField = "ClientName";
            comboClient.DataValueField = "ClientName";
            comboClient.DataBind();
            comboClient.SelectedIndex = -1;
            comboClient.Text = string.Empty;
        }
        public void BindReports()
        {

            comboReport.DataSource = UtilityFactory.GetEnumDescriptionList(typeof(BCSReportSelect));
            comboReport.SelectedIndex = -1;
            comboReport.DataBind();
            
        }
        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            Session["ClientName"] = comboClient.SelectedItem.Text;

            switch (comboClient.SelectedItem.Text)
            {
                case "GIM":
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CUSIPReport").Visible = false;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("WatchList").Visible = false;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CustomerDocUpdate").Visible = false;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CustomerDocUpdateDetails").Visible = false;

                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("GatewayDocUpdate").Selected = true;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("LiveUpdate").Visible = true;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("Fullfillment").Visible = true;
                    LoadGatewayDocUpdateCombo();
                    
                    pvdocUpdateDownStream.Selected = true;

                    break;
                case "GMS":
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CUSIPReport").Visible = false;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("WatchList").Visible = false;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CustomerDocUpdate").Visible = false;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CustomerDocUpdateDetails").Visible = false;

                    
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("LiveUpdate").Visible = true;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("Fullfillment").Visible = true;
                    LoadGatewayDocUpdateCombo();

                    LoadCustomerDocUpdateCombo();
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CustomerDocUpdate").Visible = true;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CustomerDocUpdate").Selected = true;                    
                    pvfilteredIP.Selected = true;

                    divCustomerDocUPDT_FilterIP.Visible = false;
                    GridCustomerDocUpdateEntries.Visible = false;
                    GridCustomerDocUpdateHeader.Visible = false;
                    break;
                case "TRP":
                    Response.Redirect("BCSTRPReports.aspx");
                    break;
                default:

                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CUSIPReport").Visible = true;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("WatchList").Visible = true;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CustomerDocUpdate").Visible = true;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CustomerDocUpdateDetails").Visible = true;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("LiveUpdate").Visible = true;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("SLINKReport").Visible = true;

                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("CUSIPReport").Selected = true;
                    BCSReportDashboardTabStrip.Tabs.FindTabByValue("Fullfillment").Visible = true;
                    ClearCusipReport();
                    pvCusipReports.Selected = true;
                    LoadCusipReportTypes();
                    LoadGatewayDocUpdateCombo();
                    break;
            }



            dvReports.Visible = true;
            dvClientSelection.Visible = false;

            lnkHome.Visible = true;
            SetClientName(Session["ClientName"].ToString());
        }
        protected void btnGenerateReportSelection_Click(object sender, EventArgs e)
        {
            switch (comboReport.SelectedItem.Text)
            {
                case "Duplicate CUSIP Report":
                    Response.Redirect("BCSRemoveDuplicateCUSIP.aspx");
                    break;
                case "Security Type Report":
                     Response.Redirect("RPSecurityTypeFeed.aspx");
                     break;
                case "Missing CUSIP(s) In RP":
                     Response.Redirect("RPCUSIPMissingReport.aspx");
                     break;
                    
            }
        }
        private void SetClientName(string clientName)
        {
            lblCusipClientName.Text = clientName + ": CUSIP - Report";
            lblWatchListClientName.Text = clientName + ": WatchList - Report";
            lblFilteredIPClientName.Text = lblCustomerDocUPDT_IPNUClientName.Text =  clientName + ": Customer - Doc Update";
            lblDocUpdateClientName.Text = clientName + ": Gateway - Doc Update";
            lblDailyUpdateClientName.Text = clientName + ": Customer  - Doc Update (Details)";
            lblSlinkClentName.Text = clientName + ": SLINK - Report";
            lblLiveUpdateClientName.Text = clientName + " : Live Update";
            lblFullfillmentClientName.Text = clientName + " : Fullfillment Info";

        }

        public void LoadCusipReportTypes()
        {

            comboReportType.DataSource = UtilityFactory.GetEnumDescriptionList(typeof(BCSReportType));
            comboReportType.SelectedIndex = 0;
            comboReportType.DataBind();
        }

        public void LoadSlinkStatus()
        {
            List<string> lstStatus = new List<string>();
            lstStatus.Add("--Select Status--");
            UtilityFactory.GetEnumDescriptionList(typeof(SlinkStatus)).ForEach(x =>
                {
                    lstStatus.Add(x);
                });
            comboStatus.DataSource = lstStatus;
            comboStatus.SelectedIndex = 0;
            comboStatus.DataBind();
        }

        protected void BCSReportDashboardTabStrip_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
        {
            switch (e.Tab.Index)
            {
                case 0: // Cusip reports
                    int startIndex = GridCUSIPReports.CurrentPageIndex * GridCUSIPReports.PageSize;
                    int endIndex = startIndex + GridCUSIPReports.PageSize;
                    BindToGridCUSIPReports(true, startIndex, endIndex);

                    break;
                case 1:  // WatchList
                    LoadWatchListgrid();
                    break;
                case 2:   // Filtered IP file
                    LoadFilteredIPGrid();
                    break;
                case 3:  // Doc Update DownStream                    
                    LoadDocUpdateDownStreamGrid();
                    break;

                case 4:   // Daily Update                    
                    BindDailyUpdateReport();
                    GridDailyUpdateHeader.DataBind();
                    GridDailyUpdateDetail.DataBind();
                    break;
                case 5:        // Slink Report           
                    //ClearSlinkReport();
                    //BindSlinkGrid(true);
                    LoadSlinkStatus();
                    break;
                case 6: // Live Update   
                    {
                        //ClearLiveUpdate();

                        int sIndex = GridLiveUpdate.CurrentPageIndex * GridLiveUpdate.PageSize;
                        int eIndex = sIndex + GridLiveUpdate.PageSize;

                        Session["ClientName"] = comboClient.SelectedItem.Text;

                        switch (comboClient.SelectedItem.Text)
                        {
                            case "GIM":
                            case "GMS":
                                BindToGridLiveUpdateForGIM(false, sIndex, eIndex);
                                break;                            
                            case "Transamerica":
                                BindToGridLiveUpdateForCustomer(false, sIndex, eIndex);
                                break;
                            case "AllianceBernstein":
                                BindToGridLiveUpdateForCustomer(false, sIndex, eIndex);
                                break;
                        }
                        LoadLiveUpdateStatus();
                        break;
                    }
                case 7:  // Fullfillment
                    BindToGridFullfillment();
                    break;
                default:
                    break;
            }
        }

        protected void btnGenerateCusipRpt_Click(object sender, EventArgs e)
        {
            int startIndex = GridCUSIPReports.CurrentPageIndex * GridCUSIPReports.PageSize;
            int endIndex = startIndex + GridCUSIPReports.PageSize;
            BindToGridCUSIPReports(true, startIndex, endIndex);
        }

        protected void btnClearCusipReport_Click(object sender, EventArgs e)
        {
            ClearCusipReport();
        }

        private void ClearCusipReport()
        {
            comboReportType.ClearSelection();
            startDate.Clear();
            endDate.Clear();
            GridCUSIPReports.Visible = false;
            trDateRange.Visible = true;
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            dvReports.Visible = false;
            dvClientSelection.Visible = true;
            ClearClientSelection();
            BindClients();
            Session.Clear();
            lnkHome.Visible = false;

            ClearCusipReport();
            clearWatchList_Click(clearWatchList, null);
            clearfilteredIP_Click(clearfilteredIP, null);
            cleardocUpdateDownStream_Click(cleardocUpdateDownStream, null);
            btnClearDailyUpdate_Click(btnClearDailyUpdate, null);
            btnClearSlinkReport_Click(btnClearSlinkReport, null);
            btnLiveUpdateClear_Click(btnLiveUpdateClear, null);
            btnClearFullfillment_Click(btnClearFullfillment, null);
        }

        protected void GridCUSIPReports_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var sort = GridCUSIPReports.MasterTableView.SortExpressions;
            string sortOrder = sort[0].SortOrder.ToString();
            sortOrder = sortOrder.Contains("Descending") ? "Desc" : "Asc";
            int startIndex = GridCUSIPReports.CurrentPageIndex * GridCUSIPReports.PageSize;
            int endIndex = startIndex + GridCUSIPReports.PageSize;
            BindToGridCUSIPReports(false, startIndex, endIndex, sort[0].FieldName, sortOrder);

        }

        private void BindToGridCUSIPReports(bool isReBindGrid, int startindex, int endindex, string sortField = null, string sortOrder = null)
        {
            GridCUSIPReports.Visible = false;
            List<BCSReports> bCSCusipReports = new List<BCSReports>();
            int virtualcount = 0;
            var clientName = Session["ClientName"].ToString();
            var reportType = comboReportType.SelectedValue;

            if (sortField == null && sortOrder == null)
            {
                sortField = "CUSIP_WL";
                sortOrder = "Asc";
            }
            if (!string.IsNullOrEmpty(reportType))
            {
                bCSCusipReports = new BCSDocUpdateApprovalFactory().GetBCSReports(clientName, reportType, startDate.SelectedDate, endDate.SelectedDate, startindex, endindex, sortField, sortOrder, out virtualcount);

                GridCUSIPReports.Visible = true;

                GridCUSIPReports.VirtualItemCount = virtualcount;
                GridCUSIPReports.DataSource = bCSCusipReports;
            }
            else
            {
                GridCUSIPReports.Visible = false;
                GridCUSIPReports.DataSource = null;
            }

            if (isReBindGrid)
            {
                GridCUSIPReports.CurrentPageIndex = 0;
                GridCUSIPReports.DataBind();
            }
        }

        protected void GridCUSIPReports_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {

            var sort = GridCUSIPReports.MasterTableView.SortExpressions;
            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "Asc";
            sortOrder = sortOrder.Contains("Descending") ? "Desc" : "Asc";

            int pageIndex = e.NewPageIndex;
            int startIndex = pageIndex * GridCUSIPReports.PageSize;// *GridCUSIPReports.PageSize;
            int endIndex = startIndex + GridCUSIPReports.PageSize;
            BindToGridCUSIPReports(false, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CUSIP_WL", sortOrder);



        }

        protected void GridCUSIPReports_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            var sort = GridCUSIPReports.MasterTableView.SortExpressions;
            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "Asc";
            sortOrder = sortOrder.Contains("Descending") ? "Desc" : "Asc";
            int pageSize = e.NewPageSize;

            int startIndex = GridCUSIPReports.CurrentPageIndex * pageSize;
            int endIndex = startIndex + pageSize;
            BindToGridCUSIPReports(false, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CUSIP_WL", sortOrder);

        }

        private void DownloadFile(string fileLocation)
        {

            string attachment = "attachment; filename=" + Path.GetFileName(fileLocation);
            Response.Clear();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "text/plain";
            Response.WriteFile(fileLocation);
            Response.End();
        }

        protected void GridWatchListEntries_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "download_file")
            {
                DownloadFile(e.CommandArgument.ToString());
            }
        }



        protected void genearateWatchList_Click(object sender, EventArgs e)
        {
            LoadWatchListgrid();
        }

        protected void clearWatchList_Click(object sender, EventArgs e)
        {
            watchListDate.Clear();
            GridWatchListEntries.Visible = false;
            GridCUSIPReports.Visible = false;

        }

        protected void GridCUSIPReports_ItemDataBound(object sender, GridItemEventArgs e)
        {
            var reportType = comboReportType.SelectedValue;
            switch (comboReportType.SelectedValue)
            {
                case "Difference Report":
                    {
                        if (e.Item is GridHeaderItem && e.Item.OwnerTableView.Name == "TabViewCusip")
                        {
                            GridTableView detailTable = (GridTableView)e.Item.OwnerTableView;
                            detailTable.GetColumn("Status").Display = true;

                            detailTable.GetColumn("Class_RP").Display = true;
                            detailTable.GetColumn("CUSIP_RP").Display = true;
                            detailTable.GetColumn("FundName_RP").Display = true;
                        }
                    }
                    break;
                case "Added Report":
                    {
                        if (e.Item is GridHeaderItem && e.Item.OwnerTableView.Name == "TabViewCusip")
                        {
                            GridTableView detailTable = (GridTableView)e.Item.OwnerTableView;
                            detailTable.GetColumn("Status").Display = false;

                            detailTable.GetColumn("Class_RP").Display = true;
                            detailTable.GetColumn("CUSIP_RP").Display = true;
                            detailTable.GetColumn("FundName_RP").Display = true;
                        }
                    }
                    break;
                case "Removed Report":
                    {
                        if (e.Item is GridHeaderItem && e.Item.OwnerTableView.Name == "TabViewCusip")
                        {
                            GridTableView detailTable = (GridTableView)e.Item.OwnerTableView;
                            detailTable.GetColumn("Status").Display = false;

                            detailTable.GetColumn("Class_RP").Display = true;
                            detailTable.GetColumn("CUSIP_RP").Display = true;
                            detailTable.GetColumn("FundName_RP").Display = true;
                        }
                    }
                    break;
                case "CUSIP Not Present in RP":
                    {
                        if (e.Item is GridHeaderItem && e.Item.OwnerTableView.Name == "TabViewCusip")
                        {
                            GridTableView detailTable = (GridTableView)e.Item.OwnerTableView;
                            detailTable.GetColumn("Status").Display = false;
                            detailTable.GetColumn("DateModified").Display = false;
                            detailTable.GetColumn("Class_RP").Display = false;
                            detailTable.GetColumn("CUSIP_RP").Display = false;
                            detailTable.GetColumn("FundName_RP").Display = false;
                        }

                    }
                    break;
                default:
                    break;
            }


        }

        protected void genearatefilteredIP_Click(object sender, EventArgs e)
        {
            LoadFilteredIPGrid();
        }

        protected void GridfilteredIPEntries_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName == "download_file")
            {
                DownloadFile(e.CommandArgument.ToString());
            }
        }

        private void LoadGatewayDocUpdateCombo()
        {
            comboDocUpdateFiles.DataSource = UtilityFactory.GetEnumDescriptionList(typeof(DocUpdateDownstream));
            comboDocUpdateFiles.SelectedIndex = 0;
            comboDocUpdateFiles.DataBind();
        }

        private void LoadCustomerDocUpdateCombo()
        {
            comboCustomerDocUpdateFiles.DataSource = UtilityFactory.GetEnumDescriptionList(typeof(CustomerUpdateDownstream));
            comboCustomerDocUpdateFiles.SelectedIndex = 0;
            comboCustomerDocUpdateFiles.DataBind();
        }

        protected void genearatedocUpdateDownStream_Click(object sender, EventArgs e)
        {
            LoadDocUpdateDownStreamGrid();
        }

        protected void cleardocUpdateDownStream_Click(object sender, EventArgs e)
        {
            docUpdateDownStreamDate.Clear();
            comboDocUpdateFiles.ClearSelection();
            GriddocUpdateDownStreamEntries.Visible = false;
            GriddocUpdateDownStreamEntriesHeader.Visible = false;
        }

        protected void clearfilteredIP_Click(object sender, EventArgs e)
        {
            if (divCustomerDocUPDT_FilterIP.Visible)
            {
                filteredIPDate.Clear();
                GridfilteredIPEntries.Visible = false;
            }
            else
            {
                CustomerDocUpdateDownStreamDate.Clear();
                comboCustomerDocUpdateFiles.ClearSelection();
                GridCustomerDocUpdateHeader.Visible = false;
                GridCustomerDocUpdateEntries.Visible = false;
            }

        }

        protected void GriddocUpdateDownStreamEntries_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "download_file")
            {
                DownloadFile(e.CommandArgument.ToString());
            }
        }

        protected void GridCUSIPReports_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
        {

        }


        #region DailyUpdateReport

        protected void btnGenerateDailyUpdate_Click(object sender, EventArgs e)
        {
            BindDailyUpdateReport();
            GridDailyUpdateHeader.DataBind();
            GridDailyUpdateDetail.DataBind();
        }

        private void BindDailyUpdateReport(string sortField = null, string sortOrder = null)
        {
            GridDailyUpdateHeader.Visible = false;
            GridDailyUpdateDetail.Visible = false;
            int detailCount = 0;
            // bind header
            List<BCSDailyIPReportData> lstbCSDailyUpdateReports = new List<BCSDailyIPReportData>();
            if (datPickDailyUpdateHistory.SelectedDate != null)
            {
                BCSDailyIPReportData objDailyUpdateReports = new BCSDailyIPReportData();
                var clientName = Session["ClientName"].ToString();

                if (sortField != null && sortOrder != null)
                    objDailyUpdateReports = new ServiceFactory().GetDailyIPReportDetails(clientName, Convert.ToDateTime(datPickDailyUpdateHistory.SelectedDate), sortField, sortOrder);
                else
                    objDailyUpdateReports = new ServiceFactory().GetDailyIPReportDetails(clientName, Convert.ToDateTime(datPickDailyUpdateHistory.SelectedDate), "RecordType", "Asc");

                lstbCSDailyUpdateReports.Add(objDailyUpdateReports);
                GridDailyUpdateHeader.Visible = true;
                GridDailyUpdateDetail.Visible = true;

                dvHeaderInfo.Style.Add("Display", "block");
                dvDetailInfo.Style.Add("Display", "block");
            }
            GridDailyUpdateHeader.DataSource = lstbCSDailyUpdateReports;

            //Bind detail
            GridDailyUpdateDetail.DataSource = new List<BCSDailyIPReportDetailRecords>();
            GridDailyUpdateDetail.ClientSettings.Scrolling.AllowScroll = true;

            if (lstbCSDailyUpdateReports.Count > 0)
            {
                GridDailyUpdateDetail.DataSource = lstbCSDailyUpdateReports[0].DetailRecords;
                detailCount = lstbCSDailyUpdateReports[0].DetailRecords.Count;
                GridDailyUpdateDetail.Height = 80;
                if (detailCount > 0)
                {
                    GridDailyUpdateDetail.Height = 400;
                    GridDailyUpdateDetail.ClientSettings.Scrolling.AllowScroll = true;
                }
            }

            lblDetailCount.Text = detailCount.ToString();
        }

        protected void btnClearDailyUpdate_Click(object sender, EventArgs e)
        {
            datPickDailyUpdateHistory.Clear();
            GridDailyUpdateHeader.Visible = false;
            GridDailyUpdateDetail.Visible = false;
            dvHeaderInfo.Style.Add("Display", "none");
            dvDetailInfo.Style.Add("Display", "none");
        }


        protected void GridDailyUpdateDetail_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var sort = GridDailyUpdateDetail.MasterTableView.SortExpressions;
            string sortOrder = sort[0].SortOrder.ToString();
            sortOrder = sortOrder.Contains("Descending") ? "Desc" : "Asc";

            BindDailyUpdateReport(sort[0].FieldName, sortOrder);
        }
        #endregion

        #region Load Watch List Reports
        private void LoadWatchListgrid()
        {
            List<SANFileDetails> watchListData = new List<SANFileDetails>();
            string doumentPath = string.Empty;
            switch (Session["ClientName"].ToString())
            {
                case "Transamerica": doumentPath = ConfigurationManager.AppSettings.Get(watchListTransamericaDocumentPath); break;
                case "AllianceBernstein": doumentPath = ConfigurationManager.AppSettings.Get(watchListAllianceBernsteinDocumentPath); break;
            }
            GridWatchListEntries.Visible = false;
            if (watchListDate.SelectedDate != null)
            {
                watchListData = new BCSDocUpdateApprovalFactory().GetSANFileDetails(doumentPath, Convert.ToDateTime(watchListDate.SelectedDate), "*.txt");
                GridWatchListEntries.Visible = true;
            }

            GridWatchListEntries.DataSource = watchListData;
            GridWatchListEntries.DataBind();
        }
        private void LoadFilteredIPGrid()
        {
            List<SANFileDetails> watchListData = new List<SANFileDetails>();
            string doumentPath = string.Empty;
            switch (Session["ClientName"].ToString())
            {
                case "GMS": doumentPath = ConfigurationManager.AppSettings.Get("CustomerDocUPDTPathGMS");
                    divCustomerDocUPDT_FilterIP.Visible = false;
                    divCustomerDocUPDT_IP_NU.Visible = true;
                    break;
                case "Transamerica": doumentPath = ConfigurationManager.AppSettings.Get(filteredIpTransamericaDocumentPath); 
                    divCustomerDocUPDT_FilterIP.Visible = true;
                    divCustomerDocUPDT_IP_NU.Visible = false;
                    break;
                case "AllianceBernstein": doumentPath = ConfigurationManager.AppSettings.Get(filteredIpAllianceBernsteinDocumentPath); 
                    divCustomerDocUPDT_FilterIP.Visible = true;
                    divCustomerDocUPDT_IP_NU.Visible = false;
                    break;
            }
            if (divCustomerDocUPDT_FilterIP.Visible)
            {
                GridfilteredIPEntries.Visible = false;
                if (filteredIPDate.SelectedDate != null)
                {
                    watchListData = new BCSDocUpdateApprovalFactory().GetSANFileDetails(doumentPath, Convert.ToDateTime(filteredIPDate.SelectedDate), "*.txt");
                    GridfilteredIPEntries.Visible = true;
                }
                GridfilteredIPEntries.DataSource = watchListData;
                GridfilteredIPEntries.DataBind();
            }
            else
            {
                List<SANFileDetails> CustDocUPDTDetails = new List<SANFileDetails>();
                GridCustomerDocUpdateEntries.Visible = false;
                GridCustomerDocUpdateHeader.Visible = false;
                string filter = string.Empty;
                switch (comboCustomerDocUpdateFiles.SelectedValue)
                {
                    case "NU-File": filter = "*NU*.txt"; break;
                    case "IP-File": filter = "*IP*.txt"; break;
                    case "Customer - Doc Update": filter = "*.txt"; break;

                }
                if (CustomerDocUpdateDownStreamDate.SelectedDate != null)
                {
                    CustDocUPDTDetails = new BCSDocUpdateApprovalFactory().GetSANFileDetails(doumentPath, Convert.ToDateTime(CustomerDocUpdateDownStreamDate.SelectedDate), filter);
                    GridCustomerDocUpdateEntries.Visible = true;
                    GridCustomerDocUpdateHeader.Visible = true;
                }

                List<BCSGatewayDocUpdateData> lstHeaderData = new List<BCSGatewayDocUpdateData>();
                foreach (SANFileDetails dirName in CustDocUPDTDetails)
                {
                    lstHeaderData.Add(new BCSDocUpdateApprovalFactory().GetDocUpdateHedaerData(dirName.DirectoryName));
                }

                GridCustomerDocUpdateHeader.DataSource = lstHeaderData;
                GridCustomerDocUpdateHeader.DataBind();

                GridCustomerDocUpdateEntries.DataSource = CustDocUPDTDetails;
                GridCustomerDocUpdateEntries.DataBind();
            }

        }

        private void LoadDocUpdateDownStreamGrid()
        {
            List<SANFileDetails> watchListData = new List<SANFileDetails>();
            GriddocUpdateDownStreamEntries.Visible = false;
            GriddocUpdateDownStreamEntriesHeader.Visible = false;
            string filter = string.Empty;
            switch (comboDocUpdateFiles.SelectedValue)
            {
                case "NU-File": filter = "*NU*.txt"; break;
                case "IP-File": filter = "*IP*.txt"; break;
                case "Gateway - Doc Update": filter = "*.txt"; break;

            }
            if (docUpdateDownStreamDate.SelectedDate != null)
            {
                watchListData = new BCSDocUpdateApprovalFactory().GetSANFileDetails(ConfigurationManager.AppSettings.Get(docUpdateDocumentPath), Convert.ToDateTime(docUpdateDownStreamDate.SelectedDate), filter);
                GriddocUpdateDownStreamEntries.Visible = true;
                GriddocUpdateDownStreamEntriesHeader.Visible = true;
            }

            List<BCSGatewayDocUpdateData> lstHeaderData = new List<BCSGatewayDocUpdateData>();
            foreach (SANFileDetails dirName in watchListData)
            {
                lstHeaderData.Add(new BCSDocUpdateApprovalFactory().GetDocUpdateHedaerData(dirName.DirectoryName));
            }

            GriddocUpdateDownStreamEntriesHeader.DataSource = lstHeaderData;
            GriddocUpdateDownStreamEntriesHeader.DataBind();

            GriddocUpdateDownStreamEntries.DataSource = watchListData;
            GriddocUpdateDownStreamEntries.DataBind();
        }
        #endregion

        #region SlinkReport
        protected void btnGenerateSlinkReport_Click(object sender, EventArgs e)
        {
            BindSlinkGrid(true, GridSlinkReports.PageSize);
        }

        protected void btnClearSlinkReport_Click(object sender, EventArgs e)
        {
            ClearSlinkReport();
        }
        private void ClearSlinkReport()
        {
            comboStatus.ClearSelection();
            comboStatus.SelectedIndex = 0;
            txtDocId.Text = "";
            slinkReportDate.Clear();
            GridSlinkReports.Visible = false;
            GridSlinkReportsCount.Visible = false;

        }

        protected void GridSlinkReports_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        private void BindSlinkGrid(bool isReBindGrid, int endIndex, int startIndex = 1, string sortField = null, string sortOrder = null)
        {
            List<SLINKReportDetails> linkReports = new List<SLINKReportDetails>();
            string status = null;
            int virtualcount = 0;
            if (comboStatus.SelectedValue != "--Select Status--")
                status = comboStatus.SelectedValue;
            var clientName = Session["ClientName"].ToString();
            var selectedStatus = string.IsNullOrEmpty(status) ? null : status;
            string docID = string.IsNullOrEmpty(txtDocId.Text) ? null : txtDocId.Text;


            object countDetails = null;

            if (sortField != null && sortOrder != null)
                linkReports = new BCSDocUpdateApprovalFactory().GetSLinkReportDetails(clientName, selectedStatus, slinkReportDate.SelectedDate, docID, sortField, sortOrder, startIndex, endIndex, out virtualcount, out countDetails);
            else
                linkReports = new BCSDocUpdateApprovalFactory().GetSLinkReportDetails(clientName, selectedStatus, slinkReportDate.SelectedDate, docID, "SLINKFileName", "Asc", startIndex, endIndex, out virtualcount, out countDetails);
            GridSlinkReports.Visible = true;
            GridSlinkReportsCount.Visible = true;
            GridSlinkReports.VirtualItemCount = virtualcount;
            linkReports.ForEach(p =>
            {
                p.ReceivedDate = GetLocaltime(p.ReceivedDate.ToUniversalTime(), int.Parse(hdnDateOffSet.Value));
            });

            GridSlinkReports.DataSource = linkReports;

            if (isReBindGrid)
            {
                GridSlinkReports.CurrentPageIndex = 0;
                GridSlinkReports.DataBind();
            }
            
            IList<object> objList = new List<object>();
            objList.Add(countDetails);

            GridSlinkReportsCount.DataSource = objList;
            GridSlinkReportsCount.DataBind();
        }


        protected void GridSlinkReports_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var sort = GridSlinkReports.MasterTableView.SortExpressions;
            string sortOrder = null, fieldName = null;
            if (sort.Count > 0)
            {
                sortOrder = sort[0].SortOrder.ToString().Contains("Descending") ? "Desc" : "Asc";
                fieldName = sort[0].FieldName;
            }
            int startIndex = GridSlinkReports.PageSize * GridSlinkReports.CurrentPageIndex + 1;
            int endIndex = GridSlinkReports.PageSize * (GridSlinkReports.CurrentPageIndex + 1);
            BindSlinkGrid(false, endIndex, startIndex, fieldName, sortOrder);
        }
        #endregion

        #region LiveUpdate
        public void LoadLiveUpdateStatus()
        {
            List<string> lstStatus = new List<string>();
            lstStatus.Add("--Select Status--");
            UtilityFactory.GetEnumDescriptionList(typeof(LiveUpdateStatus)).ForEach(x =>
            {
                lstStatus.Add(x);
            });
            cmbLiveStatus.DataSource = lstStatus;
            cmbLiveStatus.SelectedIndex = 0;
            cmbLiveStatus.DataBind();
        }
        protected void btnGenerateLiveUpdate_Click(object sender, EventArgs e)
        {
            int startIndex = GridLiveUpdate.CurrentPageIndex * GridLiveUpdate.PageSize;
            int endIndex = startIndex + GridLiveUpdate.PageSize;

            Session["ClientName"] = comboClient.SelectedItem.Text;

            switch (comboClient.SelectedItem.Text)
            {

                case "GIM":
                case "GMS":
                    BindToGridLiveUpdateForGIM(true, startIndex, endIndex);
                    break;
                case "Transamerica":
                    BindToGridLiveUpdateForCustomer(true, startIndex, endIndex);
                    break;
                case "AllianceBernstein":
                    BindToGridLiveUpdateForCustomer(true, startIndex, endIndex);
                    break;
            }

        }

        protected void btnLiveUpdateClear_Click(object sender, EventArgs e)
        {
            ClearLiveUpdate();
        }

        private void ClearLiveUpdate()
        {
            GridLiveUpdate.Visible = false;
            divCUSIPCount.Visible = false;
            divGridLiveUpdateInvalidCUSIPs.Visible = false;
            divDocIdsCount.Visible = false;
            divGridLiveUpdateInvalidDocIds.Visible = false;
            txtCusip.Text = "";
            txtAcc.Text = "";
            txtDocumentID.Text = "";
            //cmbCompanyName.ClearSelection();
            cmbLiveStatus.ClearSelection();
            cmbLiveStatus.SelectedIndex = 0;
        }

        private void BindToGridLiveUpdateForGIM(bool isReBindGrid, int startIndex, int endIndex)
        {
            List<string> lstInvalidCUSIPs = new List<string>();
            List<string> lstInvalidDocIds = new List<string>();
            string status = null;
            if (cmbLiveStatus.SelectedIndex != -1 && cmbLiveStatus.SelectedItem.Text != ("--Select Status--"))
            {
                status = cmbLiveStatus.SelectedItem.Text;
            }
            GridLiveUpdate.Visible = true;
            BCSDocUpdateApprovalCUSIPData bCSDocUpdateApprovalCUSIPData = new BCSDocUpdateApprovalFactory().GetBCSDocUpdateApprovalAllCUSIPData(string.IsNullOrWhiteSpace(txtCusip.Text) ? null : txtCusip.Text.Trim(), string.IsNullOrWhiteSpace(txtAcc.Text) ? null : txtAcc.Text.Trim(), startIndex, endIndex, status, string.IsNullOrWhiteSpace(txtDocumentID.Text) ? null : txtDocumentID.Text.Trim(), lstInvalidCUSIPs, lstInvalidDocIds);

            bCSDocUpdateApprovalCUSIPData.AllCUSIPDetails.ForEach(p =>
            {
                p.StatusDate = GetLocaltimeString(DateTime.Parse(p.StatusDate).ToUniversalTime(), int.Parse(hdnDateOffSet.Value));
            });

            GridLiveUpdate.DataSource = bCSDocUpdateApprovalCUSIPData.AllCUSIPDetails;
            GridLiveUpdate.VirtualItemCount = bCSDocUpdateApprovalCUSIPData.AllCUSIPDetailsTotalCount;

            if (isReBindGrid)
            {
                GridLiveUpdate.CurrentPageIndex = 0;
                GridLiveUpdate.Rebind();
            }

            if (lstInvalidCUSIPs.Count > 0)
            {
                List<object> obj = new List<object>();
                lstInvalidCUSIPs.ForEach(p =>
                {
                    obj.Add(new { CUSIP = p });
                });

                GridLiveUpdateInvalidCUSIPs.DataSource = obj;
                GridLiveUpdateInvalidCUSIPs.DataBind();
                divGridLiveUpdateInvalidCUSIPs.Visible = true;
                lblGridLiveUpdateInvalidCUSIPs.Text = "Below CUSIPs are not present in " + Session["ClientName"];
            }
            else
            {
                divGridLiveUpdateInvalidCUSIPs.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtCusip.Text))
            {
                divCUSIPCount.Visible = false;
            }
            else
            {
                string[] cusipDeliminator = new string[] { "\n" };
                string[] lstCUSIP = txtCusip.Text.Replace("\r", "").Replace("\t", "").Split(cusipDeliminator, StringSplitOptions.RemoveEmptyEntries);

                IList<object> objList = new List<object>();
                objList.Add(new
                {
                    TotalCUSIPs = lstCUSIP.Length,
                    CUSIPsFound = lstCUSIP.Length - lstInvalidCUSIPs.Count,
                    MissingCUSIPs = lstInvalidCUSIPs.Count
                });

                GridLiveUpdateCUSIPDetails.DataSource = objList;
                GridLiveUpdateCUSIPDetails.DataBind();

                divCUSIPCount.Visible = true;
            }

            //Invalid DocIds
            if (lstInvalidDocIds.Count > 0)
            {
                List<object> obj = new List<object>();
                lstInvalidDocIds.ForEach(p =>
                {
                    obj.Add(new { DocId = p });
                });

                GridLiveUpdateInvalidDocIds.DataSource = obj;
                GridLiveUpdateInvalidDocIds.DataBind();
                divGridLiveUpdateInvalidDocIds.Visible = true;
                lblGridLiveUpdateInvalidDocIds.Text = "Below Doc Ids are not present in " + Session["ClientName"];
            }
            else
            {
                divGridLiveUpdateInvalidDocIds.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtDocumentID.Text))
            {
                divDocIdsCount.Visible = false;
            }
            else
            {
                string[] docIdDeliminator = new string[] { "\n" };
                string[] lstDocId = txtDocumentID.Text.Replace("\r", "").Replace("\t", "").Split(docIdDeliminator, StringSplitOptions.RemoveEmptyEntries);

                IList<object> objList = new List<object>();
                objList.Add(new
                {
                    TotalDocIds = lstDocId.Length,
                    DocIdsFound = lstDocId.Length - lstInvalidDocIds.Count,
                    MissingDocIds = lstInvalidDocIds.Count
                });

                GridLiveUpdateDocIdDetails.DataSource = objList;
                GridLiveUpdateDocIdDetails.DataBind();

                divDocIdsCount.Visible = true;
            }
        }

        private void BindToGridLiveUpdateForCustomer(bool isReBindGrid, int startIndex, int endIndex)
        {

            List<string> lstInvalidCUSIPs = new List<string>();
            List<string> lstInvalidDocIds = new List<string>();
            string status = null;
            if (cmbLiveStatus.SelectedIndex > -1 && cmbLiveStatus.SelectedItem.Text != ("--Select Status--"))
            {
                status = cmbLiveStatus.SelectedItem.Text;
            }
            GridLiveUpdate.Visible = true;
            string clientName = Session["ClientName"].ToString();
            BCSDocUpdateApprovalCUSIPData bCSTransamericaCustomerDocUpdateAllCUSIPData = new BCSDocUpdateApprovalFactory().GetBCSLiveUpdateCustomerDocUpdateAllCUSIPData(clientName, string.IsNullOrWhiteSpace(txtCusip.Text) ? null : txtCusip.Text.Trim(), string.IsNullOrWhiteSpace(txtAcc.Text) ? null : txtAcc.Text.Trim(), startIndex, endIndex, status, string.IsNullOrWhiteSpace(txtDocumentID.Text) ? null : txtDocumentID.Text.Trim(), lstInvalidCUSIPs, lstInvalidDocIds);
            bCSTransamericaCustomerDocUpdateAllCUSIPData.AllCUSIPDetails.ForEach(p =>
            {
                p.StatusDate = GetLocaltimeString(DateTime.Parse(p.StatusDate).ToUniversalTime(), int.Parse(hdnDateOffSet.Value));
            });
            GridLiveUpdate.DataSource = bCSTransamericaCustomerDocUpdateAllCUSIPData.AllCUSIPDetails;
            GridLiveUpdate.VirtualItemCount = bCSTransamericaCustomerDocUpdateAllCUSIPData.AllCUSIPDetailsTotalCount;

            if (isReBindGrid)
            {
                GridLiveUpdate.CurrentPageIndex = 0;
                GridLiveUpdate.DataBind();
            }

            if (lstInvalidCUSIPs.Count > 0)
            {
                List<object> obj = new List<object>();
                lstInvalidCUSIPs.ForEach(p =>
                {
                    obj.Add(new { CUSIP = p });
                });

                GridLiveUpdateInvalidCUSIPs.DataSource = obj;
                GridLiveUpdateInvalidCUSIPs.DataBind();
                divGridLiveUpdateInvalidCUSIPs.Visible = true;
                lblGridLiveUpdateInvalidCUSIPs.Text = "Below CUSIPs are not present in " + clientName;
            }
            else
            {
                divGridLiveUpdateInvalidCUSIPs.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtCusip.Text))
            {
                divCUSIPCount.Visible = false;
            }
            else
            {
                string[] cusipDeliminator = new string[] { "\n" };
                string[] lstCUSIP = txtCusip.Text.Replace("\r", "").Replace("\t", "").Split(cusipDeliminator, StringSplitOptions.RemoveEmptyEntries);

                IList<object> objList = new List<object>();
                objList.Add(new
                {
                    TotalCUSIPs = lstCUSIP.Length,
                    CUSIPsFound = lstCUSIP.Length - lstInvalidCUSIPs.Count,
                    MissingCUSIPs = lstInvalidCUSIPs.Count
                });

                GridLiveUpdateCUSIPDetails.DataSource = objList;
                GridLiveUpdateCUSIPDetails.DataBind();

                divCUSIPCount.Visible = true;
            }
            //Invalid DocIds
            if (lstInvalidDocIds.Count > 0)
            {
                List<object> obj = new List<object>();
                lstInvalidDocIds.ForEach(p =>
                {
                    obj.Add(new { DocId = p });
                });

                GridLiveUpdateInvalidDocIds.DataSource = obj;
                GridLiveUpdateInvalidDocIds.DataBind();
                divGridLiveUpdateInvalidDocIds.Visible = true;
                lblGridLiveUpdateInvalidDocIds.Text = "Below Doc Ids are not present in GIM";
            }
            else
            {
                divGridLiveUpdateInvalidDocIds.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtDocumentID.Text))
            {
                divDocIdsCount.Visible = false;
            }
            else
            {
                string[] docIdDeliminator = new string[] { "\n" };
                string[] lstDocId = txtDocumentID.Text.Replace("\r", "").Replace("\t", "").Split(docIdDeliminator, StringSplitOptions.RemoveEmptyEntries);

                IList<object> objList = new List<object>();
                objList.Add(new
                {
                    TotalDocIds = lstDocId.Length,
                    DocIdsFound = lstDocId.Length - lstInvalidDocIds.Count,
                    MissingDocIds = lstInvalidDocIds.Count
                });

                GridLiveUpdateDocIdDetails.DataSource = objList;
                GridLiveUpdateDocIdDetails.DataBind();

                divDocIdsCount.Visible = true;
            }

        }

        protected void GridLiveUpdate_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            int startIndex = GridLiveUpdate.CurrentPageIndex * GridLiveUpdate.PageSize;

            int endIndex = startIndex + GridLiveUpdate.PageSize;


            Session["ClientName"] = comboClient.SelectedItem.Text;

            switch (comboClient.SelectedItem.Text)
            {
                case "GIM":
                case "GMS":
                    BindToGridLiveUpdateForGIM(false, startIndex, endIndex);
                    break;
                case "Transamerica":
                    BindToGridLiveUpdateForCustomer(false, startIndex, endIndex);
                    break;
                case "AllianceBernstein":
                    BindToGridLiveUpdateForCustomer(false, startIndex, endIndex);
                    break;
            }

        }
        #endregion

        #region FullfillmentInfo
        protected void btnGenerateFullfillmentInfo_Click(object sender, EventArgs e)
        {
            BindToGridFullfillment();
        }

        private void BindToGridFullfillment()
        {
            DateTime date = Convert.ToDateTime(dateFullfillment.SelectedDate);
            if (dateFullfillment.SelectedDate != null)
            {
                string FolderPath = string.Empty;
                switch (Session["ClientName"].ToString())
                {
                    case "GIM":
                        FolderPath = ConfigurationManager.AppSettings.Get("FullfillmentInfoGIM").ToString();
                        break;
                    case "GMS":
                        FolderPath = ConfigurationManager.AppSettings.Get("FullfillmentInfoGMS").ToString();
                        break;
                    case "Transamerica":
                        FolderPath = ConfigurationManager.AppSettings.Get("FullfillmentInfoTransamerica").ToString();
                        break;
                    case "AllianceBernstein":
                        FolderPath = ConfigurationManager.AppSettings.Get("FullfillmentInfoAllianceBernstein").ToString();
                        break;
                    default:
                        break;
                }

                var files = Directory.EnumerateFiles(FolderPath, "Response_*.xml", SearchOption.TopDirectoryOnly)
         .Select(fn => new FileInfo(fn));

                var fileInfo = files.Where(fn => fn.LastWriteTime.ToShortDateString() == date.ToShortDateString());
                List<BCSFullfillmentInfo> lstFullfillmentInfo = new List<BCSFullfillmentInfo>();
                foreach (FileInfo file in fileInfo)
                {
                    XDocument doc = XDocument.Load(new StreamReader(file.FullName));
                    string transID = doc.Descendants("Response").FirstOrDefault().Element("TransId").Value;
                    var data = from item in doc.Descendants("file")
                               select new
                               {
                                   transID = transID,
                                   cusip = item.Element("cusip").Value,
                                   message = item.Element("message").Value
                               };
                    foreach (var r in data)
                    {
                        BCSFullfillmentInfo objFullInfo = new BCSFullfillmentInfo()
                        {
                            TransId = r.transID,
                            Cusip = r.cusip,
                            Message = r.message
                        };
                        lstFullfillmentInfo.Add(objFullInfo);
                    }
                }
                GridFullfillmentInfo.DataSource = lstFullfillmentInfo;
                GridFullfillmentInfo.DataBind();
                GridFullfillmentInfo.Visible = true;

                var countDetails = new
                {
                    TotalCount = lstFullfillmentInfo.Count,
                    Completed = lstFullfillmentInfo.Count(p => p.Message == "ok"),
                    NotAvailable = lstFullfillmentInfo.Count(p => p.Message != "ok")
                };
                IList<object> objList = new List<object>();
                objList.Add(countDetails);

                GridFullfillmentInfoCount.DataSource = objList;
                GridFullfillmentInfoCount.DataBind();
                GridFullfillmentInfoCount.Visible = true;
            }
        }

        protected void btnClearFullfillment_Click(object sender, EventArgs e)
        {
            ClearFullfillment();
        }

        private void ClearFullfillment()
        {
            dateFullfillment.Clear();
            GridFullfillmentInfo.Visible = false;
            GridFullfillmentInfoCount.Visible = false;
        }

        #endregion

        protected void comboReportType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            trDateRange.Visible = e.Text == "CUSIP Not Present in RP" ? false : true;
            GridCUSIPReports.Visible = false;
            GridCUSIPReports.MasterTableView.SortExpressions.Clear();
        }

        protected void GridLiveUpdate_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                DateTime statusDate = DateTime.Parse(item["StatusDate"].Text);

                if (statusDate < (DateTime.Now.AddDays(-1)) && (item["Status"].Text == "OP" || item["Status"].Text == "AP"))
                {
                    item["Status"].CssClass = "cssLiveUpdateStatusDate";
                }
            }
        }

        public string GetLocaltimeString(DateTime utcDate, int offset)
        {
            //Note:  The time-zone offset is the difference, in minutes, between UTC and local time.   i.e  offset = utc - localtime
            TimeSpan interval = TimeSpan.FromMinutes(Convert.ToInt32(offset));
            return (utcDate - interval).ToString();
        }

        public DateTime GetLocaltime(DateTime utcDate, int offset)
        {
            //Note:  The time-zone offset is the difference, in minutes, between UTC and local time.   i.e  offset = utc - localtime
            TimeSpan interval = TimeSpan.FromMinutes(Convert.ToInt32(offset));
            return (utcDate - interval);
        }

    }
}