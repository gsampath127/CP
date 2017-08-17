// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ************************************************************************

namespace RRD.FSG.RP.Model.Interfaces
{
    #region ISearchDetailCopyAs<TSearchDetail, TModel>

    /// <summary>
    /// Creates a means to copy search details from one entity to a newly created entity.
    /// </summary>
    /// <typeparam name="TSearchDetail">Type of entity to create.</typeparam>
    /// <typeparam name="TModel">Type of entity the search detail is for.</typeparam>
    public interface ISearchDetailCopyAs<TSearchDetail, TModel>
        : ISearchDetailCopyAs<TSearchDetail>
        where TSearchDetail : ISearchDetail<TModel>
        where TModel : IModel
    {
        #region Methods

        /// <summary>
        /// Instantiates a new entity and copies the properties from this entity to the new one.
        /// </summary>
        /// <typeparam name="TCopy">Type of entity to create.</typeparam>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <returns>A new entity with the search details copied.</returns>
        TCopy CopyAs<TCopy, TEntity>()
            where TCopy : TSearchDetail, new()
            where TEntity : TModel;

        #endregion
    }

    #endregion

    #region ISearchDetailCopyAs<TSearchDetail>

    /// <summary>
    /// Creates a means to copy search details from one entity to a newly created entity.
    /// </summary>
    /// <typeparam name="TSearchDetail">Type of entity to create.</typeparam>
    public interface ISearchDetailCopyAs<TSearchDetail>
        where TSearchDetail : ISearchDetail
    {
        #region Methods

        /// <summary>
        /// Instantiates a new entity and copies the properties from this entity to the new one.
        /// </summary>
        /// <typeparam name="TCopy">Type of entity to create.</typeparam>
        /// <returns>A new entity with the search details copied.</returns>
        TCopy CopyAs<TCopy>()
            where TCopy : TSearchDetail, new();

        #endregion
    }

#endregion
}
