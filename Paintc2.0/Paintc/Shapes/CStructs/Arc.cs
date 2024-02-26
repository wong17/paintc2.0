namespace Paintc.Shapes.CStructs
{
    /* 
     * arc (int x, int y, int ang1, int ang2, int rad);
     * 
     * Dibuja un arco con centro (x,y), ángulo inicial (en grados) ang1, ángulo terminal (en grados)
     * ang2 y radio rad, usando el color actual y el grosor de línea fijado por setlinestyle. 
     */

    public struct Arc
    {
        public int x, y, angle1, angle2, radius;
    }
}
