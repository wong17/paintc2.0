using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Paintc.Adorners
{
    public class PolygonSelectionAdorner(UIElement adornedElement) : Adorner(adornedElement)
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            var polygon = (Polygon)AdornedElement;
            // Rectángulo final que rodea la figura
            Rect rect = polygon.RenderedGeometry.Bounds;
            // Crear trazo de lineas discontinuas para usar como borde de la figura/forma
            Pen renderPen = new(Brushes.DodgerBlue, 2)
            {
                DashStyle = new DashStyle([4, 4], 0)
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

            // Dibujamos el rectángulo para mostrar el ancho y el alto
            double textRectWidth = 70;
            double textRectHeight = 20;
            Rect textRectBounds = new(rect.Left + (rect.Width - textRectWidth) / 2, rect.Bottom + 10, textRectWidth, textRectHeight);

            drawingContext.DrawRectangle(Brushes.DodgerBlue, new Pen(Brushes.DodgerBlue, 1), textRectBounds);

            // Dibujamos texto con el ancho y alto de la figura dentro del rectángulo
            FormattedText formattedText = new($"{Convert.ToInt32(rect.Width)}x{Convert.ToInt32(rect.Height)}",
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Arial"),
                12,
                Brushes.White,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);

            double textX = textRectBounds.Left + (textRectBounds.Width - formattedText.Width) / 2;
            double textY = textRectBounds.Top + (textRectBounds.Height - formattedText.Height) / 2;

            drawingContext.DrawText(formattedText, new Point(textX, textY));
        }
    }
}
