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
    /// Class ClientDocumentObjectModel.
    /// </summary>
    public class ClientDocumentObjectModel : AuditedBaseModel<int>, IComparable<ClientDocumentObjectModel>
    {
        /// <summary>
        /// ClientDocumentId
        /// </summary>
        /// <value>The client document identifier.</value>
        public int ClientDocumentId { get; set; }
        /// <summary>
        /// ClientDocumentTypeId
        /// </summary>
        /// <value>The client document type identifier.</value>
        public int ClientDocumentTypeId { get; set; }
        /// <summary>
        /// ClientDocumentTypeName
        /// </summary>
        /// <value>The name of the client document type.</value>
        public string ClientDocumentTypeName { get; set; }
        /// <summary>
        /// FileName
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }
        /// <summary>
        /// MimeType
        /// </summary>
        /// <value>The type of the MIME.</value>
        public string MimeType { get; set; }
        /// <summary>
        /// IsPrivate
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        public bool IsPrivate { get; set; }
        /// <summary>
        /// ContentUri
        /// </summary>
        /// <value>The content URI.</value>
        public string ContentUri { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        /// FileData
        /// </summary>
        /// <value>The file data.</value>
        public byte[] FileData { get; set; }

        /// <summary>
        /// Compares the two Site entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(ClientDocumentObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
