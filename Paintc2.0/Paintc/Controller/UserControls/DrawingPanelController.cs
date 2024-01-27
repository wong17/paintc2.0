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
        private readonly MatrixTransform matrixTransform = new();

        private bool isSpacebarPressed = false;
        private Point lastMousePosition;

        public DrawingPanelController(DrawingPanel drawingPanel)
        {
            _DrawingPanel = drawingPanel;
            InitController();
        }

        private void InitController()
        {
            _DrawingPanel.MainScrollViewer.PreviewMouseWheel += ScrollViewer_PreviewMouseWheel;
            _DrawingPanel.MainScrollViewer.PreviewMouseMove += ScrollViewer_PreviewMouseMove;
            _DrawingPanel.MainScrollViewer.PreviewKeyDown += ScrollViewer_PreviewKeyDown;
            _DrawingPanel.MainScrollViewer.PreviewKeyUp += ScrollViewer_PreviewKeyUp;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control)
            {
                e.Handled = true;
                return;
            }
            // Positivo si se gira hacia adelante (hacia el monitor), Negativo si se gira hacia atras (hacia el usuario)
            var factor = (e.Delta > 0) ? (1.09) : (1 / 1.09);
            // Obtener posición del mouse
            Point mousePosition = e.GetPosition(_DrawingPanel.CustomCanvas);
            // Crear matriz de escalación asignando la matriz de transformación actual (anterior)
            Matrix scaleMatrix = matrixTransform.Matrix;
            // Escalar centrado en la posición actual del mouse
            scaleMatrix.ScaleAt(factor, factor, mousePosition.X, mousePosition.Y);
            // Asignar la nueva matriz escalada a la matriz de transformación
            matrixTransform.Matrix = scaleMatrix;

            // Aplica la transformación de escala al Canvas
            _DrawingPanel.CustomCanvas.LayoutTransform = matrixTransform;

            // Ajusta las barras de desplazamiento del ScrollViewer
            _DrawingPanel.MainScrollViewer.ScrollToHorizontalOffset(_DrawingPanel.MainScrollViewer.HorizontalOffset * factor);
            _DrawingPanel.MainScrollViewer.ScrollToVerticalOffset(_DrawingPanel.MainScrollViewer.VerticalOffset * factor);

            // Indica que el evento ha sido manejado
            e.Handled = true;
        }

        private void ScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isSpacebarPressed)
            {
                // Obtener nueva posición del mouse
                Point newMousePosition = e.GetPosition(_DrawingPanel.CustomCanvas);
                // Calcular diferencia en el cambio de posición
                Vector delta = Point.Subtract(newMousePosition, lastMousePosition);
                // Ajustar scroll
                _DrawingPanel.MainScrollViewer.ScrollToHorizontalOffset(_DrawingPanel.MainScrollViewer.HorizontalOffset - delta.X);
                _DrawingPanel.MainScrollViewer.ScrollToVerticalOffset(_DrawingPanel.MainScrollViewer.VerticalOffset - delta.Y);

                // Actualiza ultima posición
                lastMousePosition = newMousePosition;
            }
        }

        private void ScrollViewer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                lastMousePosition = Mouse.GetPosition(_DrawingPanel.CustomCanvas);
                isSpacebarPressed = true;
            }
        }

        private void ScrollViewer_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                isSpacebarPressed = false;
            }
        }
    }
}
