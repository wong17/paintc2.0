using Paintc.Controller.UserControls;
using System.Windows.Controls;

namespace Paintc.View.UserControls
{
    /// <summary>
    /// Interaction logic for PropertiesPanel.xaml
    /// </summary>
    public partial class PropertiesPanel : UserControl
    {
        public PropertiesPanel()
        {
            InitializeComponent();
            _ = new DrawingPanelPropertiesController(this);
        }
    }
}
