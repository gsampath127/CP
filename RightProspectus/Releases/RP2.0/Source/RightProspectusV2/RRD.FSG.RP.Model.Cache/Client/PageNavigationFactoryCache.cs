// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model.Cache
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-27-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using System;

namespace RRD.FSG.RP.Model.Cache.Client
{
    /// <summary>
    /// Class PageNavigationFactoryCache.
    /// </summary>
    public class PageNavigationFactoryCache : 
        BaseFactoryCache<PageNavigationFactory, PageNavigationObjectModel, PageNavigationKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageNavigationFactoryCache" /> class.
        /// </summary>
        /// <param name="dataAccess">The data access.</param>
        public PageNavigationFactoryCache(IDataAccess dataAccess) :
            base(new PageNavigationFactory(dataAccess))
        {

        }

        /// <summary>
        /// Defines the collection cache key to be used for the dictionary stored in cache that holds all the retrieved items.
        /// </summary>
        /// <value>The collection cache key.</value>
        public override string CollectionCacheKey
        {
            get { return string.Format("{0}:{1}", "PageNavigationFactoryCacheKey", (this.Factory.ConnectionString ?? string.Empty).GetHashCode()); }
        }

        /// <summary>
        /// Defines the stored procedure (parameterless) that is used to build the query notification used by sql cache dependency feature of HttpCache object.
        /// </summary>
        /// <value>The collection dependency check stored procedure.</value>
        public override string CollectionDependencyCheckStoredProcedure
        {
            get { return "RPV2HostedAdmin_PageNavigationData_CacheDependencyCheck"; }
        }

        /// <summary>
        /// Defines the default sliding expiration to be used for the entities returned by the underlying factory. Defaults to one day.
        /// </summary>
        /// <value>The sliding expiration.</value>
        public override TimeSpan SlidingExpiration
        {
            get { return TimeSpan.FromMinutes(30); }
        }
    }
}
