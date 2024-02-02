using Paintc.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Model
{
    public class EllipseShape : ShapeBase
    {
        private readonly Ellipse _ellipse;

        public EllipseShape()
        {
            _ellipse = new Ellipse
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
        }

        public override void SetLastMousePosition(Point lastPosition)
        {
            LastMousePosition = lastPosition;
            Canvas.SetLeft(_ellipse, lastPosition.X);
            Canvas.SetTop(_ellipse, lastPosition.Y);
            _ellipse.Width = 0;
            _ellipse.Height = 0;
        }

        public override void SetCurrentMousePosition(Point currentPosition)
        {
            CurrentMousePosition = currentPosition;
            double width = currentPosition.X - LastMousePosition.X;
            double height = currentPosition.Y - LastMousePosition.Y;
            double radius = Math.Min(Math.Abs(width), Math.Abs(height)) / 2;
            _ellipse.Width = radius * 2;
            _ellipse.Height = radius * 2;
        }

        public override Shape GetShape() => _ellipse;
    }

}
