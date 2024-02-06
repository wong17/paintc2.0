using Paintc.Core;
using Paintc.Model;
using Paintc.Service;
using Paintc.View.UserControls;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paintc.Controller.UserControls
{
    public class DrawingPanelPropertiesController : ControllerBase
    {
        private readonly DrawingPanelProperties _drawingPanelProperties;
        private readonly List<GraphicMode> _graphicModes;
        private GraphicMode? _currentGraphiceMode;

        private readonly List<CGAColor> _colors;
        private CGAColor _currentColor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drawingPanelProperties"></param>
        public DrawingPanelPropertiesController(DrawingPanelProperties drawingPanelProperties)
        {
            _drawingPanelProperties = drawingPanelProperties;
            _drawingPanelProperties.GraphicsModeCmbbox.SelectionChanged += GraphicsModeCmbbox_SelectionChanged;
            _drawingPanelProperties.BackgroundColorCmbbox.SelectionChanged += BackgroundColorCmbbox_SelectionChanged;

            _colors = CGAColorPaletteService.GetColorPalette();
            _currentColor = _colors.First();

            _graphicModes = GraphicModeService.GetGraphicModes();
            _currentGraphiceMode = _graphicModes[0];

            _drawingPanelProperties.BackgroundColorCmbbox.ItemsSource = _colors;
            _drawingPanelProperties.BackgroundColorCmbbox.DisplayMemberPath = "Cpalette";
            _drawingPanelProperties.BackgroundColorCmbbox.SelectedItem = _currentColor;

            _drawingPanelProperties.GraphicsModeCmbbox.ItemsSource = _graphicModes;
            _drawingPanelProperties.GraphicsModeCmbbox.DisplayMemberPath = "DisplayName";
            _drawingPanelProperties.GraphicsModeCmbbox.SelectedItem = _currentGraphiceMode;

            CanvasResizerService.Instance.UpdateGraphicModeSelectionEventHandler += UpdateGraphicModeSelection;
            CanvasBackgroundColorChangerService.Instance.ChangeBackgroundColor(_currentColor);
            _drawingPanelProperties.BackgroundColorRectangle.Fill = new SolidColorBrush(_currentColor.Color);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="flag"></param>
        private void UpdateGraphicModeSelection(object? sender, bool flag)
        {
            if (flag)
                _currentGraphiceMode = _drawingPanelProperties.GraphicsModeCmbbox.SelectedItem as GraphicMode;
            else
            {
                /* para resetear la selección en el combobox cuando el usuario no quizo cambiar el tamaño sin ejecutar el evento SelectionChanged. */
                _drawingPanelProperties.GraphicsModeCmbbox.SelectionChanged -= GraphicsModeCmbbox_SelectionChanged;
                _drawingPanelProperties.GraphicsModeCmbbox.SelectedItem = _currentGraphiceMode;
                _drawingPanelProperties.GraphicsModeCmbbox.SelectionChanged += GraphicsModeCmbbox_SelectionChanged;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphicsModeCmbbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_drawingPanelProperties.GraphicsModeCmbbox.SelectedItem is not GraphicMode selectedGraphicMode || selectedGraphicMode.DisplayName is null)
                return;

            // Notificar solo cuando se seleccione una resolución distinta
            if (!selectedGraphicMode.DisplayName.Equals(_currentGraphiceMode))
                CanvasResizerService.Instance.UpdateGraphicMode(selectedGraphicMode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundColorCmbbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_drawingPanelProperties.BackgroundColorCmbbox.SelectedItem is not CGAColor color)
                return;

            if (color.Cpalette != _currentColor.Cpalette)
            {
                // Actualizar nuevo color...
                _currentColor = color;
                // Cambiar color del fondo
                CanvasBackgroundColorChangerService.Instance.ChangeBackgroundColor(_currentColor);
                _drawingPanelProperties.BackgroundColorRectangle.Fill = new SolidColorBrush(_currentColor.Color);
            }
        }
    }
}
