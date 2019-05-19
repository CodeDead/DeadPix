using System;
using System.Windows.Media;
using System.Windows.Threading;

namespace DeadPix.Business.Locator
{
    internal delegate void ColorChangedEvent(Color color);

    /// <summary>
    /// Internal sealed class that contains the logic for locating dead pixels
    /// </summary>
    public sealed class LocatorController
    {
        #region Variables
        private bool _randomColors;
        private int _interval;
        private readonly Random _rnd;
        private readonly DispatcherTimer _dispatcherTimer;
        #endregion

        #region Events
        internal event ColorChangedEvent ColorChangedEvent;
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
            get => _interval;
            set
            {
                if (value <= 0) throw new ArgumentException(nameof(value));
                _interval = value;
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, value);
            }
        }
        #endregion

        /// <summary>
        /// Initialize a new LocatorController
        /// </summary>
        internal LocatorController()
        {
            _rnd = new Random();
            _interval = 1000;
            SelectedColor = Colors.White;

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, _interval);
        }

        /// <summary>
        /// Method that is called when the timer has ticked
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The EventArgs</param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            SelectedColor = GenerateColor();
            ColorChangedEvent?.Invoke(SelectedColor);
        }

        /// <summary>
        /// Generate a new random color
        /// </summary>
        /// <returns>A randomly generated color</returns>
        internal Color GenerateColor()
        {
            return Color.FromRgb((byte)_rnd.Next(1, 255), (byte)_rnd.Next(1, 255), (byte)_rnd.Next(1, 255));
        }
    }
}
