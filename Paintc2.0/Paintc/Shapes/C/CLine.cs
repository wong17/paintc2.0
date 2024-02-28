using Paintc.Core;

namespace Paintc.Shapes.C
{
    /*
     * line (int x1, int y1, int x2, int y2);
     * 
     * Dibuja una línea desde (x1, y1) hasta (x2, y2) usando el color fijado por setcolor
     * y el patrón y grosor de línea fijados por setlinestyle
     */

    public class CLine : SimpleShapeBase
    {
        public required int X1 { get; set; }
        public required int Y1 { get; set; }
        public required int X2 { get; set; }
        public required int Y2 { get; set; }
        public int LineStyle { get; set; }
        public int LineThickness { get; set; }
        public int Color { get; set; }
        public required string? Name { get; set; }
    }
}