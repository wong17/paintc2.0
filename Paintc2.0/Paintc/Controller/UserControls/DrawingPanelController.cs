using Paintc.View.UserControls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Paintc.Controller.UserControls
{
    public class DrawingPanelController
    {
        private readonly DrawingPanel _DrawingPanel;

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
            _DrawingPanel.CustomCanvas.MouseWheel += CustomCanvas_MouseWheel;
        }

        private void CustomCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) return;

            // Positivo si se gira hacia adelante (hacia el monitor), Negativo si se gira hacia atras (hacia el usuario)
            var factor = (e.Delta > 0) ? (0.1) : (-0.1);
            // Obtener posición del mouse sobre el canvas
            Point mousePosition = e.GetPosition(_DrawingPanel.CustomCanvas);
            // Aplicar escalación centrada sobre la posición actual del mouse
            scaleTransform.CenterX = mousePosition.X;
            scaleTransform.CenterY = mousePosition.Y;
            scaleTransform.ScaleX = Math.Max(1.0, scaleTransform.ScaleX + factor);
            scaleTransform.ScaleY = Math.Max(1.0, scaleTransform.ScaleY + factor);
            // Ajustar las barras de desplazamiento del ScrollViewer
            _DrawingPanel.MainScrollViewer.ScrollToHorizontalOffset(_DrawingPanel.MainScrollViewer.HorizontalOffset + mousePosition.X * factor);
            _DrawingPanel.MainScrollViewer.ScrollToVerticalOffset(_DrawingPanel.MainScrollViewer.VerticalOffset + mousePosition.Y * factor);
        }
    }
}
