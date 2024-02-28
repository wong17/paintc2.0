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
    using Shapes.C;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class CRectangleTemplate : BaseTemplate
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write(@"
#include <graphics.h>
#include <conio.h>

void main()
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
            
            #line 25 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(settings.BackgroundColor));
            
            #line default
            #line hidden
            this.Write(");\r\n\r\n    /* Coordenadas del rectángulo */\r\n    int x1 = ");
            
            #line 28 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.X1));
            
            #line default
            #line hidden
            this.Write(", y1 = ");
            
            #line 28 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.Y1));
            
            #line default
            #line hidden
            this.Write(", x2 = ");
            
            #line 28 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.X2));
            
            #line default
            #line hidden
            this.Write(", y2 = ");
            
            #line 28 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.Y2));
            
            #line default
            #line hidden
            this.Write(";\r\n    /* Coordenadas de la barra interna con relleno */\r\n    int x1_bar = x1 + 1" +
                    ", y1_bar = y1 + 1, x2_bar = x2 - 1, y2_bar = y2 - 1;\r\n\r\n    /* Estilos, colores." +
                    ".. */\r\n    int color = ");
            
            #line 33 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.Color));
            
            #line default
            #line hidden
            this.Write(";\r\n    int borderColor = ");
            
            #line 34 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.BorderColor));
            
            #line default
            #line hidden
            this.Write(";\r\n    int borderLineStyle = ");
            
            #line 35 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.BorderLineStyle));
            
            #line default
            #line hidden
            this.Write(";\r\n    int fillPattern = ");
            
            #line 36 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rectangle.FillPattern));
            
            #line default
            #line hidden
            this.Write(";\r\n    int borderLineThickness = ");
            
            #line 37 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
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

    getch();
	closegraph(); /* Return the system to text mode	*/
	return 0;
}

");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 55 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CRectangleTemplate.tt"
 
	public CRectangle rectangle { get; set; }
    public CanvasSettings settings { get; set; }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
