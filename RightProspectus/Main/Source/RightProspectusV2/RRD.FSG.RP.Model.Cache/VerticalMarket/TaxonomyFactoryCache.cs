﻿// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model.Cache
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using System;


namespace RRD.FSG.RP.Model.Cache.VerticalMarket
{
    /// <summary>
    /// Factory cache for <see cref="Client" /> entities.
    /// </summary>
    public class TaxonomyFactoryCache
        : BaseFactoryCache<TaxonomyFactory, TaxonomyObjectModel,TaxonomyKey>
    {
        #region Constructors



        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomyFactoryCache" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public TaxonomyFactoryCache(IDataAccess dataAccess)
            : base(new TaxonomyFactory(dataAccess)) { }



        #endregion

        #region Public Properties

        /// <summary>
        /// Defines the collection cache key to be used for the dictionary stored in cache that holds all the retrieved items.
        /// </summary>
        /// <value>The collection cache key.</value>
        public override string CollectionCacheKey
        {
            get { return string.Format("{0}:{1}", "TaxonomyFactoryCacheKey", (this.Factory.ConnectionString ?? string.Empty).GetHashCode()); }
        }

        /// <summary>
        /// Defines the default sliding expiration to be used for the entities returned by the underlying factory.
        /// </summary>
        /// <value>The collection dependency check stored procedure.</value>
        public override string CollectionDependencyCheckStoredProcedure
        {
            get { return "RPV2HostedAdmin_TaxonomyData_CacheDependencyCheck"; }
        }

        /// <summary>
        /// Defines the default sliding expiration to be used for the entities returned by the underlying factory. Defaults to one day.
        /// </summary>
        /// <value>The sliding expiration.</value>
        public override TimeSpan SlidingExpiration
        {
            get { return TimeSpan.FromHours(1); }
        }

        #endregion
    }
}
