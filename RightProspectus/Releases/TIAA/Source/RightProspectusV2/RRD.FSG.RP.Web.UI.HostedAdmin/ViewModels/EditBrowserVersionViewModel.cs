using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    public class EditBrowserVersionViewModel
    {
        /// <summary>
        /// SelectedID
        /// </summary>
        public int SelectedID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public List<DisplayValuePair> Name { get; set; }

        /// <summary>
        /// SelectedName
        /// </summary>
        public string SelectedName { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        [DisplayName("Minimum Version")]
        public int? Version { get; set; }

        /// <summary>
        /// DownloadURL
        /// </summary>
        [DisplayName("Download URL")]
        public string DownloadURL { get; set; }
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
        [DisplayName("Modified By")]
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>The name of the modified by.</value>
        [DisplayName("Modified By")]
        public string ModifiedByName { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The modified date.</value>
        [DisplayName("Modified Date")]
        public string ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }


    }
}