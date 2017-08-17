// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-29-2015
// ***********************************************************************

using System;
using System.Collections.Generic;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class ClientDocumentGroupObjectModel.
    /// </summary>
    public class ClientDocumentGroupObjectModel : AuditedBaseModel<int>, IComparable<ClientDocumentGroupObjectModel>
    {

        #region Entity Properties

        /// <summary>
        /// Gets or sets the ClientDocumentGroupId identifier.
        /// </summary>
        /// <value>The ClientDocumentGroupId identifier.</value>
        public int ClientDocumentGroupId { get; set; }

        /// <summary>
        /// Gets or sets the ParentClientDocumentGroupId.
        /// </summary>
        /// <value>The ParentClientDocumentGroupId.</value>
        public int? ParentClientDocumentGroupId { get; set; }

        /// <summary>
        /// Gets or sets the CssClass.
        /// </summary>
        /// <value>The CssClass.</value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the ClientDocuments.
        /// </summary>
        /// <value>The ClientDocument.</value>
        public List<ClientDocumentObjectModel> ClientDocuments { get; set; }

        #endregion

        /// <summary>
        /// Compares the two Site entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(ClientDocumentGroupObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

    }
}
