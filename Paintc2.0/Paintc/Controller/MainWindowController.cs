using Paintc.Core;
using Paintc.View;
using Paintc.Views;
using System.Windows;
using System.Windows.Controls;

namespace Paintc.Controller
{
    public class MainWindowController : ControllerBase
    {
        private readonly MainWindow MainWindow;

        public MainWindowController(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            InitController();
        }

        private void InitController()
        {
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

                    break;
                default: 
                    break;
            }
        }

    }
}
