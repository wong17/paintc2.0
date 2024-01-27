using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Paintc.Controller
{
    /* Se utiliza para notificar a las vistas cuando una propiedad ha cambiado. */
    public class ControllerBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
