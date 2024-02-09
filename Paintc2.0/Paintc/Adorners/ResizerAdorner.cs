using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace Paintc.Adorners
{
    public class ResizerAdorner : Adorner
    {
        private readonly VisualCollection _visuals;
        private readonly Thumb _topLeft, _topCenter, _topRight, _bottomLeft, _bottomCenter, _bottomRight, _middleLeft, _middleRight;

        public ResizerAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _topLeft = Create();
            _topCenter = Create();
            _topRight = Create();
            _bottomLeft = Create();
            _bottomCenter = Create();
            _bottomRight = Create();
            _middleLeft = Create();
            _middleRight = Create();

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
        /// Create a new thumb
        /// </summary>
        /// <returns></returns>
        private Thumb Create()
        {
            var thumb = new Thumb() { 
                Background = Brushes.DodgerBlue, 
                Width = 10, Height = 10, 
                BorderBrush = new SolidColorBrush(Colors.DodgerBlue)
            };
            thumb.DragDelta += ThumbDragDelta;
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
            Rect adornedElementRect = new(AdornedElement.DesiredSize);
            double halfWidth = adornedElementRect.Width / 2;
            double halfHeight = adornedElementRect.Height / 2;

            _topLeft.Arrange(new Rect(adornedElementRect.TopLeft.X - 5, adornedElementRect.TopLeft.Y - 5, 10, 10));
            _topCenter.Arrange(new Rect(adornedElementRect.TopLeft.X + halfWidth - 5, adornedElementRect.TopLeft.Y - 5, 10, 10));
            _topRight.Arrange(new Rect(adornedElementRect.TopRight.X - 5, adornedElementRect.TopRight.Y - 5, 10, 10));

            _bottomLeft.Arrange(new Rect(adornedElementRect.BottomLeft.X - 5, adornedElementRect.BottomLeft.Y - 5, 10, 10));
            _bottomCenter.Arrange(new Rect(adornedElementRect.BottomLeft.X + halfWidth - 5, adornedElementRect.BottomLeft.Y - 5, 10, 10));
            _bottomRight.Arrange(new Rect(adornedElementRect.BottomRight.X - 5, adornedElementRect.BottomRight.Y - 5, 10, 10));

            _middleLeft.Arrange(new Rect(adornedElementRect.TopLeft.X - 5, adornedElementRect.TopLeft.Y + halfHeight - 5, 10, 10));
            _middleRight.Arrange(new Rect(adornedElementRect.TopRight.X - 5, adornedElementRect.TopRight.Y + halfHeight - 5, 10, 10));

            return base.ArrangeOverride(finalSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThumbDragDelta(object sender, DragDeltaEventArgs e)
        {

        }
    }
}
