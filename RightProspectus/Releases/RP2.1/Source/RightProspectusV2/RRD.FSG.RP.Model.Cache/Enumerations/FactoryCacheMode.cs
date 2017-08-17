// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model.Cache
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

namespace RRD.FSG.RP.Model.Cache
{
    /// <summary>
    /// Determines the caching mode for a factory cache implementation.
    /// </summary>
    public enum FactoryCacheMode
    {
        /// <summary>
        /// Entities are cached as needed.
        /// </summary>
        Granular = 0,

        /// <summary>
        /// All entities are cached with one fetch from the underlying data store, either internally or from calls to CacheEntities methods.
        /// </summary>
        All = 1
    }
}
