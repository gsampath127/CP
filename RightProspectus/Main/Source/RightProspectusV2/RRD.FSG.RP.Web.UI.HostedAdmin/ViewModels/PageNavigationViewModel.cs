// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 11-13-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************

using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// View modal for Page Navigation administration
    /// </summary>
    public class PageNavigationViewModel
    {
        /// <summary>
        /// Primary key identifier for the PageNavigation entity.
        /// </summary>
        /// <value>The page navigation identifier.</value>
        public int PageNavigationId { get; set; }

        /// <summary>
        /// Key used to look up the navigation xml.
        /// </summary>
        /// <value>The navigation key.</value>
        [DisplayName("Navigation Key")]
        public string NavigationKey { get; set; }

        /// <summary>
        /// Page the xml navigation belongs to.
        /// </summary>
        /// <value>The name of the page.</value>
        [DisplayName("Page")]
        public string PageName { get; set; }

        // <summary>
        /// <summary>
        /// Gets or sets the page description.
        /// </summary>
        /// <value>The page description.</value>
        public string PageDescription { get; set; }

        /// <summary>
        /// Defines the language-culture combination this navigation xml menu is for.
        /// </summary>
        /// <value>The language culture.</value>
        [DisplayName("Language Culture")]
        public string LanguageCulture { get; set; }

        /// <summary>
        /// Xml defining the navigation menu. Can be a combination of elements defining known menu features as well as elements defining custom menu items and hierarchies.
        /// </summary>
        /// <value>The navigation XML.</value>
        [DisplayName("Navigation XML")]
        public string NavigationXML { get; set; }

        /// <summary>
        /// Utc date and time of creation/updation.
        /// </summary>
        /// <value>The UTC modified date.</value>
        [DisplayName("Modified Date")]
        public string UtcModifiedDate { get; set; }

        /// <summary>
        /// User who created/updated this version.
        /// </summary>
        /// <value>The modified by.</value>
        [DisplayName("Modified By")]
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        /// <value>The version.</value>
        [DisplayName("Version")]
        public string Version { get; set; }
        /// <summary>
        /// VersionID from Page Navigation Version table
        /// </summary>
        /// <value>The version identifier.</value>
        public int VersionID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is proofing.
        /// </summary>
        /// <value><c>true</c> if this instance is proofing; otherwise, <c>false</c>.</value>
        public bool IsProofing { get; set; }       
    }
}