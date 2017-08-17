// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

using System.Collections.Generic;

/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Class ClientDataFromSystem.
    /// </summary>
    public class ClientDataFromSystem
    {
        /// <summary>
        /// Gets or sets the client database connections.
        /// </summary>
        /// <value>The client database connections.</value>
        public List<ClientDbConnection> ClientDbConnections { get; set; }

        /// <summary>
        /// Gets or sets the template pages.
        /// </summary>
        /// <value>The template pages.</value>
        public List<HostedTemplatePage> TemplatePages { get; set; }

        /// <summary>
        /// Gets or sets the hosted template navigations.
        /// </summary>
        /// <value>The hosted template navigations.</value>
        public List<HostedTemplateNavigation> HostedTemplateNavigations { get; set; }

        /// <summary>
        /// Gets or sets the hosted template page navigations.
        /// </summary>
        /// <value>The hosted template page navigations.</value>
        public List<HostedTemplatePageNavigation> HostedTemplatePageNavigations { get; set; }
    }
}
