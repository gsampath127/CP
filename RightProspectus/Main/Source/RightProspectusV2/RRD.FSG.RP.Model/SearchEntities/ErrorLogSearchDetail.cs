// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-31-2015
//
// Last Modified By : 
// Last Modified On : 10-31-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RRD.FSG.RP.Model.SearchEntities
{
    /// <summary>
    /// Class ErrorLogSearchDetail.
    /// </summary>
    public class ErrorLogSearchDetail : SearchDetail<ErrorLogObjectModel>, ISearchDetailCopyAs<ErrorLogSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// ErrorCode.
        /// </summary>
        /// <value>The error code.</value>
        public int? ErrorCode { get; set; }
        /// <summary>
        /// ErrorCodeCompare.
        /// </summary>
        /// <value>The error code compare.</value>
        public ValueCompare ErrorCodeCompare { get; set; }
        /// <summary>
        /// FromErrorDate.
        /// </summary>
        /// <value>From error date.</value>
        public DateTime? FromErrorDate { get; set; }
        /// <summary>
        /// ToErrorDate.
        /// </summary>
        /// <value>To error date.</value>
        public DateTime? ToErrorDate { get; set; }
        /// <summary>
        /// Title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
        /// <summary>
        /// Title.
        /// </summary>
        /// <value>The title compare.</value>
        public ValueCompare TitleCompare { get; set; }
        /// <summary>
        /// ProcessName.
        /// </summary>
        /// <value>The name of the process.</value>
        public string ProcessName { get; set; }
        /// <summary>
        /// ProcessNameCompare.
        /// </summary>
        /// <value>The process name compare.</value>
        public ValueCompare ProcessNameCompare { get; set; }
        /// <summary>
        /// EventId.
        /// </summary>
        /// <value>The event identifier.</value>
        public int? EventId { get; set; }
        /// <summary>
        /// EventIdCompare.
        /// </summary>
        /// <value>The event identifier compare.</value>
        public ValueCompare EventIdCompare { get; set; }

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<ErrorLogObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.ErrorCode,entity.ErrorCode,this.ErrorCodeCompare)
                    && (FromErrorDate == null || (entity.ErrorUtcDate.Value.Date >= this.FromErrorDate.Value.Date && entity.ErrorUtcDate.Value.Date <= this.ToErrorDate.Value.Date.AddDays(1)))
                    && this.Match(this.Title,entity.Title,this.TitleCompare)
                    && this.Match(this.ProcessName,entity.ProcessName,this.ProcessNameCompare)
                    && this.Match(this.EventId,entity.EventId,this.EventIdCompare);
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
            where TCopy : ErrorLogSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.ErrorCode = this.ErrorCode;
            copy.ErrorCodeCompare = this.ErrorCodeCompare;
            copy.FromErrorDate = this.FromErrorDate;
            copy.ToErrorDate = this.ToErrorDate;
            copy.Title = this.Title;
            copy.TitleCompare = this.TitleCompare;
            copy.ProcessName = this.ProcessName;
            copy.ProcessNameCompare = this.ProcessNameCompare;
            copy.EventId = this.EventId;
            copy.EventIdCompare = this.EventIdCompare;
            return copy;
        }

        #endregion
    }
}
