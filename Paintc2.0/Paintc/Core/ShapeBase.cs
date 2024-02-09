using Paintc.Adorners;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Core
{
    public abstract class ShapeBase : ObservableObject
    {
        public string? Name { get; set; }
        protected readonly Color Color;

        protected ShapeBase(string? name, Color color)
        {
            Name = name;
            Color = color;
        }

        protected Point LastMousePosition { get; set; }
        protected Point CurrentMousePosition { get; set; }
        public abstract void SetLastMousePosition(Point lastPosition);
        public abstract void SetCurrentMousePosition(Point currentPosition);
        public abstract Shape GetShape();

        #region DEPENDENCY_PROPERTIES

        public static readonly DependencyProperty ShowResizeAdornerProperty =
            DependencyProperty.RegisterAttached("ShowResizeAdorner", typeof(bool), typeof(ShapeBase), new FrameworkPropertyMetadata(false, OnShowResizeAdornerChanged));
        public static bool GetShowResizeAdorner(UIElement element) => (bool)element.GetValue(ShowResizeAdornerProperty);
        public static void SetShowResizeAdorner(UIElement element, bool value) => element.SetValue(ShowResizeAdornerProperty, value);


        public static readonly DependencyProperty ShowSelectionAdornerProperty =
            DependencyProperty.RegisterAttached("ShowSelectionAdorner", typeof(bool), typeof(ShapeBase), new FrameworkPropertyMetadata(false, OnShowSelectionAdornerChanged));
        public static bool GetShowSelectionAdorner(UIElement element) => (bool)element.GetValue(ShowSelectionAdornerProperty);
        public static void SetShowSelectionAdorner(UIElement element, bool value) => element.SetValue(ShowSelectionAdornerProperty, value);

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnShowResizeAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool showAdorner = (bool)e.NewValue;
            if (showAdorner)
            {
                if (d is Rectangle rectangle)
                {
                    var adornerLayer = AdornerLayer.GetAdornerLayer(rectangle);
                    adornerLayer?.Add(new ResizerAdorner(rectangle));
                }

                if (d is Ellipse ellipse)
                {
                    var adornerLayer = AdornerLayer.GetAdornerLayer(ellipse);
                    adornerLayer?.Add(new ResizerAdorner(ellipse));
                }

                return;
            }

            // Ocultar el adorno
            if (d is Rectangle rect)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(rect);
                if (adornerLayer is null)
                    return;

                var adorners = adornerLayer.GetAdorners(rect);
                foreach (var adorner in adorners)
                {
                    if (adorner is null)
                        continue;

                    if (adorner is ResizerAdorner)
                        adornerLayer.Remove(adorner);
                }
            }

            if (d is Ellipse ellip)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(ellip);
                if (adornerLayer is null)
                    return;

                var adorners = adornerLayer.GetAdorners(ellip);
                foreach (var adorner in adorners)
                {
                    if (adorner is null)
                        continue;

                    if (adorner is ResizerAdorner)
                        adornerLayer.Remove(adorner);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnShowSelectionAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool showAdorner = (bool)e.NewValue;
            if (showAdorner)
            {
                if (d is Rectangle rectangle)
                {
                    var adornerLayer = AdornerLayer.GetAdornerLayer(rectangle);
                    adornerLayer?.Add(new SelectionAdorner(rectangle));
                }

                if (d is Ellipse ellipse)
                {
                    var adornerLayer = AdornerLayer.GetAdornerLayer(ellipse);
                    adornerLayer?.Add(new SelectionAdorner(ellipse));
                }

                return;
            }

            // Ocultar el adorno
            if (d is Rectangle rect)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(rect);
                if (adornerLayer is null)
                    return;

                var adorners = adornerLayer.GetAdorners(rect);
                foreach (var adorner in adorners)
                {
                    if (adorner is null)
                        continue;

                    if (adorner is SelectionAdorner)
                        adornerLayer.Remove(adorner);
                }
            }

            if (d is Ellipse ellip)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(ellip);
                if (adornerLayer is null)
                    return;

                var adorners = adornerLayer.GetAdorners(ellip);
                foreach (var adorner in adorners)
                {
                    if (adorner is null)
                        continue;

                    if (adorner is SelectionAdorner)
                        adornerLayer.Remove(adorner);
                }
            }
        }
    }
}
