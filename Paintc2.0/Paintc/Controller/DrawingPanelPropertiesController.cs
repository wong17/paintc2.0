using Paintc.Core;
using Paintc.Model;
using Paintc.Service;
using Paintc.View.UserControls;
using System.Windows.Controls;

namespace Paintc.Controller
{
    public class DrawingPanelPropertiesController : ControllerBase
    {
        private readonly DrawingPanelProperties _drawingPanelProperties;
        public List<GraphicMode> graphicModes;
        private GraphicMode? _currentGraphiceMode;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drawingPanelProperties"></param>
        public DrawingPanelPropertiesController(DrawingPanelProperties drawingPanelProperties)
        {
            _drawingPanelProperties = drawingPanelProperties;
            _drawingPanelProperties.GraphicsModeCmbbox.SelectionChanged += GraphicsModeCmbbox_SelectionChanged;
            
            graphicModes = GraphicModeService.GetGraphicModes();
            _currentGraphiceMode = graphicModes[0];
            
            _drawingPanelProperties.GraphicsModeCmbbox.ItemsSource = graphicModes;
            _drawingPanelProperties.GraphicsModeCmbbox.DisplayMemberPath = "DisplayName";
            _drawingPanelProperties.GraphicsModeCmbbox.SelectedItem = _currentGraphiceMode;
            
            CanvasResizerService.Instance.UpdateGraphicModeSelectionEventHandler += UpdateGraphicModeSelection;
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
    }
}
