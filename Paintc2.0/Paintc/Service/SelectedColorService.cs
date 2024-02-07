using Paintc.Enums;

namespace Paintc.Service
{
    public class SelectedColorService
    {
        // Singleton
        private static readonly SelectedColorService instance = new();
        public static SelectedColorService Instance => instance;
        private SelectedColorService() { }

        // Código a ejecutar cuando se produzca un cambio
        public event EventHandler<CGAColorPalette>? UpdateSelectedColorEventHandler;
        private void NotifyObservers(CGAColorPalette color) => UpdateSelectedColorEventHandler?.Invoke(this, color);
        public void UpdateSelectedColor(CGAColorPalette color) => NotifyObservers(color);
    }
}
