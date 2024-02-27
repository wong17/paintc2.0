using Paintc.Core;
using Paintc.Enums;
using Paintc.Service.Collections;
using Paintc.Shapes.C;
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
                StrokeThickness = 1
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

        /// <summary>
        /// Devuelve un objeto de tipo CPencil
        /// </summary>
        /// <returns></returns>
        public override SimpleShapeBase GetSimpleShape()
        {
            int color = (int)CGAColorPalette.White;

            if (_polyLine.Stroke is SolidColorBrush strokeBrush)
                color = (int)CGAColorPaletteService.GetCGAColorPalette(strokeBrush.Color);

            /* Crea una lista de pixels a partir de cada punto que forma el trazo */
            List<CPixel> pixels = [];
            var points = GetPoints();
            foreach (var point in points)
            {
                var pixel = new CPixel
                {
                    X = (int)double.Truncate(point.X),
                    Y = (int)double.Truncate(point.Y),
                    Color = color
                };
                pixels.Add(pixel);
            }

            CPencil pencil = new()
            {
                Name = Name,
                Pixels = pixels
            };

            return pencil;
        }
    }
}