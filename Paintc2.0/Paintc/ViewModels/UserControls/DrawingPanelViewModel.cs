using Paintc.Core;
using Paintc.Enums;
using Paintc.Model;
using Paintc.Service;
using Paintc.Views.UserControls;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Paintc.ViewModels.UserControls
{
    public class DrawingPanelViewModel : ViewModel
    {
        private readonly DrawingPanel _drawingPanel;
        private GraphicMode? _graphicMode;

        private static SolidColorBrush? _colorBrush;
        private static ResourceDictionary? _brushResource;
        private static DrawingBrush? _gridBrush;

        private static double _oldGridSize = 1;

        #region ATTACHED_PROPERTIES

        public static bool GetShowGrid(DependencyObject obj) => (bool)obj.GetValue(ShowGridProperty);

        public static void SetShowGrid(DependencyObject obj, bool value) => obj.SetValue(ShowGridProperty, value);

        // Using a DependencyProperty as the backing store for ShowGrid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowGridProperty =
            DependencyProperty.RegisterAttached("ShowGrid", typeof(bool), typeof(DrawingPanelViewModel), new PropertyMetadata(false, ShowGridCallback));

        private static void ShowGridCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Obtener el GeometryDrawing "DrawingBackground" del DrawingBrush "_gridBrush"
            if (d is not Canvas canvas || _gridBrush is null || _gridBrush.Drawing is not DrawingGroup drawingGroup)
                return;

            var value = (bool)e.NewValue;
            if (value)
            {
                var canvasWidth = canvas.ActualWidth;
                var cellSize = _oldGridSize == 1 ? canvasWidth / _oldGridSize / 10 : _oldGridSize;

                _gridBrush.Viewport = new Rect(0, 0, cellSize, cellSize);

                GeometryDrawing? geometryDrawing = drawingGroup?.Children[0] as GeometryDrawing;
                if (geometryDrawing is not null)
                    geometryDrawing.Brush = _colorBrush;

                RectangleGeometry? rectangleGeometry = geometryDrawing?.Geometry as RectangleGeometry;
                if (rectangleGeometry is not null)
                    rectangleGeometry.Rect = new Rect(0, 0, cellSize, cellSize);

                canvas.Background = _gridBrush;
                return;
            }

            canvas.Background = _colorBrush;
        }

        public static double GetGridSize(DependencyObject obj) => (double)obj.GetValue(GridSizeProperty);

        public static void SetGridSize(DependencyObject obj, double value) => obj.SetValue(GridSizeProperty, value);

        // Using a DependencyProperty as the backing store for GridSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridSizeProperty =
            DependencyProperty.RegisterAttached("GridSize", typeof(double), typeof(DrawingPanelViewModel), new PropertyMetadata((double)0, UpdateGridSizeCallback));

        private static void UpdateGridSizeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Canvas canvas || _gridBrush is null || _gridBrush.Drawing is not DrawingGroup drawingGroup)
                return;

            var newSize = (double)e.NewValue;
            if (newSize != 0)
            {
                var canvasWidth = canvas.ActualWidth;
                var cellSize = canvasWidth / newSize / 10;

                _gridBrush.Viewport = new Rect(0, 0, cellSize, cellSize);

                GeometryDrawing? geometryDrawing = drawingGroup?.Children[0] as GeometryDrawing;
                if (geometryDrawing is not null)
                    geometryDrawing.Brush = _colorBrush;

                RectangleGeometry? rectangleGeometry = geometryDrawing?.Geometry as RectangleGeometry;
                if (rectangleGeometry is not null)
                    rectangleGeometry.Rect = new Rect(0, 0, cellSize, cellSize);

                _oldGridSize = cellSize;
                canvas.Background = _gridBrush;
                return;
            }
            
            canvas.Background = _colorBrush;
        }

        #endregion

        #region ZOOM

        /// <summary>
        /// Factor de incremento/decremento de la matriz del canvas
        /// </summary>
        private const double SCALE_FACTOR = 0.1;

        /// <summary>
        /// Maximo y minimo de zoom permitido
        /// </summary>
        private const double MAX_ZOOM_IN = 5.0;

        private const double MAX_ZOOM_OUT = 0.9;

        /// <summary>
        /// Para aplicar escalación de la matriz del canvas centrada en la posición actual del mouse
        /// </summary>
        private readonly ScaleTransform scaleTransform = new();

        #endregion ZOOM

        public DrawingPanelViewModel(DrawingPanel drawingPanel)
        {
            _drawingPanel = drawingPanel;
            _brushResource = new()
            {
                Source = new Uri("/Paintc;component/Resources/Brushes/DrawingBrushes.xaml", UriKind.RelativeOrAbsolute)
            };
            _gridBrush = (DrawingBrush?)_brushResource["GridLineBrush"];

            DrawingHandler.Instance.DrawingPanel = drawingPanel;
            PropertiesPanelService.Instance.CanvasResizerEventHandler += CanvasResizerEventHandler;
            PropertiesPanelService.Instance.ChangeBackgroundColorEventHandler += ChangeBackgroundColorEventHandler;
            PropertiesPanelService.Instance.ShowGridEventHandler += ShowGridEventHandler;
            PropertiesPanelService.Instance.UpdateGridSizeEventHandler += UpdateGridSizeEventHandler;

            // Events
            _drawingPanel.CustomCanvas.LayoutTransform = scaleTransform;
            _drawingPanel.MainScrollViewer.PreviewMouseWheel += MainScrollViewer_PreviewMouseWheel;
            _drawingPanel.MainScrollViewer.PreviewMouseMove += MainScrollViewer_PreviewMouseMove;
            _drawingPanel.CustomCanvas.MouseWheel += CustomCanvas_MouseWheel;
            _drawingPanel.CustomCanvas.MouseLeftButtonDown += CustomCanvas_MouseLeftButtonDown;
            _drawingPanel.CustomCanvas.MouseRightButtonDown += CustomCanvas_MouseRightButtonDown;
            _drawingPanel.CustomCanvas.MouseMove += CustomCanvas_MouseMove;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (DrawingHandler.Instance.State == DrawingState.Finished || DrawingHandler.Instance.Toolbox is null)
                return;

            /* Para terminar y agregar figura al explorador cuando se esta dibujando y se suelta el click izquierdo fuera del canvas. */
            var currentTool = DrawingHandler.Instance.Toolbox.CurrentTool;
            if (e.LeftButton == MouseButtonState.Released && (currentTool != ToolType.SelectTool || currentTool != ToolType.FillerTool || currentTool != ToolType.EraserTool))
            {
                DrawingHandler.Instance.FinishAndAddShape();
                e.Handled = true;
            }
        }

        #region ZOOM

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) return;

            // Obtener posición del mouse sobre el canvas
            Point mousePosition = e.GetPosition(_drawingPanel.CustomCanvas);
            // Positivo si se gira hacia adelante (hacia el monitor), Negativo si se gira hacia atras (hacia el usuario)
            var factor = (e.Delta > 0) ? SCALE_FACTOR : -SCALE_FACTOR;
            // Aplicar escalación centrada en la posición actual del mouse
            scaleTransform.CenterX = mousePosition.X;
            scaleTransform.CenterY = mousePosition.Y;
            scaleTransform.ScaleX = factor < 0 ? Math.Max(MAX_ZOOM_OUT, scaleTransform.ScaleX + factor) : Math.Min(MAX_ZOOM_IN, scaleTransform.ScaleX + factor);
            scaleTransform.ScaleY = factor < 0 ? Math.Max(MAX_ZOOM_OUT, scaleTransform.ScaleY + factor) : Math.Min(MAX_ZOOM_IN, scaleTransform.ScaleY + factor);
            // Ajustar las barras de desplazamiento del ScrollViewer
            _drawingPanel.MainScrollViewer.ScrollToHorizontalOffset(_drawingPanel.MainScrollViewer.HorizontalOffset + mousePosition.X * factor);
            _drawingPanel.MainScrollViewer.ScrollToVerticalOffset(_drawingPanel.MainScrollViewer.VerticalOffset + mousePosition.Y * factor);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Delta > 0 && IsMaxZoomInReached()) e.Handled = true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private bool IsMaxZoomInReached() => scaleTransform.ScaleX == MAX_ZOOM_IN && scaleTransform.ScaleY == MAX_ZOOM_IN;

        #endregion ZOOM

        #region PROPERTIESPANEL_SERVICE

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="graphicMode"></param>
        public void CanvasResizerEventHandler(object? sender, GraphicMode? graphicMode)
        {
            _graphicMode = graphicMode;

            if (_graphicMode is null) return;

            // Comprobar si se ha dibujado...
            if (DrawingHandler.Instance.Shapes.Count != 0)
            {
                // Preguntar si desea cambiar el tamaño de la ventana...
                var result = MessageBox.Show("Are you sure you want to resize the drawing area? Any unsaved progress will be lost.", "Warning",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    PropertiesPanelService.Instance.UpdateGraphicModeSelection(false);
                    return;
                }

                DrawingHandler.Instance.ClearDrawingPanel();
            }
            _drawingPanel.CustomCanvas.Width = _graphicMode.Width;
            _drawingPanel.CustomCanvas.Height = _graphicMode.Height;
            PropertiesPanelService.Instance.UpdateGraphicModeSelection(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="color"></param>
        public void ChangeBackgroundColorEventHandler(object? sender, CGAColor color)
        {
            _colorBrush = new SolidColorBrush(color.Color);
            /* 
             * Si ya estaba activo el grid se oculta y se vuelve a mostrar cambiando el relleno de los rectángulos 
             * por el nuevo color y se vuelve a dibujar el grid.
             */
            if (GetShowGrid(_drawingPanel.CustomCanvas) && _gridBrush is not null)
            {
                SetShowGrid(_drawingPanel.CustomCanvas, false);
                SetGridSize(_drawingPanel.CustomCanvas, 0);
                SetShowGrid(_drawingPanel.CustomCanvas, true);
                SetGridSize(_drawingPanel.CustomCanvas, 1);
            }
            else
                _drawingPanel.CustomCanvas.Background = _colorBrush;
        }

        /// <summary>
        /// Se ejecuta cuando se invoca el metodo 'ShowGrid' del servicio 'PropertiesPanelService'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="flag"></param>
        private void ShowGridEventHandler(object? sender, bool flag) => SetShowGrid(_drawingPanel.CustomCanvas, flag);

        /// <summary>
        /// Se ejecuta cuando se invoca el metodo 'UpdateGridSize' del servicio 'PropertiesPanelService'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void UpdateGridSizeEventHandler(object? sender, double value) => SetGridSize(_drawingPanel.CustomCanvas, value);

        #endregion PROPERTIESPANEL_SERVICE

        #region CUSTOMCANVAS_EVENTS

        private void CustomCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            DrawingHandler.Instance.OnMouseMove(sender, e);
            StatusBarPanelService.Instance.UpdateMousePosition(e.GetPosition(_drawingPanel.CustomCanvas));
        }

        private void CustomCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => 
            DrawingHandler.Instance.OnMouseLeftButtonDown(sender, e);

        private void CustomCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e) =>
            DrawingHandler.Instance.OnMouseRightButtonDown(sender, e);

        #endregion CUSTOMCANVAS_EVENTS
    }
}