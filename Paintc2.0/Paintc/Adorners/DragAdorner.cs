using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Paintc.Adorners
{
    public class DragAdorner : Adorner
    {
        private readonly VisualCollection _visuals;
        private readonly Thumb _thumb;

        public DragAdorner(UIElement adornedElement) : base(adornedElement)
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
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Size size = AdornedElement.RenderSize;
            double centerX = (size.Width - _thumb.RenderSize.Width) / 2;
            double centerY = (size.Height - _thumb.RenderSize.Height) / 2;
            _thumb.Arrange(new Rect(new Point(centerX, centerY), _thumb.RenderSize));

            return base.ArrangeOverride(finalSize);
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (AdornedElement is not FrameworkElement adornedElement)
                return;

            if (adornedElement.Parent is not Canvas parentCanvas)
                return;

            double newLeft = Canvas.GetLeft(adornedElement) + e.HorizontalChange;
            double newTop = Canvas.GetTop(adornedElement) + e.VerticalChange;
            double newRight = Canvas.GetRight(adornedElement) + e.HorizontalChange;
            double newBottom = Canvas.GetBottom(adornedElement) + e.VerticalChange;

            if (newLeft >= 0 && newLeft + adornedElement.ActualWidth <= parentCanvas.ActualWidth)
            {
                Canvas.SetLeft(adornedElement, newLeft);
                Canvas.SetRight(adornedElement, newRight);
            }

            if (newTop >= 0 && newTop + adornedElement.ActualHeight <= parentCanvas.ActualHeight)
            {
                Canvas.SetTop(adornedElement, newTop);
                Canvas.SetBottom(adornedElement, newBottom);
            }
        }
    }
}
