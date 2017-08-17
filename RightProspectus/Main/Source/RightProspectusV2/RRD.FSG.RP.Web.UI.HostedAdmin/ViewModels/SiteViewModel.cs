
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// View model for Site Adminstration
    /// </summary>
    public class SiteViewModel
    {
        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteID { get; set; }
        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateID { get; set; }
        /// <summary>
        /// Gets or sets the default page identifier.
        /// </summary>
        /// <value>The default page identifier.</value>
        public int DefaultPageID { get; set; }
        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }
        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        /// <value>The name of the template.</value>
        public string TemplateName { get; set; }
        /// <summary>
        /// Gets or sets the default name of the page.
        /// </summary>
        /// <value>The default name of the page.</value>
        public string DefaultPageName { get; set; }
        /// <summary>
        /// Gets or sets the page description.
        /// </summary>
        /// <value>The page description.</value>
        public string PageDescription { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
    }
}