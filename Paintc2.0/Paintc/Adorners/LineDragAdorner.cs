using Paintc.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Adorners
{
    public class LineDragAdorner : Adorner
    {
        private readonly VisualCollection _visuals;
        private readonly Thumb _thumb;

        public LineDragAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _thumb = new Thumb
            {
                Cursor = Cursors.Hand,
                Width = 15,
                Height = 15,
                Background = Brushes.DodgerBlue,
                BorderBrush = new SolidColorBrush(Colors.DodgerBlue)
            };
            _thumb.DragDelta += Thumb_DragDelta;
            _visuals = new VisualCollection(this) { _thumb };
        }

        /// <summary>
        ///
        /// </summary>
        protected override int VisualChildrenCount => _visuals.Count;

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index) => _visuals[index];

        /// <summary>
        /// Dibuja el thumb para arrastrar la figura en el centro de esta
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Line adornedLine = (Line)AdornedElement;
            double middleX = (adornedLine.X2 + adornedLine.X1) / 2 - (_thumb.Width / 2);
            double middleY = (adornedLine.Y2 + adornedLine.Y1) / 2 - (_thumb.Height / 2);
            double angle = GetInclinationAngle(adornedLine);
            _thumb.RenderTransform = new RotateTransform(angle, _thumb.Width / 2, _thumb.Height / 2);
            _thumb.Arrange(new Rect(new Point(middleX, middleY), _thumb.RenderSize));
            return base.ArrangeOverride(finalSize);
        }

        /// <summary>
        /// m = y2 - y1 / x2 - x1
        /// m = tan(x)
        /// atan(m) = x, donde x es devuelto en radianes y se pasa a grados
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static double GetInclinationAngle(Line line) => Math.Atan2(line.Y2 - line.Y1, line.X2 - line.X1) * (180 / Math.PI);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (AdornedElement is not Line adornedLine)
                return;

            if (adornedLine.Parent is not Canvas parentCanvas)
                return;

            double startX = adornedLine.X1 + e.HorizontalChange;
            double endX = adornedLine.X2 + e.HorizontalChange;
            double startY = adornedLine.Y1 + e.VerticalChange;
            double endY = adornedLine.Y2 + e.VerticalChange;

            if (startX >= 0 && endX <= parentCanvas.ActualWidth)
            {
                adornedLine.X1 = startX;
                adornedLine.X2 = endX;
            }

            if (startY >= 0 && endY <= parentCanvas.ActualHeight)
            {
                adornedLine.Y1 = startY;
                adornedLine.Y2 = endY;
            }

            PropertiesPanelService.Instance.UpdatePropertiesPanel();
        }
    }
}
