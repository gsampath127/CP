// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace RRD.FSG.RP.Model.SearchEntities.System
{
    /// <summary>
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class ClientSearchDetail
        : AuditedSearchDetail<ClientObjectModel>, ISearchDetailCopyAs<ClientSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>The client identifier.</value>
        public int? ClientID { get; set; }
        /// <summary>
        /// Determines the type of comparison for the ClientID property.
        /// </summary>
        /// <value>The client identifier compare.</value>
        public ValueCompare ClientIDCompare { get; set; }
        /// <summary>
        /// Gets the name of the associated connection string of the clients being searched.
        /// </summary>
        /// <value>The name of the client connection string.</value>
        public string ClientConnectionStringName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the ClientConnectionStringName property..
        /// </summary>
        /// <value>The client connection string name compare.</value>
        public TextCompare ClientConnectionStringNameCompare { get; set; }
        /// <summary>
        /// Gets the name of the database of the clients being searched.
        /// </summary>
        /// <value>The name of the client database.</value>
        public string ClientDatabaseName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the ClientDatabaseName property..
        /// </summary>
        /// <value>The client database name compare.</value>
        public TextCompare ClientDatabaseNameCompare { get; set; }
        /// <summary>
        /// Gets the name of the database of the clients being searched.
        /// </summary>
        /// <value>The name of the client.</value>
        public string ClientName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the ClientName property..
        /// </summary>
        /// <value>The client name compare.</value>
        public TextCompare ClientNameCompare { get; set; }
        /// <summary>
        /// Gets or sets the vertical market identifier.
        /// </summary>
        /// <value>The vertical market identifier.</value>
        public int? VerticalMarketId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the VerticalMarketId property.
        /// </summary>
        /// <value>The vertical market identifier compare.</value>
        public ValueCompare VerticalMarketIdCompare { get; set; }

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<ClientObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.ClientID,entity.ClientID,this.ClientIDCompare)
                    && this.Match(this.ClientName,entity.ClientName,this.ClientNameCompare)
                    && this.Match(this.ClientConnectionStringName,entity.ClientConnectionStringName,this.ClientConnectionStringNameCompare)
                    && this.Match(this.ClientDatabaseName,entity.ClientDatabaseName,this.ClientDatabaseNameCompare)
                    && this.Match(this.VerticalMarketId,entity.VerticalMarketId,this.VerticalMarketIdCompare);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns an enumeration of DbParameter entities based on values set in th underlying search detail entity.
        /// </summary>
        /// <param name="dataAccess">Data access instance used to create the parameters.</param>
        /// <returns>A collection of DbParameter entities.</returns>
        public override IEnumerable<DbParameter> GetSearchParameters(IDataAccess dataAccess)
        {
            List<DbParameter> parameters = base.GetSearchParameters(dataAccess) as List<DbParameter>;
            if (!string.IsNullOrWhiteSpace(this.ClientConnectionStringName))
            {
                parameters.Add(dataAccess.CreateParameter("ClientConnectionStringName", DbType.String, this.ClientConnectionStringName));
            }

            if (!string.IsNullOrWhiteSpace(this.ClientDatabaseName))
            {
                parameters.Add(dataAccess.CreateParameter("ClientDatabaseName", DbType.String, this.ClientDatabaseName));
            }

            if (this.VerticalMarketId.HasValue)
            {
                parameters.Add(dataAccess.CreateParameter("VerticalMarketId", DbType.Int32, this.VerticalMarketId.Value));
            }

            return parameters;
        }

        /// <summary>
        /// Creates a new search detail and copies the parameters from this one to it.
        /// </summary>
        /// <typeparam name="TCopy">Type of search detail to create.</typeparam>
        /// <returns>A new search detail with the same search parameters as this one.</returns>
        public virtual new TCopy CopyAs<TCopy>()
            where TCopy : ClientSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.ClientID = this.ClientID;
            copy.ClientIDCompare = this.ClientIDCompare;
            copy.ClientName = this.ClientName;
            copy.ClientNameCompare = this.ClientNameCompare;
            copy.ClientConnectionStringName = this.ClientConnectionStringName;
            copy.ClientConnectionStringNameCompare = this.ClientConnectionStringNameCompare;
            copy.ClientDatabaseName = this.ClientDatabaseName;
            copy.ClientDatabaseNameCompare = this.ClientDatabaseNameCompare;
            copy.VerticalMarketId = this.VerticalMarketId;
            copy.VerticalMarketIdCompare = this.VerticalMarketIdCompare;
            return copy;
        }

        #endregion
    }
}
