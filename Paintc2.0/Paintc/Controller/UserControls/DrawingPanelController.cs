using Paintc.Core;
using Paintc.View.UserControls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Paintc.Controller.UserControls
{
    public class DrawingPanelController : ControllerBase
    {
        private readonly DrawingPanel DrawingPanel;
        private readonly DrawingHandler _drawingHandler;

        #region ZOOM

        private const double SCALE_FACTOR = 0.1;
        private const double MAX_ZOOM_IN = 5.0;
        private const double MAX_ZOOM_OUT = 0.9;
        private readonly ScaleTransform scaleTransform = new();

        #endregion

        public DrawingPanelController(DrawingPanel drawingPanel)
        {
            DrawingPanel = drawingPanel;
            _drawingHandler = new DrawingHandler(DrawingPanel);
            InitController();
        }

        private void InitController()
        {
            // Events
            DrawingPanel.CustomCanvas.LayoutTransform = scaleTransform;
            DrawingPanel.MainScrollViewer.PreviewMouseWheel += MainScrollViewer_PreviewMouseWheel;
            DrawingPanel.CustomCanvas.MouseWheel += CustomCanvas_MouseWheel;
            DrawingPanel.CustomCanvas.MouseLeftButtonDown += CustomCanvas_MouseLeftButtonDown;
            DrawingPanel.CustomCanvas.MouseRightButtonDown += CustomCanvas_MouseRightButtonDown;
            DrawingPanel.CustomCanvas.MouseMove += CustomCanvas_MouseMove;
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
            _drawingHandler.OnMouseMove(sender, e);
        }

        private void CustomCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _drawingHandler.OnMouseLeftButtonDown(sender, e);
        }

        private void CustomCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _drawingHandler.OnMouseRightButtonDown(sender, e);
        }

    }
}
