using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Scheduler
{
    public class MinutesSchedulerEntry : SchedulerEntryBase
    {
        public MinutesSchedulerEntry(string expression)
        {
            Initialize(expression, 0, 59);
        }
    }
}
