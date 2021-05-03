using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OhmValueCalculatorApp.Models
{
    public class OhmResultModel
    {
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public decimal OhmValue { get; set; }
        public decimal? ToleranceValue { get; set; }
        public decimal? OhmDenominatedValue { get; set; }
        public string OhmDenominatedLetter { get; set; }
    }
}