using Paintc.Commands;
using Paintc.Core;
using Paintc.Enums;
using Paintc.Model;
using Paintc.Service;
using Paintc.Service.Collections;
using Paintc.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Paintc.Controller.UserControls
{
    public class ToolboxPanelController : ControllerBase
    {
        public ICommand? ToolsButtonsClick { get; private set; }
        public ICommand? CGAButtonsClick { get; private set; }
        public ICommand? RemoveAllShapesClick { get; private set; }
        public ICommand? RemoveShapeClick { get; private set; }
        public ICommand? ListViewSelectedItem { get; private set; }
        public ICommand? ViewSourceCodeClick { get; private set; }

        /// <summary>
        /// Lista de herramientas disponibles para utilizar
        /// </summary>
        public ObservableCollection<Tool> ToolItems { get; private set; } = [];

        /// <summary>
        /// Lista de colores disponibles para utilizar
        /// </summary>
        public ObservableCollection<CGAColor> ColorPaletteItems { get; private set; }

        /// <summary>
        /// Lista de formas que se muestran en el explorador de formas
        /// </summary>
        public ObservableCollection<ShapeBase?> ShapesList { get; private set; } = DrawingHandler.Instance.Shapes;

        public ToolboxPanelController()
        {
            ToolItems = new ObservableCollection<Tool>(ToolCollectionService.GetTools());
            ColorPaletteItems = new ObservableCollection<CGAColor>(CGAColorPaletteService.GetColorPalette());

            ToolsButtonsClick = new RelayCommand((obj) => true, ToolsButtonsClickCommand);
            CGAButtonsClick = new RelayCommand((obj) => true, CGAButtonsClickCommand);
            RemoveAllShapesClick = new RelayCommand((obj) => true, RemoveAllShapesClickCommand);
            RemoveShapeClick = new RelayCommand((obj) => true, RemoveShapeClickCommand);
            ListViewSelectedItem = new RelayCommand((obj) => true, ListViewSelectedItemCommand);
            ViewSourceCodeClick = new RelayCommand((obj) => true, ViewSourceCodeCommand);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        private void ListViewSelectedItemCommand(object? shapeName)
        {
            if (shapeName is null)
                return;

            var shape = DrawingHandler.Instance.Shapes
                .Where(s => s is not null)
                .First(s => s?.Name is not null && s.Equals(shapeName));

            if (shape is null)
                return;

            DrawingHandler.Instance.ShowSelectedShapeAdorners(shape.GetShape());
            ToolboxPanelService.Instance.UpdateCurrentTool(ToolType.SelectTool);
            StatusBarPanelService.Instance.UpdateCurrentTool(ToolType.SelectTool);
        }

        /// <summary>
        /// Elimina la forma seleccionada del canvas y explorador de formas
        /// </summary>
        /// <param name="shapeName"></param>
        private void RemoveShapeClickCommand(object? shapeName)
        {
            if (shapeName is null)
                return;

            var result = MessageBox.Show($"Are you sure you want to remove {shapeName} from the drawing area?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
                return;
            // Elimina figura/forma del canvas
            DrawingHandler.Instance.RemoveShape(shapeName.ToString());
        }

        /// <summary>
        /// Notifica cada vez que se selecciona una herramienta diferente
        /// </summary>
        /// <param name="sender"></param>
        private void ToolsButtonsClickCommand(object? sender)
        {
            if (sender is null)
                return;

            var toolType = (ToolType)sender;
            ToolboxPanelService.Instance.UpdateCurrentTool(toolType);
            StatusBarPanelService.Instance.UpdateCurrentTool(toolType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter"></param>
        private void CGAButtonsClickCommand(object? parameter)
        {
            if (parameter is null)
                return;

            var color = (CGAColorPalette)parameter;
            ToolboxPanelService.Instance.UpdateSelectedColor(color);
            StatusBarPanelService.Instance.UpdateCurrentColor(color);
        }

        /// <summary>
        /// Elimina todas las formas del canvas
        /// </summary>
        /// <param name="parameter"></param>
        private void RemoveAllShapesClickCommand(object? parameter)
        {
            if (DrawingHandler.Instance.Shapes.Count == 0) return;

            var result = MessageBox.Show("Are you sure you want to remove all from the drawing area? Any unsaved progress will be lost.", "Warning",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                DrawingHandler.Instance.ClearDrawingPanel();
            }
        }

        /// <summary>
        /// Muestra el código c para dibujar la figura utilizando graphics.h
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ViewSourceCodeCommand(object? parameter)
        {
            DialogService.OpenDialog<ViewSourceCodeWindow>(Application.Current.MainWindow, null);
        }
    }
}