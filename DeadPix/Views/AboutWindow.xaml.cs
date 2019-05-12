using System;
using System.Windows;
using System.Windows.Input;
using DeadPix.Business.Gui;

namespace DeadPix.Views
{
    /// <inheritdoc cref="Syncfusion.Windows.Shared.ChromelessWindow" />
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow
    {
        /// <inheritdoc />
        /// <summary>
        /// Initialize a new AboutWindow object
        /// </summary>
        public AboutWindow()
        {
            InitializeComponent();
            ChangeStyle();

            try
            {
                if (Properties.Settings.Default.WindowDraggable)
                {
                    MouseDown += OnMouseDown;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DeadPix", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method that is called when the Window should be dragged
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The MouseButtonEventArgs</param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Change the style of this window
        /// </summary>
        private void ChangeStyle()
        {
            StyleManager.ChangeStyle(this);
        }

        /// <summary>
        /// Close the window
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Open the file containing the license for DeadPix
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnLicense_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\gpl.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DeadPix", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Open the CodeDead website
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnCodeDead_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://codedead.com/");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DeadPix", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
