using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OhmValueCalculatorApp.Models
{
    public class ResistorColorCodeResultModel
    {
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public List<ColorCodeModel> ColorAandBCodeList { get; set; }
        public List<ColorCodeModel> MultiplierColorCodeList { get; set; }
        public List<ColorCodeModel> ToleranceColorCodeList { get; set; }
    }
}