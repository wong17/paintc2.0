using Paintc.Enums;
using System.Windows.Media;

namespace Paintc.Model
{
    public class CGAColor(CGAColorPalette cpalette, Color color)
    {
        public CGAColorPalette Cpalette { get; set; } = cpalette;
        public Color Color { get; set; } = color;
    }
}
