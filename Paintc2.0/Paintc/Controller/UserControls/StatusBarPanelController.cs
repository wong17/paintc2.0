using Paintc.Enums;
using Paintc.Service;
using Paintc.Service.Collections;
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
            // Herramienta inicial: Selection, Color inicial: Black
            StatusBarPanelService.Instance.UpdateCurrentTool(ToolType.SelectTool);
            StatusBarPanelService.Instance.UpdateCurrentColor(CGAColorPalette.Black);
            SelectedColorService.Instance.UpdateSelectedColor(CGAColorPalette.Black);
        }
        /// <summary>
        /// Actualiza la posición del mouse sobre el canvas en la barra de estado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="point"></param>
        private void UpdateMousePositionEventHandler(object? sender, Point point)
        {
            _statusBarPanel.MousePositionLabel.Content = $"Mouse position: ({Convert.ToInt32(point.X)},{Convert.ToInt32(point.Y)})";
        }

        /// <summary>
        /// Actualiza la herramienta seleccionada/actual en la barra de estado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tool"></param>
        private void UpdateCurrentToolEventHandler(object? sender, ToolType tool)
        {
            _statusBarPanel.SelectedToolLabel.Content = $"Current tool: {tool.ToString().Replace("Tool", "")}";
        }

        /// <summary>
        /// Actualiza el color seleccionado/actual en la barra de estado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="color"></param>
        private void UpdateCurrentColorEventHandler(object? sender, CGAColorPalette color)
        {
            _statusBarPanel.SelectedColorRectangle.Fill = new SolidColorBrush(CGAColorPaletteService.GetColor(color));
        }
    }
}
