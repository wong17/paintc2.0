using Paintc.Core;
using Paintc.Shapes;

namespace Paintc.Controller.UserControls.ShapeProperties
{
    public class PropertiesPanelNavigator : ControllerBase
    {
        private object? _currentPanel;
        private readonly RectanglePropertiesController _rectangleProperties;
        private readonly EllipsePropertiesController _ellipseProperties;
        private readonly LinePropertiesController _lineProperties;
        private readonly PencilPropertiesController _pencilProperties;

        public PropertiesPanelNavigator()
        {
            _rectangleProperties = new();
            _ellipseProperties = new();
            _lineProperties = new();
            _pencilProperties = new();
        }

        public object? GetPropertiesPanel(ShapeBase? shapeBase)
        {
            if (shapeBase is null) 
                return null;

            if (shapeBase is RectangleShape rectangle)
            {
                _rectangleProperties.RectangleShape = rectangle;
                _currentPanel = _rectangleProperties;
            }
            else if (shapeBase is EllipseShape ellipse)
            {
                _ellipseProperties.EllipseShape = ellipse;
                _currentPanel = _ellipseProperties;
            }
            else if (shapeBase is LineShape line)
            {
                _lineProperties.LineShape = line;
                _currentPanel = _lineProperties;
            }
            else if (shapeBase is FreeShape free)
            {
                _pencilProperties.FreeShape = free;
                _currentPanel = _pencilProperties;
            }

            return _currentPanel;
        }

        public void UpdatePropertiesPanel(ShapeBase? shapeBase)
        {
            if (_currentPanel is null || shapeBase is null)
                return;


        }
    }
}
