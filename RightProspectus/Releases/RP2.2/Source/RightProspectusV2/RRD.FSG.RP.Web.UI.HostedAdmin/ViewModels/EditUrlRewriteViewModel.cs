// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class EditUrlRewriteViewModel.
    /// </summary>
    public class EditUrlRewriteViewModel
    {
        /// <summary>
        /// Gets or sets the pattern names.
        /// </summary>
        /// <value>The pattern names.</value>
        [DisplayName("Pattern Name")]
        public List<DisplayValuePair> PatternNames { get; set; }

        /// <summary>
        /// Gets or sets the URL rewrite identifier.
        /// </summary>
        /// <value>The URL rewrite identifier.</value>
        public int UrlRewriteId { get; set; }

        /// <summary>
        /// Gets or sets the name of the pattern.
        /// </summary>
        /// <value>The name of the pattern.</value>
        [DisplayName("Pattern Name")]
        public string PatternName { get; set; }

        /// <summary>
        /// Gets or sets the match pattern.
        /// </summary>
        /// <value>The match pattern.</value>
        [DisplayName("Match Pattern")]
        public string MatchPattern { get; set; }

        /// <summary>
        /// Gets or sets the rewrite format.
        /// </summary>
        /// <value>The rewrite format.</value>
        [DisplayName("Rewrite Format")]
        public string RewriteFormat { get; set; }

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
        public DateTime? UTCLastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>The name of the modified by.</value>
        public string ModifiedByName { get; set; }
    }
}