using Paintc.Commands;
using Paintc.Core;
using System.Windows.Input;

namespace Paintc.Controller.UserControls.ShapeProperties.Util
{
    public class PropertiesPanelNavigation : ObservableObject
    {
        private object? _currentPropertiesPanel;

        public object? CurrentPropertiesPanel
        {
            get => _currentPropertiesPanel;
            set
            {
                _currentPropertiesPanel = value;
                SetField(ref _currentPropertiesPanel, value);
            }
        }

        public ICommand? RectanglePropertiesPanelCommand { get; private set; }
        public ICommand? EllipsePropertiesPanelCommand { get; private set; }
        public ICommand? LinePropertiesPanelCommand { get; private set; }
        public ICommand? PencilPropertiesPanelCommand { get; private set; }

        public PropertiesPanelNavigation()
        {
            RectanglePropertiesPanelCommand = new RelayCommand((obj) => true, ShowRectanglePropertiesPanel);
            EllipsePropertiesPanelCommand = new RelayCommand((obj) => true, ShowEllipsePropertiesPanel);
            LinePropertiesPanelCommand = new RelayCommand((obj) => true, ShowLinePropertiesPanel);
            PencilPropertiesPanelCommand = new RelayCommand((obj) => true, ShowPencilPropertiesPanel);
        }

        private void ShowPencilPropertiesPanel(object? obj) => CurrentPropertiesPanel = new PencilPropertiesController();

        private void ShowLinePropertiesPanel(object? obj) => CurrentPropertiesPanel = new LinePropertiesController();

        private void ShowEllipsePropertiesPanel(object? obj) => CurrentPropertiesPanel = new EllipsePropertiesController();

        private void ShowRectanglePropertiesPanel(object? obj) => CurrentPropertiesPanel = new RectanglePropertiesController();
    }
}