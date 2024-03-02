using Paintc.Controller.UserControls.ShapeProperties.Interface;
using Paintc.Core;
using Paintc.Shapes;

namespace Paintc.Controller.UserControls.ShapeProperties
{
    public class PencilPropertiesController : ObservableObject, IPropertiesController
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
