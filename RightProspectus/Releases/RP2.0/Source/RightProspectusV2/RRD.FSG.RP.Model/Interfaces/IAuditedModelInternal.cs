// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using System;

namespace RRD.FSG.RP.Model.Interfaces
{
    /// <summary>
    /// Internal interface for all audited entity types. Used by factories to set read only properties related to last modification of the entity.
    /// </summary>
    internal interface IAuditedModelInternal
    {
        /// <summary>
        /// Sets the last modified date property of the entity. Date must be UTC.
        /// </summary>
        /// <param name="utcLastModified">UTC date to set as last modified date.</param>
        void SetLastModified(DateTime? utcLastModified);

        /// <summary>
        /// Set the modified by property of the entity.
        /// </summary>
        /// <param name="modifiedBy">Integer identifier of the user who last modified this entity.</param>
        void SetModifiedBy(int? modifiedBy);
    }
}
