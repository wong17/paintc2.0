using Paintc.Enums;
using Paintc.Model;
using Paintc.Service;
using Paintc.View.UserControls;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Paintc.Controller.UserControls
{
    public class DrawingPanelController
    {
        private readonly DrawingPanel DrawingPanel;
        private Toolbox? toolbox;

        #region ZOOM
        
        private const double SCALE_FACTOR = 0.1;
        private const double MAX_ZOOM_IN = 5.0;
        private const double MAX_ZOOM_OUT = 0.9;
        private readonly ScaleTransform scaleTransform = new();

        #endregion

        public DrawingPanelController(DrawingPanel drawingPanel)
        {
            DrawingPanel = drawingPanel;
            InitController();
        }

        private void InitController()
        {
            ToolService.Instance.ToolboxEventHandler += ToolboxEventHandler;
            // Events
            DrawingPanel.CustomCanvas.LayoutTransform = scaleTransform;
            DrawingPanel.MainScrollViewer.PreviewMouseWheel += MainScrollViewer_PreviewMouseWheel;
            DrawingPanel.CustomCanvas.MouseWheel += CustomCanvas_MouseWheel;
            DrawingPanel.CustomCanvas.MouseLeftButtonDown += CustomCanvas_MouseLeftButtonDown;
            DrawingPanel.CustomCanvas.MouseMove += CustomCanvas_MouseMove;
        }

        private void ToolboxEventHandler(object? sender, Toolbox toolbox)
        {
            this.toolbox = toolbox;
        }

        #region ZOOM

        private void CustomCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) return;

            // Obtener posición del mouse sobre el canvas
            Point mousePosition = e.GetPosition(DrawingPanel.CustomCanvas);
            // Positivo si se gira hacia adelante (hacia el monitor), Negativo si se gira hacia atras (hacia el usuario)
            var factor = (e.Delta > 0) ? SCALE_FACTOR : -SCALE_FACTOR;
            // Aplicar escalación centrada en la posición actual del mouse
            scaleTransform.CenterX = mousePosition.X;
            scaleTransform.CenterY = mousePosition.Y;
            scaleTransform.ScaleX = factor < 0 ? Math.Max(MAX_ZOOM_OUT, scaleTransform.ScaleX + factor) : Math.Min(MAX_ZOOM_IN, scaleTransform.ScaleX + factor);
            scaleTransform.ScaleY = factor < 0 ? Math.Max(MAX_ZOOM_OUT, scaleTransform.ScaleY + factor) : Math.Min(MAX_ZOOM_IN, scaleTransform.ScaleY + factor);
            // Ajustar las barras de desplazamiento del ScrollViewer
            DrawingPanel.MainScrollViewer.ScrollToHorizontalOffset(DrawingPanel.MainScrollViewer.HorizontalOffset + mousePosition.X * factor);
            DrawingPanel.MainScrollViewer.ScrollToVerticalOffset(DrawingPanel.MainScrollViewer.VerticalOffset + mousePosition.Y * factor);
        }

        private void MainScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Delta > 0 && IsMaxZoomInReached()) e.Handled = true;
        }

        private bool IsMaxZoomInReached() => scaleTransform.ScaleX == MAX_ZOOM_IN && scaleTransform.ScaleY == MAX_ZOOM_IN;

        #endregion

        private void CustomCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            
            if (toolbox is null)
                return;

            switch (toolbox.CurrentTool)
            {
                case ToolType.SelectTool:
                    SelectToolOnMouseMove(e);
                    break;
                case ToolType.RectangleTool:
                    RectangleToolOnMouseMove(e);
                    break;
                case ToolType.CircleTool:
                    CircleToolOnMouseMove(e);
                    break;
                case ToolType.EraserTool:
                    EraseToolOnMouseMove(e);
                    break;
                case ToolType.PencilTool:
                    PencilToolOnMouseMove(e);
                    break;
                case ToolType.LineTool:
                    LineToolOnMouseMove(e);
                    break;
                default:
                    break;
            }
        }

        private void SelectToolOnMouseMove(MouseEventArgs e)
        {
            Debug.WriteLine($"SelectTool: {toolbox?.CurrentTool} mouse move");
        }

        private void RectangleToolOnMouseMove(MouseEventArgs e)
        {
            Debug.WriteLine($"RectangleTool: {toolbox?.CurrentTool} mouse move");
        }

        private void CircleToolOnMouseMove(MouseEventArgs e)
        {
            Debug.WriteLine($"CircleTool: {toolbox?.CurrentTool} mouse move");
        }

        private void EraseToolOnMouseMove(MouseEventArgs e)
        {
            Debug.WriteLine($"EraseTool: {toolbox?.CurrentTool} mouse move");
        }

        private void PencilToolOnMouseMove(MouseEventArgs e)
        {
            Debug.WriteLine($"PencilTool: {toolbox?.CurrentTool} mouse move");
        }

        private void LineToolOnMouseMove(MouseEventArgs e)
        {
            Debug.WriteLine($"LineTool: {toolbox?.CurrentTool} mouse move");
        }

        private void CustomCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (toolbox is null) 
                return;

            switch (toolbox.CurrentTool)
            {
                case ToolType.SelectTool:
                    break;
                case ToolType.RectangleTool:
                    break;
                case ToolType.CircleTool:
                    break;
                case ToolType.PolygonTool:
                    break;
                case ToolType.FillerTool:
                    break;
                case ToolType.EraserTool:
                    break;
                case ToolType.PencilTool:
                    break;
                case ToolType.LineTool:
                    break;
                default:
                    break;
            }

            Debug.WriteLine($"SE SELECCIONO: {toolbox?.CurrentTool} desde toolbox hacia drawingpanel");
        }
    }
}
