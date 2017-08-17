// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.System;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.SortDetail.System
{
    /// <summary>
    /// Class ReportScheduleSortDetail.
    /// </summary>
    public class ReportScheduleSortDetail : AuditedSortDetail<ReportScheduleObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new ReportScheduleSortColumn Column
        {
            get { return (ReportScheduleSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }
        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<ReportScheduleObjectModel> Sort(IEnumerable<ReportScheduleObjectModel> source)
        {
            switch (this.Column)
            {
                case ReportScheduleSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case ReportScheduleSortColumn.ReportName:
                    return this.Sort(source, entity => entity.ReportId);
                case ReportScheduleSortColumn.FrequencyType:
                    return this.Sort(source, entity => entity.FrequencyType);
                case ReportScheduleSortColumn.FrequencyInterval:
                    return this.Sort(source, entity => entity.FrequencyInterval);
                case ReportScheduleSortColumn.FrequencyDescription:
                    return this.Sort(source, entity => entity.FrequencyDescription);
                case ReportScheduleSortColumn.IsEnabled:
                    return this.Sort(source, entity => entity.IsEnabled);
                case ReportScheduleSortColumn.UtcFirstScheduledRunDate:
                    return this.Sort(source, entity => entity.UtcFirstScheduledRunDate);
                case ReportScheduleSortColumn.UtcLastActualRunDate:
                    return this.Sort(source, entity => entity.UtcLastActualRunDate);
                case ReportScheduleSortColumn.UtcNextScheduledRunDate:
                    return this.Sort(source, entity => entity.UtcNextScheduledRunDate);
                default:
                    return base.Sort(source);
            }
        }

        /// <summary>
        /// Compares two entities using teh sort properties of this instance.
        /// </summary>
        /// <param name="x">First entity to compare.</param>
        /// <param name="y">Second entity to compare.</param>
        /// <returns>A negative number if x is less than y, a positive number if x is greater than y, and 0 if they are the same.</returns>
        public override int Compare(ReportScheduleObjectModel x, ReportScheduleObjectModel y)
        {
            switch (this.Column)
            {
                case ReportScheduleSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case ReportScheduleSortColumn.ReportName:
                    return this.Compare(x.ReportId, y.ReportId);
                case ReportScheduleSortColumn.FrequencyType:
                    return this.Compare(x.FrequencyType, y.FrequencyType);
                case ReportScheduleSortColumn.FrequencyInterval:
                    return this.Compare(x.FrequencyInterval, y.FrequencyInterval);
                case ReportScheduleSortColumn.FrequencyDescription:
                    return this.Compare(x.FrequencyDescription, y.FrequencyDescription);
                case ReportScheduleSortColumn.IsEnabled:
                    return this.Compare(x.IsEnabled, y.IsEnabled);
                case ReportScheduleSortColumn.UtcFirstScheduledRunDate:
                    return this.Compare(x.UtcFirstScheduledRunDate.GetValueOrDefault(), y.UtcFirstScheduledRunDate.GetValueOrDefault());
                case ReportScheduleSortColumn.UtcLastScheduledRunDate:
                    return this.Compare(x.UtcLastScheduledRunDate.GetValueOrDefault(), y.UtcLastScheduledRunDate.GetValueOrDefault());
                case ReportScheduleSortColumn.UtcLastActualRunDate:
                    return this.Compare(x.UtcLastActualRunDate.GetValueOrDefault(), y.UtcLastActualRunDate.GetValueOrDefault());
                case ReportScheduleSortColumn.UtcNextScheduledRunDate:
                    return this.Compare(x.UtcNextScheduledRunDate.GetValueOrDefault(), y.UtcNextScheduledRunDate.GetValueOrDefault());
                              
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
