using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintc.Core
{
    public abstract class ShapeBase : ObservableObject
    {
        public string? Name { get; set; }
        protected readonly Color Color;

        protected ShapeBase(string? name, Color color)
        {
            Name = name;
            Color = color;
        }

        protected Point LastMousePosition { get; set; }
        protected Point CurrentMousePosition { get; set; }
        public abstract void SetLastMousePosition(Point lastPosition);
        public abstract void SetCurrentMousePosition(Point currentPosition);
        public abstract Shape GetShape();
    }
}
