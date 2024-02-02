using Paintc.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Model
{
    public class RectangleShape : ShapeBase
    {
        private readonly Rectangle _rectangle;

        public RectangleShape()
        {
            _rectangle = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
        }

        public override Shape GetShape() => _rectangle;

        public override void SetCurrentMousePosition(Point currentPosition)
        {
            CurrentMousePosition = currentPosition;
            double width = currentPosition.X - LastMousePosition.X;
            double height = currentPosition.Y - LastMousePosition.Y;
            _rectangle.Width = Math.Abs(width);
            _rectangle.Height = Math.Abs(height);
        }

        public override void SetLastMousePosition(Point lastPosition)
        {
            LastMousePosition = lastPosition;
            _rectangle.Width = 0;
            _rectangle.Height = 0;
            Canvas.SetLeft(_rectangle, lastPosition.X);
            Canvas.SetTop(_rectangle, lastPosition.Y);
        }
    }
}
