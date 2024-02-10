using Paintc.Core;
using Paintc.Enums;
using Paintc.Factory;
using Paintc.Model;
using Paintc.Service;
using Paintc.Service.Collections;
using Paintc.Shapes;
using Paintc.View.UserControls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Paintc.Controller
{
    public class DrawingHandler
    {
        // Singleton
        private static readonly DrawingHandler instance = new();
        public static DrawingHandler Instance => instance;
        private DrawingHandler() { }

        #region PROPERTIES

        private DrawingPanel? _drawingPanel;
        public DrawingPanel? DrawingPanel
        {
            get => _drawingPanel;
            set
            {
                _drawingPanel = value;
                ToolSelectionService.Instance.ToolboxEventHandler += ToolboxEventHandler;
                ToolSelectionService.Instance.UpdateCurrentTool(ToolType.SelectTool);
                
                SelectedColorService.Instance.UpdateSelectedColorEventHandler += UpdateSelectedColorEventHandler;
            }
        }

        /// <summary>
        /// Contiene todos los elementos que hay sobre el canvas
        /// </summary>
        public ObservableCollection<ShapeBase?> Shapes { get; } = [];
        /// <summary>
        /// Referencia de la figura/forma que se esta dibujando
        /// </summary>
        private ShapeBase? _currentShape;
        /// <summary>
        /// Referencia de la figura/forma que se selecciono de ultimo con la herramienta SelectTool (la figura que tiene el adorner)
        /// </summary>
        private ShapeBase? _selectedShape;
        /// <summary>
        /// Referencia de la herramienta que se esta utilizando
        /// </summary>
        private Toolbox? _toolbox;
        /// <summary>
        /// 
        /// </summary>
        private DrawingState _state = DrawingState.Finished;
        /// <summary>
        /// 
        /// </summary>
        private Point _lastMousePosition = new();
        private Point _currentMousePosition = new();
        /// <summary>
        /// Contador de figuras/formas dibujadas
        /// </summary>
        private int _globalShapeCounter = 0;
        /// <summary>
        /// 
        /// </summary>
        private CGAColorPalette? _currentColor;

        #endregion

        #region TOOLSERVICE_EVENT

        /// <summary>
        /// Se ejecuta cada vez que se selecciona una nueva herramienta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="toolbox"></param>
        private void ToolboxEventHandler(object? sender, Toolbox toolbox)
        {
            _toolbox = toolbox;
            _state = DrawingState.Finished;
            _currentShape = null;
        }

        #endregion

        #region SELECTEDCOLOR_EVENT

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void UpdateSelectedColorEventHandler(object? sender, CGAColorPalette color)
        {
            if (_currentColor == color)
                return;

            _currentColor = color;
        }

        #endregion

        #region DRAWINGPANEL_EVENTS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_toolbox is null || _drawingPanel is null || _currentColor is null || _state == DrawingState.Drawing) return;

            if (_toolbox.CurrentTool == ToolType.FillerTool)
            {
                FillerToolLeftButtonDown(sender, e);
                _state = DrawingState.Finished;
                return;
            }

            if (_toolbox.CurrentTool == ToolType.SelectTool)
            {
                SelectToolLeftButtonDown(sender, e);
                _state = DrawingState.Finished;
                return;
            }

            _currentShape = ShapeFactory.Create(_toolbox.CurrentTool, GenerateShapeName(_toolbox.CurrentTool), CGAColorPaletteService.GetColor(_currentColor));
            if (_currentShape is null)
                return;

            _state = DrawingState.Drawing;
            _lastMousePosition = e.GetPosition(_drawingPanel.CustomCanvas);
       
            // Para las demás herramientas
            _currentShape.SetLastMousePosition(_lastMousePosition);
            _drawingPanel.CustomCanvas.Children.Add(_currentShape.GetShape());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_toolbox is null) return;

            // Si se pulsa solo Click derecho no cierra el poligono
            if (_toolbox?.CurrentTool == ToolType.PolygonTool)
            {
                if (_currentShape is PolygonShape polygone)
                {
                    polygone.LeaveOpenPolygone();
                    Shapes.Add(_currentShape);
                }
            }
            // Si se pulsa Ctrl + Click derecho cierra el poligono si es posible
            if (_toolbox?.CurrentTool == ToolType.PolygonTool && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (_currentShape is PolygonShape polygone)
                {
                    polygone.ClosePolygone();
                    Shapes.Add(_currentShape);
                }
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_drawingPanel is null) return;

            if (_toolbox?.CurrentTool == ToolType.FillerTool)
                return;

            if (_toolbox?.CurrentTool == ToolType.PolygonTool)
            {

                return;
            }

            // para las demás herramientas...
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _currentMousePosition = e.GetPosition(_drawingPanel.CustomCanvas);
                _currentShape?.SetCurrentMousePosition(_currentMousePosition);
            }
            else if (e.LeftButton == MouseButtonState.Released && _state == DrawingState.Drawing)
            {
                _state = DrawingState.Finished;
                Shapes.Add(_currentShape);
            }
        }

        #endregion

        /// <summary>
        /// Limpia el canvas, lista de formas y contador
        /// </summary>
        public void ClearDrawingPanel()
        {
            if (_drawingPanel is null) return;
            // Eliminar elementos del canvas y de la lista de formas
            var canvasChildrens = _drawingPanel.CustomCanvas.Children;
            foreach (var shape in Shapes)
            {
                if (canvasChildrens.Contains(shape?.GetShape()))
                    canvasChildrens.Remove(shape?.GetShape());
            }
            Shapes.Clear();
            // Reiniciar contador de figuras
            _globalShapeCounter = 0;
        }

        /// <summary>
        /// Elimina una figura/forma del canvas y explorador de figuras
        /// </summary>
        /// <param name="shapeName"></param>
        public void RemoveShape(string? shapeName)
        {
            if (_drawingPanel is null || shapeName is null)
                return;

            ShapeBase? shape = Shapes.Where(s => s is not null && s.Name is not null && s.Name.Equals(shapeName)).FirstOrDefault();
            if (shape is null)
            {
                MessageBox.Show($"An error ocurred, {shapeName} was not found in shape explorer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_drawingPanel.CustomCanvas.Children.Contains(shape.GetShape()))
            {
                MessageBox.Show($"An error ocurred, {shapeName} was not found in the drawing area.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Elimina forma del canvas y de la lista de formas
            _drawingPanel.CustomCanvas.Children.Remove(shape.GetShape());
            Shapes.Remove(shape);
        }

        /// <summary>
        /// Genera el nombre de la nueva figura/forma dibujada en el canvas
        /// </summary>
        /// <param name="tool"></param>
        /// <returns></returns>
        private string GenerateShapeName(ToolType tool) => $"{tool.ToString().Replace("Tool", "")}-{++_globalShapeCounter}";

        #region TOOL_LEFTMOUSEDOWN

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillerToolLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectToolLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Si se hace click en el canvas quitar adorno de la figura anteriormente seleccionada
            if (e.OriginalSource == _drawingPanel?.CustomCanvas && _selectedShape is not null)
            {
                ShapeBase.SetShowResizeAdorner(_selectedShape.GetShape(), false);
                ShapeBase.SetShowSelectionAdorner(_selectedShape.GetShape(), false);
                _selectedShape = null;
                return;
            }

            // Mostrar adorno de cada figura que se haga click...
            if (e.OriginalSource is Rectangle rectangle)
            {
                // Si ya hay un rectangulo seleccionado...
                if (_selectedShape is not null)
                {
                    ShapeBase.SetShowSelectionAdorner(_selectedShape.GetShape(), false);
                    ShapeBase.SetShowResizeAdorner(_selectedShape.GetShape(), false);
                    _selectedShape = null;
                }
                _selectedShape = Shapes.Where(s => s is RectangleShape r && r.GetShape().Equals(rectangle)).First();
            }

            if (e.OriginalSource is Ellipse ellipse)
            {
                // Si ya hay una ellipse seleccionada...
                if (_selectedShape is not null)
                {
                    ShapeBase.SetShowSelectionAdorner(_selectedShape.GetShape(), false);
                    ShapeBase.SetShowResizeAdorner(_selectedShape.GetShape(), false);
                    _selectedShape = null;
                }
                _selectedShape = Shapes.Where(s => s is EllipseShape e && e.GetShape().Equals(ellipse)).First();
            }

            if (_selectedShape is null)
                return;

            ShapeBase.SetShowSelectionAdorner(_selectedShape.GetShape(), true);
            ShapeBase.SetShowResizeAdorner(_selectedShape.GetShape(), true);
        }

        #endregion
    }
}
