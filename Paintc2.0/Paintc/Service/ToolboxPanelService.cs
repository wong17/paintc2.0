using Paintc.Enums;
using Paintc.Model;

namespace Paintc.Service
{
    public class ToolboxPanelService
    {
        private static readonly ToolboxPanelService _instance = new();
        public static ToolboxPanelService Instance => _instance;

        private ToolboxPanelService()
        { }

        private readonly Toolbox toolbox = new();

        public void UpdateCurrentTool(ToolType currentTool)
        {
            if (toolbox.CurrentTool != currentTool)
            {
                toolbox.CurrentTool = currentTool;
                NotifyObservers(toolbox);
            }
        }

        // Código a ejecutar cuando se produzca un cambio
        public event EventHandler<Toolbox>? ToolboxEventHandler;

        private void NotifyObservers(Toolbox toolbox) => ToolboxEventHandler?.Invoke(this, toolbox);

        // Código a ejecutar cuando se produzca un cambio
        public event EventHandler<CGAColorPalette>? UpdateSelectedColorEventHandler;

        public void UpdateSelectedColor(CGAColorPalette color) => UpdateSelectedColorEventHandler?.Invoke(this, color);
    }
}