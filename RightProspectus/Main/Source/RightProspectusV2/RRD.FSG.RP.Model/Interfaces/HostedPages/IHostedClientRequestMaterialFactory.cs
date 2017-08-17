// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.HostedPages;
using System;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.Interfaces.HostedPages
{
    /// <summary>
    /// Interface IHostedClientRequestMaterialFactory
    /// </summary>
    public interface IHostedClientRequestMaterialFactory
    {
        /// <summary>
        /// Saves the email details.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">The site.</param>
        /// <param name="emailHistoryObjectModel">The email history object model.</param>
        void SaveEmailDetails(string clientName, string site, RequestMaterialEmailHistory emailHistoryObjectModel);
        /// <summary>
        /// Saves the print details.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">The site.</param>
        /// <param name="emailPrintObjectModel">The email print object model.</param>
        void SavePrintDetails(string clientName, string site, RequestMaterialPrintHistory emailPrintObjectModel);
        /// <summary>
        /// Updates the email click date.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <returns>System.Int32.</returns>
        int UpdateEmailClickDate(string clientName, Guid uniqueId, int documentTypeId);
        /// <summary>
        /// Gets the request material print requests.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="reportFromDate">The report from date.</param>
        /// <param name="reportToDate">The report to date.</param>
        /// <returns>List&lt;RequestMaterialPrintHistory&gt;.</returns>
        List<RequestMaterialPrintHistory> GetRequestMaterialPrintRequests(string clientName, DateTime reportFromDate, DateTime reportToDate);
    }
}
