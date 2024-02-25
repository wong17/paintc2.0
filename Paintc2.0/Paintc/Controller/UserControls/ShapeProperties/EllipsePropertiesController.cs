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

        /// <summary>
        /// 
        /// </summary>
        public void UpdateProperties()
        {
            if (_ellipseShape is null)
                return;

            TopLeftX = double.Truncate(Canvas.GetLeft(_ellipseShape.GetShape()) * 100) / 100;
            TopLeftY = double.Truncate(Canvas.GetTop(_ellipseShape.GetShape()) * 100) / 100;
            BottomRightX = double.Truncate(Canvas.GetRight(_ellipseShape.GetShape()) * 100) / 100;
            BottomRightY = double.Truncate(Canvas.GetBottom(_ellipseShape.GetShape()) * 100) / 100;
            RadiusX = double.Truncate(Math.Abs((BottomRightX - TopLeftX) / 2) * 100) / 100;
            RadiusY = double.Truncate(Math.Abs((BottomRightY - TopLeftY) / 2) * 100) / 100;
            StartAngle = 0;
            EndAngle = 360;
            Width = double.Truncate(_ellipseShape.GetShape().Width * 100) / 100;
            Height = double.Truncate(_ellipseShape.GetShape().Height * 100) / 100;
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
