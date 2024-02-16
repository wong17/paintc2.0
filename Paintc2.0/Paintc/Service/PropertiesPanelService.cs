using Paintc.Core;
using Paintc.Model;

namespace Paintc.Service
{
    public class PropertiesPanelService
    {
        private static readonly PropertiesPanelService _instance = new();
        public static PropertiesPanelService Instance => _instance;

        private PropertiesPanelService()
        { }

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

        // Notifica cuando hay una forma/figura seleccionada para habilitar o deshabilitar opciones
        public event EventHandler<ShapeBase?>? SetEnableShapeOptionsEventHandler;

        public void SetEnableShapeOptions(ShapeBase? selecteShape) => NotifyObservers(selecteShape);

        private void NotifyObservers(ShapeBase? selecteShape) => SetEnableShapeOptionsEventHandler?.Invoke(this, selecteShape);
    }
}