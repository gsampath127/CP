// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************


/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Class HostedTemplateNavigation.
    /// </summary>
    public class HostedTemplateNavigation
    {
        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateID { get; set; }

        /// <summary>
        /// Gets or sets the navigation key.
        /// </summary>
        /// <value>The navigation key.</value>
        public string NavigationKey { get; set; }
       
        /// <summary>
        /// Gets or sets the XSL transform.
        /// </summary>
        /// <value>The XSL transform.</value>
        public string XslTransform { get; set; }
        
        /// <summary>
        /// Gets or sets the default navigation XML.
        /// </summary>
        /// <value>The default navigation XML.</value>
        public string DefaultNavigationXml { get; set; }
    }
}
