using Paintc.Core;

namespace Paintc.Shapes.C
{
    /* 
     * rectangle (int x1, int y1, int x2, int y2);
     * 
     * Dibuja un rectangle con izquierda superior (x1, y1) y derecha inferior en (x2, y2)
     * usando el color fijado por setcolor y el patrón y grosor de línea fijados por
     * setlinestyle.  
     */

    public sealed class CRectangle : SimpleShapeBase
    {
        public required int X1 { get; set; }
        public required int Y1 { get; set; }
        public required int X2 { get; set; } 
        public required int Y2 { get; set; }
        public int Color {  get; set; }
        public int BorderColor { get; set; }
        public int FillPattern { get; set; }
        public int LineStyle { get; set; }
        public int Thickness { get; set; }
        public string? Name { get; set; }
    }
}
