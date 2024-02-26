namespace Paintc.Shapes.CStructs
{
    /* 
     * rectangle (int x1, int y1, int x2, int y2);
     * 
     * Dibuja un rectangle con izquierda superior (x1, y1) y derecha inferior en (x2, y2)
     * usando el color fijado por setcolor y el patrón y grosor de línea fijados por
     * setlinestyle.  
     */

    public struct Rectangle
    {
        public int x1, y1, x2, y2;
    }
}
