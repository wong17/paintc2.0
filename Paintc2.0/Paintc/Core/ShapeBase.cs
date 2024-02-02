using System.Windows;
using System.Windows.Shapes;

namespace Paintc.Core
{
    public abstract class ShapeBase
    {
        protected Point LastMousePosition { get; set; }
        protected Point CurrentMousePosition { get; set; }
        public abstract void SetLastMousePosition(Point lastPosition);
        public abstract void SetCurrentMousePosition(Point currentPosition);
        public abstract Shape GetShape();
    }
}
