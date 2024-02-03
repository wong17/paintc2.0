using Paintc.Core;
using Paintc.Model;
using Paintc.Service;
using Paintc.View.UserControls;
using System.Windows.Controls;

namespace Paintc.Controller
{
    public class DrawingPanelPropertiesController : ControllerBase
    {
        private readonly DrawingPanelProperties drawingPanelProperties;
        public List<GraphicMode> graphicModes;
        private int currentGraphiceModeIndex = 0;

        public DrawingPanelPropertiesController(DrawingPanelProperties drawingPanelProperties)
        {
            this.drawingPanelProperties = drawingPanelProperties;
            this.drawingPanelProperties.GraphicsModeCmbbox.SelectionChanged += GraphicsModeCmbbox_SelectionChanged;
            graphicModes = GraphicModeService.GetGraphicModes();
            this.drawingPanelProperties.GraphicsModeCmbbox.ItemsSource = graphicModes;
            this.drawingPanelProperties.GraphicsModeCmbbox.DisplayMemberPath = "DisplayName";
            this.drawingPanelProperties.GraphicsModeCmbbox.SelectedIndex = currentGraphiceModeIndex;
        }

        private void GraphicsModeCmbbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedGraphicModeIndex = drawingPanelProperties.GraphicsModeCmbbox.SelectedIndex;
            if (selectedGraphicModeIndex != currentGraphiceModeIndex)
            {
                CanvasResizerService.Instance.UpdateGraphicMode(drawingPanelProperties.GraphicsModeCmbbox.SelectedItem as GraphicMode);
                currentGraphiceModeIndex = selectedGraphicModeIndex;
            }
        }
    }
}
