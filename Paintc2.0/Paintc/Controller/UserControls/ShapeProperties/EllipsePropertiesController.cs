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

        private double _radiusX;

        public double RadiusX
        {
            get => _radiusX;
            private set => SetField(ref _radiusX, value);
        }

        private double _radiusY;

        public double RadiusY
        {
            get => _radiusY;
            private set => SetField(ref _radiusY, value);
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

            TopLeftX = double.Truncate(left * 100) / 100;
            TopLeftY = double.Truncate(top * 100) / 100;
            BottomRightX = double.Truncate(right * 100) / 100;
            BottomRightY = double.Truncate(bottom * 100) / 100;
            RadiusX = double.Truncate(Math.Abs((right - left) / 2) * 100) / 100;
            RadiusY = double.Truncate(Math.Abs((bottom - top) / 2) * 100) / 100;
            StartAngle = 0;
            EndAngle = 360;
            Width = double.Truncate(_ellipseShape.GetShape().Width * 100) / 100;
            Height = double.Truncate(_ellipseShape.GetShape().Height * 100) / 100;
            MiddlePointX = Convert.ToInt32(double.Truncate((right + left) / 2));
            MiddlePointY = Convert.ToInt32(double.Truncate((bottom + top) / 2));
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
