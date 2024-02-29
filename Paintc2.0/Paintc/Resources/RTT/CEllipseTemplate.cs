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
    
    #line 1 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class CEllipseTemplate : BaseTemplate
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("/* Code generated by Paintc */\r\n#include <graphics.h>\r\n#include <conio.h>\r\n\r\n/* D" +
                    "ibuja ");
            
            #line 11 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.Name));
            
            #line default
            #line hidden
            this.Write(" */\r\nvoid draw");
            
            #line 12 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.Name));
            
            #line default
            #line hidden
            this.Write(@"();

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
            
            #line 27 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(settings.BackgroundColor));
            
            #line default
            #line hidden
            this.Write(");\r\n\r\n\t/* Your code here... */\r\n\tdraw");
            
            #line 30 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.Name));
            
            #line default
            #line hidden
            this.Write("();\r\n\r\n\tgetch();\r\n\tclosegraph(); /* Return the system to text mode\t*/\r\n\treturn 0;" +
                    "\r\n}\r\n\r\nvoid draw");
            
            #line 37 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.Name));
            
            #line default
            #line hidden
            this.Write("()\r\n{\r\n\t/* Coordenadas del centro de la ellipse */\r\n\tint centerX = ");
            
            #line 40 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.X));
            
            #line default
            #line hidden
            this.Write(", centerY = ");
            
            #line 40 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.Y));
            
            #line default
            #line hidden
            this.Write(";\r\n\t/* Angulo inicio y fin */\r\n\tint startAngle = ");
            
            #line 42 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.StartAngle));
            
            #line default
            #line hidden
            this.Write(", endAngle = ");
            
            #line 42 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.EndAngle));
            
            #line default
            #line hidden
            this.Write(";\r\n\t/* Radios */\r\n\tint xRadius = ");
            
            #line 44 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.XRadius));
            
            #line default
            #line hidden
            this.Write(", yRadius = ");
            
            #line 44 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.YRadius));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\n\t/* Estilos, colores... */\r\n\tint color = ");
            
            #line 47 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.Color));
            
            #line default
            #line hidden
            this.Write(";\r\n\tint borderColor = ");
            
            #line 48 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.BorderColor));
            
            #line default
            #line hidden
            this.Write(";\r\n\tint borderLineStyle = ");
            
            #line 49 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.BorderLineStyle));
            
            #line default
            #line hidden
            this.Write(";\r\n\tint fillPattern = ");
            
            #line 50 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.FillPattern));
            
            #line default
            #line hidden
            this.Write(";\r\n\tint borderLineThickness = ");
            
            #line 51 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ellipse.BorderLineThickness));
            
            #line default
            #line hidden
            this.Write(@";

	/* Establecer el color del borde */
	setcolor(borderColor);
	/* Establecer el estilo de línea del borde */
	setlinestyle(borderLineStyle, 0, borderLineThickness);
	/* Establecer el estilo y el color de relleno */
	setfillstyle(fillPattern, color);
	/* Dibujar la elipse */
	ellipse(centerX, centerY, startAngle, endAngle, xRadius, yRadius);
}

");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 63 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\CEllipseTemplate.tt"
 
	public CEllipse ellipse { get; set; }
	public CanvasSettings settings { get; set; }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
