using Paintc.Enums;
using System.Windows.Media;

namespace Paintc.Model
{
    public class FillPattern(FillPatternStyle fillPatternStyle, DrawingBrush? drawing)
    {
        public FillPatternStyle FillPatternStyle { get; set; } = fillPatternStyle;
        public DrawingBrush? Drawing { get; } = drawing;
    }
}
