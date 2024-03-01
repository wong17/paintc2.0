using Paintc.Core;

namespace Paintc.Shapes.C
{
    public class CPencil : SimpleShapeBase
    {
        public required List<CPixel> Pixels { get; set; } = [];
    }
}
