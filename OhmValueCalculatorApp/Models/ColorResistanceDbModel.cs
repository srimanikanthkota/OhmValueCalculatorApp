using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OhmValueCalculatorApp.Models
{
    public class ColorResistanceDbModel
    {
        public string ColorCode { get; set; }
        public string ColorValue { get; set; }
        public int? SignificantFigure { get; set; }
        public decimal Multiplier { get; set; }
        public decimal? Tolerance { get; set; }
        public string OhmValueLetter { get; set; }
        public decimal? Divisor { get; set; }
        public int VisibilityOrder { get; set; }
    }
}