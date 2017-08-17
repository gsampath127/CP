// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RRD.FSG.RP.Model.Interfaces
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    /// <typeparam name="TModel">Type of entity the sort details are for.</typeparam>
    public interface ISortDetail<TModel>
        : ISortDetail, IComparer<TModel>
        where TModel : IModel
    {
        #region Methods

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        IEnumerable<TModel> Sort(IEnumerable<TModel> source);

        #endregion
    }

    /// <summary>
    /// Defines sort properties for a particular entity class.
    /// </summary>
    public interface ISortDetail
    {
        #region Properties

        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        SortColumn Column { get; set; }

        /// <summary>
        /// Order of the sort.
        /// </summary>
        /// <value>The order.</value>
        SortOrder Order { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new instance of a sort detail and copies the sort details to the new entity.
        /// </summary>
        /// <typeparam name="TSortDetail">Type of sort detail to create.</typeparam>
        /// <returns>A new instance of a sort detail entity with the sort properties copied.</returns>
        TSortDetail CopyAs<TSortDetail>()
            where TSortDetail : ISortDetail, new();

        #endregion
    }
}
