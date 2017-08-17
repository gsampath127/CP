// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class ClientDocumentGroupViewModel.
    /// </summary>
    public class ClientDocumentGroupViewModel
    {
        /// <summary>
        /// Gets or sets the ClientDocumentGroupId identifier.
        /// </summary>
        /// <value>The ClientDocumentGroupId identifier.</value>
        public int ClientDocumentGroupId { get; set; }

        /// <summary>
        /// Gets or sets the selected level identifier.
        /// </summary>
        /// <value>The selected level identifier.</value>
        public int? SelectedClientDocumentGroupId { get; set; }

        /// <summary>
        /// Gets or sets the ParentClientDocumentGroupId.
        /// </summary>
        /// <value>The ParentClientDocumentGroupId.</value>
        public int? ParentClientDocumentGroupId { get; set; }

        /// <summary>
        /// Gets or sets the CssClass.
        /// </summary>
        /// <value>The CssClass.</value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ParentClientDocumentGroup.
        /// </summary>
        /// <value>The ParentClientDocumentGroup.</value>
        [DisplayName("Parent Client Document Group")]
        public List<DisplayValuePair> ParentClientDocumentGroup { get; set; }

        /// <summary>
        /// Gets or sets the SuccessOrFailedMessage.
        /// </summary>
        /// <value>The SuccessOrFailedMessage.</value>
        public string SuccessOrFailedMessage { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
        [DisplayName("Modified By")]
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the UTC last modified date.
        /// </summary>
        /// <value>The UTC last modified date.</value>
        [DisplayName("Modified Date")]
        public string UTCLastModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>The name of the modified by.</value>
        public string ModifiedByName { get; set; }
                
    }
}