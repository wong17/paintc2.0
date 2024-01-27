using Paintc.Command;
using Paintc.Model;
using Paintc.View;
using Paintc.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Paintc.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public List<GraphicMode> GraphicsModes { get; }

        public ICommand ExitCommand { get; }
        public ICommand AboutCommand { get; }

        public MainWindowViewModel()
        {
            GraphicsModes = GraphicModeViewModel.GetGraphicModes();
            ExitCommand = new RelayCommand(ExitCommandExecute);
            AboutCommand = new RelayCommand(AboutCommandExecute);
        }

        private void AboutCommandExecute(object? obj) => new AboutWindow() { Owner = Application.Current.MainWindow }.ShowDialog();

        private void ExitCommandExecute(object? obj) => Application.Current.Shutdown();
    }
}
