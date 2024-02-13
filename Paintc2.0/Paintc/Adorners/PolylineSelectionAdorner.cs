using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Paintc.Adorners
{
    public class PolylineSelectionAdorner(UIElement adornedElement) : Adorner(adornedElement)
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            var polyline = (Polyline)AdornedElement;
            // Rectángulo final que rodea la figura
            Rect rect = polyline.RenderedGeometry.Bounds;
            // Crear trazo de lineas discontinuas para usar como borde de la figura/forma
            Pen renderPen = new(Brushes.DodgerBlue, 2)
            {
                DashStyle = new DashStyle(new double[] { 4, 4 }, 0)
            };
            // Crea la animación de desplazamiento del trazo de lineas discontinuas
            DoubleAnimation dashOffsetAnimation = new()
            {
                From = 0,
                To = 15,
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever,
                SpeedRatio = 1
            };
            // Aplicamos la animación al DashOffset del DashStyle del Pen
            renderPen.DashStyle.BeginAnimation(DashStyle.OffsetProperty, dashOffsetAnimation);
            // Dibujamos el rectángulo con el trazo animado
            drawingContext.DrawRectangle(Brushes.Transparent, renderPen, rect);
        }
    }
}
