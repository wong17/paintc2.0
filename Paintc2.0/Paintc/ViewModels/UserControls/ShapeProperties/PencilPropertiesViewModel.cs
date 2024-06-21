using Paintc.ViewModels.UserControls.ShapeProperties.Interface;
using Paintc.Core;
using Paintc.Shapes;

namespace Paintc.ViewModels.UserControls.ShapeProperties
{
    public class PencilPropertiesViewModel : ObservableObject, IPropertiesViewModel
    {
        private PencilShape? _freeShape;
        public PencilShape? FreeShape 
        { 
            get => _freeShape; 
            set
            {
                SetField(ref _freeShape, value);
                UpdateProperties();
            }
        }

        public void SetShape(ShapeBase shape)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateProperties()
        {
            
        }
    }
}
