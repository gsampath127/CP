// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model.Cache
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using System;


namespace RRD.FSG.RP.Model.Cache.Client
{
    /// <summary>
    /// Class VerticalXmlExportFactoryCache.
    /// </summary>
    public class VerticalXmlExportFactoryCache: BaseFactoryCache<VerticalXmlExportFactory, VerticalXmlExportObjectModel, int>
    {
        #region Constructors



        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalXmlExportFactoryCache" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public VerticalXmlExportFactoryCache(IDataAccess dataAccess)
            : base(new VerticalXmlExportFactory(dataAccess)) { }



        #endregion

        #region Public Properties

        /// <summary>
        /// Defines the collection cache key to be used for the dictionary stored in cache that holds all the retrieved items.
        /// </summary>
        /// <value>The collection cache key.</value>
        public override string CollectionCacheKey
        {
            get { return string.Format("{0}:{1}", "VerticalXmlExportFactoryCacheKey", (this.Factory.ConnectionString ?? string.Empty).GetHashCode()); }
        }

        /// <summary>
        /// Defines the default sliding expiration to be used for the entities returned by the underlying factory.
        /// </summary>
        /// <value>The collection dependency check stored procedure.</value>
        public override string CollectionDependencyCheckStoredProcedure
        {
            get { return "RPV2HostedAdmin_VerticalXmlExportData_CacheDependencyCheck"; }
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
