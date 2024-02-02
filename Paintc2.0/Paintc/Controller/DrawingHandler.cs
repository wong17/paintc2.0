using Paintc.Core;
using Paintc.Enums;
using Paintc.Model;
using Paintc.Service;
using Paintc.View.UserControls;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Paintc.Controller
{
    public class DrawingHandler
    {
        private readonly DrawingPanel _drawingPanel;
        private readonly List<ShapeBase?> _shapes = [];
        private ShapeBase? _currentShape;
        private Toolbox? _toolbox;
        private DrawingState _state = DrawingState.Finished;
        
        private Point _lastMousePosition = new();
        private Point _currentMousePosition = new();

        public DrawingHandler(DrawingPanel drawingPanel)
        {
            _drawingPanel = drawingPanel;
            ToolService.Instance.ToolboxEventHandler += ToolboxEventHandler;
            ToolService.Instance.UpdateCurrentTool(ToolType.SelectTool);
        }

        private void ToolboxEventHandler(object? sender, Toolbox toolbox)
        {
            _toolbox = toolbox;
            _state = DrawingState.Finished;
            _currentShape = null;
        }

        public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_toolbox is null || _state == DrawingState.Drawing) return;

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

    }
}
