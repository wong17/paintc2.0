using Paintc.ViewModels.UserControls.ShapeProperties.Interface;
using Paintc.Core;
using Paintc.Shapes;

namespace Paintc.ViewModels.UserControls.ShapeProperties
{
    public class LinePropertiesViewModel : ObservableObject, IPropertiesViewModel
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

            StartX = double.Truncate(_lineShape.GetPoints()[0].X * 100) / 100;
            StartY = double.Truncate(_lineShape.GetPoints()[0].Y * 100) / 100;
            EndX = double.Truncate(_lineShape.GetPoints()[1].X * 100) / 100;
            EndY = double.Truncate(_lineShape.GetPoints()[1].Y * 100) / 100;
            Length = double.Truncate(Math.Sqrt(Math.Pow(EndX - StartX, 2) + Math.Pow(EndY - StartY, 2)) * 100) / 100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        public void SetShape(ShapeBase shape)
        {
            LineShape = (LineShape)shape;
        }
    }
}
