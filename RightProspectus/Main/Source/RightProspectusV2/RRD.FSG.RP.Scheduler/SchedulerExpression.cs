using RRD.FSG.RP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Scheduler
{
    public class SchedulerExpression
    {
        public string Minutes { get; set; }
		public string Hours { get; set; }
		public string Days { get; set; }
		public string Months { get; set; }
		public string DaysOfWeek { get; set; }

		public SchedulerExpression()
			: this("*", "*", "*", "*", "*")
		{

		}
        public SchedulerExpression(string minutes, string hours, string days, string months, string daysOfWeek)
		{
			if (string.IsNullOrEmpty(minutes))
			{
				throw new ArgumentNullException("minutes");
			}
			if (string.IsNullOrEmpty(hours))
			{
				throw new ArgumentNullException("hours");
			}
			if (string.IsNullOrEmpty(days))
			{
				throw new ArgumentNullException("days");
			}
			if (string.IsNullOrEmpty(months))
			{
				throw new ArgumentNullException("months");
			}
			if (string.IsNullOrEmpty(daysOfWeek))
			{
				throw new ArgumentNullException("daysOfWeek");
			}
			Minutes = minutes;
			Hours = hours;
			Days = days;
			Months = months;
			DaysOfWeek = daysOfWeek;
		}

        public override string ToString()
        {
            return Minutes + " " + Hours + " " + Days + " " + Months + " " + DaysOfWeek;
        }

        /// <summary>
        /// Creates the scheduler expression based on the frquencytype and frequency interval
        /// </summary>
        /// <param name="frequencyType">Frequency type</param>
        /// <param name="frequencyInterval">Frequency interval</param>
        /// <returns></returns>
        public SchedulerExpression CreateExpression(FrequencyType frequencyType, int frequencyInterval)
        {
            switch (frequencyType)
            {
                case RRD.FSG.RP.Model.FrequencyType.RunOnce:
                    return SchedulerBuilder.CreateMinutelyTrigger();
                case RRD.FSG.RP.Model.FrequencyType.EveryXDays:
                    return SchedulerBuilder.CreateMonthlyTrigger(1, 31, frequencyInterval);
                case RRD.FSG.RP.Model.FrequencyType.Weekly:
                    return SchedulerBuilder.CreateDailyTrigger(new SchedulerBuilder.DayOfWeek[] { (SchedulerBuilder.DayOfWeek)frequencyInterval });
                case RRD.FSG.RP.Model.FrequencyType.Monthly:
                    return SchedulerBuilder.CreateMonthlyTrigger(frequencyInterval);
                case RRD.FSG.RP.Model.FrequencyType.Quarterly:
                    return SchedulerBuilder.CreateYearlyTrigger(1, 12, 3);
                case RRD.FSG.RP.Model.FrequencyType.BiAnnually:
                    return SchedulerBuilder.CreateYearlyTrigger(1, 12, 6);
                case RRD.FSG.RP.Model.FrequencyType.Annually:
                    return SchedulerBuilder.CreateYearlyTrigger(1);
                default:
                    return SchedulerBuilder.CreateHourlyTrigger(new int[] { 0, 30 });
            }
        }
    }
}
