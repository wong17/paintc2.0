using System.Windows;

namespace Paintc.AttachedProperties.Window
{
    public class MainWindowAttachedProperty : DependencyObject
    {
        public static bool GetSaveWindowState(UIElement element) => (bool)element.GetValue(SaveWindowStateProperty);

        public static void SetSaveWindowState(UIElement element, bool value) => element.SetValue(SaveWindowStateProperty, value);

        // Using a DependencyProperty as the backing store for SaveWindowState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SaveWindowStateProperty =
            DependencyProperty.RegisterAttached("SaveWindowState", typeof(bool), typeof(MainWindowAttachedProperty), new PropertyMetadata(false));
    }
}
