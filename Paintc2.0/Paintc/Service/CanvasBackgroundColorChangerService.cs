using Paintc.Model;

namespace Paintc.Service
{
    public class CanvasBackgroundColorChangerService
    {
        private static CanvasBackgroundColorChangerService _instance = new();
        public static CanvasBackgroundColorChangerService Instance => _instance;
        private CanvasBackgroundColorChangerService() { }

        // Notifica cuando se selecciona un color de fondo diferente para el canvas
        public event EventHandler<CGAColor>? ChangeBackgroundColorEventHandler;
        public void ChangeBackgroundColor(CGAColor color) => NotifyObservers(color);
        private void NotifyObservers(CGAColor color) => ChangeBackgroundColorEventHandler?.Invoke(this, color);
    }
}
