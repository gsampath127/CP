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
    #region IAuditedModel<TKey>

    /// <summary>
    /// Entity interface that defines read only audit properties.
    /// </summary>
    /// <typeparam name="TKey">Primary key identifier type parameter.</typeparam>
    public interface IAuditedModel<TKey> : IModel<TKey>, IAuditedModel
    {
    }

    #endregion

    #region IAuditedModel

    /// <summary>
    /// Entity interface that defines read only audit properties.
    /// </summary>
    public interface IAuditedModel
        : IModel
    {
        #region Properties

        /// <summary>
        /// Gets the last modified date of the entity. Must be UTC.
        /// </summary>
        /// <value>The last modified.</value>
        DateTime? LastModified { get; }

        /// <summary>
        /// Gets the identifier of the user who last modified the entity.
        /// </summary>
        /// <value>The modified by.</value>
        int? ModifiedBy { get; }

        #endregion
    }

    #endregion
}
