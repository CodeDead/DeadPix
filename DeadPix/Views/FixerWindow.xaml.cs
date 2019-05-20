using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DeadPix.Business.Controller;
using DeadPix.Business.Utils;

namespace DeadPix.Views
{
    /// <summary>
    /// Interaction logic for FixerWindow.xaml
    /// </summary>
    public partial class FixerWindow
    {
        #region Variables
        private readonly SolidColorBrush _solidBrush;
        #endregion

        /// <summary>
        /// Initialize a new FixerWindow
        /// </summary>
        /// <param name="fixerController">The FixerController that can be used to fix dead or stuck pixels</param>
        public FixerWindow(FixerController fixerController)
        {
            _solidBrush = new SolidColorBrush(Utils.GenerateColor());
            InitializeComponent();

            Background = _solidBrush;

            fixerController.ColorChangedEvent += FixerControllerOnColorChangedEvent;
            fixerController.FixerStoppedEvent += FixerControllerOnFixerStoppedEvent;
            fixerController.Start();
        }

        /// <summary>
        /// Method that is called when the FixerController has stopped
        /// </summary>
        private void FixerControllerOnFixerStoppedEvent()
        {
            Close();
        }

        /// <summary>
        /// Method that is called when the background color should change
        /// </summary>
        /// <param name="color">The new background color</param>
        private void FixerControllerOnColorChangedEvent(Color color)
        {
            _solidBrush.Color = color;
        }

        /// <summary>
        /// Method that is called when the user double clicks on the window
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The MouseButtonEventArgs</param>
        private void FixerWindow_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Normal;
                ResizeMode = ResizeMode.CanResize;
            }
            else
            {
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
                ResizeMode = ResizeMode.NoResize;
            }
        }

        /// <summary>
        /// Method that is called when a key is released
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The KeyEventArgs</param>
        private void FixerWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) Close();
        }
    }
}
