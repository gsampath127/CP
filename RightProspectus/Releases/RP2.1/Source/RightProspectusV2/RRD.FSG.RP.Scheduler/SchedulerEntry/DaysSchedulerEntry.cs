
namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// 
    /// </summary>
    public class DaysSchedulerEntry : SchedulerEntryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public DaysSchedulerEntry(string expression)
        {
            Initialize(expression, 1, 31);
        }
    }
}
