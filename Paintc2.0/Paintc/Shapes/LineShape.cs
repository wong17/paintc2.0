using Paintc.Core;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Shapes
{
    public class LineShape : ShapeBase
    {
        public Line Line { get; }
        public LineShape(string? name, Color color) : base(name, color)
        {
            Line = new Line()
            {
                Stroke = new SolidColorBrush(color),
                StrokeThickness = 1
            };
        }

        public override void SetCurrentMousePosition(Point currentPosition)
        {
            Line.X2 = currentPosition.X;
            Line.Y2 = currentPosition.Y;
        }

        public override void SetLastMousePosition(Point lastPosition)
        {
            Line.X1 = lastPosition.X;
            Line.Y1 = lastPosition.Y;
            Line.X2 = lastPosition.X;
            Line.Y2 = lastPosition.Y;
        }

        public PointCollection GetPoints()
        {
            Point startLine = new(Line.X1, Line.Y1);
            Point endLine = new(Line.X2, Line.Y2);
            return [startLine, endLine];
        }

        public override Shape GetShape() => Line;
    }
}
