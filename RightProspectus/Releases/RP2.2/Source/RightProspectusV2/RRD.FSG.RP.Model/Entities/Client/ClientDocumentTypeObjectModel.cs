// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************

using System;


/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class ClientDocumentTypeObjectModel.
    /// </summary>
    public class ClientDocumentTypeObjectModel : AuditedBaseModel<int>, IComparable<ClientDocumentTypeObjectModel>
    {
        /// <summary>
        /// DocumentTypeId
        /// </summary>
        /// <value>The client document type identifier.</value>
        public int ClientDocumentTypeId { get; set; }
        /// <summary>
        /// Gets or sets the HostedDocumentsDisplayCount .
        /// </summary>
        /// <value>The HostedDocumentsDisplayCount .</value>
        public int HostedDocumentsDisplayCount { get; set; }
        /// <summary>
        /// Gets or sets the FTPName .
        /// </summary>
        /// <value>The FTPName .</value>
        public string FTPName { get; set; }
        /// <summary>
        /// Gets or sets the FTPUsername .
        /// </summary>
        /// <value>The FTPUsername .</value>
        public string FTPUsername { get; set; }
        /// <summary>
        /// Gets or sets the FTPPassword .
        /// </summary>
        /// <value>The FTPPassword .</value>
        public string FTPPassword { get; set; }
        /// <summary>
        /// Gets or sets the IsSFTP .
        /// </summary>
        /// <value>The IsSFTP .</value>
        public bool IsSFTP { get; set; }
        
        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>

        public int CompareTo(ClientDocumentTypeObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
