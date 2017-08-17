
namespace RRD.FSG.RP.Scheduler
{
    public class DaysOfWeekSchedulerEntry : SchedulerEntryBase
    {
        public DaysOfWeekSchedulerEntry(string expression)
        {
            Initialize(expression, 0, 6);
        }
    }
}
