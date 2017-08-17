﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class SchedulerEntryBase : IScheduleEntry
    {
        /// <summary>
        /// 
        /// </summary>
        public static int RolledOver = -1;

        /// <summary>
        /// 
        /// </summary>
        public List<int> Values
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Expression
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int MinValue
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaxValue
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int First
        {
            get { return Values[0]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public int Next(int start)
        {
            // Find the next value in the list just
            //	after the supplied parameter.
            foreach (int value in Values)
            {
                if (value >= start)
                {
                    // Found one...exit early
                    return value;
                }
            }

            // Did not find one...
            return RolledOver;
        }

        /// <summary>
        /// Initializes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        protected void Initialize(string expression, int minValue, int maxValue)
        {
            if (string.IsNullOrEmpty(expression))
            {
                //throw new CronEntryException("Expression cannot be null or empty.");
            }
            Expression = expression;
            MinValue = minValue;
            MaxValue = maxValue;
            ParseExpression();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ParseExpression()
        {
            // line examples:
            //	5				Just # 5
            //	1-10			1,2,3,4,5,6,7,8,9,10
            //	2,3,9			2,3,9
            //	2,3,5-7			2,3,5,6,7
            //	1-10/3			1,4,7,10
            //	2,3,4-10/2		2,3,4,6,8,10
            //	*/3				minValue,minValue+3,...<=maxValue

            // Init return values.
            Values = new List<int>();

            // Split the individual entries
            int commaIndex = Expression.IndexOf(",");
            if (commaIndex == -1)
            {
                // Only one entry
                ParseEntry(Expression);
            }
            else
            {
                // Multiple entries...parse each one.
                string[] entrys = Expression.Split(new[] { ',' });
                foreach (string entry in entrys)
                {
                    ParseEntry(entry.Trim());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        private void ParseEntry(string entry)
        {
            // Ensure the entry is not empty.
            if (string.IsNullOrEmpty(entry))
            {
                throw new ArgumentException("Expression", "Entry is empty.");
            }

            // Initialize the indexing information to
            //	add all the values from min to max.
            int minUsed = MinValue;
            int maxUsed = MaxValue;
            int interval = -1;

            // Is there an interval specified?
            if (entry.IndexOf("/") != -1)
            {
                string[] vals = entry.Split('/');
                entry = vals[0];
                if (string.IsNullOrEmpty(entry))
                {
                    throw new ArgumentException("Expression", "Entry is empty.");
                }
                if (!int.TryParse(vals[1], out interval))
                {
                    throw new ArgumentOutOfRangeException("Expression","Found unexpected character. entry=" + entry);
                }
                if (interval < 1)
                {
                    throw new ArgumentOutOfRangeException("Expression", "Interval out of bounds.");
                }
            }

            // Is this a wild card
            if (entry[0] == '*' && entry.Length == 1)
            {
                // Wild card only.
                AddValues(minUsed, maxUsed, interval);
            }
            else
            {
                // No wild card.
                // Is this a range?
                if (entry.IndexOf("-") != -1)
                {
                    // Found a range.
                    string[] vals = entry.Split('-');
                    if (!int.TryParse(vals[0], out minUsed))
                    {
                        throw new ArgumentOutOfRangeException("Expression", "Found unexpected character. entry=" + entry);
                    }
                    if (minUsed < MinValue)
                    {
                        throw new ArgumentOutOfRangeException("Expression", "Minimum value less than expected.");
                    }
                    if (!int.TryParse(vals[1], out maxUsed))
                    {
                        throw new ArgumentOutOfRangeException("Expression", "Found unexpected character. entry=" + entry);
                    }
                    if (maxUsed > MaxValue)
                    {
                        throw new ArgumentOutOfRangeException("Expression", "Maximum value greater than expected.");
                    }
                    if (minUsed > maxUsed)
                    {
                        throw new ArgumentOutOfRangeException("Expression", "Maximum value less than minimum value.");
                    }
                    AddValues(minUsed, maxUsed, interval);
                }
                else
                {
                    // Must be a single number.
                    if (!int.TryParse(entry, out minUsed))
                    {
                        throw new ArgumentOutOfRangeException("Expression", "Found unexpected character. entry=" + entry);
                    }
                    if (minUsed < MinValue)
                    {
                        throw new ArgumentOutOfRangeException("Expression", "Value is less than minimum expected.");
                    }
                    if (interval == -1)
                    {
                        // No interval (eg. '5' or '9')
                        maxUsed = minUsed;
                        if (maxUsed > MaxValue)
                        {
                            throw new ArgumentOutOfRangeException("Expression", "Value is greater than maximum expected.");
                        }
                        AddValues(minUsed, maxUsed, interval);
                    }
                    else
                    {
                        // Interval and a single number
                        // eg. '2/5'  --> 2,7,12,17,...<max
                        maxUsed = MaxValue;
                        AddValues(minUsed, maxUsed, interval);
                    }
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minUsed"></param>
        /// <param name="maxUsed"></param>
        /// <param name="interval"></param>
        private void AddValues(int minUsed, int maxUsed, int interval)
        {
            if (interval == -1)
            {
                // No interval was specified...use 1
                interval = 1;
            }
            for (int i = minUsed; i <= maxUsed; i += interval)
            {
                if (!Values.Contains(i))
                {
                    Values.Add(i);
                }
            }
        }
    }
}
