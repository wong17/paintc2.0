using Paintc.Controller.UserControls.ShapeProperties.Interface;
using Paintc.Core;
using Paintc.Shapes;

namespace Paintc.Controller.UserControls.ShapeProperties
{
    public class PropertiesPanelNavigator : ControllerBase
    {
        private IPropertiesController? _currentPanel;
        private ShapeBase? _currentShape;
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

        /// <summary>
        /// Devuelve el panel de propiedades según la figura que se selecciono
        /// </summary>
        /// <param name="shapeBase"></param>
        /// <returns></returns>
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

            _currentShape = shapeBase;
            return _currentPanel;
        }

        /// <summary>
        /// Actualiza las propiedades del panel según los cambios que presente la figura
        /// </summary>
        /// <param name="shapeBase"></param>
        public void UpdatePropertiesPanel()
        {
            if (_currentPanel is null || _currentShape is null)
                return;

            _currentPanel.UpdateProperties();
        }
    }
}
