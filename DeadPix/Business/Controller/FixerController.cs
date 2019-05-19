using System;
using System.Windows.Threading;

namespace DeadPix.Business.Controller
{
    /// <summary>
    /// Public sealed class that contains the logic for fixing dead or stuck pixels
    /// </summary>
    public sealed class FixerController
    {
        #region Variables
        private readonly DispatcherTimer _dispatcherTimer;
        private int _stopAfter;
        private long _startTime;
        #endregion

        #region Events
        internal event FixerStopped FixerStoppedEvent;
        internal event Events.ColorChangedEvent ColorChangedEvent;
        #endregion

        #region Delegates
        internal delegate void FixerStopped();
        #endregion

        #region Properties
        internal int Interval {
            set
            {
                if (value <= 0) throw new ArgumentException(nameof(value));
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, value);
            }
        }

        internal int StopAfter
        {
            private get => _stopAfter;
            set
            {
                if (value < 0) throw new ArgumentException(nameof(value));
                _stopAfter = value;
            }
        }
        #endregion

        /// <summary>
        /// Initialize a new FixerController
        /// </summary>
        internal FixerController()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            Interval = 1000;
            StopAfter = 0;
        }

        /// <summary>
        /// Start the Fixer
        /// </summary>
        internal void Start()
        {
            _startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            _dispatcherTimer.Start();
        }

        /// <summary>
        /// Method that is called when the timer has ticked
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The EventArgs</param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            ColorChangedEvent?.Invoke(Utils.Utils.GenerateColor());
            if (_stopAfter == 0) return;
            long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (currentTime - _startTime <= StopAfter) return;
            _dispatcherTimer.Stop();
            FixerStoppedEvent?.Invoke();
        }
    }
}
