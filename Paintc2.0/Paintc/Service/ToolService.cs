using Paintc.Model;

namespace Paintc.Service
{
    public class ToolService
    {
        // Singleton
        private static readonly ToolService instance = new();
        public static ToolService Instance => instance;
        private ToolService() { }

        private readonly Toolbox Toolbox = new();

        public void UpdateCurrentTool(string currentTool)
        {
            if (Toolbox.CurrentTool != currentTool)
            {
                Toolbox.CurrentTool = currentTool;
                NotifyObservers(Toolbox);
            }
        }

        // Código a ejecutar cuando se produzca un cambio
        public event EventHandler<Toolbox>? ToolboxEventHandler;
        private void NotifyObservers(Toolbox toolbox) => ToolboxEventHandler?.Invoke(this, toolbox);
    }

}
