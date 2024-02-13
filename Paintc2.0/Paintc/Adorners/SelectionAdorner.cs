using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Paintc.Adorners
{
    public class SelectionAdorner : Adorner
    {
        private readonly Size _size;
        private readonly Pen _renderPen;
        private readonly DoubleAnimation _dashOffsetAnimation;

        public SelectionAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _size = AdornedElement.RenderSize;
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

        /// <summary>
        /// Dibuja un borde de lineas discontinuas en movimiento al seleccionar una figura
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            // Dibujamos el rectángulo con el trazo animado
            Rect rectangleBounds = new(0, 0, _size.Width, _size.Height);
            drawingContext.DrawRectangle(Brushes.Transparent, _renderPen, rectangleBounds);
        }
    }
}
