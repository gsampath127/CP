﻿// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model.Cache
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using System;

namespace RRD.FSG.RP.Model.Cache.System
{
    /// <summary>
    /// Factory cache for <see cref="Roles" /> entities.
    /// </summary>
    public class RolesFactoryCache
        : BaseFactoryCache<RolesFactory, RolesObjectModel, int>
    {
        #region Constructors



        /// <summary>
        /// Initializes a new instance of the <see cref="RolesFactoryCache" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public RolesFactoryCache(IDataAccess dataAccess)
            : base(new RolesFactory(dataAccess)) { }



        #endregion

        #region Public Properties

        /// <summary>
        /// Defines the collection cache key to be used for the dictionary stored in cache that holds all the retrieved items.
        /// </summary>
        /// <value>The collection cache key.</value>
        public override string CollectionCacheKey
        {
            get { return string.Format("{0}:{1}", "RolesFactoryCacheKey", (this.Factory.ConnectionString ?? string.Empty).GetHashCode()); }
        }

        /// <summary>
        /// Defines the default sliding expiration to be used for the entities returned by the underlying factory.
        /// </summary>
        /// <value>The collection dependency check stored procedure.</value>
        public override string CollectionDependencyCheckStoredProcedure
        {
            get { return "RPV2HostedSites_StaticRolesData_CacheDependencyCheck"; }
        }

        /// <summary>
        /// Defines the default sliding expiration to be used for the entities returned by the underlying factory. Defaults to one day.
        /// </summary>
        /// <value>The sliding expiration.</value>
        public override TimeSpan SlidingExpiration
        {
            get { return TimeSpan.FromMinutes(30); }
        }

        #endregion
    }
}
