using System.Windows;

namespace Paintc.Service
{
    public static class DialogService
    {
        /// <summary>
        /// Open a dialog window centered based on the owner's position.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        public static void OpenDialog<T>(Window parent, object? dataContext) where T : Window
        {
            var window = Activator.CreateInstance<T>();

            if (dataContext is not null)
                window.DataContext = dataContext;

            window.Owner = parent;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
    }
}