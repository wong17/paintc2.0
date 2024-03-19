using Paintc.Core;
using Paintc.Enums;
using Paintc.Service.Collections;
using Paintc.Shapes.C;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Shapes
{
    public class PolygonShape : ShapeBase
    {
        public bool IsActivePolygon { get; set; } = true;
        private readonly Polygon _polygon;

        public PolygonShape(string? name, Color color) : base(name, color)
        {
            _polygon = new()
            {
                Stroke = new SolidColorBrush(color),
                Fill = new SolidColorBrush(color),
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

        public PointCollection GetPoints() => _polygon.Points;

        public override Shape GetShape() => _polygon;

        public override SimpleShapeBase GetSimpleShape()
        {
            int color = (int)CGAColorPalette.White;
            int fill = (int)CGAColorPalette.White;

            if (_polygon.Stroke is SolidColorBrush strokeBrush)
                color = (int)CGAColorPaletteService.GetCGAColorPalette(strokeBrush.Color);

            if (_polygon.Fill is SolidColorBrush fillBrush)
                fill = (int)CGAColorPaletteService.GetCGAColorPalette(fillBrush.Color);

            /* Crea una lista de vértices */
            List<CVertex> vertices = [];
            var points = GetPoints();
            foreach (var point in points)
            {
                var vertex = new CVertex
                {
                    X = (int)double.Truncate(point.X),
                    Y = (int)double.Truncate(point.Y)
                };
                vertices.Add(vertex);
            }

            CPoly polygon = new()
            {
                Name = Name,
                Vertices = vertices,
                Stroke = color,
                Fill = fill,
                FillPattern = Convert.ToInt32(FillPattern.SOLID_FILL)
            };

            return polygon;
        }
    }
}