using Paintc.Commands;
using Paintc.Core;
using Paintc.Service;
using Paintc.View;
using System.Windows;
using System.Windows.Input;

namespace Paintc.Controller
{
    public class MainWindowController : ControllerBase
    {
        public ICommand SaveMenuItemClick { get; private set; }
        public ICommand ExitMenuItemClick { get; private set; }
        public ICommand AboutMenuItemClick { get; private set; }
        public ICommand TabSelectionChanged {  get; private set; }

        public MainWindowController()
        {
            SaveMenuItemClick = new RelayCommand((obj) => true, SaveMenuItemClickCommand);
            ExitMenuItemClick = new RelayCommand((obj) => true, ExitMenuItemClickCommand);
            AboutMenuItemClick = new RelayCommand((obj) => true, AboutMenuItemClickCommand);
            TabSelectionChanged = new RelayCommand((obj) => true, TabSelectionChangedCommand);
        }

        /// <summary>
        /// Muestra información acerca del programa
        /// </summary>
        /// <param name="obj"></param>
        private void AboutMenuItemClickCommand(object? obj)
        {
            DialogService.OpenDialog<AboutWindow>(Application.Current.MainWindow, new AboutWindowController());
        }

        /// <summary>
        /// Cierra la aplicación
        /// </summary>
        /// <param name="obj"></param>
        private void ExitMenuItemClickCommand(object? obj)
        {
            if (DrawingHandler.Instance.Shapes.Count == 0)
            {
                Application.Current.Shutdown();
                return;
            }

            var result = MessageBox.Show("Do you want to exit without saving?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.No)
                return;

            Application.Current.Shutdown();
        }

        /// <summary>
        /// Guarda el contenido del canvas en formato .bmp
        /// </summary>
        /// <param name="obj"></param>
        private void SaveMenuItemClickCommand(object? obj)
        {
            CanvasImageSaverService.SaveCanvasContent();
        }

        /// <summary>
        /// Actualiza la lista de figuras que contiene el canvas para generar el nuevo código fuente
        /// y mostrarlo en SourceCodePanel
        /// </summary>
        /// <param name="obj"></param>
        private void TabSelectionChangedCommand(object? selectedIndex)
        {
            if (selectedIndex is null || selectedIndex is not int index)
                return;

            // Si se selecciona el tab "Source code"
            if (index == 1)
                SourceCodePanelService.Instance.SetPrimitiveShapesCollection(DrawingHandler.Instance.GetSimpleShapes());
        }
    }
}