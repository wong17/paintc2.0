using Paintc.Adorners;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Core
{
    public abstract class ShapeBase(string? name, Color color) : DependencyObject
    {
        public string? Name { get; set; } = name;
        protected readonly Color Color = color;
        protected Point LastMousePosition { get; set; }
        protected Point CurrentMousePosition { get; set; }

        public abstract void SetLastMousePosition(Point lastPosition);

        public abstract void SetCurrentMousePosition(Point currentPosition);

        public abstract Shape GetShape();

        #region DEPENDENCY_PROPERTY

        public bool IsDraggableProperty
        {
            get { return (bool)GetValue(IsDraggablePropertyProperty); }
            set { SetValue(IsDraggablePropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDraggableProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDraggablePropertyProperty =
            DependencyProperty.Register("IsDraggableProperty", typeof(bool), typeof(ShapeBase), new PropertyMetadata(true, IsDraggablePropertyChanged));

        public bool IsResizableProperty
        {
            get { return (bool)GetValue(IsResizablePropertyProperty); }
            set { SetValue(IsResizablePropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsResizableProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsResizablePropertyProperty =
            DependencyProperty.Register("IsResizableProperty", typeof(bool), typeof(ShapeBase), new PropertyMetadata(true, IsResizablePropertyChanged));

        #endregion DEPENDENCY_PROPERTY

        /// <summary>
        /// Muestra u oculta el adorno de arrastrar a la figura seleccionada
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void IsDraggablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ShapeBase shapeBase)
                return;

            bool newValue = (bool)e.NewValue;
            SetShowDragAdorner(shapeBase.GetShape(), newValue);
        }

        /// <summary>
        /// Muestra u oculta el adorno de cambiar de tamaño a la figura seleccionada
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void IsResizablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ShapeBase shapeBase)
                return;

            bool newValue = (bool)e.NewValue;
            SetShowResizeAdorner(shapeBase.GetShape(), newValue);
        }

        #region ATTACHED_PROPERTIES

        public static readonly DependencyProperty ShowResizeAdornerProperty =
            DependencyProperty.RegisterAttached("ShowResizeAdorner", typeof(bool), typeof(ShapeBase), new FrameworkPropertyMetadata(false, OnShowResizeAdornerChanged));

        public static bool GetShowResizeAdorner(UIElement element) => (bool)element.GetValue(ShowResizeAdornerProperty);

        public static void SetShowResizeAdorner(UIElement element, bool value) => element.SetValue(ShowResizeAdornerProperty, value);

        public static readonly DependencyProperty ShowSelectionAdornerProperty =
            DependencyProperty.RegisterAttached("ShowSelectionAdorner", typeof(bool), typeof(ShapeBase), new FrameworkPropertyMetadata(false, OnShowSelectionAdornerChanged));

        public static bool GetShowSelectionAdorner(UIElement element) => (bool)element.GetValue(ShowSelectionAdornerProperty);

        public static void SetShowSelectionAdorner(UIElement element, bool value) => element.SetValue(ShowSelectionAdornerProperty, value);

        public static readonly DependencyProperty ShowDragAdornerProperty =
            DependencyProperty.RegisterAttached("ShowDragAdorner", typeof(bool), typeof(ShapeBase), new FrameworkPropertyMetadata(false, OnShowDragAdornerChanged));

        public static bool GetShowDragAdorner(UIElement element) => (bool)element.GetValue(ShowDragAdornerProperty);

        public static void SetShowDragAdorner(UIElement element, bool value) => element.SetValue(ShowDragAdornerProperty, value);

        #endregion ATTACHED_PROPERTIES

        /// <summary>
        /// Muestra el adorno de cambiar de tamaño
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnShowResizeAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Shape shape)
                return;

            bool showAdorner = Convert.ToBoolean(e.NewValue);
            var adornerLayer = AdornerLayer.GetAdornerLayer(shape);
            // Si es la herramienta de PencilTool
            if (showAdorner && shape is Polyline)
            {
                adornerLayer?.Add(new PolylineResizerAdorner(shape));
                return;
            }
            // Si es la herramienta de LineTool
            if (showAdorner && shape is Line)
            {
                return;
            }

            // Si es otra herramienta...
            if (showAdorner && (shape is Rectangle || shape is Ellipse))
            {
                adornerLayer?.Add(new ResizerAdorner(shape));
                return;
            }

            // Ocultar el adorno si es la herramienta de PencilTool
            if (!showAdorner && shape is Polyline)
            {
                RemoveAdorner<PolylineResizerAdorner>(shape);
                return;
            }
            // Ocultar el adorno si es la herramienta de LineTool
            if (!showAdorner && shape is Line)
            {
                return;
            }

            // Ocultar el adorno si es otra herramienta...
            RemoveAdorner<ResizerAdorner>(shape);
        }

        /// <summary>
        /// Muestra el adorno de selección
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnShowSelectionAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Shape shape)
                return;

            bool showAdorner = Convert.ToBoolean(e.NewValue);
            var adornerLayer = AdornerLayer.GetAdornerLayer(shape);
            // Si es la herramienta de PencilTool
            if (showAdorner && shape is Polyline)
            {
                adornerLayer?.Add(new PolylineSelectionAdorner(shape));
                return;
            }
            // Si es la herramienta de LineTool
            if (showAdorner && shape is Line)
            {
                adornerLayer?.Add(new LineSelectionAdorner(shape));
                return;
            }
            // Si es otra herramienta...
            if (showAdorner && (shape is Rectangle || shape is Ellipse))
            {
                adornerLayer?.Add(new SelectionAdorner(shape));
                return;
            }

            // Ocultar el adorno si es la herramienta de PencilTool
            if (!showAdorner && shape is Polyline)
            {
                RemoveAdorner<PolylineSelectionAdorner>(shape);
                return;
            }
            // Ocultar el adorno si es la herramienta de LineTool
            if (!showAdorner && shape is Line)
            {
                RemoveAdorner<LineSelectionAdorner>(shape);
                return;
            }
            // Ocultar el adorno si es otra herramienta...
            RemoveAdorner<SelectionAdorner>(shape);
        }

        /// <summary>
        /// Muestra el adorno de arrastrar
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnShowDragAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Shape shape)
                return;

            bool showAdorner = Convert.ToBoolean(e.NewValue);
            var adornerLayer = AdornerLayer.GetAdornerLayer(shape);
            // Si es la herramienta de PencilTool
            if (showAdorner && shape is Polyline)
            {
                return;
            }
            // Si es la herramienta de LineTool
            if (showAdorner && shape is Line)
            {
                return;
            }

            // Si es otra herramienta...
            if (showAdorner && (shape is Rectangle || shape is Ellipse))
            {
                adornerLayer?.Add(new DragAdorner(shape));
                return;
            }

            // Ocultar el adorno si es la herramienta de PencilTool
            if (!showAdorner && shape is Polyline)
            {
                return;
            }
            // Ocultar el adorno si es la herramienta de LineTool
            if (!showAdorner && shape is Line)
            {
                return;
            }

            RemoveAdorner<DragAdorner>(shape);
        }

        /// <summary>
        /// Elimina/oculta el adorno <T> especificado de la figura
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shape"></param>
        private static void RemoveAdorner<T>(Shape shape) where T : Adorner
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(shape);
            if (adornerLayer is null)
                return;

            var adorners = adornerLayer.GetAdorners(shape);
            if (adorners is null)
                return;

            foreach (var adorner in adorners)
            {
                if (adorner is null)
                    continue;

                if (adorner is T)
                    adornerLayer.Remove(adorner);
            }
        }
    }
}