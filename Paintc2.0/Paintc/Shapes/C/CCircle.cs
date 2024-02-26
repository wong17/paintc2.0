using Paintc.Core;

namespace Paintc.Shapes.C
{
    /*
     * circle (int x, int y, int rad);
     * 
     * Esta función se utiliza para dibujar un círculo. Los argumentos x e y definen el centro del
     * círculo, mientras que el argumento radio define el radio del círculo. El círculo no es
     * rellenado pero es dibujado usando el color actual.  
     */

    public class CCircle : SimpleShapeBase
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
        public int BorderColor { get; set; }
        public int Pattern { get; set; }
    }
}
