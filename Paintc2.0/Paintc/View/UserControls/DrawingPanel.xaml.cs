using Paintc.Controller.UserControls;
using System.Windows.Controls;

namespace Paintc.View.UserControls
{
    /// <summary>
    /// Interaction logic for _drawingPanel.xaml
    /// </summary>
    public partial class DrawingPanel : UserControl
    {
        public DrawingPanel()
        {
            InitializeComponent();
            _ = new DrawingPanelController(this);
        }
    }
}
