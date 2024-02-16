using Paintc.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Shapes
{
    public class RectangleShape : ShapeBase
    {
        private readonly Rectangle _rectangle;

        public RectangleShape(string? name, Color color) : base(name, color)
        {
            _rectangle = new Rectangle
            {
                Stroke = new SolidColorBrush(color),
                StrokeThickness = 1,
                Fill = new SolidColorBrush(color),
                MinHeight = 10,
                MinWidth = 10
            };
        }

        public override Shape GetShape() => _rectangle;

        public override void SetCurrentMousePosition(Point currentPosition)
        {
            CurrentMousePosition = currentPosition;
            double width = currentPosition.X - LastMousePosition.X;
            double height = currentPosition.Y - LastMousePosition.Y;
            double left = width < 0 ? currentPosition.X : LastMousePosition.X;
            double top = height < 0 ? currentPosition.Y : LastMousePosition.Y;
            double rectWidth = Math.Abs(width);
            double rectHeight = Math.Abs(height);
            double right = left + rectWidth;
            double bottom = top + rectHeight;

            if (_rectangle.Parent is not Canvas canvas)
                return;

            Canvas.SetLeft(_rectangle, left);
            Canvas.SetTop(_rectangle, top);
            _rectangle.Width = rectWidth;
            _rectangle.Height = rectHeight;

            Canvas.SetRight(_rectangle, right > canvas.ActualWidth ? canvas.ActualWidth : right);
            Canvas.SetBottom(_rectangle, bottom > canvas.ActualHeight ? canvas.ActualHeight : bottom);
        }

        public override void SetLastMousePosition(Point lastPosition)
        {
            LastMousePosition = lastPosition;
            Canvas.SetLeft(_rectangle, lastPosition.X);
            Canvas.SetTop(_rectangle, lastPosition.Y);
        }
    }
}