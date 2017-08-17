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
