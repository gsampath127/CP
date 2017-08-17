
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Display Value Pair for binding dropdowns, list boxes etc
    /// </summary>
    public class DisplayValuePair
    {
        /// <summary>
        /// Property that will be used to display the text
        /// </summary>
        /// <value>The display.</value>
        public string Display { get; set; }
        /// <summary>
        /// Property that will be used to hold value of shown text
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }
        /// <summary>
        /// Property to determine if value is selected
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public bool Selected { get; set; }

    }
}