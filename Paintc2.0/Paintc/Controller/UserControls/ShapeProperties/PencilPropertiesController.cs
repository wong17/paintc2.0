using Paintc.Core;
using Paintc.Shapes;

namespace Paintc.Controller.UserControls.ShapeProperties
{
    public class PencilPropertiesController : ControllerBase
    {
        private FreeShape? _freeShape;
        public FreeShape? FreeShape 
        { 
            get => _freeShape; 
            set
            {
                SetField(ref _freeShape, value);
            }
        }
    }
}
