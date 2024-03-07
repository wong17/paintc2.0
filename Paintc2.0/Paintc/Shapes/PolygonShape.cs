using Paintc.Core;
using Paintc.Shapes.C;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Shapes
{
    public class PolygonShape : ShapeBase
    {
        public bool IsActivePolygon { get; set; } = true;
        public bool IsPolygonClosed { get; set; } = false;
        private readonly Polygon _polygon;

        public PolygonShape(string? name, Color color) : base(name, color)
        {
            _polygon = new()
            {
                Stroke = new SolidColorBrush(color),
                StrokeThickness = 1
            };
        }

        public override void SetLastMousePosition(Point lastPosition)
        {
            LastMousePosition = lastPosition;
            _polygon.Points.Add(LastMousePosition);
        }

        public override void SetCurrentMousePosition(Point currentPosition)
        {
            if (currentPosition != LastMousePosition)
            {
                CurrentMousePosition = currentPosition;
                _polygon.Points.Add(CurrentMousePosition);
                LastMousePosition = CurrentMousePosition;
            }
        }

        public override Shape GetShape() => _polygon;

        // Pendiente
        public override SimpleShapeBase GetSimpleShape()
        {
            CPoly polygon = new()
            {
                Name = Name
            };

            return polygon;
        }
    }
}