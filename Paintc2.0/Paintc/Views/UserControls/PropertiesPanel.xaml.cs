using Paintc.ViewModels.UserControls;
using System.Windows.Controls;

namespace Paintc.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PropertiesPanel.xaml
    /// </summary>
    public partial class PropertiesPanel : UserControl
    {
        public PropertiesPanel()
        {
            InitializeComponent();
            _ = new PropertiesPanelViewModel(this);
        }
    }
}