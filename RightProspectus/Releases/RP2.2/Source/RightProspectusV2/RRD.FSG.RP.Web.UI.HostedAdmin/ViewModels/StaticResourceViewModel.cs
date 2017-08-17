
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class StaticResourceViewModel.
    /// </summary>
    public class StaticResourceViewModel
    {
        /// <summary>
        /// Gets or sets the static resource identifier.
        /// </summary>
        /// <value>The static resource identifier.</value>
        public string StaticResourceId { get; set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageURL { get; set; }
        /// <summary>
        /// Gets or sets the type of the MIME.
        /// </summary>
        /// <value>The type of the MIME.</value>
        public string MimeType { get; set; }
        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The modified date.</value>
        public string ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the ClientName.
        /// </summary>
        /// <value>The ClientName.</value>
        public string ClientName { get; set; }
        /// <summary>
        /// Gets or sets the StaticResourceURL.
        /// </summary>
        /// <value>The StaticResourceURL.</value>
        public string StaticResourceURL { get; set; }
    }
}