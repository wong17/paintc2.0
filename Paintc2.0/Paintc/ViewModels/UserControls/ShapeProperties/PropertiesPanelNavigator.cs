using Paintc.ViewModels.UserControls.ShapeProperties.Interface;
using Paintc.Core;
using Paintc.Shapes;

namespace Paintc.ViewModels.UserControls.ShapeProperties
{
    public class PropertiesPanelNavigator : ViewModel
    {
        private IPropertiesViewModel? _currentPanel;
        private ShapeBase? _currentShape;
        private readonly Dictionary<Type, IPropertiesViewModel> _propertiesControllers;

        public PropertiesPanelNavigator()
        {
            _propertiesControllers = new Dictionary<Type, IPropertiesViewModel>
            {
                { typeof(RectangleShape), new RectanglePropertiesViewModel() },
                { typeof(EllipseShape), new EllipsePropertiesViewModel() },
                { typeof(LineShape), new LinePropertiesViewModel() },
                { typeof(PencilShape), new PencilPropertiesViewModel() }
            };
        }

        /// <summary>
        /// Devuelve el panel de propiedades según la figura que se selecciono
        /// </summary>
        /// <param name="shapeBase"></param>
        /// <returns></returns>
        public IPropertiesViewModel? GetPropertiesPanel(ShapeBase? shapeBase)
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