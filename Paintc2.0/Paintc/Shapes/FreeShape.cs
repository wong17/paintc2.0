using Paintc.Core;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Shapes
{
    public class FreeShape : ShapeBase
    {
        private readonly Polyline _polyLine;

        public FreeShape(string? name, Color color) : base(name, color)
        {
            _polyLine = new Polyline
            {
                Stroke = new SolidColorBrush(color),
                StrokeThickness = 2
            };
        }

        public override void SetLastMousePosition(Point lastPosition)
        {
            LastMousePosition = lastPosition;
            _polyLine.Points.Add(LastMousePosition);
        }

        public override void SetCurrentMousePosition(Point currentPosition)
        {
            if (currentPosition != LastMousePosition)
            {
                CurrentMousePosition = currentPosition;
                _polyLine.Points.Add(CurrentMousePosition);
                LastMousePosition = CurrentMousePosition;
            }
        }

        public PointCollection GetPoints() => _polyLine.Points;

        public override Shape GetShape() => _polyLine;
    }
}
