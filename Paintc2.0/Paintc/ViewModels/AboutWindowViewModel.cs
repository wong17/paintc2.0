using Paintc.Commands;
using Paintc.Core;
using System.Diagnostics;
using System.Windows;

namespace Paintc.ViewModels
{
    public class AboutWindowViewModel : ViewModel
    {
        public RelayCommand OpenGithubRepositoryCommand { get; private set; }

        public AboutWindowViewModel()
        {
            OpenGithubRepositoryCommand = new RelayCommand(obj => true, OpenGithubRepository);
        }

        private void OpenGithubRepository(object? uri)
        {
            if (uri is null)
                return;

            try
            {
                Process.Start(new ProcessStartInfo((string)uri) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}