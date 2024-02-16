using Paintc.Enums;
using System.Windows.Media;

namespace Paintc.Model
{
    public class Tool(ToolType type, string? tooltip, DrawingImage? imageSource)
    {
        public ToolType Type { get; set; } = type;
        public string? Tooltip { get; set; } = tooltip;
        public DrawingImage? ImageSource { get; set; } = imageSource;
    }
}