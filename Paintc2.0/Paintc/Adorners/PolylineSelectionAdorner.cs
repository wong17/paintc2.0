using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Paintc.Adorners
{
    public class PolylineSelectionAdorner : Adorner
    {
        private readonly Rect rect;
        private readonly Pen _renderPen;
        private readonly DoubleAnimation _dashOffsetAnimation;

        public PolylineSelectionAdorner(UIElement adornedElement) : base(adornedElement)
        {
            var polyline = (Polyline) adornedElement;
            // Rectángulo final que rodea la figura
            rect = polyline.RenderedGeometry.Bounds;
            // Crear trazo de lineas discontinuas para usar como borde de la figura/forma
            _renderPen = new(Brushes.DodgerBlue, 2)
            {
                DashStyle = new DashStyle(new double[] { 4, 4 }, 0)
            };
            // Crea la animación de desplazamiento del trazo de lineas discontinuas
            _dashOffsetAnimation = new()
            {
                From = 0,
                To = 15,
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever,
                SpeedRatio = 1
            };
            // Aplicamos la animación al DashOffset del DashStyle del Pen
            _renderPen.DashStyle.BeginAnimation(DashStyle.OffsetProperty, _dashOffsetAnimation);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            // Dibujamos el rectángulo con el trazo animado
            drawingContext.DrawRectangle(Brushes.Transparent, _renderPen, rect);
        }
    }
}
