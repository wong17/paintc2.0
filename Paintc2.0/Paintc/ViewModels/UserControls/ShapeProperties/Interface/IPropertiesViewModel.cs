using Paintc.Core;

namespace Paintc.ViewModels.UserControls.ShapeProperties.Interface
{
    public interface IPropertiesViewModel 
    {
        void SetShape(ShapeBase shape);
        void UpdateProperties();
    }
}
