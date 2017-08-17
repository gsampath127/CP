using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Scheduler
{
    public static class SchedulerBuilder
    {
        public enum DayOfWeek
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        } ;

        #region Minutely Triggers

        public static SchedulerExpression CreateMinutelyTrigger()
        {
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "*",
                Hours = "*",
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }

        #endregion

        #region Hourly Triggers

        public static SchedulerExpression CreateHourlyTrigger()
        {
            return CreateHourlyTrigger(0);
        }
        public static SchedulerExpression CreateHourlyTrigger(int triggerMinute)
        {
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = triggerMinute.ToString(),
                Hours = "*",
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateHourlyTrigger(int[] triggerMinutes)
        {

            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = triggerMinutes.ConvertArrayToString(),
                Hours = "*",
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateHourlyTrigger(int firstMinuteToTrigger, int lastMinuteToTrigger)
        {
            return CreateHourlyTrigger(firstMinuteToTrigger, lastMinuteToTrigger, 1);
        }
        public static SchedulerExpression CreateHourlyTrigger(int firstMinuteToTrigger, int lastMinuteToTrigger, int interval)
        {
            string value = firstMinuteToTrigger + "-" + lastMinuteToTrigger;
            if (interval != 1)
            {
                value += "/" + interval;
            }
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = value,
                Hours = "*",
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }

        #endregion

        #region Daily Triggers

        public static SchedulerExpression CreateDailyTrigger()
        {
            return CreateDailyTrigger(0);
        }
        public static SchedulerExpression CreateDailyTrigger(int triggerHour)
        {
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = triggerHour.ToString(),
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateDailyTrigger(int[] triggerHours)
        {
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = triggerHours.ConvertArrayToString(),
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateDailyTrigger(int firstHourToTrigger, int lastHourToTrigger)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, 1);
        }
        public static SchedulerExpression CreateDailyTrigger(int firstHourToTrigger, int lastHourToTrigger, int interval)
        {
            string value = firstHourToTrigger + "-" + lastHourToTrigger;
            if (interval != 1)
            {
                value += "/" + interval;
            }
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = value,
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateDailyTrigger(DayOfWeek[] daysOfWeekFilter)
        {
            return CreateDailyTrigger(0, daysOfWeekFilter);
        }
        public static SchedulerExpression CreateDailyTrigger(int triggerHour, DayOfWeek[] daysOfWeekFilter)
        {
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = triggerHour.ToString(),
                Days = "*",
                Months = "*",
                DaysOfWeek = daysOfWeekFilter.ConvertArrayToString()
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateDailyTrigger(int[] triggerHours, DayOfWeek[] daysOfWeekFilter)
        {
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = triggerHours.ConvertArrayToString(),
                Days = "*",
                Months = "*",
                DaysOfWeek = daysOfWeekFilter.ConvertArrayToString()
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateDailyTrigger(int firstHourToTrigger, int lastHourToTrigger, DayOfWeek[] daysOfWeekFilter)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, 1, daysOfWeekFilter);
        }
        public static SchedulerExpression CreateDailyTrigger(int firstHourToTrigger, int lastHourToTrigger, int interval, DayOfWeek[] daysOfWeekFilter)
        {
            string value = firstHourToTrigger + "-" + lastHourToTrigger;
            if (interval != 1)
            {
                value += "/" + interval;
            }
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = value,
                Days = "*",
                Months = "*",
                DaysOfWeek = daysOfWeekFilter.ConvertArrayToString()
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateDailyOnlyWeekDayTrigger()
        {
            return CreateDailyOnlyWeekDayTrigger(0);
        }
        public static SchedulerExpression CreateDailyOnlyWeekDayTrigger(int triggerHour)
        {
            return CreateDailyTrigger(triggerHour, GetWeekDays());
        }
        public static SchedulerExpression CreateDailyOnlyWeekDayTrigger(int[] triggerHours)
        {
            return CreateDailyTrigger(triggerHours, GetWeekDays());
        }
        public static SchedulerExpression CreateDailyOnlyWeekDayTrigger(int firstHourToTrigger, int lastHourToTrigger)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, GetWeekDays());
        }
        public static SchedulerExpression CreateDailyOnlyWeekDayTrigger(int firstHourToTrigger, int lastHourToTrigger, int interval)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, interval, GetWeekDays());
        }
        public static SchedulerExpression CreateDailyOnlyWeekEndTrigger()
        {
            return CreateDailyOnlyWeekEndTrigger(0);
        }
        public static SchedulerExpression CreateDailyOnlyWeekEndTrigger(int triggerHour)
        {
            return CreateDailyTrigger(triggerHour, GetWeekEndDays());
        }
        public static SchedulerExpression CreateDailyOnlyWeekEndTrigger(int[] triggerHours)
        {
            return CreateDailyTrigger(triggerHours, GetWeekEndDays());
        }
        public static SchedulerExpression CreateDailyOnlyWeekEndTrigger(int firstHourToTrigger, int lastHourToTrigger)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, GetWeekEndDays());
        }
        public static SchedulerExpression CreateDailyOnlyWeekEndTrigger(int firstHourToTrigger, int lastHourToTrigger, int interval)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, interval, GetWeekEndDays());
        }

        #endregion

        #region Monthly Triggers

        public static SchedulerExpression CreateMonthlyTrigger()
        {
            return CreateMonthlyTrigger(0);
        }
        public static SchedulerExpression CreateMonthlyTrigger(int triggerDay)
        {
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = "0",
                Days = triggerDay.ToString(),
                Months = "*",
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateMonthlyTrigger(int[] triggerDays)
        {
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = "0",
                Days = triggerDays.ConvertArrayToString(),
                Months = "*",
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateMonthlyTrigger(int firstDayToTrigger, int lastDayToTrigger)
        {
            return CreateMonthlyTrigger(firstDayToTrigger, lastDayToTrigger, 1);
        }
        public static SchedulerExpression CreateMonthlyTrigger(int firstDayToTrigger, int lastDayToTrigger, int interval)
        {
            string value = firstDayToTrigger + "-" + lastDayToTrigger;
            if (interval != 1)
            {
                value += "/" + interval;
            }
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = "0",
                Days = value,
                Months = "*",
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }

        #endregion

        #region Yearly Triggers

        public static SchedulerExpression CreateYearlyTrigger()
        {
            return CreateYearlyTrigger(0);
        }
        public static SchedulerExpression CreateYearlyTrigger(int triggerMonth)
        {
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = "0",
                Days = "0",
                Months = triggerMonth.ToString(),
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateYearlyTrigger(int[] triggerMonths)
        {
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = "0",
                Days = "0",
                Months = triggerMonths.ConvertArrayToString(),
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }
        public static SchedulerExpression CreateYearlyTrigger(int firstMonthToTrigger, int lastMonthToTrigger)
        {
            return CreateYearlyTrigger(firstMonthToTrigger, lastMonthToTrigger, 1);
        }
        public static SchedulerExpression CreateYearlyTrigger(int firstMonthToTrigger, int lastMonthToTrigger, int interval)
        {
            string value = firstMonthToTrigger + "-" + lastMonthToTrigger;
            if (interval != 1)
            {
                value += "/" + interval;
            }
            SchedulerExpression schedulerExpression = new SchedulerExpression
            {
                Minutes = "0",
                Hours = "0",
                Days = "0",
                Months = value,
                DaysOfWeek = "*"
            };
            return schedulerExpression;
        }

        #endregion

        private static string ConvertArrayToString(this IEnumerable<int> list)
        {
            StringBuilder result = new StringBuilder();
            List<int> values = new List<int>(list);
            values.Sort();
            for (int i = 0; i < values.Count; i++)
            {
                result.Append(values[i].ToString());
                if (i != values.Count - 1)
                {
                    result.Append(",");
                }
            }
            return result.ToString();
        }
        private static string ConvertArrayToString(this DayOfWeek[] list)
        {
            StringBuilder result = new StringBuilder();
            List<int> values = new List<int>();
            for (int i = 0; i < list.Length; i++)
            {
                values.Add((int)list[i]);
            }
            values.Sort();
            for (int i = 0; i < values.Count; i++)
            {
                result.Append(values[i].ToString());
                if (i != values.Count - 1)
                {
                    result.Append(",");
                }
            }
            return result.ToString();
        }
        private static DayOfWeek[] GetWeekDays()
        {
            return new[]
			       	{
			       		DayOfWeek.Monday, 
			       		DayOfWeek.Tuesday, 
			       		DayOfWeek.Wednesday, 
			       		DayOfWeek.Thursday,
			       		DayOfWeek.Friday
			       	};
        }
        private static DayOfWeek[] GetWeekEndDays()
        {
            return new[]
			       	{
			       		DayOfWeek.Sunday, 
			       		DayOfWeek.Saturday
			       	};
        }
    }
}
