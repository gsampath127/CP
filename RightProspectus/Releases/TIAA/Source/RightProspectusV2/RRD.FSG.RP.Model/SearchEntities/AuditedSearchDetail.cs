// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;



namespace RRD.FSG.RP.Model
{
    /// <summary>
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    /// <typeparam name="TAuditedModel">Type of entity to search.</typeparam>
    public class AuditedSearchDetail<TAuditedModel>
        : AuditedSearchDetail, ISearchDetail<TAuditedModel>, ISearchDetailCopyAs<AuditedSearchDetail<TAuditedModel>, TAuditedModel>
        where TAuditedModel : IAuditedModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditedSearchDetail{TAuditedModel}"/> class.
        /// </summary>
        public AuditedSearchDetail()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditedSearchDetail{TAuditedModel}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public AuditedSearchDetail(AuditedSearchDetail<TAuditedModel> source)
            : base(source)
        { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public virtual new Func<TAuditedModel, bool> SearchPredicate
        {
            get { return entity => base.SearchPredicate(entity); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Searches a collection of entities using the search parameters set in this search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search.</typeparam>
        /// <param name="source">Source collection to search.</param>
        /// <returns>A collection of entities that match the search criteria.</returns>
        public new IEnumerable<TEntity> Search<TEntity>(IEnumerable<TEntity> source)
            where TEntity : TAuditedModel
        {
            return from entity in source
                   where this.SearchPredicate(entity)
                   select entity;
        }

        /// <summary>
        /// Instantiates a new entity and copies the properties from this entity to the new one.
        /// </summary>
        /// <typeparam name="TCopy">Type of entity to create.</typeparam>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <returns>A new entity with the search details copied.</returns>
        public TCopy CopyAs<TCopy, TEntity>()
            where TCopy : AuditedSearchDetail<TAuditedModel>, new()
            where TEntity : TAuditedModel
        {
            return this.CopyAs<TCopy>();
        }

        /// <summary>
        /// Instantiates a new entity and copies the properties from this entity to the new one.
        /// </summary>
        /// <typeparam name="TCopy">Type of entity to create.</typeparam>
        /// <returns>A new entity with the search details copied.</returns>
        public virtual new TCopy CopyAs<TCopy>()
            where TCopy : AuditedSearchDetail<TAuditedModel>, new()
        {
            return base.CopyAs<TCopy>();
        }

        #endregion

        #region ISearchDetail

        /// <summary>
        /// Searches a collection of entities using the search parameters set in this search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search.</typeparam>
        /// <param name="source">Source collection to search.</param>
        /// <returns>A collection of entities that match the search criteria.</returns>
        IEnumerable<TEntity> ISearchDetail.Search<TEntity>(IEnumerable<TEntity> source)
        {
            if (typeof(TEntity) == typeof(TAuditedModel))
            {
                return this.Search(source.Cast<TAuditedModel>()).Cast<TEntity>();
            }

            else if (typeof(TEntity) == typeof(IAuditedModel))
            {
                return (this as AuditedSearchDetail).Search(source.Cast<IAuditedModel>()).Cast<TEntity>();
            }

            return (this as SearchDetail).Search(source);
        }

        #endregion
    }

    /// <summary>
    /// Class AuditedSearchDetail.
    /// </summary>
    public class AuditedSearchDetail
        : SearchDetail, IAuditedSearchDetail, ISearchDetailCopyAs<IAuditedSearchDetail>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditedSearchDetail"/> class.
        /// </summary>
        public AuditedSearchDetail()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditedSearchDetail"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public AuditedSearchDetail(AuditedSearchDetail source)
            : base(source)
        {
            this.ModifiedBy = source.ModifiedBy;
            this.ModifiedByCompare = source.ModifiedByCompare;
            this.LastModified = source.LastModified;
            this.LastModifiedCompare = source.LastModifiedCompare;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the last modified date of the entities being searched. Must be UTC.
        /// </summary>
        /// <value>The modified by.</value>
        public int? ModifiedBy { get; set; }

        /// <summary>
        /// Determines the type of comparison for the ModifiedBy property.
        /// </summary>
        /// <value>The modified by compare.</value>
        public ValueCompare ModifiedByCompare { get; set; }

        /// <summary>
        /// Gets the identifier of the user who last modified the entities being searched.
        /// </summary>
        /// <value>The last modified.</value>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Determines the type of comparison for the LastModified property.
        /// </summary>
        /// <value>The last modified compare.</value>
        public ValueCompare LastModifiedCompare { get; set; }

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public new Func<IAuditedModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.ModifiedBy, entity.ModifiedBy, this.ModifiedByCompare)
                    && this.Match(this.LastModified, entity.LastModified, this.LastModifiedCompare);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns an enumeration of DbParameter entities based on values set in th underlying search detail entity.
        /// </summary>
        /// <param name="dataAccess">Data access instance used to create the parameters.</param>
        /// <returns>A collection of DbParameter entities.</returns>
        /// <exception cref="System.ArgumentException">LastModified</exception>
        public override IEnumerable<DbParameter> GetSearchParameters(IDataAccess dataAccess)
        {
            List<DbParameter> parameters = base.GetSearchParameters(dataAccess) as List<DbParameter>;
            if (this.ModifiedBy.HasValue)
            {
                parameters.Add(dataAccess.CreateParameter("ModifiedBy", DbType.Int32, this.ModifiedBy.Value));
                parameters.Add(dataAccess.CreateParameter("ModifedByCompare", DbType.Int32, this.ModifiedByCompare));
            }

            if (this.LastModified.HasValue)
            {
                if (this.LastModified.Value.Kind != DateTimeKind.Utc)
                {
                    throw new ArgumentException("LastModified", string.Format("LastModified date must be a UTC date. THe date assigned is of type {0}", this.LastModified.Value.Kind));
                }

                parameters.Add(dataAccess.CreateParameter("UtcLastModified", DbType.DateTime, this.LastModified.Value));
                parameters.Add(dataAccess.CreateParameter("UtcLastModifiedCompare", DbType.Int32, this.LastModifiedCompare));
            }

            return parameters;
        }

        /// <summary>
        /// Creates a new search detail and copies the parameters from this one to it.
        /// </summary>
        /// <typeparam name="TCopy">Type of search detail to create.</typeparam>
        /// <returns>A new search detail with the same search parameters as this one.</returns>
        public virtual new TCopy CopyAs<TCopy>()
            where TCopy : IAuditedSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.ModifiedBy = this.ModifiedBy;
            copy.ModifiedByCompare = this.ModifiedByCompare;
            copy.LastModified = this.LastModified;
            copy.LastModifiedCompare = this.LastModifiedCompare;
            return copy;
        }

        /// <summary>
        /// Searches a collection of entities using only search parameters exposed by IAuditSearchDetail, set in this search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search.</typeparam>
        /// <param name="source">Source collection to search.</param>
        /// <returns>A collection of entities that match the search criteria.</returns>
        public new IEnumerable<TEntity> Search<TEntity>(IEnumerable<TEntity> source)
            where TEntity : IAuditedModel
        {
            return from entity in source
                   where this.SearchPredicate(entity)
                   select entity;
        }

        #endregion

        #region ISearchDetail

        /// <summary>
        /// Searches a collection of entities using the search parameters set in this search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search.</typeparam>
        /// <param name="source">Source collection to search.</param>
        /// <returns>A collection of entities that match the search criteria.</returns>
        IEnumerable<TEntity> ISearchDetail.Search<TEntity>(IEnumerable<TEntity> source)
        {
            if (source is IAuditedModel)
            {
                return this.Search(source.Cast<IAuditedModel>()).Cast<TEntity>();
            }

            return (this as SearchDetail).Search(source);
        }

        #endregion
    }
}
