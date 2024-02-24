using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Paintc.Adorners
{
    public class LineSelectionAdorner(UIElement adornedElement) : Adorner(adornedElement)
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (AdornedElement is not Line adornedLine)
                return;

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

            // Aplicamos animación a la linea
            var startPoint = adornedLine.RenderTransform.Transform(new Point(adornedLine.X1, adornedLine.Y1));
            var endPoint = adornedLine.RenderTransform.Transform(new Point(adornedLine.X2, adornedLine.Y2));
            drawingContext.DrawLine(renderPen, startPoint, endPoint);

            // Dibujamos el rectángulo para mostrar el ancho y el alto
            double rectWidth = 50;
            double rectHeight = 20;

            double startTextRectX = (startPoint.X + endPoint.X) / 2 - (rectWidth / 2); // punto medio en x de la linea - mitad del ancho del rectángulo
            double startTextRectY = (startPoint.Y + endPoint.Y) / 2 - (rectHeight / 2);// punto medio en y de la linea - mitad del alto del rectángulo
            double yOffset = 20;
            double angle = GetInclinationAngle(adornedLine); 

            Rect textRectBounds = new(startTextRectX, startTextRectY + yOffset, rectWidth, rectHeight);
            // Rotamos y dibujamos el rectángulo
            drawingContext.PushTransform(new RotateTransform(angle, textRectBounds.Left + textRectBounds.Width / 2, textRectBounds.Top + textRectBounds.Height / 2));
            drawingContext.DrawRectangle(Brushes.DodgerBlue, new Pen(Brushes.DodgerBlue, 1), textRectBounds);

            // Dibujamos texto con la longitud de la linea
            FormattedText formattedText = new($"{Convert.ToInt32(GetHypotenuse(adornedLine))}",
            CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Arial"),
                12,
                Brushes.White,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);

            double textX = textRectBounds.Left + (textRectBounds.Width - formattedText.Width) / 2;
            double textY = textRectBounds.Top + (textRectBounds.Height - formattedText.Height) / 2;
            // Dibujamos el texto
            drawingContext.DrawText(formattedText, new Point(textX, textY));

            // Restauramos la transformación del rectángulo que contiene la longitud de la linea
            drawingContext.Pop();
        }

        private static double GetHypotenuse(Line line) => Math.Sqrt(Math.Pow(line.X2 - line.X1, 2) + Math.Pow(line.Y2 - line.Y1, 2));

        /// <summary>
        /// m = y2 - y1 / x2 - x1
        /// m = tan(x)
        /// atan(m) = x, donde x es devuelto en radianes y se pasa a grados
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static double GetInclinationAngle(Line line) => Math.Atan2(line.Y2 - line.Y1, line.X2 - line.X1) * (180 / Math.PI);
    }
}
