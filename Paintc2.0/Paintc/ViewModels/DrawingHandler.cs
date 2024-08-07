﻿using Paintc.Core;
using Paintc.Enums;
using Paintc.Factory;
using Paintc.Model;
using Paintc.Service;
using Paintc.Service.Collections;
using Paintc.Shapes;
using Paintc.Views.UserControls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.ViewModels
{
    public class DrawingHandler
    {
        // Singleton
        private static readonly DrawingHandler instance = new();

        public static DrawingHandler Instance => instance;

        private DrawingHandler()
        { }

        #region PROPERTIES

        private DrawingPanel? _drawingPanel;

        public DrawingPanel? DrawingPanel
        {
            get => _drawingPanel;
            set
            {
                _drawingPanel = value;
                ToolboxPanelService.Instance.ToolboxEventHandler += ToolboxEventHandler;
                ToolboxPanelService.Instance.UpdateCurrentTool(ToolType.SelectTool);

                ToolboxPanelService.Instance.UpdateSelectedColorEventHandler += UpdateSelectedColorEventHandler;
                ToolboxPanelService.Instance.UpdateSelectedColor(CGAColorPalette.White);

                PropertiesPanelService.Instance.SaveCurrentBackgroundColorEventHandler += SaveCurrentBackgroundColorEventHandler;
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

        public Toolbox? Toolbox { get => _toolbox; }

        /// <summary>
        ///
        /// </summary>
        private DrawingState _state = DrawingState.Finished;

        public DrawingState State { get => _state; }

        /// <summary>
        /// Posición (x,y) del mouse sobre el canvas
        /// </summary>
        private Point _lastMousePosition = new();

        private Point _currentMousePosition = new();

        /// <summary>
        /// Contador de figuras/formas dibujadas
        /// </summary>
        private int _globalShapeCounter = 0;

        /// <summary>
        /// Color seleccionado para dibujar con la herramienta
        /// </summary>
        private CGAColorPalette? _currentColor;

        /// <summary>
        /// Color de fondo actual del canvas
        /// </summary>
        private CGAColorPalette? _currentBackgroundColor;

        /// <summary>
        /// Contiene mensajes de ayuda en base a la herramienta seleccionada
        /// </summary>
        private readonly Dictionary<ToolType, string> _toolHints = new()
        {
            { ToolType.PolygonTool, "Press left click to add a vertex | Press right click to close the polygon" },
            { ToolType.LineTool, "Press left click and drag the mouse to draw a line" },
            { ToolType.RectangleTool, "Press left click and drag the mouse to draw a rectangle" },
            { ToolType.EllipseTool, "Press left click and drag the mouse to draw a ellipse" },
            { ToolType.EraserTool, "Press left click to select a shape to remove" },
            { ToolType.FillerTool, "Press left click to select a shape to change fill color" },
            { ToolType.PencilTool, "While pressing left click drag the mouse to create a free shape" },
            { ToolType.SelectTool, "Press left click to select a shape to view its properties" }
        };

        #endregion PROPERTIES

        #region TOOLBAR_PANEL

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

            // Limpiar seleccion al seleccionar una nueva herramienta
            if (_selectedShape is not null)
            {
                SetShowAdornersAttachedProperties(_selectedShape.GetShape(), false);
                _selectedShape = null;
                PropertiesPanelService.Instance.SetEnableShapeOptions(_selectedShape);
                PropertiesPanelService.Instance.ShowPropertiesPanel(_selectedShape);
            }

            // Agregar hint del uso de la herramienta "Ctrl + Scroll: Zoom in/out (inside canvas)"
            StatusBarPanelService.Instance.UpdateHintText(_toolHints.GetValueOrDefault(_toolbox.CurrentTool));
        }

        /// <summary>
        /// Se ejecuta cada vez que se selecciona un color 
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

        #endregion TOOLBAR_PANEL

        #region DRAWINGPANEL_EVENTS

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_toolbox is null || _drawingPanel is null || _currentColor is null || _state == DrawingState.Drawing)
            {
                //MessageBox.Show("Caja de herramienta, canvas, color inicial o estado inicial no se han iniciado", 
                //    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

            if (_toolbox.CurrentTool == ToolType.EraserTool)
            {
                EraserToolLeftButtonDown(sender, e);
                _state = DrawingState.Finished;
                return;
            }

            if (_toolbox.CurrentTool == ToolType.PolygonTool)
            {
                PolygonToolLeftButtonDown(sender, e);
                return;
            }

            /* Crear nueva figura en base a la herramienta seleccionada */
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
            if (_toolbox is null || _drawingPanel is null)
            {
                MessageBox.Show("Caja de herramienta o canvas no se han iniciado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Si se esta dibujando un poligono cerrarlo
            if (_currentShape is PolygonShape poly && poly.IsActivePolygon)
            {
                poly.IsActivePolygon = false;
                _state = DrawingState.Finished;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_drawingPanel is null)
            {
                MessageBox.Show("Canvas no se ha iniciado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_toolbox?.CurrentTool == ToolType.FillerTool
                || _toolbox?.CurrentTool == ToolType.EraserTool || _toolbox?.CurrentTool == ToolType.PolygonTool) return;

            // para las demás herramientas...
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _currentMousePosition = e.GetPosition(_drawingPanel.CustomCanvas);
                _currentShape?.SetCurrentMousePosition(_currentMousePosition);
            }
            else if (e.LeftButton == MouseButtonState.Released && _state == DrawingState.Drawing)
            {
                FinishAndAddShape();
            }
        }

        #endregion DRAWINGPANEL_EVENTS

        #region DRAWING_HANDLER

        /// <summary>
        /// Se invoca desde el evento MouseMove de esta clase cuando se esta dibujando una figura y se suelta el click izquierdo sobre el canvas,
        /// se invoca desde el evento PreviewMouseMove del ScrollViewer cuando se esta dibujando la figura y se suelta el click izquierdo fuera del canvas
        /// </summary>
        public void FinishAndAddShape()
        {
            _state = DrawingState.Finished;
            Shapes.Add(_currentShape);
        }

        /// <summary>
        /// Limpia el canvas, lista de formas, contador y panel de propiedades
        /// </summary>
        public void ClearDrawingPanel()
        {
            if (_drawingPanel is null)
            {
                MessageBox.Show("Canvas no se ha iniciado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
            // Reiniciar panel de propiedades
            if (_selectedShape is not null)
            {
                SetShowAdornersAttachedProperties(_selectedShape.GetShape(), false);
                _selectedShape = null;
                PropertiesPanelService.Instance.SetEnableShapeOptions(_selectedShape);
                PropertiesPanelService.Instance.ShowPropertiesPanel(_selectedShape);
            }
        }

        /// <summary>
        /// Elimina una figura/forma del canvas y explorador de figuras
        /// </summary>
        /// <param name="shapeName"></param>
        public void RemoveShape(string? shapeName)
        {
            if (_drawingPanel is null || shapeName is null)
            {
                MessageBox.Show("Canvas o nombre de la figura no se ha iniciado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

            // Si la figura a eliminar es la que esta actualmente seleccionada...
            if (_selectedShape is not null && shape.Equals(_selectedShape))
            {
                SetShowAdornersAttachedProperties(_selectedShape.GetShape(), false);
                _selectedShape = null;
            }

            // Elimina forma del canvas y de la lista de formas
            _drawingPanel.CustomCanvas.Children.Remove(shape.GetShape());
            Shapes.Remove(shape);
        }

        /// <summary>
        /// Busca y devuelve la figura por su nombre o null sino se encuentra
        /// </summary>
        /// <param name="shapeName"></param>
        /// <returns></returns>
        public ShapeBase? GetShapeBase(string? shapeName) => Shapes.Where(s => s is not null && s.Name is not null && s.Name.Equals(shapeName)).FirstOrDefault();

        /// <summary>
        /// Genera el nombre de la nueva figura/forma dibujada en el canvas
        /// </summary>
        /// <param name="tool"></param>
        /// <returns></returns>
        private string GenerateShapeName(ToolType tool) => $"{tool.ToString().Replace("Tool", "")}{++_globalShapeCounter}";

        /// <summary>
        /// Devuelve el color de fondo actual del canvas para ser usado por SourceViewWindow y su controlador
        /// para generar el código C poniendo el fondo del canvas del mismo color.
        /// </summary>
        public int GetBackgroundColor()
        {
            if (_drawingPanel is null)
                return Convert.ToInt32(CGAColorPalette.Black); // Fondo por defecto

            // Devuelve el último color de fondo del canvas antes de poner la imagen para usarlo en la plantilla de código
            if (_drawingPanel.CustomCanvas.Background is not SolidColorBrush brush)
                return Convert.ToInt32(CGAColorPaletteService.GetCGAColorPalette(CGAColorPaletteService.GetColor(_currentBackgroundColor)));

            return Convert.ToInt32(CGAColorPaletteService.GetCGAColorPalette(brush.Color)); // Fondo actual en el canvas
        }

        /// <summary>
        /// Retorna una lista que contiene la forma primitiva de cada figura dibujada en el canvas
        /// para generar el código C y mostrarlo en el panel SourceCodePanel
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<SimpleShapeBase>? GetSimpleShapes()
        {
            var primitiveShapes = new ObservableCollection<SimpleShapeBase>();
            
            foreach (var shape in Shapes)
            {
                if (shape is not null)
                    primitiveShapes.Add(shape.GetSimpleShape());
            }

            return primitiveShapes;
        }

        #endregion DRAWING_HANDLER

        #region ADORNERS

        /// <summary>
        /// Mostrar u ocultar todos los adornos de cada figura por medio de sus attached properties
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="show"></param>
        private static void SetShowAdornersAttachedProperties(Shape shape, bool show)
        {
            ShapeBase.SetShowSelectionAdorner(shape, show);
            ShapeBase.SetShowResizeAdorner(shape, show);
            ShapeBase.SetShowDragAdorner(shape, show);
        }

        /// <summary>
        /// Mostrar adorno de selección y de cambio de tamaño de la figura
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="show"></param>
        private static void SetResizerAdornerAttachedProperty(Shape shape, bool show)
        {
            ShapeBase.SetShowSelectionAdorner(shape, show);
            ShapeBase.SetShowResizeAdorner(shape, show);
        }

        /// <summary>
        /// Muestra adorno de selección y arrastrable de la figura
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="show"></param>
        private static void SetDraggableAdornerAttachedProperty(Shape shape, bool show)
        {
            ShapeBase.SetShowSelectionAdorner(shape, show);
            ShapeBase.SetShowDragAdorner(shape, show);
        }

        /// <summary>
        /// Muestra el adorno de la figura seleccionada
        /// </summary>
        /// <param name="shape"></param>
        public void ShowSelectedShapeAdorners(Shape shape)
        {
            if (_selectedShape is not null)
            {
                SetShowAdornersAttachedProperties(_selectedShape.GetShape(), false);
                _selectedShape = null;
            }

            _selectedShape = Shapes.Where(s => s is not null && s.GetShape().Equals(shape)).First();

            if (_selectedShape is null)
                return;

            // Sino se puede arrastrar ni cambiar de tamaño
            if (!_selectedShape.IsResizableProperty && !_selectedShape.IsDraggableProperty)
            {
                ShapeBase.SetShowSelectionAdorner(_selectedShape.GetShape(), true);
                return;
            }

            // Si se puede cambiar de tamaño pero no arrastrar
            if (_selectedShape.IsResizableProperty && !_selectedShape.IsDraggableProperty)
                SetResizerAdornerAttachedProperty(_selectedShape.GetShape(), true);
            // Si se puede arrastrar pero no cambiar el tamaño
            else if (_selectedShape.IsDraggableProperty && !_selectedShape.IsResizableProperty)
                SetDraggableAdornerAttachedProperty(_selectedShape.GetShape(), true);
            // Si se puede arrastrar y cambiar el tamaño
            else
                SetShowAdornersAttachedProperties(_selectedShape.GetShape(), true);
        }

        #endregion ADORNERS

        #region PROPERTIES_PANEL

        /// <summary>
        /// Se dispara cuando se selecciona el checkbox para habilitar/deshabilitar que una figura sea arrastrable
        /// </summary>
        /// <param name="isDraggable"></param>
        public void SetIsDraggableSelectedShape(bool isDraggable)
        {
            if (_selectedShape is null)
                return;

            _selectedShape.IsDraggableProperty = isDraggable;
        }

        /// <summary>
        /// Se dispara cuando se selecciona el checkbox para habilitar/deshabilitar que una figura se pueda cambiar de tamaño
        /// </summary>
        /// <param name="isResizable"></param>
        public void SetIsResizableSelectedShape(bool isResizable)
        {
            if (_selectedShape is null)
                return;

            _selectedShape.IsResizableProperty = isResizable;
        }

        /// <summary>
        /// Guarda el ultimo color de fondo (color actual) del canvas al momento que se selecciona una imagen como fondo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCurrentBackgroundColorEventHandler(object? sender, CGAColor currentBgColor)
        {
            _currentBackgroundColor = currentBgColor.Cpalette;
        }

        #endregion PROPERTIES_PANEL

        #region TOOL_LEFTMOUSEDOWN

        /// <summary>
        /// Evento click izquierdo de la herramienta FillerTool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillerToolLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Si se hace click en una figura...
            if (e.OriginalSource is Shape shape)
            {
                var selectedShape = Shapes.FirstOrDefault(s => s is not null && s.GetShape().Equals(shape));
                if (selectedShape is null)
                    return;

                selectedShape.GetShape().Fill = new SolidColorBrush(CGAColorPaletteService.GetColor(_currentColor));
            }
        }

        /// <summary>
        /// Evento click izquierdo de la herramienta SelectTool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectToolLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Si se hace click en el canvas quitar adorno y ocultar panel de propiedades de la figura anteriormente seleccionada
            if (e.OriginalSource == _drawingPanel?.CustomCanvas && _selectedShape is not null)
            {
                SetShowAdornersAttachedProperties(_selectedShape.GetShape(), false);
                _selectedShape = null;
                PropertiesPanelService.Instance.SetEnableShapeOptions(_selectedShape);
                PropertiesPanelService.Instance.ShowPropertiesPanel(_selectedShape);
                return;
            }

            // Mostrar adorno y panel de propiedades de la figura que se haga click...
            if (e.OriginalSource is Shape shape)
            {
                ShowSelectedShapeAdorners(shape);
                PropertiesPanelService.Instance.SetEnableShapeOptions(_selectedShape);
                PropertiesPanelService.Instance.ShowPropertiesPanel(_selectedShape);
            }
        }

        /// <summary>
        /// Evento click izquierdo de la herramienta EraserTool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EraserToolLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Sino se seleccionada una figura o el canvas es null
            if (e.OriginalSource is not Shape shape || _drawingPanel is null)
                return;

            // Buscar figura seleccionada
            _selectedShape = Shapes.Where(s => s is not null && s.GetShape().Equals(shape)).First();

            if (_selectedShape is null)
                return;

            // Si la figura a eliminar es la que esta actualmente seleccionada, se ocultan los adornos
            if (shape.Equals(_selectedShape))
                SetShowAdornersAttachedProperties(_selectedShape.GetShape(), false);

            // Elimina figura del canvas y de la lista de formas
            _drawingPanel.CustomCanvas.Children.Remove(_selectedShape.GetShape());
            Shapes.Remove(_selectedShape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PolygonToolLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_toolbox is null || _drawingPanel is null)
                return;

            // Si se esta dibujando un poligono...
            if (_currentShape is PolygonShape poly && poly.IsActivePolygon)
            {
                // Tomar nuevo punto y asignar al poligono actual
                _currentMousePosition = e.GetPosition(_drawingPanel.CustomCanvas);
                _currentShape?.SetCurrentMousePosition(_currentMousePosition);
                return;
            }

            // Si se va a dibujar un poligono...
            _currentShape = ShapeFactory.Create(_toolbox.CurrentTool, GenerateShapeName(_toolbox.CurrentTool), CGAColorPaletteService.GetColor(_currentColor));
            if (_currentShape is null)
                return;

            // Dibujar primer vertice del poligono
            _state = DrawingState.Drawing;
            _lastMousePosition = e.GetPosition(_drawingPanel.CustomCanvas);

            // Asignar punto y agregar al canvas
            _currentShape.SetLastMousePosition(_lastMousePosition);
            _drawingPanel.CustomCanvas.Children.Add(_currentShape.GetShape());
        }

        #endregion TOOL_LEFTMOUSEDOWN
    }
}