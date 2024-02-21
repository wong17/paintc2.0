using Paintc.Controller.UserControls.ShapeProperties;
using Paintc.Core;
using Paintc.Model;
using Paintc.Service;
using Paintc.Service.Collections;
using Paintc.View.UserControls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paintc.Controller.UserControls
{
    public class PropertiesPanelController : ControllerBase
    {
        private readonly PropertiesPanel _drawingPanelProperties;

        private readonly List<GraphicMode> _graphicModes;
        private GraphicMode? _currentGraphiceMode;

        private readonly ObservableCollection<CGAColor> _colors;
        private CGAColor _currentColor;

        private bool _shouldInvokeCheckedEvents = false;

        private readonly PropertiesPanelNavigator _panelsNavigator;

        /// <summary>
        ///
        /// </summary>
        /// <param name="drawingPanelProperties"></param>
        public PropertiesPanelController(PropertiesPanel drawingPanelProperties)
        {
            _drawingPanelProperties = drawingPanelProperties;
            // Events
            _drawingPanelProperties.GraphicsModeCmbbox.SelectionChanged += GraphicsModeCmbbox_SelectionChanged;
            _drawingPanelProperties.BackgroundColorCmbbox.SelectionChanged += BackgroundColorCmbbox_SelectionChanged;
            PropertiesPanelService.Instance.SetEnableShapeOptionsEventHandler += SetEnableShapeOptions;
            _drawingPanelProperties.IsDraggableCheckbox.Checked += IsDraggableCheckbox_Checked;
            _drawingPanelProperties.IsResizableCheckbox.Checked += IsResizableCheckbox_Checked;
            _drawingPanelProperties.IsDraggableCheckbox.Unchecked += IsDraggableCheckbox_Unchecked;
            _drawingPanelProperties.IsResizableCheckbox.Unchecked += IsResizableCheckbox_Unchecked;

            // Canvas background colors, default: White
            _colors = CGAColorPaletteService.GetColorPalette();
            _currentColor = _colors.First();
            _drawingPanelProperties.BackgroundColorCmbbox.ItemsSource = _colors;
            _drawingPanelProperties.BackgroundColorCmbbox.DisplayMemberPath = "Cpalette";
            _drawingPanelProperties.BackgroundColorCmbbox.SelectedItem = _currentColor;

            // Canvas sizes, graphics mode
            _graphicModes = GraphicModeCollectionService.GetGraphicModes();
            _currentGraphiceMode = _graphicModes[0];
            _drawingPanelProperties.GraphicsModeCmbbox.ItemsSource = _graphicModes;
            _drawingPanelProperties.GraphicsModeCmbbox.DisplayMemberPath = "DisplayName";
            _drawingPanelProperties.GraphicsModeCmbbox.SelectedItem = _currentGraphiceMode;

            PropertiesPanelService.Instance.UpdateGraphicModeSelectionEventHandler += UpdateGraphicModeSelection;
            PropertiesPanelService.Instance.ChangeBackgroundColor(_currentColor);
            _drawingPanelProperties.BackgroundColorRectangle.Fill = new SolidColorBrush(_currentColor.Color);

            _panelsNavigator = new();
            PropertiesPanelService.Instance.ShowPropertiesPanelEventHandler += ShowPropertiesPanel;
        }

        /// <summary>
        /// Se invoca cuando se selecciona una figura en el canvas para habiltar opciones en el panel de propiedades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="shapeBase"></param>
        private void SetEnableShapeOptions(object? sender, ShapeBase? shapeBase)
        {
            if (shapeBase is null)
            {
                /* Para evitar invocar el evento "checked" de ambos checkboxs y que estos cambien el valor de la propiedad IsDraggable e IsResizable de la figura sin querer. */
                _shouldInvokeCheckedEvents = false;

                _drawingPanelProperties.IsDraggableCheckbox.IsEnabled = _drawingPanelProperties.IsResizableCheckbox.IsEnabled = false;
                _drawingPanelProperties.IsDraggableCheckbox.IsChecked = _drawingPanelProperties.IsResizableCheckbox.IsChecked = false;

                return;
            }
            _shouldInvokeCheckedEvents = true;

            _drawingPanelProperties.IsDraggableCheckbox.IsEnabled = _drawingPanelProperties.IsResizableCheckbox.IsEnabled = true;
            _drawingPanelProperties.IsDraggableCheckbox.IsChecked = shapeBase.IsDraggableProperty;
            _drawingPanelProperties.IsResizableCheckbox.IsChecked = shapeBase.IsResizableProperty;
        }

        /// <summary>
        /// Actualiza el item seleccionado en el combobox que contiene los modos gráficos cuando el usuario decide no cambiar la resolución actual
        /// sin disparar el evento SelectionChanged del combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="flag"></param>
        private void UpdateGraphicModeSelection(object? sender, bool flag)
        {
            if (flag)
                _currentGraphiceMode = _drawingPanelProperties.GraphicsModeCmbbox.SelectedItem as GraphicMode;
            else
            {
                /* para resetear la selección en el combobox cuando el usuario no quizo cambiar el tamaño sin ejecutar el evento SelectionChanged. */
                _drawingPanelProperties.GraphicsModeCmbbox.SelectionChanged -= GraphicsModeCmbbox_SelectionChanged;
                _drawingPanelProperties.GraphicsModeCmbbox.SelectedItem = _currentGraphiceMode;
                _drawingPanelProperties.GraphicsModeCmbbox.SelectionChanged += GraphicsModeCmbbox_SelectionChanged;
            }
        }

        /// <summary>
        /// Notifica que se debe de cambiar el tamaño del canvas cuando se selecciona una resolución diferente desde el panel de propiedades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphicsModeCmbbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_drawingPanelProperties.GraphicsModeCmbbox.SelectedItem is not GraphicMode selectedGraphicMode || selectedGraphicMode.DisplayName is null)
                return;

            // Notificar solo cuando se seleccione una resolución distinta
            if (!selectedGraphicMode.DisplayName.Equals(_currentGraphiceMode))
                PropertiesPanelService.Instance.UpdateGraphicMode(selectedGraphicMode);
        }

        /// <summary>
        /// Notifica que se debe de cambiar el color del canvas cuando se selecciona un color desde el panel de propiedades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundColorCmbbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_drawingPanelProperties.BackgroundColorCmbbox.SelectedItem is not CGAColor color)
                return;

            if (color.Cpalette != _currentColor.Cpalette)
            {
                // Actualizar nuevo color...
                _currentColor = color;
                // Cambiar color del fondo
                PropertiesPanelService.Instance.ChangeBackgroundColor(_currentColor);
                _drawingPanelProperties.BackgroundColorRectangle.Fill = new SolidColorBrush(_currentColor.Color);
            }
        }

        /// <summary>
        /// Actualiza la Dependency Property "IsResizableProperty" de la figura cuando se marca IsResizableCheckbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsResizableCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (_shouldInvokeCheckedEvents)
                DrawingHandler.Instance.SetIsResizableSelectedShape(_drawingPanelProperties.IsResizableCheckbox.IsChecked ?? false);
        }

        /// <summary>
        /// Actualiza la Dependency Property "IsResizableProperty" de la figura cuando se desmarca IsResizableCheckbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsResizableCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            DrawingHandler.Instance.SetIsResizableSelectedShape(_drawingPanelProperties.IsResizableCheckbox.IsChecked ?? false);
        }

        /// <summary>
        /// Actualiza la Dependency Property "IsDraggableProperty" de la figura cuando se marca IsDraggableCheckbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsDraggableCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (_shouldInvokeCheckedEvents)
                DrawingHandler.Instance.SetIsDraggableSelectedShape(_drawingPanelProperties.IsDraggableCheckbox.IsChecked ?? false);
        }

        /// <summary>
        /// Actualiza la Dependency Property "IsDraggableProperty" de la figura cuando se desmarca IsDraggableCheckbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsDraggableCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            DrawingHandler.Instance.SetIsDraggableSelectedShape(_drawingPanelProperties.IsDraggableCheckbox.IsChecked ?? false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPropertiesPanel(object? sender, ShapeBase? shapeBase)
        {
            _drawingPanelProperties.ShapePropertiesContent.Content = _panelsNavigator.GetPropertiesPanel(shapeBase);
        }
    }
}