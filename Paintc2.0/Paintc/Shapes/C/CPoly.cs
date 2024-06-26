﻿using Paintc.Core;

namespace Paintc.Shapes.C
{
    /* 
     * drawpoly (int puntos, int *vertice);
     * 
     * Dibuja una polilínea (una figura de segmentos de líneas unidas) de npuntos
     * vértices usando el color actual fijado por setcolor y el patrón y grosor de línea
     * fijados por setlinestyle. 
     */

    public sealed class CPoly : SimpleShapeBase
    {
        public int Fill { get; set; }
        public int Stroke { get; set; }
        public int FillPattern { get; set; }
        public required List<CVertex> Vertices { get; set; } = [];
    }
}
