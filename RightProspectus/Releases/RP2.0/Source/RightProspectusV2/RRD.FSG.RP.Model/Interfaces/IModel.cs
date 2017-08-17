// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Interfaces
{
    #region IModel<TKey>

    /// <summary>
    /// Base interface for all entity models.
    /// </summary>
    /// <typeparam name="TKey">Primary key identifier type parameter.</typeparam>
    public interface IModel<TKey>
        : IModel
    {
        #region Properties

        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        TKey Key { get; }

        #endregion
    }

    #endregion

    #region IModel

    /// <summary>
    /// Base interface for all entity models.
    /// </summary>
    public interface IModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the entity.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }

        #endregion
    }

    #endregion
}
