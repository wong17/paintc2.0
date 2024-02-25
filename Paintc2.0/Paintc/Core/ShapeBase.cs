using Paintc.Adorners;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Core
{
    public abstract class ShapeBase(string? name, Color color) : DependencyObject
    {
        /* Contiene el adorno para cambiar el tamaño de la figura */
        private static readonly Dictionary<Type, Type> ResizeAdorners = new()
        {
            { typeof(Rectangle), typeof(ResizerAdorner) },
            { typeof(Ellipse), typeof(ResizerAdorner) },
            { typeof(Line), typeof(LineResizerAdorner) },
            { typeof(Polyline), typeof(PolylineResizerAdorner) }
        };

        /* Contiene el adorno para indicar que la figura esta seleccionada */
        private static readonly Dictionary<Type, Type> SelectionAdorners = new()
        {
            { typeof(Rectangle), typeof(SelectionAdorner) },
            { typeof(Ellipse), typeof(SelectionAdorner) },
            { typeof(Line), typeof(LineSelectionAdorner) },
            { typeof(Polyline), typeof(PolylineSelectionAdorner) }
        };

        /* Contiene el adorno para arrastrar la figura seleccionada */
        private static readonly Dictionary<Type, Type> DragAdorners = new()
        {
            { typeof(Rectangle), typeof(DragAdorner) },
            { typeof(Ellipse), typeof(DragAdorner) },
            { typeof(Line), typeof(LineDragAdorner) }
        };

        /* Nombre para identificarla en el explorador de figuras */
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

        #region ATTACHED_PROPERTIES_CALLBACKS

        /// <summary>
        /// Muestra el adorno de cambiar de tamaño
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnShowResizeAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => HandleAdorners(d, e, ResizeAdorners);

        /// <summary>
        /// Muestra el adorno de selección
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnShowSelectionAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => HandleAdorners(d, e, SelectionAdorners);

        /// <summary>
        /// Muestra el adorno de arrastrar
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnShowDragAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => HandleAdorners(d, e, DragAdorners);

        /// <summary>
        /// Contiene la lógica para agregar o eliminar el adorno de una figura.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="adornersDictionary"></param>
        private static void HandleAdorners(DependencyObject d, DependencyPropertyChangedEventArgs e, Dictionary<Type, Type> adornersDictionary)
        {
            if (d is not Shape shape)
                return;

            bool showAdorner = Convert.ToBoolean(e.NewValue);
            /* Obtener capa de adornos de la figura */
            var adornerLayer = AdornerLayer.GetAdornerLayer(shape);
            Type shapeType = shape.GetType();
            /* Si no se pudo obtener el adorno asociado a la figura... */
            if (!adornersDictionary.TryGetValue(shapeType, out Type? adornerType))
                return;
            /* Crea una instancia del adorno pasandole por parámatro la figura */
            Adorner? adorner = (Adorner?)Activator.CreateInstance(adornerType, shape);
            if (adorner is null)
                return;

            /* Si se obtuvo el adorno y showAdorner es true, se agrega a la figura */
            if (showAdorner)
                adornerLayer?.Add(adorner);
            else
                RemoveAdorner(adorner, shape);
        }

        /// <summary>
        /// Elimina/oculta el adorno <T> especificado de la figura
        /// </summary>
        /// <param name="adornerType"></param>
        /// <param name="shape"></param>
        private static void RemoveAdorner(Adorner adornerType, Shape shape)
        {
            /* Obtener capa de adornos de la figura */
            var adornerLayer = AdornerLayer.GetAdornerLayer(shape);
            if (adornerLayer is null)
                return;
            /* Obtener todos los adornos de la figura */
            var adorners = adornerLayer.GetAdorners(shape);
            if (adorners is null)
                return;
            /* Eliminar el adorno indicado "adornerType" */
            foreach (var adorner in adorners)
            {
                if (adorner is null)
                    continue;

                if (adorner.GetType() == adornerType.GetType())
                    adornerLayer.Remove(adorner);
            }
        }

        #endregion
    }
}