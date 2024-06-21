using Paintc.Core;
using System.Windows;

namespace Paintc.Service.Interface
{
    public interface IWindowManager
    {
        void ShowWindow(ViewModel viewModel, Window? owner = null);

        void CloseWindow();
    }
}
