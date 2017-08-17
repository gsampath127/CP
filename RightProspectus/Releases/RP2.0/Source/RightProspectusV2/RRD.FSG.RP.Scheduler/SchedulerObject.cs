using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Scheduler
{
    public class SchedulerObject
    {
        public delegate void SchedulerEvent(SchedulerObject schedulerObject);
        public event SchedulerEvent OnSchedulerTrigger;
        public event SchedulerEvent OnStarted;
        public event SchedulerEvent OnStopped;
        public event SchedulerEvent OnThreadAbort;

        private readonly SchedulerObjectDataContext schedulerObjectDataContext;
        private readonly Guid id = Guid.NewGuid();
        private readonly object startStopLock = new object();
        private readonly EventWaitHandle wh = new AutoResetEvent(false);
        private Thread thread;
        private bool isStarted;
        private bool isStopRequested;
        private DateTime nextSchedulerTrigger;

        public Guid Id { get { return id; } }
        public object Object { get { return schedulerObjectDataContext.Object; } }
        public DateTime LastTigger { get { return schedulerObjectDataContext.LastTrigger; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerObject"/> class.
        /// </summary>
        /// <param name="dataContext">The Scheduler object data context.</param>
        public SchedulerObject(SchedulerObjectDataContext dataContext)
		{
            if (dataContext == null)
			{
                throw new ArgumentNullException("schedulerObjectDataContext");
			}
            if (dataContext.Object == null)
			{
                throw new ArgumentException("schedulerObjectDataContext.Object");
			}
            if (dataContext.SchedulerSchedule == null || dataContext.SchedulerSchedule.Count == 0)
			{
                throw new ArgumentException("schedulerObjectDataContext.SchedulerSchedule");
			}
            schedulerObjectDataContext = dataContext;
		}

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            lock (startStopLock)
            {
                // Can't start if already started.
                //
                if (isStarted)
                {
                    return false;
                }
                isStarted = true;
                isStopRequested = false;

                // This is a long running process. Need to run on a thread
                //	outside the thread pool.
                //
                thread = new Thread(ThreadRoutine);
                thread.Start();
            }

            // Raise the started event.
            //
            if (OnStarted != null)
            {
                OnStarted(this);
            }

            return true;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            lock (startStopLock)
            {
                // Can't stop if not started.
                //
                if (!isStarted)
                {
                    return false;
                }
                isStarted = false;
                isStopRequested = true;

                // Signal the thread to wake up early
                //
                wh.Set();

                // Wait for the thread to join.
                //
                if (!thread.Join(5000))
                {
                    thread.Abort();

                    // Raise the thread abort event.
                    //
                    if (OnThreadAbort != null)
                    {
                        OnThreadAbort(this);
                    }
                }
            }

            // Raise the stopped event.
            //
            if (OnStopped != null)
            {
                OnStopped(this);
            }
            return true;
        }

        /// <summary>
        /// Cron object thread routine.
        /// </summary>
        private void ThreadRoutine()
        {
            // Continue until stop is requested.
            //
            while (!isStopRequested)
            {
                // Determine the next cron trigger
                //
                DetermineNextSchedulerTrigger(out nextSchedulerTrigger);

                TimeSpan sleepSpan = nextSchedulerTrigger - DateTime.Now;
                if (sleepSpan.TotalMilliseconds < 0)
                {
                    // Next trigger is in the past. Trigger the right away.
                    //
                    sleepSpan = new TimeSpan(0, 0, 0, 0, 50);
                }

                // Wait here for the timespan or until I am triggered
                //	to wake up.
                //
                if (!wh.WaitOne(sleepSpan))
                {
                    // Timespan is up...raise the trigger event
                    //
                    if (OnSchedulerTrigger != null)
                    {
                        OnSchedulerTrigger(this);
                    }

                    // Update the last trigger time.
                    //
                    schedulerObjectDataContext.LastTrigger = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Determines the next cron trigger.
        /// </summary>
        /// <param name="nextTrigger">The next trigger.</param>
        private void DetermineNextSchedulerTrigger(out DateTime nextTrigger)
        {
            nextTrigger = DateTime.MaxValue;
            foreach (SchedulerSchedule schedulerSchedule in schedulerObjectDataContext.SchedulerSchedule)
            {
                DateTime thisTrigger;
                if (schedulerSchedule.GetNext(LastTigger, out thisTrigger))
                {
                    if (thisTrigger < nextTrigger)
                    {
                        nextTrigger = thisTrigger;
                    }
                }
            }
        }
    }
}
