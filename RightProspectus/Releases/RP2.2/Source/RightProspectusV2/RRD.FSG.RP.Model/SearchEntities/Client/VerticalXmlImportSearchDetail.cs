// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-27-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RRD.FSG.RP.Model.SearchEntities.Client
{
    /// <summary>
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class VerticalXmlImportSearchDetail : SearchDetail<VerticalXmlImportObjectModel>, ISearchDetailCopyAs<VerticalXmlImportSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// FromImportDate.
        /// </summary>
        /// <value>From import date.</value>
        public DateTime? FromImportDate { get; set; }
        /// <summary>
        /// ToImportDate.
        /// </summary>
        /// <value>To import date.</value>
        public DateTime? ToImportDate { get; set; }
        /// <summary>
        /// ExportedBy.
        /// </summary>
        /// <value>The imported by.</value>
        public int? ImportedBy { get; set; }
        /// <summary>
        /// Determines the type of comparison for the ExportedBy property.
        /// </summary>
        /// <value>The imported by compare.</value>
        public ValueCompare ImportedByCompare { get; set; }
        #endregion
        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<VerticalXmlImportObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && (FromImportDate == null || (entity.ImportDate.Date >= this.FromImportDate.Value.Date && entity.ImportDate.Date <= this.ToImportDate.Value.Date.AddDays(1)))
                    && this.Match(this.ImportedBy, entity.ImportedBy, this.ImportedByCompare);
            }
        }

        #region Public Methods

        /// <summary>
        /// Returns an enumeration of DbParameter entities based on values set in th underlying search detail entity.
        /// </summary>
        /// <param name="dataAccess">Data access instance used to create the parameters.</param>
        /// <returns>A collection of DbParameter entities.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<DbParameter> GetSearchParameters(RRD.DSA.Core.DAL.IDataAccess dataAccess)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new search detail and copies the parameters from this one to it.
        /// </summary>
        /// <typeparam name="TCopy">Type of search detail to create.</typeparam>
        /// <returns>A new search detail with the same search parameters as this one.</returns>
        public virtual new TCopy CopyAs<TCopy>()
            where TCopy : VerticalXmlImportSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.FromImportDate = this.FromImportDate;
            copy.ToImportDate = this.ToImportDate;
            copy.ImportedBy = this.ImportedBy;
            copy.ImportedByCompare = this.ImportedByCompare;
            return copy;
        }

        #endregion
    }
}
