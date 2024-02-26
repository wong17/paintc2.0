using Paintc.Controller.UserControls.ShapeProperties.Interface;
using Paintc.Core;
using Paintc.Shapes.CSClasses;

namespace Paintc.Controller.UserControls.ShapeProperties
{
    public class PropertiesPanelNavigator : ControllerBase
    {
        private IPropertiesController? _currentPanel;
        private ShapeBase? _currentShape;
        private readonly Dictionary<Type, IPropertiesController> _propertiesControllers;

        public PropertiesPanelNavigator()
        {
            _propertiesControllers = new Dictionary<Type, IPropertiesController>
            {
                { typeof(RectangleShape), new RectanglePropertiesController() },
                { typeof(EllipseShape), new EllipsePropertiesController() },
                { typeof(LineShape), new LinePropertiesController() },
                { typeof(FreeShape), new PencilPropertiesController() }
            };
        }

        /// <summary>
        /// Devuelve el panel de propiedades según la figura que se selecciono
        /// </summary>
        /// <param name="shapeBase"></param>
        /// <returns></returns>
        public IPropertiesController? GetPropertiesPanel(ShapeBase? shapeBase)
        {
            if (shapeBase is null || !_propertiesControllers.ContainsKey(shapeBase.GetType()))
                return null;

            _currentPanel = _propertiesControllers[shapeBase.GetType()];
            _currentPanel.SetShape(shapeBase);
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