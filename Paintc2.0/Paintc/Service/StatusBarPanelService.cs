using Paintc.Enums;
using System.Windows;

namespace Paintc.Service
{
    public class StatusBarPanelService
    {
        // Singleton
        private static readonly StatusBarPanelService instance = new();
        public static StatusBarPanelService Instance => instance;
        private StatusBarPanelService() { }

        // Código a ejecutar cuando se produzca un cambio en la posición del mouse sobre el canvas
        public event EventHandler<Point>? UpdateMousePositionEventHandler;
        private void NotifyMousePositionObservers(Point point) => UpdateMousePositionEventHandler?.Invoke(this, point);
        public void UpdateMousePosition(Point point) => NotifyMousePositionObservers(point);

        // Código a ejecutar cuando se seleccione una herramienta diferente desde el panel de herramientas
        public event EventHandler<ToolType>? UpdateCurrentToolEventHandler;
        private void NotifyCurrentToolObservers(ToolType tool) => UpdateCurrentToolEventHandler?.Invoke(this, tool);
        public void UpdateCurrentTool(ToolType tool) => NotifyCurrentToolObservers(tool);

        // Código a ejecutar cuando se seleccione un color diferente desde el panel de herramientas
        public event EventHandler<CGAColorPalette>? UpdateCurrentColorEventHandler;
        private void NotifyCurrentColorObservers(CGAColorPalette color) => UpdateCurrentColorEventHandler?.Invoke(this, color);
        public void UpdateCurrentColor(CGAColorPalette color) => NotifyCurrentColorObservers(color);
    }
}
