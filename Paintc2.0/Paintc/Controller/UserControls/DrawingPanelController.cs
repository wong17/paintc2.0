using Paintc.View.UserControls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Paintc.Controller.UserControls
{
    public class DrawingPanelController
    {
        private readonly DrawingPanel _DrawingPanel;

        private const double SCALE_FACTOR = 0.1;
        private const double MAX_ZOOM_IN = 5.0;
        private const double MAX_ZOOM_OUT = 0.9;

        // Crear matrix que va a contener la transformación a aplicar
        private readonly ScaleTransform scaleTransform = new();

        public DrawingPanelController(DrawingPanel drawingPanel)
        {
            _DrawingPanel = drawingPanel;
            InitController();
        }

        private void InitController()
        {
            _DrawingPanel.CustomCanvas.LayoutTransform = scaleTransform;
            _DrawingPanel.MainScrollViewer.PreviewMouseWheel += MainScrollViewer_PreviewMouseWheel;
            _DrawingPanel.CustomCanvas.MouseWheel += CustomCanvas_MouseWheel;
        }

        private void CustomCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) return;

            // Obtener posición del mouse sobre el canvas
            Point mousePosition = e.GetPosition(_DrawingPanel.CustomCanvas);
            // Positivo si se gira hacia adelante (hacia el monitor), Negativo si se gira hacia atras (hacia el usuario)
            var factor = (e.Delta > 0) ? SCALE_FACTOR : -SCALE_FACTOR;
            // Aplicar escalación centrada en la posición actual del mouse
            scaleTransform.CenterX = mousePosition.X;
            scaleTransform.CenterY = mousePosition.Y;
            scaleTransform.ScaleX = factor < 0 ? Math.Max(MAX_ZOOM_OUT, scaleTransform.ScaleX + factor) : Math.Min(MAX_ZOOM_IN, scaleTransform.ScaleX + factor);
            scaleTransform.ScaleY = factor < 0 ? Math.Max(MAX_ZOOM_OUT, scaleTransform.ScaleY + factor) : Math.Min(MAX_ZOOM_IN, scaleTransform.ScaleY + factor);
            // Ajustar las barras de desplazamiento del ScrollViewer
            _DrawingPanel.MainScrollViewer.ScrollToHorizontalOffset(_DrawingPanel.MainScrollViewer.HorizontalOffset + mousePosition.X * factor);
            _DrawingPanel.MainScrollViewer.ScrollToVerticalOffset(_DrawingPanel.MainScrollViewer.VerticalOffset + mousePosition.Y * factor);
        }

        private void MainScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Delta > 0 && IsMaxZoomInReached()) e.Handled = true;
        }

        private bool IsMaxZoomInReached() => scaleTransform.ScaleX == MAX_ZOOM_IN && scaleTransform.ScaleY == MAX_ZOOM_IN;
    }
}
