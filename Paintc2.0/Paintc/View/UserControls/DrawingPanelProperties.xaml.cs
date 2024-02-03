using Paintc.Controller;
using System.Windows.Controls;

namespace Paintc.View.UserControls
{
    /// <summary>
    /// Interaction logic for DrawingPanelProperties.xaml
    /// </summary>
    public partial class DrawingPanelProperties : UserControl
    {
        public DrawingPanelProperties()
        {
            InitializeComponent();
            _ = new DrawingPanelPropertiesController(this);
        }
    }
}
