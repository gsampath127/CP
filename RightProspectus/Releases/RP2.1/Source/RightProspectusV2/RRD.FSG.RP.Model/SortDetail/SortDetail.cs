// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RRD.FSG.RP.Model
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    /// <typeparam name="TModel">Type of entity the sort details are for.</typeparam>
    public class SortDetail<TModel>
        : ISortDetail<TModel>
        where TModel : IModel
    {
        #region Public Properties

        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual SortColumn Column { get; set; }

        /// <summary>
        /// Order of the sort.
        /// </summary>
        /// <value>The order.</value>
        public SortOrder Order { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public virtual IEnumerable<TModel> Sort(IEnumerable<TModel> source)
        {
            switch (this.Column)
            {
                case SortColumn.Unspecified:
                    return source;
                case SortColumn.Name:
                    return this.Sort(source, entity => entity.Name);
                case SortColumn.Description:
                    return this.Sort(source, entity => entity.Description);
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Cannot sort by SortColumn with value of {0}", this.Column));

            }
        }

        /// <summary>
        /// Compares two entities using teh sort properties of this instance.
        /// </summary>
        /// <param name="x">First entity to compare.</param>
        /// <param name="y">Second entity to compare.</param>
        /// <returns>A negative number if x is less than y, a positive number if x is greater than y, and 0 if they are the same.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public virtual int Compare(TModel x, TModel y)
        {
            switch (this.Column)
            {
                case SortColumn.Name:
                    return this.Compare(x.Name, x.Name);
                case SortColumn.Description:
                    return this.Compare(x.Description, y.Description);
                case SortColumn.Unspecified:
                    return 0;
                default:
                    throw new InvalidOperationException(string.Format("Compare not valid with sort column of {0}", this.Column));
            }
        }

        /// <summary>
        /// Creates a new instance of a sort detail and copies the sort details to the new entity.
        /// </summary>
        /// <typeparam name="TSortDetail">Type of sort detail to create.</typeparam>
        /// <returns>A new instace of a sort detail entity with the sort properties copied.</returns>
        public TSortDetail CopyAs<TSortDetail>()
            where TSortDetail : ISortDetail, new()
        {
            return new TSortDetail { Column = this.Column, Order = this.Order };
        }

        #endregion

        #region Protected Internal Methods

        /// <summary>
        /// Sorts a collection either ascending or descending (dependent on the instance setting) using the passed in selector function.
        /// </summary>
        /// <typeparam name="TOrderKey">Type of value to order with.</typeparam>
        /// <param name="source">Collection to order.</param>
        /// <param name="keySelector">Key selector delegate function used to order the collection.</param>
        /// <returns>An ordered collection of the entities passed in.</returns>
        protected internal IEnumerable<TModel> Sort<TOrderKey>(IEnumerable<TModel> source, Func<TModel, TOrderKey> keySelector)
        {
            switch (this.Order)
            {
                case SortOrder.Descending:
                    return source.OrderByDescending(keySelector);
                default:
                    return source.OrderBy(keySelector);
            }
        }

        /// <summary>
        /// Compares two values taking into account sort order for the instance.
        /// </summary>
        /// <typeparam name="T">Type of values to compare.</typeparam>
        /// <param name="x">First item to compare.</param>
        /// <param name="y">Second item to compare.</param>
        /// <returns>the results of the CompareTo implementation fo the comparable type, negated if the sort order for the instance is descending.</returns>
        protected internal int Compare<T>(T x, T y)
            where T : IComparable<T>
        {
            int compare = x.CompareTo(y);
            if (this.Order == SortOrder.Descending)
            {
                compare *= -1;
            }

            return compare;
        }

        #endregion
    }
}
