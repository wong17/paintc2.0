using Paintc.Core;
using Paintc.Shapes;
using System.Windows.Controls;

namespace Paintc.Controller.UserControls.ShapeProperties
{
    public class RectanglePropertiesController : ControllerBase
    {
        private RectangleShape? _rectangleShape;

        public RectangleShape? RectangleShape
        {
            get => _rectangleShape;
            set
            {
                SetField(ref _rectangleShape, value);

                if (_rectangleShape is not null)
                {
                    TopLeftX = Canvas.GetLeft(_rectangleShape.GetShape());
                    TopLeftY = Canvas.GetTop(_rectangleShape.GetShape());
                    BottomRightX = Canvas.GetRight(_rectangleShape.GetShape());
                    BottomRightY = Canvas.GetBottom(_rectangleShape.GetShape());
                    Width = _rectangleShape.GetShape().Width;
                    Height = _rectangleShape.GetShape().Height;
                }
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
    }
}