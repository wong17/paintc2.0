using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
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
        /// Create a new thumb
        /// </summary>
        /// <returns></returns>
        private static Thumb Create()
        {
            var thumb = new Thumb() { 
                Background = Brushes.DodgerBlue, 
                Width = 10, Height = 10, 
                BorderBrush = new SolidColorBrush(Colors.DodgerBlue)
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
        private void TopLeftDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is not Thumb || AdornedElement is not FrameworkElement adornedElement) 
                return;

            double yCanvasTop = Canvas.GetTop(adornedElement);
            double xCanvasLeft = Canvas.GetLeft(adornedElement);
            double deltaY = e.VerticalChange;
            double deltaX = e.HorizontalChange;

            if (adornedElement.Parent is not Canvas parentCanvas)
                return;

            var mousePosition = Mouse.GetPosition(parentCanvas);
            if (mousePosition.X < 0 || mousePosition.Y < 0)
                return;

            if (deltaY < 0 && yCanvasTop == 0 || deltaX < 0 && xCanvasLeft == 0)
                return;

            double deltaH = adornedElement.ActualHeight - deltaY;
            double deltaW = adornedElement.ActualWidth - deltaX;

            double topOffset = deltaY;
            double leftOffset = deltaX;

            if (deltaH < adornedElement.MinHeight)
            {
                deltaH = adornedElement.MinHeight;
                topOffset = adornedElement.ActualHeight - adornedElement.MinHeight;
            }

            if (deltaH > parentCanvas.ActualHeight)
                deltaH = parentCanvas.ActualHeight;

            if (deltaW < adornedElement.MinWidth)
            {
                deltaW = adornedElement.MinWidth;
                leftOffset = adornedElement.ActualWidth - adornedElement.MinWidth;
            }

            if (deltaW > parentCanvas.ActualWidth)
                deltaW = parentCanvas.ActualWidth;

            double newCanvasTop = yCanvasTop + topOffset;
            if (newCanvasTop < 0)
                newCanvasTop = 0;

            double newCanvasLeft = xCanvasLeft + leftOffset;
            if (newCanvasLeft < 0)
                newCanvasLeft = 0;

            adornedElement.Height = deltaH;
            Canvas.SetTop(adornedElement, newCanvasTop);
            adornedElement.Width = deltaW;
            Canvas.SetLeft(adornedElement, newCanvasLeft);
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

            /* Coordenada 'y' de la figura sobre el canvas (esquina superior izquierda) */
            double yCanvasTop = Canvas.GetTop(adornedElement);

            /* Desplazamiento del thumb sobre el eje y */
            double deltaY = e.VerticalChange;

            /* Referencia del canvas para conocer altura máxima que puede tomar la figura. */
            if (adornedElement.Parent is not Canvas parentCanvas)
                return;

            /*
             * Si el desplazamiento del thumb es negativo (hacia arriba) y la figura ya esta tocando la parte
             * superior del canvas, salir y no cambiar tamaño.
             */
            if (deltaY < 0 && yCanvasTop == 0)
                return;

            /* 
             * Cambio en la altura de la figura. 
             * 
             * Se trabaja con el eje 'y' del lado positivo para hacer el cálculo, por lo que el desplazamiento 
             * 'deltaH' inicial parte desde la posición inicial del thumb como origen. 
             */
            double deltaH = adornedElement.ActualHeight - deltaY;
            /* 
             * Desplazamiento de la coordenada y (esquina superior izquierda) de la figura sobre el canvas,
             * como valor inicial se asigna el desplazamiento en 'y' del thumb. Ya que ambos puntos estan al mismo nivel 
             * en el canvas pero su coordenada 'y' relativa a la figura es 0, por lo que sirve para medir el desplazamiento
             * en 'y' de la figura sobre el canvas.
             */
            double topOffset = deltaY;
            /* 
             * Si el desplazamiento del thumb es positivo (hacia abajo) y la nueva altura de la figura es menor que 
             * la altura minima permitada...
             */
            if (deltaH < adornedElement.MinHeight)
            {
                /* 
                 * Se asigna como nueva altura la minima permitada y el cambio en el desplazamiento superior de la figura 
                 * sobre el canvas será igual a la diferencia entre la altura actual y la minima de la figura.
                 */
                deltaH = adornedElement.MinHeight;
                topOffset = adornedElement.ActualHeight - adornedElement.MinHeight;
            }
            /* Se asigna como máxima altura de la figura el alto del canvas */
            if (deltaH > parentCanvas.ActualHeight)
                deltaH = parentCanvas.ActualHeight;

            /*
             * La nueva coordenada 'y' de la figura sera la anterior más la nueva, si esta es menor que 0, es decir que 
             * se sale del canvas entonces establecemos como coordenada 'y' de la figura 0.
             */
            double newCanvasTop = yCanvasTop + topOffset;
            if (newCanvasTop < 0)
                newCanvasTop = 0;

            /* Actualizar alto de la figura y nueva coordenada 'y' de la figura sobre el canvas. */
            adornedElement.Height = deltaH;
            Canvas.SetTop(adornedElement, newCanvasTop);
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

            double yCanvasTop = Canvas.GetTop(adornedElement);
            double xCanvasRight = Canvas.GetRight(adornedElement);

            if (adornedElement.Parent is not Canvas parentCanvas)
                return;

            var mousePosition = Mouse.GetPosition(parentCanvas);
            if (mousePosition.X > parentCanvas.ActualWidth || mousePosition.Y < 0)
                return;

            double deltaY = e.VerticalChange;
            double deltaX = e.HorizontalChange;

            if (deltaY < 0 && yCanvasTop == 0 || deltaX > 0 && xCanvasRight == parentCanvas.ActualWidth)
                return;

            double deltaH = adornedElement.ActualHeight - deltaY;
            double deltaW = adornedElement.ActualWidth + deltaX;

            double topOffset = deltaY;
            double rightOffset = deltaX;

            if (deltaH < adornedElement.MinHeight)
            {
                deltaH = adornedElement.MinHeight;
                topOffset = adornedElement.ActualHeight - adornedElement.MinHeight;
            }

            if (deltaH > parentCanvas.ActualHeight)
                deltaH = parentCanvas.ActualHeight;

            if (deltaW < adornedElement.MinWidth)
            {
                deltaW = adornedElement.MinWidth;
                rightOffset = adornedElement.ActualWidth - adornedElement.MinWidth;
            }

            if (deltaW > parentCanvas.ActualWidth)
                deltaW = parentCanvas.ActualWidth;

            double newCanvasTop = yCanvasTop + topOffset;
            if (newCanvasTop < 0)
                newCanvasTop = 0;

            double newCanvasRight = xCanvasRight + rightOffset;
            if (newCanvasRight > parentCanvas.ActualWidth)
                newCanvasRight = parentCanvas.ActualWidth;

            adornedElement.Height = deltaH;
            adornedElement.Width = deltaW;
            Canvas.SetTop(adornedElement, newCanvasTop);
            Canvas.SetRight(adornedElement, newCanvasRight);
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

            double yCanvasBottom = Canvas.GetBottom(adornedElement);
            double xCanvasLeft = Canvas.GetLeft(adornedElement);

            double deltaY = e.VerticalChange;
            double deltaX = e.HorizontalChange;

            if (adornedElement.Parent is not Canvas parentCanvas)
                return;

            var mousePosition = Mouse.GetPosition(parentCanvas);
            if (mousePosition.X < 0 || mousePosition.Y > parentCanvas.ActualHeight)
                return;

            if (deltaY > 0 && yCanvasBottom == parentCanvas.ActualHeight || deltaX < 0 && xCanvasLeft == 0)
                return;

            double deltaH = adornedElement.ActualHeight + deltaY;
            double deltaW = adornedElement.ActualWidth - deltaX;

            double bottomOffset = deltaY;
            double leftOffset = deltaX;

            if (deltaH < adornedElement.MinHeight)
            {
                deltaH = adornedElement.MinHeight;
                bottomOffset = adornedElement.ActualHeight - adornedElement.MinHeight;
            }

            if (deltaH > parentCanvas.ActualHeight)
                deltaH = parentCanvas.ActualHeight;

            if (deltaW < adornedElement.MinWidth)
            {
                deltaW = adornedElement.MinWidth;
                leftOffset = adornedElement.ActualWidth - adornedElement.MinWidth;
            }

            if (deltaW > parentCanvas.ActualWidth)
                deltaW = parentCanvas.ActualWidth;

            double newCanvasBottom = yCanvasBottom + bottomOffset;
            if (newCanvasBottom > parentCanvas.ActualHeight)
                newCanvasBottom = parentCanvas.ActualHeight;

            double newCanvasLeft = xCanvasLeft + leftOffset;
            if (newCanvasLeft < 0)
                newCanvasLeft = 0;

            adornedElement.Height = deltaH;
            Canvas.SetBottom(adornedElement, newCanvasBottom);
            adornedElement.Width = deltaW;
            Canvas.SetLeft(adornedElement, newCanvasLeft);
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

            double yCanvasBottom = Canvas.GetBottom(adornedElement);
            double deltaY = e.VerticalChange;

            if (adornedElement.Parent is not Canvas parentCanvas)
                return;

            if (deltaY > 0 && yCanvasBottom == parentCanvas.ActualHeight)
                return;

            double deltaH = adornedElement.ActualHeight + deltaY;
            double bottomOffset = deltaY;

            if (deltaH < adornedElement.MinHeight)
            {
                deltaH = adornedElement.MinHeight;
                bottomOffset = adornedElement.ActualHeight - adornedElement.MinHeight;
            }

            if (deltaH > parentCanvas.ActualHeight)
                deltaH = parentCanvas.ActualHeight;

            double newCanvasBottom = yCanvasBottom + bottomOffset;
            if (newCanvasBottom > parentCanvas.ActualHeight)
                newCanvasBottom = parentCanvas.ActualHeight;

            /* Actualizar alto de la figura y nueva coordenada 'y' de la figura sobre el canvas. */
            adornedElement.Height = deltaH;
            Canvas.SetBottom(adornedElement, newCanvasBottom);
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

            double yCanvasBottom = Canvas.GetBottom(adornedElement);
            double xCanvasRight = Canvas.GetRight(adornedElement);

            double deltaY = e.VerticalChange;
            double deltaX = e.HorizontalChange;

            if (adornedElement.Parent is not Canvas parentCanvas)
                return;

            var mousePosition = Mouse.GetPosition(parentCanvas);
            if (mousePosition.X > parentCanvas.ActualWidth || mousePosition.Y > parentCanvas.ActualHeight)
                return;

            if (deltaY > 0 && yCanvasBottom == parentCanvas.ActualHeight || deltaX > 0 && xCanvasRight == parentCanvas.ActualWidth)
                return;

            double deltaH = adornedElement.ActualHeight + deltaY;
            double deltaW = adornedElement.ActualWidth + deltaX;

            double bottomOffset = deltaY;
            double rightOffset = deltaX;

            if (deltaH < adornedElement.MinHeight)
            {
                deltaH = adornedElement.MinHeight;
                bottomOffset = adornedElement.ActualHeight - adornedElement.MinHeight;
            }

            if (deltaH > parentCanvas.ActualHeight)
                deltaH = parentCanvas.ActualHeight;

            if (deltaW < adornedElement.MinWidth)
            {
                deltaW = adornedElement.MinWidth;
                rightOffset = adornedElement.ActualWidth - adornedElement.MinWidth;
            }

            if (deltaW > parentCanvas.ActualWidth)
                deltaW = parentCanvas.ActualWidth;

            double newCanvasBottom = yCanvasBottom + bottomOffset;
            if (newCanvasBottom > parentCanvas.ActualHeight)
                newCanvasBottom = parentCanvas.ActualHeight;

            double newCanvasRight = xCanvasRight + rightOffset;
            if (newCanvasRight > parentCanvas.ActualWidth)
                newCanvasRight = parentCanvas.ActualWidth;

            adornedElement.Height = deltaH;
            adornedElement.Width = deltaW;
            Canvas.SetBottom(adornedElement, newCanvasBottom);
            Canvas.SetRight(adornedElement, newCanvasRight);
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

            double xCanvasLeft = Canvas.GetLeft(adornedElement);
            double deltaX = e.HorizontalChange;

            if (adornedElement.Parent is not Canvas parentCanvas)
                return;

            if (deltaX < 0 && xCanvasLeft == 0)
                return;

            double deltaW = adornedElement.ActualWidth - deltaX;
            double leftOffset = deltaX;

            if (deltaW < adornedElement.MinWidth)
            {
                deltaW = adornedElement.MinWidth;
                leftOffset = adornedElement.ActualWidth - adornedElement.MinWidth;
            }

            if (deltaW > parentCanvas.ActualWidth)
                deltaW = parentCanvas.ActualWidth;

            double newCanvasLeft = xCanvasLeft + leftOffset;
            if (newCanvasLeft < 0)
                newCanvasLeft = 0;

            adornedElement.Width = deltaW;
            Canvas.SetLeft(adornedElement, newCanvasLeft);
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

            double xCanvasRight = Canvas.GetRight(adornedElement);
            double deltaX = e.HorizontalChange;

            if (adornedElement.Parent is not Canvas parentCanvas)
                return;

            if (deltaX > 0 && xCanvasRight == parentCanvas.ActualWidth)
                return;

            double deltaW = adornedElement.ActualWidth + deltaX;
            double rightOffset = deltaX;

            if (deltaW < adornedElement.MinWidth)
            {
                deltaW = adornedElement.MinWidth;
                rightOffset = adornedElement.ActualWidth - adornedElement.MinWidth;
            }
            if (deltaW > parentCanvas.ActualWidth)
                deltaW = parentCanvas.ActualWidth;

            double newCanvasRight = xCanvasRight + rightOffset;
            if (newCanvasRight > parentCanvas.ActualWidth)
                newCanvasRight = parentCanvas.ActualWidth;

            adornedElement.Width = deltaW;
            Canvas.SetRight(adornedElement, newCanvasRight);

        }
    }
}
