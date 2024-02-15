using Paintc.Model;

namespace Paintc.Service
{
    public class DrawingPanelPropertiesService
    {
        private static readonly DrawingPanelPropertiesService _instance = new();
        public static DrawingPanelPropertiesService Instance => _instance;
        private DrawingPanelPropertiesService() { }

        // Cuando se seleccione una resolución en el combobox
        public event EventHandler<GraphicMode?>? CanvasResizerEventHandler;
        private void NotifyObservers(GraphicMode? graphicMode) => CanvasResizerEventHandler?.Invoke(this, graphicMode);
        public void UpdateGraphicMode(GraphicMode? graphicMode) => NotifyObservers(graphicMode);

        // Para resetear la selección y dejar el modo actual
        public event EventHandler<bool>? UpdateGraphicModeSelectionEventHandler;
        public void UpdateGraphicModeSelection(bool flag) => NotifyObservers(flag);
        private void NotifyObservers(bool flag) => UpdateGraphicModeSelectionEventHandler?.Invoke(this, flag);

        // Notifica cuando se selecciona un color de fondo diferente para el canvas
        public event EventHandler<CGAColor>? ChangeBackgroundColorEventHandler;
        public void ChangeBackgroundColor(CGAColor color) => NotifyObservers(color);
        private void NotifyObservers(CGAColor color) => ChangeBackgroundColorEventHandler?.Invoke(this, color);
    }
}
