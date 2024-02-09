using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Paintc.Adorners
{
    public class SelectionAdorner(UIElement adornedElement) : Adorner(adornedElement)
    {
        /// <summary>
        /// Dibuja un borde de lineas discontinuas en movimiento al seleccionar una figura
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            Size size = AdornedElement.RenderSize;
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
            Rect rectangleBounds = new(0, 0, size.Width, size.Height);
            drawingContext.DrawRectangle(Brushes.Transparent, renderPen, rectangleBounds);
        }
    }
}
