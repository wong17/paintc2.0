using Paintc.Core;
using Paintc.Enums;
using Paintc.Service;
using Paintc.View.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

namespace Paintc.Controller.UserControls
{
    public class ToolboxPanelController : ControllerBase
    {
        private readonly ToolboxPanel ToolboxPanel;

        public ToolboxPanelController(ToolboxPanel toolboxPanel)
        {
            ToolboxPanel = toolboxPanel;
            InitController();
        }

        private void InitController()
        {
            ToolboxPanel.SelectTool.MouseEnter += Image_MouseEnter;
            ToolboxPanel.RectangleTool.MouseEnter += Image_MouseEnter;
            ToolboxPanel.EllipseTool.MouseEnter += Image_MouseEnter;
            ToolboxPanel.PolygonTool.MouseEnter += Image_MouseEnter;
            ToolboxPanel.FillerTool.MouseEnter += Image_MouseEnter;
            ToolboxPanel.EraserTool.MouseEnter += Image_MouseEnter;
            ToolboxPanel.PencilTool.MouseEnter += Image_MouseEnter;
            ToolboxPanel.LineTool.MouseEnter += Image_MouseEnter;

            ToolboxPanel.SelectTool.MouseLeftButtonDown += ImageToolbox_MouseLeftButtonDown;
            ToolboxPanel.RectangleTool.MouseLeftButtonDown += ImageToolbox_MouseLeftButtonDown;
            ToolboxPanel.EllipseTool.MouseLeftButtonDown += ImageToolbox_MouseLeftButtonDown;
            ToolboxPanel.PolygonTool.MouseLeftButtonDown += ImageToolbox_MouseLeftButtonDown;
            ToolboxPanel.FillerTool.MouseLeftButtonDown += ImageToolbox_MouseLeftButtonDown;
            ToolboxPanel.EraserTool.MouseLeftButtonDown += ImageToolbox_MouseLeftButtonDown;
            ToolboxPanel.PencilTool.MouseLeftButtonDown += ImageToolbox_MouseLeftButtonDown;
            ToolboxPanel.LineTool.MouseLeftButtonDown += ImageToolbox_MouseLeftButtonDown;
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void ImageToolbox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Image tool)
                return;

            if (Enum.TryParse(tool.Name, out ToolType selectedTool))
                ToolService.Instance.UpdateCurrentTool(selectedTool);
        }
    }
}
