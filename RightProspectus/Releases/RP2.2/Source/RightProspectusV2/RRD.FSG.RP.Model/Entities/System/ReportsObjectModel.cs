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
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Class ReportsObjectModel.
    /// </summary>
    public class ReportsObjectModel : BaseModel<int>, IComparable<ReportsObjectModel>
    {
        /// <summary>
        /// Primary key identifier of the report entity.
        /// </summary>
        /// <value>The report identifier.</value>
        public int ReportId { get; set; }

        /// <summary>
        /// Compares the two Reports entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(ReportsObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
