using Paintc.Enums;
using System.Windows;

namespace Paintc.Service
{
    public class StatusBarPanelService
    {
        // Singleton
        private static readonly StatusBarPanelService instance = new();

        public static StatusBarPanelService Instance => instance;

        private StatusBarPanelService()
        { }

        // Código a ejecutar cuando se produzca un cambio en la posición del mouse sobre el canvas
        public event EventHandler<Point>? UpdateMousePositionEventHandler;

        public void UpdateMousePosition(Point point) => UpdateMousePositionEventHandler?.Invoke(this, point);

        // Código a ejecutar cuando se seleccione una herramienta diferente desde el panel de herramientas
        public event EventHandler<ToolType>? UpdateCurrentToolEventHandler;

        public void UpdateCurrentTool(ToolType tool) => UpdateCurrentToolEventHandler?.Invoke(this, tool);

        // Código a ejecutar cuando se seleccione un color diferente desde el panel de herramientas
        public event EventHandler<CGAColorPalette>? UpdateCurrentColorEventHandler;

        public void UpdateCurrentColor(CGAColorPalette color) => UpdateCurrentColorEventHandler?.Invoke(this, color);

        // Código a ejecutar para mostrar una ayuda al usuario sobre los controles de la herramienta actualmente seleccionada
        public event EventHandler<string?>? UpdateHintTextEventHandler;

        public void UpdateHintText(string? hint) => UpdateHintTextEventHandler?.Invoke(this, hint);
    }
}