using Paintc.Core;
using Paintc.Service.Interface;
using System.Windows;

namespace Paintc.Service.Implement
{
    public class WindowManager(WindowMapper windowMapper) : IWindowManager
    {
        // Servicio para obtener una ventana por medio de su viewmodel
        private readonly WindowMapper _windowMapper = windowMapper;

        // Se ejecuta al cerrar la ventana
        public void CloseWindow()
        {
        }

        // Se encarga de abrir una nueva ventana, obteniendola por medio de su viewmodel
        public void ShowWindow(ViewModel viewModel, Window? owner = null)
        {
            var windowType = _windowMapper.GetWindowTypeForViewModel(viewModel.GetType());
            if (windowType is null)
            {
                MessageBox.Show("No se encontro la ventana asociada al viewmodel", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Activator.CreateInstance(windowType) is not Window window)
            {
                MessageBox.Show("No se pudo instanciar la ventana", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            window.Owner = owner;
            window.DataContext = viewModel;
            window.Show();
            window.Closed += (sender, args) => CloseWindow();
        }
    }
}
