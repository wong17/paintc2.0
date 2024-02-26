namespace Paintc.Shapes.CStructs
{
    /* 
     * drawpoly (int puntos, int *vertice);
     * 
     * Dibuja una polilínea (una figura de segmentos de líneas unidas) de npuntos
     * vértices usando el color actual fijado por setcolor y el patrón y grosor de línea
     * fijados por setlinestyle. 
     */

    public struct Poly
    {
        public int nPoints;
        public int[] points;
    }
}
