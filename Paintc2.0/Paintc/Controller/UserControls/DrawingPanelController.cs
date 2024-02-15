using Paintc.Core;
using Paintc.Enums;
using Paintc.Model;
using Paintc.Service;
using Paintc.View.UserControls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Paintc.Controller.UserControls
{
    public class DrawingPanelController : ControllerBase
    {
        private readonly DrawingPanel _drawingPanel;
        private GraphicMode? _graphicMode;

        #region ZOOM

        /// <summary>
        /// Factor de incremento/decremento de la matriz del canvas
        /// </summary>
        private const double SCALE_FACTOR = 0.1;
        /// <summary>
        /// Maximo y minimo de zoom permitido
        /// </summary>
        private const double MAX_ZOOM_IN = 5.0;
        private const double MAX_ZOOM_OUT = 0.9;
        /// <summary>
        /// Para aplicar escalación de la matriz del canvas centrada en la posición actual del mouse
        /// </summary>
        private readonly ScaleTransform scaleTransform = new();

        #endregion

        public DrawingPanelController(DrawingPanel drawingPanel)
        {
            _drawingPanel = drawingPanel;

            DrawingHandler.Instance.DrawingPanel = drawingPanel;
            DrawingPanelPropertiesService.Instance.CanvasResizerEventHandler += CanvasResizerEventHandler;
            DrawingPanelPropertiesService.Instance.ChangeBackgroundColorEventHandler += ChangeBackgroundColorEventHandler;

            // Events
            _drawingPanel.CustomCanvas.LayoutTransform = scaleTransform;
            _drawingPanel.MainScrollViewer.PreviewMouseWheel += MainScrollViewer_PreviewMouseWheel;
            _drawingPanel.MainScrollViewer.PreviewMouseMove += MainScrollViewer_PreviewMouseMove;
            _drawingPanel.CustomCanvas.MouseWheel += CustomCanvas_MouseWheel;
            _drawingPanel.CustomCanvas.MouseLeftButtonDown += CustomCanvas_MouseLeftButtonDown;
            _drawingPanel.CustomCanvas.MouseRightButtonDown += CustomCanvas_MouseRightButtonDown;
            _drawingPanel.CustomCanvas.MouseMove += CustomCanvas_MouseMove;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (DrawingHandler.Instance.State == DrawingState.Finished)
                return;

            if (DrawingHandler.Instance.Toolbox is null)
                return;

            /* Para terminar y agregar figura al explorador cuando se esta dibujando y se suelta el click izquierdo fuera del canvas. */
            var currentTool = DrawingHandler.Instance.Toolbox.CurrentTool;
            if (e.LeftButton == MouseButtonState.Released && (currentTool != ToolType.SelectTool || currentTool != ToolType.FillerTool || currentTool != ToolType.EraserTool))
            {
                DrawingHandler.Instance.FinishAndAddShape();
                e.Handled = true;
            }
        }

        #region ZOOM

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) return;

            // Obtener posición del mouse sobre el canvas
            Point mousePosition = e.GetPosition(_drawingPanel.CustomCanvas);
            // Positivo si se gira hacia adelante (hacia el monitor), Negativo si se gira hacia atras (hacia el usuario)
            var factor = (e.Delta > 0) ? SCALE_FACTOR : -SCALE_FACTOR;
            // Aplicar escalación centrada en la posición actual del mouse
            scaleTransform.CenterX = mousePosition.X;
            scaleTransform.CenterY = mousePosition.Y;
            scaleTransform.ScaleX = factor < 0 ? Math.Max(MAX_ZOOM_OUT, scaleTransform.ScaleX + factor) : Math.Min(MAX_ZOOM_IN, scaleTransform.ScaleX + factor);
            scaleTransform.ScaleY = factor < 0 ? Math.Max(MAX_ZOOM_OUT, scaleTransform.ScaleY + factor) : Math.Min(MAX_ZOOM_IN, scaleTransform.ScaleY + factor);
            // Ajustar las barras de desplazamiento del ScrollViewer
            _drawingPanel.MainScrollViewer.ScrollToHorizontalOffset(_drawingPanel.MainScrollViewer.HorizontalOffset + mousePosition.X * factor);
            _drawingPanel.MainScrollViewer.ScrollToVerticalOffset(_drawingPanel.MainScrollViewer.VerticalOffset + mousePosition.Y * factor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Delta > 0 && IsMaxZoomInReached()) e.Handled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsMaxZoomInReached() => scaleTransform.ScaleX == MAX_ZOOM_IN && scaleTransform.ScaleY == MAX_ZOOM_IN;

        #endregion

        #region CANVASRESIZER_EVENT

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="graphicMode"></param>
        public void CanvasResizerEventHandler(object? sender, GraphicMode? graphicMode)
        {
            _graphicMode = graphicMode;
            
            if (_graphicMode is null) return;

            // Comprobar si se ha dibujado...
            if (DrawingHandler.Instance.Shapes.Count != 0) 
            {
                // Preguntar si desea cambiar el tamaño de la ventana...
                var result =  MessageBox.Show("Are you sure you want to resize the drawing area? Any unsaved progress will be lost.", "Warning", 
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    DrawingPanelPropertiesService.Instance.UpdateGraphicModeSelection(false);
                    return;
                }

                DrawingHandler.Instance.ClearDrawingPanel();
            }
            _drawingPanel.CustomCanvas.Width = _graphicMode.Width;
            _drawingPanel.CustomCanvas.Height = _graphicMode.Height;
            DrawingPanelPropertiesService.Instance.UpdateGraphicModeSelection(true);
        }

        #endregion

        #region CANVASBGCOLOR_EVENT

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="color"></param>
        public void ChangeBackgroundColorEventHandler(object? sender, CGAColor color)
        {
            _drawingPanel.CustomCanvas.Background = new SolidColorBrush(color.Color);
        }

        #endregion

        #region CUSTOMCANVAS_EVENTS

        private void CustomCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            DrawingHandler.Instance.OnMouseMove(sender, e);
            StatusBarPanelService.Instance.UpdateMousePosition(e.GetPosition(_drawingPanel.CustomCanvas));
        }

        private void CustomCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DrawingHandler.Instance.OnMouseLeftButtonDown(sender, e);
        }

        private void CustomCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DrawingHandler.Instance.OnMouseRightButtonDown(sender, e);
        }

        #endregion

    }
}
