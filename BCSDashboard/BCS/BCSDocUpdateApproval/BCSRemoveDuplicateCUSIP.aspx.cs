using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BCS.ObjectModel;
using BCS.ObjectModel.Factories;

namespace BCSDocUpdateApproval
{
    public partial class BCSRemoveDuplicateCUSIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divExceptionLog.Visible = false;
            if (!IsPostBack)
                LoadStatus();
        }


        public void LoadStatus()
        {
            List<string> lstStatus = new List<string>();
            lstStatus.Add("--Select Status--");
            UtilityFactory.GetEnumDescriptionList(typeof(LiveUpdateStatus)).ForEach(x =>
            {
                lstStatus.Add(x);
            });
            comboStatus.DataSource = lstStatus;
            comboStatus.SelectedIndex = 0;
            comboStatus.DataBind();
        }

        #region Duplicate CUSIP region

        protected void GridDuplicateCUSIPEntries_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                int startIndex = GridDuplicateCUSIPEntries.CurrentPageIndex * GridDuplicateCUSIPEntries.PageSize;
                int endIndex = startIndex + GridDuplicateCUSIPEntries.PageSize;
                BindToGridDuplicateCUSIPEntries(false, startIndex, endIndex);
            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }

        private void BindToGridDuplicateCUSIPEntries(bool isReBindGrid, int startIndex, int endIndex)
        {
            string status = null;
            if (comboStatus.SelectedValue != "--Select Status--")
                status = comboStatus.SelectedValue;

            BCSDocUpdateApprovalCUSIPData bCSDocUpdateApprovalCUSIPData = new BCSDocUpdateApprovalFactory().GetBCSDocUpdateApprovalDuplicateCUSIPData(string.IsNullOrWhiteSpace(txtCUSIP.Text) ? null : txtCUSIP.Text.Trim(), string.IsNullOrWhiteSpace(txtAccNumber.Text) ? null : txtAccNumber.Text.Trim(), startIndex, endIndex, status);
            GridDuplicateCUSIPEntries.DataSource = bCSDocUpdateApprovalCUSIPData.DuplicateCUSIPDetails;
            GridDuplicateCUSIPEntries.VirtualItemCount = bCSDocUpdateApprovalCUSIPData.DuplicateCUSIPDetailsTotalCount;

            if (isReBindGrid)
            {
                GridDuplicateCUSIPEntries.CurrentPageIndex = 0;
                GridDuplicateCUSIPEntries.DataBind();
            }
        }

        protected void GridDuplicateCUSIPEntries_Remove(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Remove":
                        GridDataItem item = (GridDataItem)e.Item;
                        int BCSDocUpdateId = Convert.ToInt32(item["BCSDocUpdateId"].Text);

                        new BCSDocUpdateApprovalFactory().RemoveDuplicateCUSIPUsingBCSDocUpdateID(BCSDocUpdateId);

                        int startIndex = GridDuplicateCUSIPEntries.CurrentPageIndex * GridDuplicateCUSIPEntries.PageSize;
                        int endIndex = startIndex + GridDuplicateCUSIPEntries.PageSize;
                        BindToGridDuplicateCUSIPEntries(true, startIndex, endIndex);

                        startIndex = GridAllCUSIPEntries.CurrentPageIndex * GridAllCUSIPEntries.PageSize;
                        endIndex = startIndex + GridAllCUSIPEntries.PageSize;
                        BindToGridAllCUSIPEntries(true, startIndex, endIndex);

                        break;
                }
            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }

        #endregion
        #region All CUSIP

        protected void GridAllCUSIPEntries_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                int startIndex = GridAllCUSIPEntries.CurrentPageIndex * GridAllCUSIPEntries.PageSize;

                int endIndex = startIndex + GridAllCUSIPEntries.PageSize;

                BindToGridAllCUSIPEntries(false, startIndex, endIndex);
            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }

        private void BindToGridAllCUSIPEntries(bool isReBindGrid, int startIndex, int endIndex)
        {
            List<string> lstInvalidCUSIPs = new List<string>();
            string status = null;
            if (comboStatus.SelectedValue != "--Select Status--")
                status = comboStatus.SelectedValue;

            BCSDocUpdateApprovalCUSIPData bCSDocUpdateApprovalCUSIPData = new BCSDocUpdateApprovalFactory().GetBCSDocUpdateApprovalAllCUSIPData(string.IsNullOrWhiteSpace(txtCUSIP.Text) ? null : txtCUSIP.Text.Trim(), string.IsNullOrWhiteSpace(txtAccNumber.Text) ? null : txtAccNumber.Text.Trim(), startIndex, endIndex, status,null, lstInvalidCUSIPs,null);
            GridAllCUSIPEntries.DataSource = bCSDocUpdateApprovalCUSIPData.AllCUSIPDetails;
            GridAllCUSIPEntries.VirtualItemCount = bCSDocUpdateApprovalCUSIPData.AllCUSIPDetailsTotalCount;

            if (isReBindGrid)
            {
                GridAllCUSIPEntries.CurrentPageIndex = 0;
                GridAllCUSIPEntries.Rebind();
            }            
        }
        #endregion

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                GridDuplicateCUSIPEntries.PageSize = 25;
                GridDuplicateCUSIPEntries.MasterTableView.PageSize = 25;
                int gridDuplicateCUSIPStartIndex = 0;
                int gridDuplicateCUSIPEndIndex = gridDuplicateCUSIPStartIndex + GridDuplicateCUSIPEntries.PageSize;
                BindToGridDuplicateCUSIPEntries(true, gridDuplicateCUSIPStartIndex, gridDuplicateCUSIPEndIndex);

                GridAllCUSIPEntries.PageSize = 25;
                GridAllCUSIPEntries.MasterTableView.PageSize = 25;
                int gridAllCUSIPStartIndex = 0;
                int gridAllCUSIPEndIndex = gridAllCUSIPStartIndex + GridAllCUSIPEntries.PageSize;
                BindToGridAllCUSIPEntries(true, gridAllCUSIPStartIndex, gridAllCUSIPEndIndex);

                GridDuplicateEntriesForCUstomerDocUpdate.PageSize = 25;
                GridDuplicateEntriesForCUstomerDocUpdate.MasterTableView.PageSize = 25;
                int gridCustomerDocUpdateDuplicateCUSIPStartIndex = 0;
                int gridCustomerDocUpdateDuplicateCUSIPEndIndex = gridCustomerDocUpdateDuplicateCUSIPStartIndex + GridDuplicateEntriesForCUstomerDocUpdate.PageSize;
                BindToGridDuplicateCustomerDocUpdateCUSIPEntries(true, gridCustomerDocUpdateDuplicateCUSIPStartIndex, gridCustomerDocUpdateDuplicateCUSIPEndIndex);

                GridAllEntriesCustomerDocUpdate.PageSize = 25;
                GridAllEntriesCustomerDocUpdate.MasterTableView.PageSize = 25;
                int gridCustomerDocUpdateAllCUSIPStartIndex = 0;
                int gridCustomerDocUpdateAllCUSIPEndIndex = gridCustomerDocUpdateAllCUSIPStartIndex + GridAllEntriesCustomerDocUpdate.PageSize;
                BindToGridAllCustomerDocUpdateCUSIPEntries(true, gridCustomerDocUpdateAllCUSIPStartIndex, gridCustomerDocUpdateAllCUSIPEndIndex);
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
                txtAccNumber.Text = "";
                comboStatus.ClearSelection();
                comboStatus.SelectedIndex = 0;

                GridDuplicateCUSIPEntries.PageSize = 25;
                GridDuplicateCUSIPEntries.MasterTableView.PageSize = 25;
                int gridDuplicateCUSIPStartIndex = 0;
                int gridDuplicateCUSIPEndIndex = gridDuplicateCUSIPStartIndex + GridDuplicateCUSIPEntries.PageSize;
                BindToGridDuplicateCUSIPEntries(true, gridDuplicateCUSIPStartIndex, gridDuplicateCUSIPEndIndex);

                GridAllCUSIPEntries.PageSize = 25;
                GridAllCUSIPEntries.MasterTableView.PageSize = 25;
                int gridAllCUSIPStartIndex = 0;
                int gridAllCUSIPEndIndex = gridAllCUSIPStartIndex + GridAllCUSIPEntries.PageSize;
                BindToGridAllCUSIPEntries(true, gridAllCUSIPStartIndex, gridAllCUSIPEndIndex);

                GridDuplicateEntriesForCUstomerDocUpdate.PageSize = 25;
                GridDuplicateEntriesForCUstomerDocUpdate.MasterTableView.PageSize = 25;
                int gridCustomerDocUpdateDuplicateCUSIPStartIndex = 0;
                int gridCustomerDocUpdateDuplicateCUSIPEndIndex = gridCustomerDocUpdateDuplicateCUSIPStartIndex + GridDuplicateEntriesForCUstomerDocUpdate.PageSize;
                BindToGridDuplicateCustomerDocUpdateCUSIPEntries(true, gridCustomerDocUpdateDuplicateCUSIPStartIndex, gridCustomerDocUpdateDuplicateCUSIPEndIndex);

                GridAllEntriesCustomerDocUpdate.PageSize = 25;
                GridAllEntriesCustomerDocUpdate.MasterTableView.PageSize = 25;
                int gridCustomerDocUpdateAllCUSIPStartIndex = 0;
                int gridCustomerDocUpdateAllCUSIPEndIndex = gridCustomerDocUpdateAllCUSIPStartIndex + GridAllEntriesCustomerDocUpdate.PageSize;
                BindToGridAllCustomerDocUpdateCUSIPEntries(true, gridCustomerDocUpdateAllCUSIPStartIndex, gridCustomerDocUpdateAllCUSIPEndIndex);
            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }

        protected void BCSRemoveDuplicateCUSIPTabStrip_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.Index == 0)
            {
                GridDuplicateCUSIPEntries.PageSize = 25;
                GridDuplicateCUSIPEntries.MasterTableView.PageSize = 25;
                int gridDuplicateCUSIPStartIndex = 0;
                int gridDuplicateCUSIPEndIndex = gridDuplicateCUSIPStartIndex + GridDuplicateCUSIPEntries.PageSize;
                BindToGridDuplicateCUSIPEntries(true, gridDuplicateCUSIPStartIndex, gridDuplicateCUSIPEndIndex);
            }

        }

        #region DuplicateCustomerDocUpdateCusipEntries
        protected void GridDuplicateEntriesForCUstomerDocUpdate_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                int startIndex = GridDuplicateEntriesForCUstomerDocUpdate.CurrentPageIndex * GridDuplicateEntriesForCUstomerDocUpdate.PageSize;
                int endIndex = startIndex + GridDuplicateEntriesForCUstomerDocUpdate.PageSize;
                BindToGridDuplicateCustomerDocUpdateCUSIPEntries(false, startIndex, endIndex);
            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }

        }

        private void BindToGridDuplicateCustomerDocUpdateCUSIPEntries(bool isReBindGrid, int startIndex, int endIndex)
        {
            string status = null;
            if (comboStatus.SelectedValue != "--Select Status--")
                status = comboStatus.SelectedValue;

            BCSDocUpdateApprovalCUSIPData bCSDocUpdateApprovalCUSIPData = new BCSDocUpdateApprovalFactory().GetBCSCustomerDocUpdateDuplicateCUSIPData(string.IsNullOrWhiteSpace(txtCUSIP.Text) ? null : txtCUSIP.Text.Trim(), string.IsNullOrWhiteSpace(txtAccNumber.Text) ? null : txtAccNumber.Text.Trim(), startIndex, endIndex, status);
            GridDuplicateEntriesForCUstomerDocUpdate.DataSource = bCSDocUpdateApprovalCUSIPData.DuplicateCUSIPDetails;
            GridDuplicateEntriesForCUstomerDocUpdate.VirtualItemCount = bCSDocUpdateApprovalCUSIPData.DuplicateCUSIPDetailsTotalCount;

            if (isReBindGrid)
            {
                GridDuplicateEntriesForCUstomerDocUpdate.CurrentPageIndex = 0;
                GridDuplicateEntriesForCUstomerDocUpdate.DataBind();
            }
        }

        #endregion

        #region AllCUSIPSForCustomerDocUpdate
        protected void GridAllEntriesCustomerDocUpdate_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            try
            {
                int startIndex = GridAllEntriesCustomerDocUpdate.CurrentPageIndex * GridAllEntriesCustomerDocUpdate.PageSize;

                int endIndex = startIndex + GridAllEntriesCustomerDocUpdate.PageSize;

                BindToGridAllCustomerDocUpdateCUSIPEntries(false, startIndex, endIndex);
            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }

        private void BindToGridAllCustomerDocUpdateCUSIPEntries(bool isReBindGrid, int startIndex, int endIndex)
        {
            string status = null;
            if (comboStatus.SelectedValue != "--Select Status--")
                status = comboStatus.SelectedValue;

            BCSDocUpdateApprovalCUSIPData bCSCustomerDocUpdateAllCUSIPData = new BCSDocUpdateApprovalFactory().GetBCSCustomerDocUpdateAllCUSIPData(string.IsNullOrWhiteSpace(txtCUSIP.Text) ? null : txtCUSIP.Text.Trim(), string.IsNullOrWhiteSpace(txtAccNumber.Text) ? null : txtAccNumber.Text.Trim(), startIndex, endIndex, status);
            GridAllEntriesCustomerDocUpdate.DataSource = bCSCustomerDocUpdateAllCUSIPData.AllCUSIPDetails;
            GridAllEntriesCustomerDocUpdate.VirtualItemCount = bCSCustomerDocUpdateAllCUSIPData.AllCUSIPDetailsTotalCount;

            if (isReBindGrid)
            {
                GridAllEntriesCustomerDocUpdate.CurrentPageIndex = 0;
                GridAllEntriesCustomerDocUpdate.DataBind();
            }
        }
        #endregion


        #region RemoveAndDuplicate
        protected void GridDuplicateEntriesForCUstomerDocUpdate_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (!string.Equals(e.CommandName, "page", StringComparison.OrdinalIgnoreCase))
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    int Id = Convert.ToInt32(item["BCSDocUpdateId"].Text);
                    string reportType = item["ReportType"].Text;
                    switch (e.CommandName)
                    {
                        case "RemoveDuplicates":
                            new BCSDocUpdateApprovalFactory().UpdateCustomerDocUpdateDuplicateCUSIP(Id, reportType, "Remove");
                            break;
                        case "MakeDuplicate":
                            new BCSDocUpdateApprovalFactory().UpdateCustomerDocUpdateDuplicateCUSIP(Id, reportType, "IsNotDuplicate");
                            break;

                    }

                    int startIndex = GridDuplicateEntriesForCUstomerDocUpdate.CurrentPageIndex * GridDuplicateEntriesForCUstomerDocUpdate.PageSize;
                    int endIndex = startIndex + GridDuplicateEntriesForCUstomerDocUpdate.PageSize;
                    BindToGridDuplicateCustomerDocUpdateCUSIPEntries(true, startIndex, endIndex);

                    startIndex = GridAllEntriesCustomerDocUpdate.CurrentPageIndex * GridAllEntriesCustomerDocUpdate.PageSize;
                    endIndex = startIndex + GridAllEntriesCustomerDocUpdate.PageSize;
                    BindToGridAllCustomerDocUpdateCUSIPEntries(true, startIndex, endIndex);
                }
            }
            catch (Exception ex)
            {
                int ExceptionID = (new Logging()).SaveExceptionLog(ex.ToString(), BCSApplicationName.BCSDocUpdateApproval, false);
                ExceptionLogger.SetError(UtilityFactory.GetExceptionMessageContent(ExceptionID));
                divExceptionLog.Visible = true;
            }
        }
        #endregion      



    }
}



