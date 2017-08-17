// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************

using System.Collections.Generic;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class SelectCustomerViewModel.
    /// </summary>
    public class SelectCustomerViewModel
    {
        /// <summary>
        /// Gets or sets the selected customer identifier.
        /// </summary>
        /// <value>The selected customer identifier.</value>
        public string SelectedCustomerID { get; set; }
        /// <summary>
        /// Gets or sets the customer names.
        /// </summary>
        /// <value>The customer names.</value>
        public List<DisplayValuePair> CustomerNames { get; set; }
    }
}