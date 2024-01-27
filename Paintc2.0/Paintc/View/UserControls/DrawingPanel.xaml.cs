using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Paintc.View.UserControls
{
    /// <summary>
    /// Interaction logic for DrawingPanel.xaml
    /// </summary>
    public partial class DrawingPanel : UserControl
    {
        // Crear matrix que va a contener la transformación a aplicar
        private readonly MatrixTransform matrixTransform = new();

        public DrawingPanel()
        {
            InitializeComponent();
        }

        private void MainScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) return;
            // Positivo si se gira hacia adelante (hacia el monitor), Negativo si se gira hacia atras (hacia el usuario)
            var factor = (e.Delta > 0) ? (1.09) : (1 / 1.09);
            // Obtener posición del mouse
            Point mousePosition = e.GetPosition(this);
            // Crear matriz de escalación asignando la matriz de transformación actual (anterior)
            Matrix scaleMatrix = matrixTransform.Matrix;
            // Escalar centrado en la posición actual del mouse
            scaleMatrix.ScaleAt(factor, factor, mousePosition.X, mousePosition.Y);
            // Asignar la nueva matriz escalada a la matriz de transformación
            matrixTransform.Matrix = scaleMatrix;

            // Aplica la transformación de escala al Canvas
            CustomCanvas.LayoutTransform = matrixTransform;

            // Ajusta las barras de desplazamiento del ScrollViewer
            MainScrollViewer.ScrollToHorizontalOffset(MainScrollViewer.HorizontalOffset * factor);
            MainScrollViewer.ScrollToVerticalOffset(MainScrollViewer.VerticalOffset * factor);

            // Indica que el evento ha sido manejado
            e.Handled = true;

        }

        private void CustomCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void CustomCanvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
