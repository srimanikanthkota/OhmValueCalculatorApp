using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OhmValueCalculatorApp.Models
{
    public class OhmBandInputModel
    {
        [Required]
        public string ColorA { get; set; }
        [Required]
        public string ColorB { get; set; }
        [Required]
        public string ColorC { get; set; }
        [Required]
        public string ColorD { get; set; }
    }
}