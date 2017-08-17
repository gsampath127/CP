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
    /// <typeparam name="TModel">Type of entity to search.</typeparam>
    public class SearchDetail<TModel>
        : SearchDetail, ISearchDetail<TModel>, ISearchDetailCopyAs<ISearchDetail<TModel>, TModel>
        where TModel : IModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchDetail{TModel}"/> class.
        /// </summary>
        public SearchDetail()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchDetail" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public SearchDetail(ISearchDetail source)
            : base(source)
        { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public virtual new Func<TModel, bool> SearchPredicate
        {
            get { return entity => base.SearchPredicate(entity); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Searches a collection of entities using the search parameters set in this search detail instance.
        /// </summary>
        /// <param name="source">Source collection to search.</param>
        /// <returns>A collection of entities that match the search criteria.</returns>
        public IEnumerable<TModel> Search(IEnumerable<TModel> source)
        {
            return source.Where(this.SearchPredicate);
        }

        /// <summary>
        /// Searches a collection of entities using the search parameters set in this search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search.</typeparam>
        /// <param name="source">Source collection to search.</param>
        /// <returns>A collection of entities that match the search criteria.</returns>
        public new IEnumerable<TEntity> Search<TEntity>(IEnumerable<TEntity> source)
            where TEntity : TModel
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
        public virtual TCopy CopyAs<TCopy, TEntity>()
            where TCopy : ISearchDetail<TModel>, new()
            where TEntity : TModel
        {
            return this.CopyAs<TCopy>();
        }

        /// <summary>
        /// Instantiates a new entity and copies the properties from this entity to the new one.
        /// </summary>
        /// <typeparam name="TCopy">Type of entity to create.</typeparam>
        /// <returns>A new entity with the search details copied.</returns>
        public virtual new TCopy CopyAs<TCopy>()
            where TCopy : ISearchDetail<TModel>, new()
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
            return (this as SearchDetail).CopyAs<SearchDetail<IModel>>().Search<TEntity>(source);
        }

        #endregion
    }

    /// <summary>
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class SearchDetail
        : ISearchDetail, ISearchDetailCopyAs<ISearchDetail>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchDetail"/> class.
        /// </summary>
        public SearchDetail()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchDetail"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public SearchDetail(ISearchDetail source)
        {
            this.Name = source.Name;
            this.NameCompare = source.NameCompare;
            this.Description = source.Description;
            this.DescriptionCompare = source.DescriptionCompare;
        }

        #endregion

        #region Search Properties

        /// <summary>
        /// Gets or sets the name of the entities being searched.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Determines the type of comparison for the Name property.
        /// </summary>
        /// <value>The name compare.</value>
        public TextCompare NameCompare { get; set; }

        /// <summary>
        /// Gets or sets the description of the entities being searched.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Determines the type of comparison for the Description property.
        /// </summary>
        /// <value>The description compare.</value>
        public TextCompare DescriptionCompare { get; set; }

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public Func<IModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    this.Match(this.Name, entity.Name, this.NameCompare)
                    && this.Match(this.Description, entity.Description, this.DescriptionCompare);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns an enumeration of DbParameter entities based on values set in th underlying search detail entity.
        /// </summary>
        /// <param name="dataAccess">Data access instance used to create the parameters.</param>
        /// <returns>A collection of DbParameter entities.</returns>
        public virtual IEnumerable<DbParameter> GetSearchParameters(IDataAccess dataAccess)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            if (this.Name != null)
            {
                parameters.Add(dataAccess.CreateParameter("Name", DbType.String, this.Name));
                parameters.Add(dataAccess.CreateParameter("NameCompare", DbType.Int32, this.NameCompare));
            }

            if (this.Description != null)
            {
                parameters.Add(dataAccess.CreateParameter("Description", DbType.String, this.Description));
                parameters.Add(dataAccess.CreateParameter("DescriptionCompare", DbType.Int32, this.DescriptionCompare));
            }

            return parameters;
        }

        /// <summary>
        /// Creates a new search detail and copies the parameters from this one to it.
        /// </summary>
        /// <typeparam name="TCopy">Type of search detail to create.</typeparam>
        /// <returns>A new search detail with the same search parameters as this one.</returns>
        public virtual TCopy CopyAs<TCopy>()
            where TCopy : ISearchDetail, new()
        {
            TCopy copy = new TCopy();
            copy.Name = this.Name;
            copy.NameCompare = this.NameCompare;
            copy.Description = this.Description;
            copy.DescriptionCompare = this.DescriptionCompare;
            return copy;
        }

        /// <summary>
        /// Searches a collection of entities using only search parameters exposed by ISearchDetail, set in this search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search.</typeparam>
        /// <param name="source">Source collection to search.</param>
        /// <returns>A collection of entities that match the search criteria.</returns>
        public IEnumerable<TEntity> Search<TEntity>(IEnumerable<TEntity> source)
            where TEntity : IModel
        {
            return from entity in source
                   where this.SearchPredicate(entity)
                   select entity;
        }

        #endregion

        #region Protected Internal Methods

        #region Match

        /// <summary>
        /// Performs a match test on two values using the compare type passed in.
        /// </summary>
        /// <param name="localValue">Local value to match.</param>
        /// <param name="entityValue">Entity value to match.</param>
        /// <param name="type">Type of comparison to make.</param>
        /// <returns>True if the comparison is valid, false otherwise.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">type</exception>
        protected internal bool Match(string localValue, string entityValue, TextCompare type)
        {
            if (localValue == null)
            {
                return true;
            }

            if (entityValue == null)
            {
                return false;
            }

            switch (type)
            {
                case TextCompare.Equal:
                case TextCompare.NotEqual:
                    return this.MatchBasic(localValue.ToLower(), entityValue.ToLower(), (BasicCompare)type);
                case TextCompare.GreaterThan:
                case TextCompare.GreaterThanOrEqual:
                case TextCompare.LessThan:
                case TextCompare.LessThanOrEqual:
                    return this.MatchValue(localValue, entityValue, (ValueCompare)type);
                case TextCompare.Contains:
                    return entityValue.Contains(localValue);
                case TextCompare.NotContains:
                    return !entityValue.Contains(localValue);
                case TextCompare.StartsWith:
                    return entityValue.StartsWith(localValue);
                case TextCompare.NotStartsWith:
                    return !entityValue.StartsWith(localValue);
                case TextCompare.EndsWith:
                    return entityValue.EndsWith(localValue);
                case TextCompare.NotEndsWith:
                    return !entityValue.EndsWith(localValue);
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Invalid type for string comparison: {0}", type));
            }
        }

        /// <summary>
        /// Performs a match test on two values using the compare type passed in.
        /// </summary>
        /// <typeparam name="T">Type of values to match.</typeparam>
        /// <param name="localValue">Local value to match.</param>
        /// <param name="entityValue">Entity value to match.</param>
        /// <param name="type">Type of comparison to make.</param>
        /// <returns>True if the comparison is valid, false otherwise.</returns>
        protected internal bool Match<T>(T? localValue, T? entityValue, ValueCompare type)
            where T : struct, IComparable<T>
        {
            if (!localValue.HasValue)
            {
                return true;
            }

            if (!entityValue.HasValue)
            {
                return false;
            }

            return this.MatchValue(localValue.Value, entityValue.Value, type);
        }

        /// <summary>
        /// Performs a match test on two values using the compare type passed in.
        /// </summary>
        /// <typeparam name="T">Type of values to match.</typeparam>
        /// <param name="localValue">Local value to match.</param>
        /// <param name="entityValue">Entity value to match.</param>
        /// <param name="type">Type of comparison to make.</param>
        /// <returns>True if the comparison is valid, false otherwise.</returns>
        protected internal bool Match<T>(T localValue, T entityValue, ValueCompare type)
            where T : IComparable<T>
        {
            if (ReferenceEquals(localValue, null))
            {
                return true;
            }

            if (ReferenceEquals(entityValue, null))
            {
                return false;
            }

            return this.MatchValue(localValue, entityValue, type);
        }

        /// <summary>
        /// Performs a match test on two values using the compare type passed in.
        /// </summary>
        /// <typeparam name="T">Type of values to match.</typeparam>
        /// <param name="localValue">Local value to match.</param>
        /// <param name="entityValue">Entity value to match.</param>
        /// <param name="type">Type of comparison to make.</param>
        /// <returns>True if the comparison is valid, false otherwise.</returns>
        protected internal bool Match<T>(T localValue, T entityValue, BasicCompare type)
        {
            if (ReferenceEquals(localValue, null))
            {
                return true;
            }

            if (ReferenceEquals(entityValue, null))
            {
                return false;
            }

            return this.MatchBasic(localValue, entityValue, type);
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs a match test on two values using the compare type passed in.
        /// </summary>
        /// <typeparam name="T">Type of values to match.</typeparam>
        /// <param name="localValue">Local value to match.</param>
        /// <param name="entityValue">Entity value to match.</param>
        /// <param name="type">Type of comparison to make.</param>
        /// <returns>True if the comparison is valid, false otherwise.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">type</exception>
        private bool MatchBasic<T>(T localValue, T entityValue, BasicCompare type)
        {
            switch (type)
            {
                case BasicCompare.Equal:
                    return object.Equals(localValue, entityValue);
                case BasicCompare.NotEqual:
                    return !object.Equals(localValue, entityValue);
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Invalid search type for basic comparison: {0}", type));
            }
        }

        /// <summary>
        /// Performs a match test on two values using the compare type passed in.
        /// </summary>
        /// <typeparam name="T">Type of values to match.</typeparam>
        /// <param name="localValue">Local value to match.</param>
        /// <param name="entityValue">Entity value to match.</param>
        /// <param name="type">Type of comparison to make.</param>
        /// <returns>True if the comparison is valid, false otherwise.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">type</exception>
        private bool MatchValue<T>(T localValue, T entityValue, ValueCompare type)
            where T : IComparable<T>
        {
            switch (type)
            {
                case ValueCompare.Equal:
                case ValueCompare.NotEqual:
                    return this.MatchBasic(localValue, entityValue, (BasicCompare)type);
                case ValueCompare.GreaterThan:
                    return entityValue.CompareTo(localValue) > 0;
                case ValueCompare.GreaterThanOrEqual:
                    return entityValue.CompareTo(localValue) >= 0;
                case ValueCompare.LessThan:
                    return entityValue.CompareTo(localValue) < 0;
                case ValueCompare.LessThanOrEqual:
                    return entityValue.CompareTo(localValue) <= 0;
                default:
                    throw new ArgumentOutOfRangeException("type", string.Format("Invalid search type for value comparison: {0}", type));
            }
        }

        #endregion
    }
}
