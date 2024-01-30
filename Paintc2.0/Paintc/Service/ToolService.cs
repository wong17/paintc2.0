using Paintc.Enums;
using Paintc.Model;

namespace Paintc.Service
{
    public class ToolService
    {
        // Singleton
        private static readonly ToolService instance = new();
        public static ToolService Instance => instance;
        private ToolService() { }

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
