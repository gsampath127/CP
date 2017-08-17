// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Interfaces;
using System;

/// <summary>
/// The Entities namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities
{
    /// <summary>
    /// Base class for all audited entity models.
    /// </summary>
    /// <typeparam name="TKey">Primary key identifier type parameter.</typeparam>
    public abstract class AuditedBaseModel<TKey>
        : BaseModel<TKey>, IAuditedModel<TKey>, IAuditedModelInternal
    {
        /// <summary>
        /// Gets the last modified date of the entity. Must be UTC.
        /// </summary>
        /// <value>The last modified.</value>
        public DateTime? LastModified { get; internal set; }

        /// <summary>
        /// Gets the identifier of the user who last modified the entity.
        /// </summary>
        /// <value>The modified by.</value>
        public int? ModifiedBy { get; internal set; }

        /// <summary>
        /// Sets the last modified date property of the entity. Date must be UTC.
        /// </summary>
        /// <param name="utcLastModified">UTC date to set as last modified date.</param>
        void IAuditedModelInternal.SetLastModified(DateTime? utcLastModified)
        {
            this.LastModified = utcLastModified;
        }

        /// <summary>
        /// Set the modified by property of the entity.
        /// </summary>
        /// <param name="modifiedBy">Integer identifier of the user who last modified this entity.</param>
        void IAuditedModelInternal.SetModifiedBy(int? modifiedBy)
        {
            this.ModifiedBy = modifiedBy;
        }
    }
}
