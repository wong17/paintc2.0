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

        public void UpdateGraphicMode(GraphicMode? graphicMode) => CanvasResizerEventHandler?.Invoke(this, graphicMode);

        // Para resetear la selección y dejar el modo actual
        public event EventHandler<bool>? UpdateGraphicModeSelectionEventHandler;

        public void UpdateGraphicModeSelection(bool flag) => UpdateGraphicModeSelectionEventHandler?.Invoke(this, flag);

        // Notifica cuando se selecciona un color de fondo diferente para el canvas
        public event EventHandler<CGAColor>? ChangeBackgroundColorEventHandler;

        public void ChangeBackgroundColor(CGAColor color) => ChangeBackgroundColorEventHandler?.Invoke(this, color);

        // Notifica cuando hay una forma/figura seleccionada para habilitar o deshabilitar opciones
        public event EventHandler<ShapeBase?>? SetEnableShapeOptionsEventHandler;

        public void SetEnableShapeOptions(ShapeBase? selectedShape) => SetEnableShapeOptionsEventHandler?.Invoke(this, selectedShape);

        // Notifica que panel de propiedades mostrar según la figura/forma seleccionada en el canvas
        public event EventHandler<ShapeBase?>? ShowPropertiesPanelEventHandler;

        public void ShowPropertiesPanel(ShapeBase? selectedShape) => ShowPropertiesPanelEventHandler?.Invoke(this, selectedShape);

        // Actualiza los valores del panel de propiedades mientras esta seleccionada la figura y se esta modificando
        public event EventHandler<object?>? UpdatePropertiesPanelEventHandler;

        public void UpdatePropertiesPanel(object? obj = null) => UpdatePropertiesPanelEventHandler?.Invoke(this, obj);

        // Notifica que debe mostrar el grid en el canvas
        public event EventHandler<bool>? ShowGridEventHandler;

        public void ShowGrid(bool flag) => ShowGridEventHandler?.Invoke(this, flag);

        // Actualiza el tamaño del grid
        public event EventHandler<double>? UpdateGridSizeEventHandler;

        public void UpdateGridSize(double newSize) => UpdateGridSizeEventHandler?.Invoke(this, newSize);

        // Notifica cuando se ha seleccionado una imagen para poner de fondo en el canvas
        public event EventHandler<string?>? SelectBackgroundImageEventHandler;

        public void SelectBackgroundImage(string? imagePath) => SelectBackgroundImageEventHandler?.Invoke(this, imagePath);
    }
}