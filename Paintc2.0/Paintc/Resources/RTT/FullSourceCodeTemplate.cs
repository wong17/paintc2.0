﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Paintc.Resources.RTT
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Shapes.C;
    using Core;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class FullSourceCodeTemplate : BaseTemplate
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("/* Code generated by Paintc */\r\n#include <graphics.h>\r\n#include <conio.h>\r\n\r\n");
            
            #line 13 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

foreach (var shape in shapes)
{

            
            #line default
            #line hidden
            this.Write("/* Dibuja ");
            
            #line 17 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(shape.Name));
            
            #line default
            #line hidden
            this.Write(" */\r\ndraw");
            
            #line 18 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(shape.Name));
            
            #line default
            #line hidden
            this.Write("();\r\n");
            
            #line 19 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

}

            
            #line default
            #line hidden
            this.Write(@"
int main()
{
    int GraphDriver, GraphMode, ErrorCode;
    GraphDriver = DETECT; /* Request auto-detection	*/
    initgraph(&GraphDriver, &GraphMode, """");
    ErrorCode = graphresult(); /* Read result of initialization*/
    if (ErrorCode != grOk) /* Error occured during init	*/
    { 
        printf("" Graphics System Error: %s\n"", grapherrormsg(ErrorCode));
        exit(1);
    }

    /* Establecer color de fondo */
    setbkcolor(");
            
            #line 36 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(settings.BackgroundColor));
            
            #line default
            #line hidden
            this.Write(");\r\n\r\n    /* Your code here... */\r\n    ");
            
            #line 39 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

    foreach (var shape in shapes)
    {
    
            
            #line default
            #line hidden
            this.Write("    draw");
            
            #line 43 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(shape.Name));
            
            #line default
            #line hidden
            this.Write("();\r\n    ");
            
            #line 44 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

    }
    
            
            #line default
            #line hidden
            this.Write("\r\n    getch();\r\n    closegraph(); /* Return the system to text mode\t*/\r\n    retur" +
                    "n 0;\r\n}\r\n\r\n");
            
            #line 53 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

foreach (var shape in shapes)
{

            
            #line default
            #line hidden
            this.Write("draw");
            
            #line 57 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(shape.Name));
            
            #line default
            #line hidden
            this.Write("()\r\n{\r\n    ");
            
            #line 59 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

    if (shape is CRectangle rectangle)
    {
    
            
            #line default
            #line hidden
            this.Write("        /* Coordenadas del rectángulo */\r\n        int x1 = ");
            
            #line 64 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.X1));
            
            #line default
            #line hidden
            this.Write(", y1 = ");
            
            #line 64 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.Y1));
            
            #line default
            #line hidden
            this.Write(", x2 = ");
            
            #line 64 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.X2));
            
            #line default
            #line hidden
            this.Write(", y2 = ");
            
            #line 64 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.Y2));
            
            #line default
            #line hidden
            this.Write(";\r\n        /* Coordenadas de la barra interna con relleno */\r\n        int x1_bar " +
                    "= x1 + 1, y1_bar = y1 + 1, x2_bar = x2 - 1, y2_bar = y2 - 1;\r\n\r\n        /* Estil" +
                    "os, colores... */\r\n        int color = ");
            
            #line 69 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.Color));
            
            #line default
            #line hidden
            this.Write(";\r\n        int borderColor = ");
            
            #line 70 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.BorderColor));
            
            #line default
            #line hidden
            this.Write(";\r\n        int borderLineStyle = ");
            
            #line 71 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.BorderLineStyle));
            
            #line default
            #line hidden
            this.Write(";\r\n        int fillPattern = ");
            
            #line 72 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.FillPattern));
            
            #line default
            #line hidden
            this.Write(";\r\n        int borderLineThickness = ");
            
            #line 73 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.BorderLineThickness));
            
            #line default
            #line hidden
            this.Write(@";

        /* Establecer el color del borde */
        setcolor(borderColor);
        /* Establecer el grosor y estilo de la linea */
        setlinestyle(borderLineStyle, 0, borderLineThickness);
        /* Dibujar rectangle con el color de borde 'borderColor' y estilo de linea 'lineStyle' */
        rectangle(x1, y1, x2, y2);
        /* Establecer color de relleno */
        setfillstyle(fillPattern, color);
        /* Dibujar bar con color de relleno 'color' */
        bar(x1_bar, y1_bar, x2_bar, y2_bar);
    ");
            
            #line 85 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

    }
    else if (shape is CEllipse ellipse)
    {
    
            
            #line default
            #line hidden
            this.Write("        /* Coordenadas del centro de la ellipse */\r\n\t    int centerX = ");
            
            #line 91 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.X));
            
            #line default
            #line hidden
            this.Write(", centerY = ");
            
            #line 91 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.Y));
            
            #line default
            #line hidden
            this.Write(";\r\n\t    /* Angulo inicio y fin */\r\n\t    int startAngle = ");
            
            #line 93 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.StartAngle));
            
            #line default
            #line hidden
            this.Write(", endAngle = ");
            
            #line 93 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.EndAngle));
            
            #line default
            #line hidden
            this.Write(";\r\n\t    /* Radios */\r\n\t    int xRadius = ");
            
            #line 95 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.XRadius));
            
            #line default
            #line hidden
            this.Write(", yRadius = ");
            
            #line 95 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.YRadius));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\n\t    /* Estilos, colores... */\r\n\t    int color = ");
            
            #line 98 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.Color));
            
            #line default
            #line hidden
            this.Write(";\r\n\t    int borderColor = ");
            
            #line 99 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.BorderColor));
            
            #line default
            #line hidden
            this.Write(";\r\n\t    int borderLineStyle = ");
            
            #line 100 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.BorderLineStyle));
            
            #line default
            #line hidden
            this.Write(";\r\n\t    int fillPattern = ");
            
            #line 101 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.FillPattern));
            
            #line default
            #line hidden
            this.Write(";\r\n\t    int borderLineThickness = ");
            
            #line 102 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.BorderLineThickness));
            
            #line default
            #line hidden
            this.Write(@";

	    /* Establecer el estilo y el color de relleno */
	    setfillstyle(fillPattern, color);
	    /* Dibujar ellipse con relleno */
	    fillellipse(centerX, centerY, xRadius, yRadius);
	    /* Establecer el color del borde */
	    setcolor(borderColor);
	    /* Establecer el estilo de línea del borde */
	    setlinestyle(borderLineStyle, 0, borderLineThickness);
	    /* Dibujar la elipse */
	    ellipse(centerX, centerY, startAngle, endAngle, xRadius, yRadius);
    ");
            
            #line 114 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

    }
    else if (shape is CLine line)
    {
    
            
            #line default
            #line hidden
            this.Write("        /* Coordenadas de la linea */\r\n        int x1 = ");
            
            #line 120 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.X1));
            
            #line default
            #line hidden
            this.Write(", y1 = ");
            
            #line 120 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.Y1));
            
            #line default
            #line hidden
            this.Write(", x2 = ");
            
            #line 120 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.X2));
            
            #line default
            #line hidden
            this.Write(", y2 = ");
            
            #line 120 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.Y2));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\n        /* Estilos, colores... */\r\n        int lineStyle = ");
            
            #line 123 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.LineStyle));
            
            #line default
            #line hidden
            this.Write(";\r\n        int lineThickness = ");
            
            #line 124 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.LineThickness));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\n        /* Establecer el grosor y estilo de la linea */\r\n        setlinestyl" +
                    "e(lineStyle, 0, lineThickness);\r\n        /* Establecer el color de la linea */\r\n" +
                    "        setcolor(color);\r\n        /* Dibujar la linea */\r\n        line(x1, y1, x" +
                    "2, y2);\r\n    ");
            
            #line 132 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

    }
    else if (shape is CPencil pencil)
    {
    
            
            #line default
            #line hidden
            this.Write("        /* Dibujar trazo */\r\n\t    ");
            
            #line 138 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

        foreach (var pixel in pencil.Pixels)
        {
	    
            
            #line default
            #line hidden
            this.Write("\t    putpixel(");
            
            #line 142 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pixel.X));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 142 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pixel.Y));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 142 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pixel.Color));
            
            #line default
            #line hidden
            this.Write(");\r\n\t    ");
            
            #line 143 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

        }
	    
            
            #line default
            #line hidden
            this.Write("    ");
            
            #line 146 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

    }
    
            
            #line default
            #line hidden
            this.Write("}\r\n");
            
            #line 150 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"

}

            
            #line default
            #line hidden
            this.Write("\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 154 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
 
    public ObservableCollection<SimpleShapeBase> shapes { get; set; }
    public CanvasSettings settings { get; set; }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
