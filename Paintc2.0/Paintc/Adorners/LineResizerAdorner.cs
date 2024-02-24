using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Adorners
{
    public class LineResizerAdorner : Adorner
    {
        private readonly VisualCollection _visuals;
        private readonly Thumb _startThumb, _endThumb;

        public LineResizerAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _startThumb = Create(Cursors.SizeWE);
            _endThumb = Create(Cursors.SizeWE);
            _startThumb.DragDelta += StartThumbDragDelta;
            _endThumb.DragDelta += EndThumbDragDelta;
            _visuals = new VisualCollection(this)
            {
                _startThumb,
                _endThumb
            };
        }

        /// <summary>
        /// Returns a child at the specified index from a collection of child elements.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index) => _visuals[index];

        /// <summary>
        /// Gets the number of visual child elements within this element.
        /// </summary>
        protected override int VisualChildrenCount => _visuals.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cursor"></param>
        /// <returns></returns>
        private static Thumb Create(Cursor cursor)
        {
            var thumb = new Thumb()
            {
                Background = Brushes.DodgerBlue,
                Width = 10,
                Height = 10,
                BorderBrush = new SolidColorBrush(Colors.DodgerBlue),
                Cursor = cursor
            };
            return thumb;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Line line = (Line)AdornedElement;
            double startThumbX = line.X1 - (_startThumb.Width / 2);
            double startThumbY = line.Y1 - (_startThumb.Height / 2);
            double endThumbX = line.X2 - (_endThumb.Width / 2);
            double endThumbY = line.Y2 - (_endThumb.Height / 2);
            double angle = GetInclinationAngle(line);
            _startThumb.RenderTransform = new RotateTransform(angle, _startThumb.Width / 2, _startThumb.Height / 2);
            _endThumb.RenderTransform = new RotateTransform(angle, _endThumb.Width / 2, _endThumb.Height / 2);
            _startThumb.Arrange(new Rect(new Point(startThumbX, startThumbY), _startThumb.DesiredSize));
            _endThumb.Arrange(new Rect(new Point(endThumbX, endThumbY), _endThumb.DesiredSize));

            return base.ArrangeOverride(finalSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is not Thumb || AdornedElement is not Line adornedLine)
                return;

            if (adornedLine.Parent is not Canvas parentCanvas)
                return;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is not Thumb || AdornedElement is not Line adornedLine)
                return;

            if (adornedLine.Parent is not Canvas parentCanvas)
                return;


        }

        /// <summary>
        /// m = y2 - y1 / x2 - x1
        /// m = tan(x)
        /// atan(m) = x, donde x es devuelto en radianes y se pasa a grados
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static double GetInclinationAngle(Line line) => Math.Atan2(line.Y2 - line.Y1, line.X2 - line.X1) * (180 / Math.PI);
    }
}
