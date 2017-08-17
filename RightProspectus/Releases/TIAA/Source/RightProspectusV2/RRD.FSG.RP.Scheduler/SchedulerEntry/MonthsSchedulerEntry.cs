using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Scheduler
{
    public class MonthsSchedulerEntry : SchedulerEntryBase
    {
        public MonthsSchedulerEntry(string expression)
        {
            Initialize(expression, 1, 12);
        }
    }
}
