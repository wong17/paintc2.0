using Paintc.ViewModels.UserControls;
using System.Windows.Controls;

namespace Paintc.Views.UserControls
{
    /// <summary>
    /// Interaction logic for _drawingPanel.xaml
    /// </summary>
    public partial class DrawingPanel : UserControl
    {
        public DrawingPanel()
        {
            InitializeComponent();
            _ = new DrawingPanelViewModel(this);
        }
    }
}