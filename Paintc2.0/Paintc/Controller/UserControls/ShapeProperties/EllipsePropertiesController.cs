using Paintc.Controller.UserControls.ShapeProperties.Interface;
using Paintc.Core;
using System.Windows.Controls;

namespace Paintc.Controller.UserControls.ShapeProperties
{
    public class EllipsePropertiesController : ObservableObject, IPropertiesController
    {
        private ShapeBase? _ellipseShape;
        public ShapeBase? EllipseShape 
        {
            get => _ellipseShape; 
            set
            {
                SetField(ref _ellipseShape, value);
                UpdateProperties();
            }
        }

        private double _topLeftX;

        public double TopLeftX
        {
            get => _topLeftX;
            private set => SetField(ref _topLeftX, value);
        }

        private double _topLeftY;

        public double TopLeftY
        {
            get => _topLeftY;
            private set => SetField(ref _topLeftY, value);
        }

        private double _bottomRightX;

        public double BottomRightX
        {
            get => _bottomRightX;
            private set => SetField(ref _bottomRightX, value);
        }

        private double _bottomRightY;

        public double BottomRightY
        {
            get => _bottomRightY;
            private set => SetField(ref _bottomRightY, value);
        }

        private double _lengthSemiAxisX;

        public double LengthSemiAxisX
        {
            get => _lengthSemiAxisX;
            private set => SetField(ref _lengthSemiAxisX, value);
        }

        private double _lengthSemiAxisY;

        public double LengthSemiAxisY
        {
            get => _lengthSemiAxisY;
            private set => SetField(ref _lengthSemiAxisY, value);
        }

        private double _startAngle;

        public double StartAngle
        {
            get => _startAngle;
            private set => SetField(ref _startAngle, value);
        }

        private double _endAngle;

        public double EndAngle
        {
            get => _endAngle;
            private set => SetField(ref _endAngle, value);
        }

        private double _width;

        public double Width
        {
            get => _width;
            private set => SetField(ref _width, value);
        }

        private double _height;

        public double Height
        {
            get => _height;
            private set => SetField(ref _height, value);
        }

        private double _middlePointX;

        public double MiddlePointX
        {
            get => _middlePointX;
            private set => SetField(ref _middlePointX, value);
        }

        private double _middlePointY;

        public double MiddlePointY
        {
            get => _middlePointY;
            private set => SetField(ref _middlePointY, value);
        }

        private double _area;

        public double Area
        {
            get => _area;
            private set => SetField(ref _area, value);
        }

        private double _perimeter;

        public double Perimeter
        {
            get => _perimeter;
            private set => SetField(ref _perimeter, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateProperties()
        {
            if (_ellipseShape is null)
                return;

            double left = Canvas.GetLeft(_ellipseShape.GetShape());
            double top = Canvas.GetTop(_ellipseShape.GetShape());
            double right = Canvas.GetRight(_ellipseShape.GetShape());
            double bottom = Canvas.GetBottom(_ellipseShape.GetShape());

            double lengthSemiAxisX = Math.Abs((right - left) / 2);
            double lengthSemiAxisY = Math.Abs((bottom - top) / 2);

            TopLeftX = double.Truncate(left * 100) / 100;
            TopLeftY = double.Truncate(top * 100) / 100;
            BottomRightX = double.Truncate(right * 100) / 100;
            BottomRightY = double.Truncate(bottom * 100) / 100;
            LengthSemiAxisX = double.Truncate(lengthSemiAxisX * 100) / 100;
            LengthSemiAxisY = double.Truncate(lengthSemiAxisY * 100) / 100;
            StartAngle = 0;
            EndAngle = 360;
            Width = double.Truncate(_ellipseShape.GetShape().Width * 100) / 100;
            Height = double.Truncate(_ellipseShape.GetShape().Height * 100) / 100;
            MiddlePointX = Convert.ToInt32(double.Truncate((right + left) / 2));
            MiddlePointY = Convert.ToInt32(double.Truncate((bottom + top) / 2));
            // A = πab, a: longitud del semi eje x, b: longitud del semi eje y
            Area = double.Truncate(Math.PI * LengthSemiAxisX * LengthSemiAxisY * 100) / 100;
            // Formula de Ramanujan para aproximación del perimetro
            Perimeter = double.Truncate(CalculatePerimeter(lengthSemiAxisX, lengthSemiAxisY) * 100) / 100; 
        }

        /// <summary>
        /// Hace un cálculo aproximado del perímetro de la elipse utilizando la fórmula de Ramanujan
        /// </summary>
        /// <param name="lengthSemiAxisX"></param>
        /// <param name="lengthSemiAxisY"></param>
        /// <returns></returns>
        private static double CalculatePerimeter(double lengthSemiAxisX, double lengthSemiAxisY)
        {
            double a = lengthSemiAxisX; 
            double b = lengthSemiAxisY;

            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            double perimeter = Math.PI * (a + b) * (1.0 + 3.0 * h / (10.0 + Math.Sqrt(4.0 - 3.0 * h)));

            return perimeter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        public void SetShape(ShapeBase shape)
        {
            EllipseShape = shape;
        }
    }
}
