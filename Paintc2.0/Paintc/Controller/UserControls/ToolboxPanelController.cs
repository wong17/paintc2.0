using Paintc.View.UserControls;
using System.Windows.Input;

namespace Paintc.Controller.UserControls
{
    public class ToolboxPanelController
    {
        private readonly ToolboxPanel _ToolboxPanel;
        
        public ToolboxPanelController(ToolboxPanel toolboxPanel)
        {
            _ToolboxPanel = toolboxPanel;
            InitController();
        }

        private void InitController()
        {
            
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}
