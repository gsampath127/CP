// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Scheduler.Interfaces;

namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// Class BaseTask.
    /// </summary>
    public abstract class BaseTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:BaseTask" /> class.
        /// </summary>
        public BaseTask()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTask"/> class.
        /// </summary>
        /// <param name="objectId">The object identifier.</param>
        /// <param name="clientObjectModel">The client object model.</param>
        protected BaseTask(int objectId, ClientObjectModel clientObjectModel)
        {
            this.ObjectId = objectId;
            this.ClientObjectModel = clientObjectModel;
        }

        /// <summary>
        /// Gets or sets Object Id
        /// </summary>
        /// <value>The object identifier.</value>
        public int ObjectId { get; set; }

        /// <summary>
        /// Gets or sets Client object.
        /// </summary>
        /// <value>The client object model.</value>
        public ClientObjectModel ClientObjectModel { get; set; }

        /// <summary>
        /// Process the task.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public abstract void Process(IReportScheduleEntry entry);
    }
}
