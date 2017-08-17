using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Scheduler
{
    public class SchedulerSchedule
    {
        private readonly MinutesSchedulerEntry minutesSchedulerEntry;
        private readonly HoursSchedulerEntry hoursSchedulerEntry;
        private readonly DaysSchedulerEntry daysSchedulerEntry;
        private readonly MonthsSchedulerEntry monthsSchedulerEntry;
        private readonly DaysOfWeekSchedulerEntry daysOfWeekSchedulerEntry;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="hours"></param>
        /// <param name="days"></param>
        /// <param name="months"></param>
        /// <param name="daysOfWeek"></param>
        private SchedulerSchedule(string minutes, string hours, string days, string months, string daysOfWeek)
        {
            minutesSchedulerEntry = new MinutesSchedulerEntry(minutes);
            hoursSchedulerEntry = new HoursSchedulerEntry(hours);
            daysSchedulerEntry = new DaysSchedulerEntry(days);
            monthsSchedulerEntry = new MonthsSchedulerEntry(months);
            daysOfWeekSchedulerEntry = new DaysOfWeekSchedulerEntry(daysOfWeek); // 0 = Sunday
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulerExpression"></param>
        /// <returns></returns>
        public static SchedulerSchedule Parse(string schedulerExpression)
        {
            if (string.IsNullOrEmpty(schedulerExpression))
            {
                throw new ArgumentException("schedulerExpression");
            }
            string[] parts = schedulerExpression.Split(' ');
            if (parts.Length != 5)
            {
                throw new ArgumentException("schedulerExpression");
            }
            return Parse(parts[0], parts[1], parts[2], parts[3], parts[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="hours"></param>
        /// <param name="days"></param>
        /// <param name="months"></param>
        /// <param name="daysOfWeek"></param>
        /// <returns></returns>
        public static SchedulerSchedule Parse(string minutes, string hours, string days, string months, string daysOfWeek)
        {
            if (string.IsNullOrEmpty(minutes))
            {
                throw new ArgumentException("minutes");
            }
            if (string.IsNullOrEmpty(hours))
            {
                throw new ArgumentException("hours");
            }
            if (string.IsNullOrEmpty(days))
            {
                throw new ArgumentException("days");
            }
            if (string.IsNullOrEmpty(months))
            {
                throw new ArgumentException("months");
            }
            if (string.IsNullOrEmpty(daysOfWeek))
            {
                throw new ArgumentException("daysOfWeek");
            }
            return new SchedulerSchedule(minutes, hours, days, months, daysOfWeek);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<DateTime> GetAll(DateTime start, DateTime end)
        {
            List<DateTime> result = new List<DateTime>();

            DateTime current = start;
            while (current <= end)
            {
                DateTime next;
                if (!GetNext(current, end, out next))
                {
                    // Did not find any new ones...return what we have
                    break;
                }
                result.Add(next);
                current = next;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public bool GetNext(DateTime start, out DateTime next)
        {
            return GetNext(start, DateTime.MaxValue, out next);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public bool GetNext(DateTime start, DateTime end, out DateTime next)
        {
            // Initialize the next output
            next = DateTime.MinValue;

            // Don't want to select the actual start date.
            DateTime baseSearch = start.AddMinutes(1.0);
            int baseMinute = baseSearch.Minute;
            int baseHour = baseSearch.Hour;
            int baseDay = baseSearch.Day;
            int baseMonth = baseSearch.Month;
            int baseYear = baseSearch.Year;

            // Get the next minute value
            int minute = minutesSchedulerEntry.Next(baseMinute);
            if (minute == SchedulerEntryBase.RolledOver)
            {
                // We need to roll forward to the next hour.
                minute = minutesSchedulerEntry.First;
                baseHour++;
                // Don't need to worry about baseHour>23 case because
                //	that will roll off our list in the next step.
            }

            // Get the next hour value
            int hour = hoursSchedulerEntry.Next(baseHour);
            if (hour == SchedulerEntryBase.RolledOver)
            {
                // Roll forward to the next day.
                minute = minutesSchedulerEntry.First;
                hour = hoursSchedulerEntry.First;
                baseDay++;
                // Don't need to worry about baseDay>31 case because
                //	that will roll off our list in the next step.
            }
            else if (hour > baseHour)
            {
                // Original hour must not have been in the list.
                //	Reset the minutes.
                minute = minutesSchedulerEntry.First;
            }

            // Get the next day value.
            int day = daysSchedulerEntry.Next(baseDay);
            if (day == SchedulerEntryBase.RolledOver)
            {
                // Roll forward to the next month
                minute = minutesSchedulerEntry.First;
                hour = hoursSchedulerEntry.First;
                day = daysSchedulerEntry.First;
                baseMonth++;
                // Need to worry about rolling over to the next year here
                //	because we need to know the number of days in a month
                //	and that is year dependent (leap year).
                if (baseMonth > 12)
                {
                    // Roll over to next year.
                    baseMonth = 1;
                    baseYear++;
                }
            }
            else if (day > baseDay)
            {
                // Original day no in the value list...reset.
                minute = minutesSchedulerEntry.First;
                hour = hoursSchedulerEntry.First;
            }
            while (day > DateTime.DaysInMonth(baseYear, baseMonth))
            {
                // Have a value for the day that is not a valid day
                //	in the current month. Move to the next month.
                minute = minutesSchedulerEntry.First;
                hour = hoursSchedulerEntry.First;
                day = daysSchedulerEntry.First;
                baseMonth++;
                // This original month could not be December because
                //	it can handle the maximum value of days (31). So
                //	we do not have to worry about baseMonth == 13 case.
            }

            // Get the next month value.
            int month = monthsSchedulerEntry.Next(baseMonth);
            if (month == SchedulerEntryBase.RolledOver)
            {
                // Roll forward to the next year.
                minute = minutesSchedulerEntry.First;
                hour = hoursSchedulerEntry.First;
                day = daysSchedulerEntry.First;
                month = monthsSchedulerEntry.First;
                baseYear++;
            }
            else if (month > baseMonth)
            {
                // Original month not in the value list...reset.
                minute = minutesSchedulerEntry.First;
                hour = hoursSchedulerEntry.First;
                day = daysSchedulerEntry.First;
            }
            while (day > DateTime.DaysInMonth(baseYear, month))
            {
                // Have a value for the day that is not a valid day
                //	in the current month. Move to the next month.
                minute = minutesSchedulerEntry.First;
                hour = hoursSchedulerEntry.First;
                day = daysSchedulerEntry.First;
                month = monthsSchedulerEntry.Next(month + 1);
                if (month == SchedulerEntryBase.RolledOver)
                {
                    // Roll forward to the next year.
                    minute = minutesSchedulerEntry.First;
                    hour = hoursSchedulerEntry.First;
                    day = daysSchedulerEntry.First;
                    month = monthsSchedulerEntry.First;
                    baseYear++;
                }
            }

            // Is the date / time we found beyond the end search contraint?
            DateTime suggested = new DateTime(baseYear, month, day, hour, minute, 0, 0);
            if (suggested >= end)
            {
                return false;
            }

            // Does the date / time we found satisfy the day of the week contraint?
            if (daysOfWeekSchedulerEntry.Values.Contains((int)suggested.DayOfWeek))
            {
                // We have a good date.
                next = suggested;
                return true;
            }

            // We need to recursively look for a date in the future. Because this
            //	search resulted in a day that does not satisfy the day of week 
            //	contraint, start the search on the next day.
            return GetNext(new DateTime(baseYear, month, day, 23, 59, 0, 0), out next);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}",
                minutesSchedulerEntry.Expression,
                hoursSchedulerEntry.Expression,
                daysSchedulerEntry.Expression,
                monthsSchedulerEntry.Expression,
                daysOfWeekSchedulerEntry.Expression);
        }
    }
}
