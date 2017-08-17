using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCSDocUpdateApproval.UserControls
{
    public partial class UIErrorDisplay : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool Visible 
        { 
            get {return UIError.Visible;}
            set 
            {  
                UIError.Visible = value; 
            }
        }
        public void SetError(string errorString)
        {
            UIError.Text = errorString;
            UIError.Visible = true;
        }
    }
}