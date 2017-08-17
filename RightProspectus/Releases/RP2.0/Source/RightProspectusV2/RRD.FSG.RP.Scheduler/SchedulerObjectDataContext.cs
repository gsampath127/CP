using System;
using System.Collections.Generic;

namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// 
    /// </summary>
    public class SchedulerObjectDataContext
    {
        /// <summary>
        /// 
        /// </summary>
        public object Object { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastTrigger { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public List<SchedulerSchedule> SchedulerSchedule { get; set; }
    }
}
