using Paintc.Controller;
using Paintc.Controller.UserControls;
using Paintc.Views;
using System.Windows;
using System.Windows.Controls;

namespace Paintc.ViewModels
{
    public class MainWindowController : ControllerBase
    {
        private readonly MainWindow MainWindow;

        private readonly DrawingPanelController _DrawingPanelController;
        private readonly ToolboxPanelController _ToolboxPanelController;

        public MainWindowController(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            _DrawingPanelController = new(MainWindow.ucDrawingPanel);
            _ToolboxPanelController = new(MainWindow.ucToolboxPanel);
            InitController();
        }

        private void InitController()
        {
            MainWindow.Loaded += Window_Loaded;
            MainWindow.AboutMenuItem.Click += MenuItem_Click;
            MainWindow.ExitMenuItem.Click += MenuItem_Click;
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
                    new AboutWindowController().ShowAboutWindow(MainWindow);
                    break;
                default: 
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.ucDrawingPanel.MainScrollViewer.Focus();
        }
    }
}
