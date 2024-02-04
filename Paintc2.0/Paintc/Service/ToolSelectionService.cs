using Paintc.Enums;
using Paintc.Model;

namespace Paintc.Service
{
    public class ToolSelectionService
    {
        // Singleton
        private static readonly ToolSelectionService instance = new();
        public static ToolSelectionService Instance => instance;
        private ToolSelectionService() { }

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
    }

}
