using Paintc.Controller.UserControls.ShapeProperties.Interface;
using Paintc.Core;
using Paintc.Shapes;

namespace Paintc.Controller.UserControls.ShapeProperties
{
    public class LinePropertiesController : ObservableObject, IPropertiesController
    {
        private LineShape? _lineShape;

        public LineShape? LineShape 
        { 
            get => _lineShape; 
            set 
            {
                SetField(ref _lineShape, value);
                UpdateProperties();
            }
        }

        private double _startX;

        public double StartX
        {
            get => _startX;
            private set => SetField(ref _startX, value);
        }

        private double _startY;

        public double StartY
        {
            get => _startY;
            private set => SetField(ref _startY, value);
        }

        private double _endX;

        public double EndX
        {
            get => _endX;
            private set => SetField(ref _endX, value);
        }

        private double _endY;

        public double EndY
        {
            get => _endY;
            private set => SetField(ref _endY, value);
        }

        private double _length;

        public double Length
        {
            get => _length;
            private set => SetField(ref _length, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateProperties()
        {
            if (_lineShape is null)
                return;

            StartX = _lineShape.GetPoints()[0].X;
            StartY = _lineShape.GetPoints()[0].Y;
            EndX = _lineShape.GetPoints()[1].X;
            EndY = _lineShape.GetPoints()[1].Y;
            Length = Math.Sqrt(Math.Pow(EndX - StartX, 2) + Math.Pow(EndY - StartY, 2));
        }
    }
}
