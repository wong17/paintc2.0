using Paintc.Controller.UserControls;
using System.Windows.Controls;

namespace Paintc.View.UserControls
{
    /// <summary>
    /// Interaction logic for StatusBarPanel.xaml
    /// </summary>
    public partial class StatusBarPanel : UserControl
    {
        public StatusBarPanel()
        {
            InitializeComponent();
            _ = new StatusBarPanelController(this);
        }
    }
}