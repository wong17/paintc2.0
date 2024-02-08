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

        private void AboutMenuItemClickCommand(object? obj)
        {
            DialogService.OpenDialog<AboutWindow>(Application.Current.MainWindow, new AboutWindowController());
        }

        private void ExitMenuItemClickCommand(object? obj)
        {
            Application.Current.Shutdown();
        }

        private void SaveMenuItemClickCommand(object? obj)
        {
            
        }

    }
}
