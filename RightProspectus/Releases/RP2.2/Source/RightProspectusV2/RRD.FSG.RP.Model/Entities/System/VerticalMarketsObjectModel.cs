// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

using System;


/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Class VerticalMarketsObjectModel.
    /// </summary>
    public class VerticalMarketsObjectModel :AuditedBaseModel<int>, IComparable<VerticalMarketsObjectModel>
    {
        /// <summary>
        /// VerticalMarketId
        /// </summary>
        /// <value>The vertical market identifier.</value>
        public int VerticalMarketId { get; set; }

        /// <summary>
        /// MarketName
        /// </summary>
        /// <value>The name of the market.</value>
        public string MarketName { get; set; }

        /// <summary>
        /// ConnectionStringName
        /// </summary>
        /// <value>The name of the connection string.</value>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// DatabaseName
        /// </summary>
        /// <value>The name of the database.</value>
        public string DatabaseName { get; set; }

        /// <summary>
        /// MarketDescription
        /// </summary>
        /// <value>The market description.</value>
        public string MarketDescription { get; set; }

        /// <summary>
        /// Compares the two Site entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(VerticalMarketsObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
