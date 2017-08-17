// ***********************************************************************
// Assembly         : RRD.FSG.RP.Utilities
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 10-27-2015
// ***********************************************************************
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RRD.FSG.RP.Utilities
{
    /// <summary>
    /// Delegate TimerEvent
    /// </summary>
    /// <param name="elapsedMilliseconds">The elapsed milliseconds.</param>
    public delegate void TimerEvent(long elapsedMilliseconds);

    /// <summary>
    /// Class HighPrecisionTimer.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public class HighPrecisionTimer : IDisposable
    {
        /// <summary>
        /// Times the begin period.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <returns>System.Int32.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible"), DllImport("winmm.dll")]
        public static extern int timeBeginPeriod(int interval);

        /// <summary>
        /// Times the set event.
        /// </summary>
        /// <param name="delay">The delay.</param>
        /// <param name="resolution">The resolution.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="user">The user.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <returns>System.Int32.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("winmm.dll")]
        private static extern int timeSetEvent(int delay, int resolution, TimerEventHandler handler, IntPtr user, int eventType);

        /// <summary>
        /// Times the kill event.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>System.Int32.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("winmm.dll")]
        private static extern int timeKillEvent(int id);

        /// <summary>
        /// The _timer callback gc handle
        /// </summary>
        private GCHandle _timerCallbackGCHandle;
        /// <summary>
        /// Delegate TimerEventHandler
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="message">The message.</param>
        /// <param name="user">The user.</param>
        /// <param name="unused1">The unused1.</param>
        /// <param name="unused2">The unused2.</param>
        private delegate void TimerEventHandler(int id, int message, IntPtr user, int unused1, int unused2);
        /// <summary>
        /// The tim e_ periodic
        /// </summary>
        private const int TIME_PERIODIC = 1;
        /// <summary>
        /// The _timer identifier
        /// </summary>
        private int _timerID;
        /// <summary>
        /// The _running
        /// </summary>
        private bool _running = false;
        /// <summary>
        /// The _time callback
        /// </summary>
        private TimerEventHandler _timeCallback;
        /// <summary>
        /// The _timer stop watch
        /// </summary>
        private Stopwatch _timerStopWatch = new Stopwatch();

        /// <summary>
        /// Gets a value indicating whether this <see cref="HighPrecisionTimer"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        public bool Running { get { return _running; } }

        /// <summary>
        /// Occurs when [timer event].
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event TimerEvent TimerEvent;

        /// <summary>
        /// Initializes static members of the <see cref="HighPrecisionTimer"/> class.
        /// </summary>
        static HighPrecisionTimer()
        {
            timeBeginPeriod(1);
        }

        /// <summary>
        /// Starts the specified interval.
        /// </summary>
        /// <param name="interval">The interval.</param>
        public void Start(int interval)
        {
            _timeCallback = new TimerEventHandler(TimerCallback);

            _timerCallbackGCHandle = GCHandle.Alloc(_timeCallback);

            _timerID = timeSetEvent(interval, 0, _timeCallback, IntPtr.Zero, TIME_PERIODIC);

            _running = true;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "error")]
        public void Stop()
        {
            int error = timeKillEvent(_timerID);

            _running = false;
        }

        /// <summary>
        /// Timers the callback.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="message">The message.</param>
        /// <param name="user">The user.</param>
        /// <param name="unused1">The unused1.</param>
        /// <param name="unused2">The unused2.</param>
        private void TimerCallback(int id, int message, IntPtr user, int unused1, int unused2)
        {
            _timerStopWatch.Stop();

            if (TimerEvent != null)
            {
                TimerEvent(_timerStopWatch.ElapsedMilliseconds);
            }

            _timerStopWatch.Reset();

            _timerStopWatch.Start();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            _timerCallbackGCHandle.Free();
        }
    }
}
