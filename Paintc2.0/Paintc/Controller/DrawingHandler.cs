using Paintc.Core;
using Paintc.Enums;
using Paintc.Factory;
using Paintc.Model;
using Paintc.Service;
using Paintc.Shapes;
using Paintc.View.UserControls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

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
            }
        }
        private readonly ObservableCollection<ShapeBase?> _shapes = [];
        public int NumberOfShapes
        {
            get => _shapes.Count;
        }
        private ShapeBase? _currentShape;
        private Toolbox? _toolbox;
        private DrawingState _state = DrawingState.Finished;
        
        private Point _lastMousePosition = new();
        private Point _currentMousePosition = new();

        #endregion

        public void ClearDrawingPanel()
        {
            if (_drawingPanel is null) return;

            var canvasChildrens = _drawingPanel.CustomCanvas.Children;
            foreach (var shape in _shapes)
            {
                if (canvasChildrens.Contains(shape?.GetShape()))
                    canvasChildrens.Remove(shape?.GetShape());
            }
            _shapes.Clear();
        }

        #region TOOLSERVICE_EVENT

        private void ToolboxEventHandler(object? sender, Toolbox toolbox)
        {
            _toolbox = toolbox;
            _state = DrawingState.Finished;
            _currentShape = null;
        }

        #endregion

        #region DRAWINGPANEL_EVENTS

        public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_toolbox is null || _drawingPanel is null || _state == DrawingState.Drawing) return;

            // Si se utiliza la herramienta de filler
            if (_toolbox.CurrentTool == ToolType.FillerTool)
            {
                // cambiar fondo del canvas...

                _state = DrawingState.Finished;
                return;
            }

            _currentShape = ShapeFactory.Create(_toolbox.CurrentTool);
            if (_currentShape is null)
                return;

            _state = DrawingState.Drawing;
            _lastMousePosition = e.GetPosition(_drawingPanel.CustomCanvas);
       
            // Para las demás herramientas
            _currentShape.SetLastMousePosition(_lastMousePosition);
            _drawingPanel.CustomCanvas.Children.Add(_currentShape.GetShape());
        }

        public void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_toolbox is null) return;

            // Si se pulsa solo Click derecho no cierra el poligono
            if (_toolbox?.CurrentTool == ToolType.PolygonTool)
            {
                if (_currentShape is PolygonShape polygone)
                {
                    polygone.LeaveOpenPolygone();
                    _shapes.Add(_currentShape);
                }
            }
            // Si se pulsa Ctrl + Click derecho cierra el poligono si es posible
            if (_toolbox?.CurrentTool == ToolType.PolygonTool && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (_currentShape is PolygonShape polygone)
                {
                    polygone.ClosePolygone();
                    _shapes.Add(_currentShape);
                }
            }
            
        }

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
                _shapes.Add(_currentShape);
            }
        }

        #endregion
    }
}
