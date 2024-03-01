using Paintc.Core;
using System.Collections.ObjectModel;

namespace Paintc.Service
{
    public class SourceCodePanelService
    {
        private static readonly SourceCodePanelService _instance = new();
        public static SourceCodePanelService Instance => _instance;

        private SourceCodePanelService()
        { }

        // Se dispara cuando se cambia al tab "Source code" desde MainWindow para mostrar el código fuente de todo el contenido del canvas
        public event EventHandler<ObservableCollection<SimpleShapeBase>?>? SetPrimitiveShapesCollectionEventHandler;

        public void SetPrimitiveShapesCollection(ObservableCollection<SimpleShapeBase>? shapesCollection) 
            => SetPrimitiveShapesCollectionEventHandler?.Invoke(this, shapesCollection);
    }
}
