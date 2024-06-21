using Paintc.AttachedProperties.Window;
using Paintc.Commands;
using Paintc.Core;
using Paintc.Service;
using Paintc.Service.Interface;
using Paintc.ViewModels.UserControls;
using Paintc.Views;
using System.Windows;
using System.Windows.Input;

namespace Paintc.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IWindowManager windowManager;
        private readonly AboutWindowViewModel aboutWindowViewModel;

        public ICommand SaveMenuItemClick { get; private set; }
        public ICommand ExitMenuItemClick { get; private set; }
        public ICommand AboutMenuItemClick { get; private set; }
        public ICommand TabSelectionChanged { get; private set; }
        public ICommand CheckedStatus { get; private set; }
        public ICommand UncheckedStatus { get; private set; }

        public ToolboxPanelViewModel ToolboxPanelViewModel { get; }
        public StatusBarPanelViewModel StatusBarPanelViewModel { get; }
        public SourceCodePanelViewModel SourceCodePanelViewModel { get; }

        public MainWindowViewModel(IWindowManager windowManager, ToolboxPanelViewModel toolboxPanelViewModel, 
            StatusBarPanelViewModel statusBarPanelViewModel, SourceCodePanelViewModel sourceCodePanelViewModel, AboutWindowViewModel aboutWindowViewModel)
        {
            SaveMenuItemClick = new RelayCommand(obj => true, SaveMenuItemClickCommand);
            ExitMenuItemClick = new RelayCommand(obj => true, ExitMenuItemClickCommand);
            AboutMenuItemClick = new RelayCommand(obj => true, AboutMenuItemClickCommand);
            TabSelectionChanged = new RelayCommand(obj => true, TabSelectionChangedCommand);
            CheckedStatus = new RelayCommand(obj => true, CheckedStatusCommand);
            UncheckedStatus = new RelayCommand(obj => true, UncheckedStatusCommand);
            
            this.windowManager = windowManager;
            this.aboutWindowViewModel = aboutWindowViewModel;

            ToolboxPanelViewModel = toolboxPanelViewModel;
            StatusBarPanelViewModel = statusBarPanelViewModel;
            SourceCodePanelViewModel = sourceCodePanelViewModel;
        }

        
        /// <summary>
        /// Muestra información acerca del programa
        /// </summary>
        /// <param name="obj"></param>
        private void AboutMenuItemClickCommand(object? obj)
        {
            windowManager.ShowWindow(aboutWindowViewModel, Application.Current.MainWindow);
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

            var result = MessageBox.Show("Do you want to exit without saving?", "Warning", 
                MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
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
        /// <param name="selectedIndex"></param>
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
        private void UncheckedStatusCommand(object? value)
        {
            if (Application.Current.MainWindow is not MainWindow mainWindow)
            {
                MessageBox.Show("MainWindow es null", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainWindowAttachedProperty.SetSaveWindowState(mainWindow, Convert.ToBoolean(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void CheckedStatusCommand(object? value)
        {
            if (Application.Current.MainWindow is not MainWindow mainWindow)
            {
                MessageBox.Show("MainWindow es null", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainWindowAttachedProperty.SetSaveWindowState(mainWindow, !Convert.ToBoolean(value));
        }

    }
}