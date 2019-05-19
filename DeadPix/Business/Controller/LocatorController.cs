using System;
using System.Windows.Media;
using System.Windows.Threading;

namespace DeadPix.Business.Controller
{
    /// <summary>
    /// Public sealed class that contains the logic for locating dead pixels
    /// </summary>
    public sealed class LocatorController
    {
        #region Variables
        private bool _randomColors;
        private readonly DispatcherTimer _dispatcherTimer;
        #endregion

        #region Events
        internal event Events.ColorChangedEvent ColorChangedEvent;
        #endregion

        #region Properties
        internal Color SelectedColor { get; set; }

        internal bool RandomizeColors
        {
            get => _randomColors;
            set
            {
                _randomColors = value;
                if (value) _dispatcherTimer.Start();
                else _dispatcherTimer.Stop();
            }
        }

        public int Interval
        {
            set
            {
                if (value <= 0) throw new ArgumentException(nameof(value));
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, value);
            }
        }
        #endregion

        /// <summary>
        /// Initialize a new LocatorController
        /// </summary>
        internal LocatorController()
        {
            SelectedColor = Colors.White;

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            Interval = 1000;
        }

        /// <summary>
        /// Method that is called when the timer has ticked
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The EventArgs</param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            SelectedColor = Utils.Utils.GenerateColor();
            ColorChangedEvent?.Invoke(SelectedColor);
        }
    }
}
