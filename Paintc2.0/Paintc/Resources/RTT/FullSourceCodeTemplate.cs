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
            this.Write(@"/* Code generated by Paintc */
#include <graphics.h>
#include <conio.h>

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
            
            #line 24 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(settings.BackgroundColor));
            
            #line default
            #line hidden
            this.Write(");\r\n\r\n    /* Your code here... */\r\n    \r\n\r\n    getch();\r\n    closegraph(); /* Ret" +
                    "urn the system to text mode\t*/\r\n    return 0;\r\n}\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 34 "D:\Repositories\paintc2.0\Paintc2.0\Paintc\Resources\RTT\FullSourceCodeTemplate.tt"
 
    public CanvasSettings settings { get; set; }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}