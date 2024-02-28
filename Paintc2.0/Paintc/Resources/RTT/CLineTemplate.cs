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
    using Paintc.Shapes.C;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CLineTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class CLineTemplate : BaseTemplate
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("﻿");
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
            
            #line 25 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CLineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(settings.BackgroundColor));
            
            #line default
            #line hidden
            this.Write(");\r\n\r\n    /* Coordenadas de la linea */\r\n    int x1 = ");
            
            #line 28 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CLineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.X1));
            
            #line default
            #line hidden
            this.Write(", y1 = ");
            
            #line 28 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CLineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.Y1));
            
            #line default
            #line hidden
            this.Write(", x2 = ");
            
            #line 28 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CLineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.X2));
            
            #line default
            #line hidden
            this.Write(", y2 = ");
            
            #line 28 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CLineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.Y2));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\n    /* Estilos, colores... */\r\n    int lineStyle = ");
            
            #line 31 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CLineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.LineStyle));
            
            #line default
            #line hidden
            this.Write(";\r\n    int lineThickness = ");
            
            #line 32 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CLineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(line.LineThickness));
            
            #line default
            #line hidden
            this.Write(@";

    /* Establecer el grosor y estilo de la linea */
    setlinestyle(lineStyle, 0, lineThickness);
    /* Establecer el color de la linea */
    setcolor(color);
    /* Dibujar la linea */
    line(x1, y1, x2, y2);

    getch();
	closegraph(); /* Return the system to text mode	*/
	return 0;
}

");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 46 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CLineTemplate.tt"
 
	public CLine line { get; set; }
    public CanvasSettings settings { get; set; }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
