using Paintc.Enums;
using Paintc.Service;
using Paintc.View.UserControls;
using System.Windows;
using System.Windows.Media;

namespace Paintc.Controller.UserControls
{
    public class StatusBarPanelController
    {
        private readonly StatusBarPanel _statusBarPanel;

        public StatusBarPanelController(StatusBarPanel statusBarPanel)
        {
            _statusBarPanel = statusBarPanel;
            StatusBarPanelService.Instance.UpdateMousePositionEventHandler += UpdateMousePositionEventHandler;
            StatusBarPanelService.Instance.UpdateCurrentToolEventHandler += UpdateCurrentToolEventHandler;
            StatusBarPanelService.Instance.UpdateCurrentColorEventHandler += UpdateCurrentColorEventHandler;

            StatusBarPanelService.Instance.UpdateCurrentTool(ToolType.SelectTool);
            StatusBarPanelService.Instance.UpdateCurrentColor(CGAColorPalette.Black);
        }

        private void UpdateMousePositionEventHandler(object? sender, Point point)
        {
            _statusBarPanel.MousePositionLabel.Content = $"Mouse position: ({Convert.ToInt32(point.X)},{Convert.ToInt32(point.Y)})";
        }

        private void UpdateCurrentToolEventHandler(object? sender, ToolType tool)
        {
            _statusBarPanel.SelectedToolLabel.Content = $"Current tool: {tool.ToString().Replace("Tool", "")}";
        }

        private void UpdateCurrentColorEventHandler(object? sender, CGAColorPalette color)
        {
            _statusBarPanel.SelectedColorRectangle.Fill = new SolidColorBrush(
                CGAColorPaletteService.GetColorPalette()
                .Where(c => c.Cpalette == color)
                .First().Color);
        }
    }
}
