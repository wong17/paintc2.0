using Paintc.Commands;
using Paintc.Core;
using Paintc.Service;
using Paintc.View;
using System.Windows;
using System.Windows.Input;

namespace Paintc.Controller
{
    public class MainWindowController : ControllerBase
    {
        public ICommand SaveMenuItemClick { get; private set; }
        public ICommand ExitMenuItemClick { get; private set; }
        public ICommand AboutMenuItemClick { get; private set; }

        public MainWindowController()
        {
            SaveMenuItemClick = new RelayCommand((obj) => true, SaveMenuItemClickCommand);
            ExitMenuItemClick = new RelayCommand((obj) => true, ExitMenuItemClickCommand);
            AboutMenuItemClick = new RelayCommand((obj) => true, AboutMenuItemClickCommand);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        private void AboutMenuItemClickCommand(object? obj)
        {
            DialogService.OpenDialog<AboutWindow>(Application.Current.MainWindow, new AboutWindowController());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        private void ExitMenuItemClickCommand(object? obj)
        {
            if (DrawingHandler.Instance.Shapes.Count == 0)
            {
                Application.Current.Shutdown();
                return;
            }

            var result = MessageBox.Show("Do you want to exit without saving?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.No)
                return;

            Application.Current.Shutdown();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        private void SaveMenuItemClickCommand(object? obj)
        {
            CanvasImageSaverService.SaveCanvasContent();
        }
    }
}