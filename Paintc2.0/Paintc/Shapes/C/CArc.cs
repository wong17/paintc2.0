using Paintc.Core;

namespace Paintc.Shapes.C
{
    /* 
     * arc (int x, int y, int ang1, int ang2, int rad);
     * 
     * Dibuja un arco con centro (x,y), ángulo inicial (en grados) ang1, ángulo terminal (en grados)
     * ang2 y radio rad, usando el color actual y el grosor de línea fijado por setlinestyle. 
     */

    public class CArc : SimpleShapeBase
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Angle1 { get; set; }
        public int Angle2 { get; set; }
        public int Radius { get; set; }
    }
}
