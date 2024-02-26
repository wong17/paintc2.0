namespace Paintc.Shapes.CStructs
{
    /*
     * line (int x1, int y1, int x2, int y2);
     * 
     * Dibuja una línea desde (x1, y1) hasta (x2, y2) usando el color fijado por setcolor
     * y el patrón y grosor de línea fijados por setlinestyle
     */

    public struct Line
    {
        public int x1, y1, x2, y2;
    }
}