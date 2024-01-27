using Paintc.View;
using Paintc.Views;

namespace Paintc.Controller
{
    public class AboutWindowController
    {
        private AboutWindow? _AboutWindow;

        public void ShowAboutWindow(MainWindow mainWindow)
        {
            _AboutWindow = new AboutWindow
            {
                Owner = mainWindow
            };
            _AboutWindow.ShowDialog();
        }
    }
}
