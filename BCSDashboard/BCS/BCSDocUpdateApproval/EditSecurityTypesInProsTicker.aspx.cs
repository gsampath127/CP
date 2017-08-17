using BCS.ObjectModel.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCSDocUpdateApproval
{
    public partial class EditSecurityTypesInProsTicker : System.Web.UI.Page
    {
        private string CUSIP;
        private string securityTypeFeedSourceName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.ServerVariables["QUERY_STRING"]))
            {
                CUSIP = Request.QueryString["cusip"].ToString();
                securityTypeFeedSourceName = "Manual";
                btnClose.Attributes["onclick"] = "window.close();";
                //btnSave.Attributes["onclick"] = "window.close();";
                if (!Page.IsPostBack)
                    LoadSecurityTypes();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = new RPSecurityTypeDashboardFactory().UpdateSecurityTypesInProsTicker(CUSIP, Convert.ToInt32(cmbSecurityType.SelectedValue), securityTypeFeedSourceName);
            if (result == 1){
                lblResult.Text = "Changes saved successfully.";
                lblResult.ForeColor = System.Drawing.Color.Green;
            }                
            else
            {
                lblResult.Text = "Error occured while saving the changes.";
                lblResult.ForeColor = System.Drawing.Color.Red;
            }
            lblResult.Visible = true;   
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void LoadSecurityTypes()
        {
            var details = new RPSecurityTypeDashboardFactory().GetCompanyandSecurityType();
            cmbSecurityType.DataSource = details.Tables[1];
            cmbSecurityType.DataValueField = "SecurityTypeID";
            cmbSecurityType.DataTextField = "SecurityTypeCode";
            cmbSecurityType.DataBind();
        }
        
    }
}