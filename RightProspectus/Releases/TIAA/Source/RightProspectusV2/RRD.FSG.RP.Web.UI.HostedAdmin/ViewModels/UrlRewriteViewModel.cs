
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class UrlRewriteViewModel.
    /// </summary>
    public class UrlRewriteViewModel
    {
        /// <summary>
        /// Gets or sets the UrlRewrite identifier.
        /// </summary>
        /// <value>The UrlRewriteId identifier.</value>
        public int UrlRewriteId { get; set; }
        /// <summary>
        /// Gets or sets the MatchPattern.
        /// </summary>
        /// <value>The MatchPatternr.</value>
        public string MatchPattern { get; set; }
        /// <summary>
        /// Gets or sets the RewriteFormat.
        /// </summary>
        /// <value>The RewriteFormat.</value>
        public string RewriteFormat { get; set; }
        /// <summary>
        /// Gets or sets the PatternName.
        /// </summary>
        /// <value>The PatternName.</value>
        public string PatternName { get; set; }

       
    }
}