using Paintc.Model;

namespace Paintc.Service
{
    public class CanvasResizerService
    {
        private static CanvasResizerService _instance = new();
        public static CanvasResizerService Instance => _instance;
        private CanvasResizerService() { }
        private GraphicMode? _graphicMode;
        public void UpdateGraphicMode(GraphicMode? graphicMode)
        {
            _graphicMode = graphicMode;
            NotifyObservers(_graphicMode);
        }

        // Código a ejecutar cuando se produzca un cambio
        public event EventHandler<GraphicMode?>? CanvasResizerEventHandler;
        private void NotifyObservers(GraphicMode? graphicMode) => CanvasResizerEventHandler?.Invoke(this, graphicMode);
    }
}
