using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Adorners
{
    public class PolygonResizerAdorner : Adorner
    {
        private readonly VisualCollection _visuals;
        private readonly Thumb _topLeft, _topCenter, _topRight, _bottomLeft, _bottomCenter, _bottomRight, _middleLeft, _middleRight;

        public PolygonResizerAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _topLeft = Create(Cursors.SizeNWSE);
            _topCenter = Create(Cursors.SizeNS);
            _topRight = Create(Cursors.SizeNESW);
            _bottomLeft = Create(Cursors.SizeNESW);
            _bottomCenter = Create(Cursors.SizeNS);
            _bottomRight = Create(Cursors.SizeNWSE);
            _middleLeft = Create(Cursors.SizeWE);
            _middleRight = Create(Cursors.SizeWE);

            _topLeft.DragDelta += TopLeftDragDelta;
            _topCenter.DragDelta += TopCenterDragDelta;
            _topRight.DragDelta += TopRightDragDelta;
            _bottomLeft.DragDelta += BottomLeftDragDelta;
            _bottomCenter.DragDelta += BottomCenterDragDelta;
            _bottomRight.DragDelta += BottomRightDragDelta;
            _middleLeft.DragDelta += MiddleLeftDragDelta;
            _middleRight.DragDelta += MiddleRightDragDelta;

            _visuals = new VisualCollection(this)
            {
                _topLeft,
                _topCenter,
                _topRight,
                _bottomLeft,
                _bottomCenter,
                _bottomRight,
                _middleLeft,
                _middleRight
            };
        }

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
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Polygon polygon = (Polygon)AdornedElement;
            Rect adornedElementRect = new(polygon.RenderedGeometry.Bounds.Size);
            Rect thumbsRect = new(polygon.ActualWidth - adornedElementRect.Size.Width, polygon.ActualHeight - adornedElementRect.Size.Height, adornedElementRect.Width, adornedElementRect.Height);

            double halfWidth = adornedElementRect.Width / 2;
            double halfHeight = adornedElementRect.Height / 2;

            _topLeft.Arrange(new Rect(thumbsRect.TopLeft.X - 5, thumbsRect.TopLeft.Y - 5, 10, 10));
            _topCenter.Arrange(new Rect(thumbsRect.TopLeft.X + halfWidth - 5, thumbsRect.TopLeft.Y - 5, 10, 10));
            _topRight.Arrange(new Rect(thumbsRect.TopRight.X - 5, thumbsRect.TopRight.Y - 5, 10, 10));

            _bottomLeft.Arrange(new Rect(thumbsRect.BottomLeft.X - 5, thumbsRect.BottomLeft.Y - 5, 10, 10));
            _bottomCenter.Arrange(new Rect(thumbsRect.BottomLeft.X + halfWidth - 5, thumbsRect.BottomLeft.Y - 5, 10, 10));
            _bottomRight.Arrange(new Rect(thumbsRect.BottomRight.X - 5, thumbsRect.BottomRight.Y - 5, 10, 10));

            _middleLeft.Arrange(new Rect(thumbsRect.TopLeft.X - 5, thumbsRect.TopLeft.Y + halfHeight - 5, 10, 10));
            _middleRight.Arrange(new Rect(thumbsRect.TopRight.X - 5, thumbsRect.TopRight.Y + halfHeight - 5, 10, 10));

            //Debug.WriteLine($"X LEFT TOP: {polygon.ActualWidth - adornedElementRect.Size.Width} Y LEFT TOP: {polygon.ActualHeight - adornedElementRect.Size.Height}");
            //Debug.WriteLine($"X LEFT BOTTOM: {polygon.ActualWidth - adornedElementRect.Size.Width} Y LEFT BOTTOM: {polygon.ActualHeight}");
            //Debug.WriteLine($"X RIGHT BOTTOM: {polygon.ActualWidth} Y RIGHT BOTTOM: {polygon.ActualHeight}");
            //Debug.WriteLine($"X RIGHT TOP: {polygon.ActualWidth} Y RIGHT TOP: {polygon.ActualHeight - adornedElementRect.Size.Height}");

            return base.ArrangeOverride(finalSize);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopLeftDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is not Thumb || AdornedElement is not FrameworkElement adornedElement)
                return;

            if (adornedElement.Parent is not Canvas parentCanvas)
                return;

            Polygon polyline = (Polygon)adornedElement;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopCenterDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is not Thumb || AdornedElement is not FrameworkElement adornedElement)
                return;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopRightDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is not Thumb || AdornedElement is not FrameworkElement adornedElement)
                return;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BottomLeftDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is not Thumb || AdornedElement is not FrameworkElement adornedElement)
                return;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BottomCenterDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is not Thumb || AdornedElement is not FrameworkElement adornedElement)
                return;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BottomRightDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is not Thumb || AdornedElement is not FrameworkElement adornedElement)
                return;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiddleLeftDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is not Thumb || AdornedElement is not FrameworkElement adornedElement)
                return;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiddleRightDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is not Thumb || AdornedElement is not FrameworkElement adornedElement)
                return;
        }
    }
}
