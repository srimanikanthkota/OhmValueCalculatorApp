using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OhmValueCalculatorApp.Models
{
    /// <summary>
    /// Color code Model
    /// </summary>
    public class ColorCodeModel
    {
        public string ColorCode { get; set; }
        public string ColorValue { get; set; }
        public int? SignificantFigure { get; set; }
    }
}