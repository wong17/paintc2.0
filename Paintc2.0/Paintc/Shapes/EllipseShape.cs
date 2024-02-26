using Paintc.Core;
using Paintc.Service.Collections;
using Paintc.Shapes.C;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Shapes
{
    public class EllipseShape : ShapeBase
    {
        private readonly Ellipse _ellipse;

        public EllipseShape(string? name, Color color) : base(name, color)
        {
            _ellipse = new Ellipse
            {
                Stroke = new SolidColorBrush(color),
                StrokeThickness = 1,
                Fill = new SolidColorBrush(color),
                MinHeight = 10,
                MinWidth = 10
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
            double left = width < 0 ? currentPosition.X : LastMousePosition.X;
            double top = height < 0 ? currentPosition.Y : LastMousePosition.Y;
            double ellipseWidth = Math.Abs(width);
            double ellipseHeight = Math.Abs(height);
            double right = Math.Round(left + ellipseWidth);
            double bottom = Math.Round(top + ellipseHeight);

            if (_ellipse.Parent is not Canvas canvas)
                return;

            Canvas.SetLeft(_ellipse, left);
            Canvas.SetTop(_ellipse, top);
            _ellipse.Width = Math.Abs(ellipseWidth);
            _ellipse.Height = Math.Abs(ellipseHeight);

            Canvas.SetRight(_ellipse, right > canvas.ActualWidth ? canvas.ActualWidth : right);
            Canvas.SetBottom(_ellipse, bottom > canvas.ActualHeight ? canvas.ActualHeight : bottom);
        }

        public override Shape GetShape() => _ellipse;

        public override SimpleShapeBase GetSimpleShape()
        {
            double middlePointX = (Canvas.GetRight(_ellipse) + Canvas.GetLeft(_ellipse)) / 2;
            double middlePointY = (Canvas.GetBottom(_ellipse) + Canvas.GetTop(_ellipse)) / 2;

            CEllipse ellipse = new()
            {
                X = Convert.ToInt32(double.Truncate(middlePointX)),
                Y = Convert.ToInt32(double.Truncate(middlePointY)),
                StartAngle = 0,
                EndAngle = 360,
                XRadius = Convert.ToInt32(double.Truncate(_ellipse.Width)) / 2,
                YRadius = Convert.ToInt32(double.Truncate(_ellipse.Height)) / 2,
                Name = Name
            };

            if (GetShape().Fill is SolidColorBrush fillBrush)
                ellipse.Color = Convert.ToInt32(CGAColorPaletteService.GetCGAColorPalette(fillBrush.Color));

            if (GetShape().Stroke is SolidColorBrush strokeBrush)
                ellipse.BorderColor = Convert.ToInt32(CGAColorPaletteService.GetCGAColorPalette(strokeBrush.Color));

            return ellipse;
        }
    }
}