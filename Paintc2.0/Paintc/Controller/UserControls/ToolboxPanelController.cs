using Paintc.Commands;
using Paintc.Core;
using Paintc.Enums;
using Paintc.Model;
using Paintc.Service;
using System.Windows.Input;

namespace Paintc.Controller.UserControls
{
    public class ToolboxPanelController : ControllerBase
    {
        //private readonly ToolboxPanel ToolboxPanel;
        public ICommand? ToolsButtonsClick { get; private set; }
        public ICommand? CGAButtonsClick { get; private set; }

        public List<Tool> ToolItems { get; private set; } = [];
        public List<CGAColor> ColorPaletteItems { get; private set; } = [];

        public ToolboxPanelController()
        {
            ToolItems.AddRange(ToolService.GetTools());
            ColorPaletteItems.AddRange(CGAColorPaletteService.GetColorPalette());
            ToolsButtonsClick = new RelayCommand((obj) => true, ToolsButtonsClickCommand);
            CGAButtonsClick = new RelayCommand((obj) => true, CGAButtonsClickCommand);
        }

        private void ToolsButtonsClickCommand(object? sender)
        {
            if (sender is null)
                return;

            var toolType = (ToolType)sender;
            ToolSelectionService.Instance.UpdateCurrentTool(toolType);
        }

        private void CGAButtonsClickCommand(object? parameter)
        {

        }
    }
}
