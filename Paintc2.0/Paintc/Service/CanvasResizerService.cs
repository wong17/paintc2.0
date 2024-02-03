using Paintc.Model;

namespace Paintc.Service
{
    public class CanvasResizerService
    {
        private static CanvasResizerService _instance = new();
        public static CanvasResizerService Instance => _instance;
        private CanvasResizerService() { }

        // Cuando se seleccione una resolución en el combobox
        public event EventHandler<GraphicMode?>? CanvasResizerEventHandler;
        private void NotifyObservers(GraphicMode? graphicMode) => CanvasResizerEventHandler?.Invoke(this, graphicMode);
        public void UpdateGraphicMode(GraphicMode? graphicMode) => NotifyObservers(graphicMode);

        // Para resetear la selección y dejar el modo actual
        public event EventHandler<bool>? UpdateGraphicModeSelectionEventHandler;
        public void UpdateGraphicModeSelection(bool flag) => NotifyObservers(flag);
        private void NotifyObservers(bool flag) => UpdateGraphicModeSelectionEventHandler?.Invoke(this, flag);
    }
}
