// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RRD.FSG.RP.Model.SearchEntities.System
{
    /// <summary>
    /// Class ReportScheduleSearchDetail.
    /// </summary>
    public class ReportScheduleSearchDetail
        : AuditedSearchDetail<ReportScheduleObjectModel>, ISearchDetailCopyAs<ReportScheduleSearchDetail>
    {
        #region Public Properties

        
        /// <summary>
        /// ReportName
        /// </summary>
        /// <value>The report identifier.</value>
        public int? ReportId { get; set; }
        /// <summary>
        /// Gets or sets the name of the report.
        /// </summary>
        /// <value>The name of the report.</value>
        public string ReportName { get; set; }
        
        /// <summary>
        /// Determines the type of comparison for the ReportId property
        /// </summary>
        /// <value>The report identifier compare.</value>
        public ValueCompare ReportIdCompare { get; set; }
        
        /// <summary>
        /// ClientId
        /// </summary>
        /// <value>The client identifier.</value>
        public int? ClientId { get; set; }
        
        /// <summary>
        /// Determines the type of comparison for the ClientId property
        /// </summary>
        /// <value>The client identifier compare.</value>
        public ValueCompare ClientIdCompare { get; set; }
        
        /// <summary>
        /// IsEnabled
        /// </summary>
        /// <value><c>null</c> if [is enabled] contains no value, <c>true</c> if [is enabled]; otherwise, <c>false</c>.</value>
        public bool? IsEnabled { get; set; }
        
        /// <summary>
        /// Determines the type of comparison for the IsEnabled property
        /// </summary>
        /// <value>The is enabled compare.</value>
        public ValueCompare IsEnabledCompare { get; set; }
        
        /// <summary>
        /// FrequencyType
        /// </summary>
        /// <value>The type of the frequency.</value>
        public int? FrequencyType { get; set; }
        
        /// <summary>
        /// Determines the type of comparison for the FrequencyType property
        /// </summary>
        /// <value>The frequency type compare.</value>
        public ValueCompare FrequencyTypeCompare { get; set; }
        
        /// <summary>
        /// FrequencyInterval
        /// </summary>
        /// <value>The frequency interval.</value>
        public int? FrequencyInterval { get; set; }
        
        /// <summary>
        /// Determines the type of comparison for the FrequencyInterval property
        /// </summary>
        /// <value>The frequency interval compare.</value>
        public ValueCompare FrequencyIntervalCompare { get; set; }

        /// <summary>
        /// FirstScheduleRunDate
        /// </summary>
        /// <value>The first schedule run date.</value>
        public DateTime? FirstScheduleRunDate { get; set; }

        /// <summary>
        /// Determines the type of comparison for the FirstScheduleRunDate property
        /// </summary>
        /// <value>The first schedule run date compare.</value>
        public ValueCompare FirstScheduleRunDateCompare { get; set; }


        /// <summary>
        /// FirstScheduleRunDate
        /// </summary>
        /// <value>The last schedule run date.</value>
        public DateTime? LastScheduleRunDate { get; set; }

        /// <summary>
        /// Determines the type of comparison for the LastScheduleRunDate property
        /// </summary>
        /// <value>The last schedule run date compare.</value>
        public ValueCompare LastScheduleRunDateCompare { get; set; }
        /// <summary>
        /// NextScheduleRunDate
        /// </summary>
        /// <value>The next schedule run date.</value>
        public DateTime? NextScheduleRunDate { get; set; }

        /// <summary>
        /// Determines the type of comparison for the NextScheduleRunDate property
        /// </summary>
        /// <value>The Next schedule run date compare.</value>
        public ValueCompare NextScheduleRunDateCompare { get; set; }


        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<ReportScheduleObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                        //&& this.Match(this.ReportScheduleId, entity.ReportScheduleId, this.ReportScheduleIdCompare)
                    && this.Match(this.ReportId, entity.ReportId, this.ReportIdCompare)
                    && this.Match(this.ClientId, entity.ClientId, this.ClientIdCompare)
                    && this.Match(this.FrequencyType, entity.FrequencyType,this.FrequencyTypeCompare)
                    && this.Match(this.FrequencyInterval, entity.FrequencyInterval, this.FrequencyIntervalCompare)
                    && this.Match(this.FirstScheduleRunDate, entity.UtcFirstScheduledRunDate, this.FirstScheduleRunDateCompare)
                    && this.Match(this.LastScheduleRunDate, entity.UtcLastScheduledRunDate, this.LastScheduleRunDateCompare)
                    && this.Match(this.NextScheduleRunDate, entity.UtcNextScheduledRunDate, this.NextScheduleRunDateCompare)
                    && this.Match(this.IsEnabled, entity.IsEnabled, this.IsEnabledCompare);
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
            where TCopy : ReportScheduleSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.ReportId = this.ReportId;
            copy.ReportIdCompare = this.ReportIdCompare;
            copy.ClientId = this.ClientId;
            copy.ClientIdCompare = this.ClientIdCompare;
            copy.IsEnabled = this.IsEnabled;
            copy.IsEnabledCompare = this.IsEnabledCompare;
            copy.FrequencyType = this.FrequencyType;
            copy.FrequencyTypeCompare = this.FrequencyTypeCompare;
            copy.FrequencyInterval = this.FrequencyInterval;
            copy.FrequencyIntervalCompare = this.FrequencyIntervalCompare;
            copy.FirstScheduleRunDate = this.FirstScheduleRunDate;
            copy.FirstScheduleRunDateCompare = this.FirstScheduleRunDateCompare;
            copy.LastScheduleRunDate = this.LastScheduleRunDate;
            copy.LastScheduleRunDateCompare = this.LastScheduleRunDateCompare;

            return copy;
        }

        #endregion
    }
}
