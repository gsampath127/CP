// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-27-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
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
    public class VerticalXmlExportSearchDetail : SearchDetail<VerticalXmlExportObjectModel>, ISearchDetailCopyAs<VerticalXmlExportSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// ExportDate.
        /// </summary>
        /// <value>From export date.</value>
        public DateTime? FromExportDate { get; set; }
        /// <summary>
        /// ExportDate.
        /// </summary>
        /// <value>To export date.</value>
        public DateTime? ToExportDate { get; set; }
        /// <summary>
        /// ExportedBy.
        /// </summary>
        /// <value>The exported by.</value>
        public int? ExportedBy { get; set; }
        /// <summary>
        /// Determines the type of comparison for the ExportedBy property.
        /// </summary>
        /// <value>The exported by compare.</value>
        public ValueCompare ExportedByCompare { get; set; }


        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<VerticalXmlExportObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && (FromExportDate == null || (entity.ExportDate.Date >= this.FromExportDate.Value.Date && entity.ExportDate.Date <= this.ToExportDate.Value.AddDays(1)))
                    && this.Match(this.ExportedBy, entity.ExportedBy, this.ExportedByCompare);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns an enumeration of DbParameter entities based on values set in th underlying search detail entity.
        /// </summary>
        /// <param name="dataAccess">Data access instance used to create the parameters.</param>
        /// <returns>A collection of DbParameter entities.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<DbParameter> GetSearchParameters(IDataAccess dataAccess)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new search detail and copies the parameters from this one to it.
        /// </summary>
        /// <typeparam name="TCopy">Type of search detail to create.</typeparam>
        /// <returns>A new search detail with the same search parameters as this one.</returns>
        public virtual new TCopy CopyAs<TCopy>()
            where TCopy : VerticalXmlExportSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.FromExportDate = this.FromExportDate;
            copy.ToExportDate = this.ToExportDate;
            copy.ExportedBy = this.ExportedBy;
            copy.ExportedByCompare = this.ExportedByCompare;
            return copy;
        }

        #endregion
    }
}
