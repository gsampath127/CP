// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************

using System;

/// <summary>
/// The VerticalMarket namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.VerticalMarket
{
    /// <summary>
    /// Class DocumentTypeObjectModel.
    /// </summary>
    public class DocumentTypeObjectModel : BaseModel<int>, IComparable<DocumentTypeObjectModel>
    {
        /// <summary>
        /// DocumentTypeId
        /// </summary>
        /// <value>The document type identifier.</value>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// DocumentTypeName
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// DocumentTypeDescription
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// DocPriority
        /// </summary>
        /// <value>The document priority.</value>
        public int DocPriority { get; set; }

        /// <summary>
        /// MarketID
        /// </summary>
        /// <value>The market identifier.</value>
        public string MarketId { get; set; }



        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(DocumentTypeObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
