using Paintc.Core;
using Paintc.Shapes.C;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Shapes
{
    public class PolygonShape : ShapeBase
    {
        public bool IsActivePolygon { get; set; } = false;
        public bool IsPolygonClosed { get; set; } = false;
        private readonly List<Line> _lines = [];
        private Line? _currentLine;

        public PolygonShape(string? name, Color color) : base(name, color)
        {
            // Pendiente
        }

        public Line? GetCurrentLine
        {
            get => _currentLine;
        }

        public override void SetLastMousePosition(Point lastPosition)
        {
            if (!IsActivePolygon)
            {
                _currentLine = new Line()
                {
                    StrokeThickness = 2,
                    Stroke = Brushes.Black,
                    X1 = lastPosition.X,
                    Y1 = lastPosition.Y,
                    X2 = lastPosition.X,
                    Y2 = lastPosition.Y
                };
                IsActivePolygon = true;
            }
            else
            {
                _currentLine = new Line()
                {
                    StrokeThickness = 2,
                    Stroke = Brushes.Black,
                    X1 = CurrentMousePosition.X,
                    Y1 = CurrentMousePosition.Y,
                    X2 = CurrentMousePosition.X,
                    Y2 = CurrentMousePosition.Y
                };
            }
            _lines.Add(_currentLine);
        }

        public override void SetCurrentMousePosition(Point currentPosition)
        {
            if (_currentLine is null || IsPolygonClosed)
                return;

            CurrentMousePosition = currentPosition;
            _currentLine.X2 = CurrentMousePosition.X;
            _currentLine.Y2 = CurrentMousePosition.Y;
        }

        public override Shape GetShape()
        {
            Polyline polyline = new();
            foreach (var line in _lines)
            {
                polyline.Points.Add(new Point(line.X1, line.Y1));
                polyline.Points.Add(new Point(line.X2, line.Y2));
            }
            return polyline;
        }

        public void LeaveOpenPolygone()
        {
            //Deja la figura abierta
            if (_lines.Count == 1)
            {
                IsPolygonClosed = true;
                return;
            }
        }

        public void ClosePolygone()
        {
            //Cierra el poligono uniendo con el primer punto
            if (_lines.Count > 1)
            {
                Line closingLine = new()
                {
                    StrokeThickness = 2,
                    Stroke = Brushes.Black,
                    X1 = CurrentMousePosition.X,
                    Y1 = CurrentMousePosition.Y,
                    X2 = _lines[0].X1,
                    Y2 = _lines[0].Y1
                };
                _lines.Add(closingLine);
                //IsActivePolygon = false;
                IsPolygonClosed = true;
            }
        }

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