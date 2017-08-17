// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 10-29-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;

namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// Class ReportSchedule.
    /// </summary>
    public class ReportSchedule
        : ReportScheduleBase<ReportScheduleEntry>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="errorReportScheduleName">Name of the error report schedule.</param>
        /// <param name="dataAccess">The data access.</param>
        public ReportSchedule(string connectionString, string serviceName, string errorReportScheduleName, IDataAccess dataAccess)
            : base(connectionString, serviceName, errorReportScheduleName)
        {
            this.Factory = new ReportScheduleEntryFactory(dataAccess);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="dataAccess">The data access.</param>
        public ReportSchedule(string connectionString, string serviceName, IDataAccess dataAccess)
            : this(connectionString, serviceName, string.Empty, dataAccess)
        {
        }

        #endregion
    }
}
