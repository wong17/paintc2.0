using Paintc.Controller.UserControls.ShapeProperties.Interface;
using Paintc.Core;
using System.Windows.Controls;

namespace Paintc.Controller.UserControls.ShapeProperties
{
    public class RectanglePropertiesController : ObservableObject, IPropertiesController
    {
        private ShapeBase? _rectangleShape;

        public ShapeBase? RectangleShape
        {
            get => _rectangleShape;
            set
            {
                SetField(ref _rectangleShape, value);
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
        /// <param name="panelProperty"></param>
        public void UpdateProperties()
        {
            if (_rectangleShape is null)
                return;
            
            TopLeftX = double.Truncate(Canvas.GetLeft(_rectangleShape.GetShape()) * 100) / 100;
            TopLeftY = double.Truncate(Canvas.GetTop(_rectangleShape.GetShape()) * 100) / 100;
            BottomRightX = double.Truncate(Canvas.GetRight(_rectangleShape.GetShape()) * 100) / 100;
            BottomRightY = double.Truncate(Canvas.GetBottom(_rectangleShape.GetShape()) * 100) / 100;
            Width = double.Truncate(_rectangleShape.GetShape().Width * 100) / 100;
            Height = double.Truncate(_rectangleShape.GetShape().Height * 100) / 100;
            Area = double.Truncate((Width * Height) * 100) / 100;
            Perimeter = double.Truncate((2 * (Width + Height)) * 100) / 100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        public void SetShape(ShapeBase shape)
        {
            RectangleShape = shape;
        }
    }
}