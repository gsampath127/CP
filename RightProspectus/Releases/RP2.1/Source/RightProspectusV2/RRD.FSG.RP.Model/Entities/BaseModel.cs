// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Interfaces;

/// <summary>
/// The Entities namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities
{
    /// <summary>
    /// Base class for all entity models.
    /// </summary>
    /// <typeparam name="TKey">Primary key identifier type parameter.</typeparam>
    public abstract class BaseModel<TKey>
        : IModel<TKey>
    {
        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        public virtual TKey Key { get; internal set; }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the entity.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; }
    }
}
