namespace Paintc.Shapes.CStructs
{
    /* 
     * ellipse (int x, int y, int comienzo_angulo, int final_angulo, int x_radio, int y_radio);
     * 
     * Esta función es usada para dibujar un arco elíptico en el color actual. El arco elíptico está
     * centrado en el punto especificado por los argumentos x e y. Ya que el arco es elíptico el
     * argumento x_radio especifica el radio horizontal y el argumento y_radio especifica el radio
     * vertical. El arco elíptico comienza con el ángulo especificado por el argumento
     * comienzo_angulo y se extiende en un sentido contrario a las agujas del reloj al ángulo
     * especificado por el argumento final_angulo. La función ellipse considera este - el eje
     * horizontal a la derecha del centro del elipse - ser 0 grados. El arco elíptico es dibujado con
     * el grosor de línea actual como es establecido por la función setlinestyle. Sin embargo, el
     * estilo de línea es ignorado por la función ellipse.  
     */

    public struct Ellipse
    {
        public int x, y, startAngle, endAngle, x_radius, y_radius;
    }
}
