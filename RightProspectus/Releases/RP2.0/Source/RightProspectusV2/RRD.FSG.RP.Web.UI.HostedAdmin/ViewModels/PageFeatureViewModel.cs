
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class PageFeatureViewModel.
    /// </summary>
    public class PageFeatureViewModel
    {
        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>The page identifier.</value>
        public int PageId { get; set; }
        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        /// <value>The name of the page.</value>
        public string PageName { get; set; }
        /// <summary>
        /// Gets or sets the page description.
        /// </summary>
        /// <value>The page description.</value>
        public string PageDescription { get; set; }
        /// <summary>
        /// Gets or sets the page key.
        /// </summary>
        /// <value>The page key.</value>
        public string PageKey { get; set; }
    }
}