using Paintc.Controller;
using Paintc.Views;
using System.Windows;
using System.Windows.Controls;

namespace Paintc.ViewModels
{
    public class MainWindowController : ControllerBase
    {
        private readonly MainWindow _MainWindow;

        public MainWindowController(MainWindow mainWindow)
        {
            _MainWindow = mainWindow;
            InitController();
        }

        private void InitController()
        {
            _MainWindow.Loaded += Window_Loaded;
            _MainWindow.AboutMenuItem.Click += MenuItem_Click;
            _MainWindow.ExitMenuItem.Click += MenuItem_Click;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not MenuItem menuItem)
                return;

            switch(menuItem.Name)
            {
                case "ExitMenuItem":
                    Application.Current.Shutdown();
                    break;
                case "AboutMenuItem":
                    new AboutWindowController().ShowAboutWindow(_MainWindow);
                    break;
                default: 
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _MainWindow.ucDrawingPanel.MainScrollViewer.Focus();
        }
    }
}
