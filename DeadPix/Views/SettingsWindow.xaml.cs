using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DeadPix.Business.Gui;

namespace DeadPix.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        #region Variables
        private readonly MainWindow _mainWindow;
        #endregion

        /// <summary>
        /// Initialize a new SettingsWindow
        /// </summary>
        public SettingsWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            StyleManager.ChangeStyle(this);
            LoadSettings();
        }

        /// <summary>
        /// Load all the required settings
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                if (Properties.Settings.Default.WindowDraggable)
                {
                    // Remove possible redundant handler
                    MouseDown -= OnMouseDown;
                    MouseDown += OnMouseDown;
                }
                else
                {
                    MouseDown -= OnMouseDown;
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
        /// Method that is called when the style has changed
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The SelectionChangedEventArgs</param>
        private void ChbStyle_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StyleManager.ChangeStyle(this);
        }

        /// <summary>
        /// Method that is called when the border thickness has changed
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedPropertyChangedEventArgs</param>
        private void SldBorderThickness_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BorderThickness = new Thickness(((Slider)sender).Value);
        }

        /// <summary>
        /// Method that is called when the opacity has changed
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedPropertyChangedEventArgs</param>
        private void SldOpacity_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Opacity = ((Slider)sender).Value / 100;
        }

        /// <summary>
        /// Method that is called when the resize border thickness has changed
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedPropertyChangedEventArgs</param>
        private void SldWindowResize_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ResizeBorderThickness = new Thickness(((Slider)sender).Value);
        }

        /// <summary>
        /// Method that is called when all settings should be reset to their default values
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset all settings?", "DeadPix", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();

                _mainWindow.LoadProperties();
                StyleManager.ChangeStyle(_mainWindow);
                StyleManager.ChangeStyle(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DeadPix", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method that is called when the properties should be saved
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.Save();

                LoadSettings();

                _mainWindow.LoadProperties();

                StyleManager.ChangeStyle(_mainWindow);
                StyleManager.ChangeStyle(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DeadPix", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method that is called when the window is closing
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The CancelEventArgs</param>
        private void SettingsWindow_OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                Properties.Settings.Default.Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DeadPix", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
