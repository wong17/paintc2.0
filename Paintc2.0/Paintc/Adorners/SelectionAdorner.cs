using System.Globalization;
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
            Rect rectangleBounds = new(0, 0, size.Width, size.Height);
            drawingContext.DrawRectangle(Brushes.Transparent, renderPen, rectangleBounds);

            // Dibujamos el rectángulo para mostrar el ancho y el alto
            double rectWidth = 70;
            double rectHeight = 20;
            Rect textRectBounds = new((size.Width - rectWidth) / 2, size.Height + 10, rectWidth, rectHeight);

            drawingContext.DrawRectangle(Brushes.DodgerBlue, new Pen(Brushes.DodgerBlue, 1), textRectBounds);

            // Dibujamos texto con el ancho y alto de la figura dentro del rectángulo
            FormattedText formattedText = new($"{Convert.ToInt32(size.Width)}x{Convert.ToInt32(size.Height)}",
                CultureInfo.GetCultureInfo("en-us"),
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