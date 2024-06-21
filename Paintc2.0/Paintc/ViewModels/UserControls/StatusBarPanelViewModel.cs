using Paintc.Core;
using Paintc.Enums;
using Paintc.Service;
using Paintc.Service.Collections;
using System.Windows;
using System.Windows.Media;

namespace Paintc.ViewModels.UserControls
{
    public class StatusBarPanelViewModel : ViewModel
    {
        private string? _mousePositionText = "Mouse position: (0,0)";

        public string? MousePositionText 
        {
            get => _mousePositionText; 
            set => SetField(ref _mousePositionText, value);
        }

        private string? _selectedToolText = "Current tool: Select";

        public string? SelectedToolText
        {
            get => _selectedToolText;
            set => SetField(ref _selectedToolText, value);
        }

        private string? _hintText = "Ctrl + Scroll: Zoom in/out (inside drawing area)";

        public string? HintText
        {
            get => _hintText;
            set => SetField(ref _hintText, value);
        }

        private SolidColorBrush? _brush;
        
        public SolidColorBrush? Brush
        {
            get => _brush;
            set => SetField(ref _brush, value);
        }

        public StatusBarPanelViewModel()
        {
            StatusBarPanelService.Instance.UpdateMousePositionEventHandler += UpdateMousePositionEventHandler;
            StatusBarPanelService.Instance.UpdateCurrentToolEventHandler += UpdateCurrentToolEventHandler;
            StatusBarPanelService.Instance.UpdateCurrentColorEventHandler += UpdateCurrentColorEventHandler;
            StatusBarPanelService.Instance.UpdateHintTextEventHandler += UpdateHintTextEventHandler;
            // Herramienta inicial: Selection, Color inicial: Black
            StatusBarPanelService.Instance.UpdateCurrentTool(ToolType.SelectTool);
            StatusBarPanelService.Instance.UpdateCurrentColor(CGAColorPalette.White);
        }

        /// <summary>
        /// Actualiza la posición del mouse sobre el canvas en la barra de estado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="point"></param>
        private void UpdateMousePositionEventHandler(object? sender, Point point) => 
            MousePositionText = $"Mouse position: ({Convert.ToInt32(point.X)},{Convert.ToInt32(point.Y)})";

        /// <summary>
        /// Actualiza la herramienta seleccionada/actual en la barra de estado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tool"></param>
        private void UpdateCurrentToolEventHandler(object? sender, ToolType tool) =>
            SelectedToolText = $"Current tool: {tool.ToString().Replace("Tool", "")}";

        /// <summary>
        /// Actualiza el color seleccionado/actual en la barra de estado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="color"></param>
        private void UpdateCurrentColorEventHandler(object? sender, CGAColorPalette color) => 
            Brush = new SolidColorBrush(CGAColorPaletteService.GetColor(color));

        /// <summary>
        /// Actualiza el mensaje de ayuda en la barra de estado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void UpdateHintTextEventHandler(object? sender, string? hint) => HintText = hint;
    }
}