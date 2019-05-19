using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CodeDead.UpdateManager.Classes;
using DeadPix.Business.Controller;
using DeadPix.Business.Gui;
using DeadPix.Business.Utils;

namespace DeadPix.Views
{
    /// <inheritdoc cref="Syncfusion.Windows.Shared.ChromelessWindow" />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Variables
        private readonly UpdateManager _updateManager;
        private readonly LocatorController _locatorController;
        #endregion

        /// <inheritdoc />
        /// <summary>
        /// Initialize a new MainWindow object
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ChangeStyle();
            LoadProperties();

            _locatorController = new LocatorController();

            StringVariables stringVariables = new StringVariables
            {
                CancelButtonText = "Cancel",
                DownloadButtonText = "Download",
                InformationButtonText = "Information",
                NoNewVersionText = "You are using the latest version!",
                TitleText = "DeadPix",
                UpdateNowText = "Would you like to update DeadPix now?"
            };

            _updateManager = new UpdateManager(Assembly.GetExecutingAssembly().GetName().Version, "https://codedead.com/Software/DeadPix/update.xml", stringVariables);

            try
            {
                if (Properties.Settings.Default.AutoUpdate)
                {
                    _updateManager.CheckForUpdate(false, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DeadPix", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Change the style of this window
        /// </summary>
        internal void ChangeStyle()
        {
            StyleManager.ChangeStyle(this);
        }

        /// <summary>
        /// Load the properties for DeadPix
        /// </summary>
        internal void LoadProperties()
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
        /// Exit DeadPix
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Open a new AboutWindow object
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void AboutMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog();
        }

        /// <summary>
        /// Open the help documentation
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void HelpMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\help.pdf");
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
        private void HomePageMenuItem_OnClick(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// Open the license file for DeadPix
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void LicenseMenuItem_OnClick(object sender, RoutedEventArgs e)
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
        /// Open the Donation page
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void DonateMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://codedead.com/?page_id=302");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DeadPix", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Check for application updates
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void UpdateMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _updateManager.CheckForUpdate(true, true);
        }

        /// <summary>
        /// Method that is called when the Locator button is activated
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnLocator_OnClick(object sender, RoutedEventArgs e)
        {
            Color selectedColor = Utils.GenerateColor();
            if (RdbRed.IsChecked != null && RdbRed.IsChecked.Value) selectedColor = Colors.Red;
            if (RdbGreen.IsChecked != null && RdbGreen.IsChecked.Value) selectedColor = Colors.Green;
            if (RdbBlue.IsChecked != null && RdbBlue.IsChecked.Value) selectedColor = Colors.Blue;
            if (RdbBlack.IsChecked != null && RdbBlack.IsChecked.Value) selectedColor = Colors.Black;
            if (RdbRandom.IsChecked != null)
            {
                if (RdbRandom.IsChecked.Value)
                {
                    _locatorController.RandomizeColors = true;
                    _locatorController.Interval = (int) SldLocatorInterval.Value;
                }
                else
                {
                    _locatorController.RandomizeColors = false;
                }
            }
            if (RdbCustom.IsChecked != null && RdbCustom.IsChecked.Value) selectedColor = CpLocatorCustom.Color;

            _locatorController.SelectedColor = selectedColor;
            new LocatorWindow(_locatorController).ShowDialog();
        }

        /// <summary>
        /// Method that is called when the SelectorWindow should be opened
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnFixer_OnClick(object sender, RoutedEventArgs e)
        {
            FixerController controller = new FixerController {Interval = (int) SldFixerInterval.Value};
            if (ChbStopAfter.IsChecked != null && IntStopAfter.Value != null && ChbStopAfter.IsChecked.Value)
            {
                int stopAfter;
                switch (CboStopAfter.SelectedIndex)
                {
                    default:
                        stopAfter = (int) IntStopAfter.Value.Value;
                        break;
                    case 1:
                        stopAfter = (int)IntStopAfter.Value.Value * 1000;
                        break;
                    case 2:
                        stopAfter = (int)IntStopAfter.Value.Value * 60 * 1000;
                        break;
                    case 3:
                        stopAfter = (int)IntStopAfter.Value.Value * 60 * 60 * 1000;
                        break;
                }

                controller.StopAfter = stopAfter;
            }
            new FixerWindow(controller).Show();
        }
    }
}
