using System.Windows;

namespace Paintc.Service
{
    public static class DialogService
    {
        /// <summary>
        /// Opens a dialog window
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataContext"></param>
        public static void OpenDialog<T>(object dataContext) where T : Window
        {
            var window = Activator.CreateInstance<T>();
            window.DataContext = dataContext;
            window.ShowDialog();
        }

        public static void OpenDialog<T>() where T : Window
        {
            var window = Activator.CreateInstance<T>();
            window.ShowDialog();
        }
    }
}
