using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
namespace BCSDocUpdateApproval
{
    public partial class RPCUSIPMissingReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dvClientSelection.Visible = true;
                BindClients();
                ((Label)Master.FindControl("lblMasterHeader")).Text = "Inline - First Dollar (FD) Dashboard";
                lnkHome.Visible = true;
            }
        }
        protected void lnkHome_Click(object sender, EventArgs e)
        {          
            Session.Clear();
            Response.Redirect("BCSReportDashBoard.aspx");
        }
        public void BindClients()
        {
            List<BCSClient> bcsClient = new ServiceFactory().GetALLClientConfig().FindAll(x => x.ShowClientInDashboard).Where(x => x.ClientName == "AllianceBernstein" || x.ClientName == "Transamerica" || x.ClientName == "TRP").ToList();
            comboClient.DataSource = bcsClient;
            comboClient.DataTextField = "ClientName";
            comboClient.DataValueField = "ClientName";
            comboClient.DataBind();
            comboClient.SelectedIndex = -1;
            comboClient.Text = string.Empty;
        }
        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string company = comboClient.SelectedItem == null ? null : comboClient.SelectedItem.Text;
            int startIndex = GridMissingCUSIP.CurrentPageIndex * GridMissingCUSIP.PageSize;
            int endIndex = startIndex + GridMissingCUSIP.PageSize;
            var sort = GridMissingCUSIP.MasterTableView.SortExpressions;
            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "ASC";
            sortOrder = sortOrder.Contains("Descending") ? "DESC" : "ASC";

            BindToGridMissingCUSIPDataEntries(true, company, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CompanyName", sortOrder);
            GridMissingCUSIP.Visible = true;
        }
        protected void btnClearCUSIPReport_Click(object sender, EventArgs e)
        {
            comboClient.Text = "";
            comboClient.ClearSelection();            
            GridMissingCUSIP.Visible = false;
        }
        protected void GridMissingCUSIP_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {


            string company = comboClient.SelectedItem == null? null:comboClient.SelectedItem.Text;            
            int startIndex = GridMissingCUSIP.CurrentPageIndex * GridMissingCUSIP.PageSize;
            int endIndex = startIndex + GridMissingCUSIP.PageSize;
            var sort = GridMissingCUSIP.MasterTableView.SortExpressions;
            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "ASC";
            sortOrder = sortOrder.Contains("Descending") ? "DESC" : "ASC";

            BindToGridMissingCUSIPDataEntries(false, company, startIndex, endIndex,sort.Count>0? sort[0].FieldName:"CompanyName",sortOrder);
        }
        protected void GridMissingCUSIP_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            string company = comboClient.SelectedItem == null ? null : comboClient.SelectedItem.Text;

            int startIndex = e.NewPageIndex * GridMissingCUSIP.PageSize;
            int endIndex = startIndex + GridMissingCUSIP.PageSize;
            var sort = GridMissingCUSIP.MasterTableView.SortExpressions;

            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "ASC";
            sortOrder = sortOrder.Contains("Descending") ? "DESC" : "ASC";
            BindToGridMissingCUSIPDataEntries(false, company, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CompanyName", sortOrder);
        }
        protected void GridMissingCUSIP_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            string company = comboClient.SelectedItem == null ? null : comboClient.SelectedItem.Text;

            int startIndex = e.NewPageSize * GridMissingCUSIP.CurrentPageIndex;
            int endIndex = startIndex + e.NewPageSize;
            var sort = GridMissingCUSIP.MasterTableView.SortExpressions;

            string sortOrder = sort.Count > 0 ? sort[0].SortOrder.ToString() : "ASC";
            sortOrder = sortOrder.Contains("Descending") ? "DESC" : "ASC";
            BindToGridMissingCUSIPDataEntries(false, company, startIndex, endIndex, sort.Count > 0 ? sort[0].FieldName : "CompanyName", sortOrder);
        }
        private void BindToGridMissingCUSIPDataEntries(bool isReBindGrid,string company, int startIndex, int endIndex,string sortColumn,string sortOrder)
        {
            int virtualCount = 0;
            List<BCSTRPReportRPCUSIPMissingData> bCSReportData = new BCSDocUpdateApprovalFactory().GetMissingCUSIP(company, startIndex, endIndex, sortColumn, sortOrder, out virtualCount);
            GridMissingCUSIP.DataSource = bCSReportData;
            GridMissingCUSIP.VirtualItemCount = virtualCount;

            if (isReBindGrid)
            {
                GridMissingCUSIP.CurrentPageIndex = 0;
                GridMissingCUSIP.DataBind();
            }
        }
    }
}