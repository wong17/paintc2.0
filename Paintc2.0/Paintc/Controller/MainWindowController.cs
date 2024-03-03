using Paintc.Commands;
using Paintc.Service;
using Paintc.View;
using System.Windows;
using System.Windows.Input;

namespace Paintc.Controller
{
    public class MainWindowController : DependencyObject
    {
        #region COMMANDS

        public ICommand SaveMenuItemClick { get; private set; }
        public ICommand ExitMenuItemClick { get; private set; }
        public ICommand AboutMenuItemClick { get; private set; }
        public ICommand TabSelectionChanged {  get; private set; }
        public ICommand CheckedStatus {  get; private set; }
        public ICommand UncheckedStatus { get; private set; }

        #endregion

        #region ATTACHED_PROPERTIES

        public static bool GetSaveWindowState(UIElement element) => (bool)element.GetValue(SaveWindowStateProperty);

        public static void SetSaveWindowState(UIElement element, bool value) => element.SetValue(SaveWindowStateProperty, value);

        // Using a DependencyProperty as the backing store for SaveWindowState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SaveWindowStateProperty =
            DependencyProperty.RegisterAttached("SaveWindowState", typeof(bool), typeof(MainWindowController), new PropertyMetadata(false));

        #endregion

        public MainWindowController()
        {
            SaveMenuItemClick = new RelayCommand(obj => true, SaveMenuItemClickCommand);
            ExitMenuItemClick = new RelayCommand(obj => true, ExitMenuItemClickCommand);
            AboutMenuItemClick = new RelayCommand(obj => true, AboutMenuItemClickCommand);
            TabSelectionChanged = new RelayCommand(obj => true, TabSelectionChangedCommand);
            CheckedStatus = new RelayCommand(obj => true, CheckedStatusCommand);
            UncheckedStatus = new RelayCommand(obj => true, UncheckedStatusCommand);
        }

        #region COMMANDS

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        // https://stackoverflow.com/questions/5566050/executing-a-command-on-checkbox-checked-or-unchecked
        private void UncheckedStatusCommand(object? value) => SetSaveWindowState(Application.Current.MainWindow, Convert.ToBoolean(value));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void CheckedStatusCommand(object? value) => SetSaveWindowState(Application.Current.MainWindow, !Convert.ToBoolean(value));

        #endregion
    }
}