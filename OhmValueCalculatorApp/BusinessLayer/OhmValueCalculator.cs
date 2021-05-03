using OhmValueCalculatorApp.DataLayer;
using OhmValueCalculatorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OhmValueCalculatorApp.BusinessLayer
{
    public class OhmValueCalculator : IOhmValueCalculator
    {
        IOhmValueRepository Repository { get; set; }

        static List<ColorResistanceDbModel> ColorResistances { get; set; }
        public OhmValueCalculator()
        {
            Repository = new OhmValueRepository();
            ColorResistances = ColorResistances ?? Repository.GetColorResistanceValues();
        }

        public ResistorColorCodeResultModel GetResitorColorCodeList()
        {
            if (ColorResistances == null || ColorResistances.Count == 0)
                return null;

            var colorCodeAandB = ColorResistances.Where(a => a.SignificantFigure != null).OrderBy(a => a.VisibilityOrder).Select(a => new ColorCodeModel { ColorCode = a.ColorCode, ColorValue = a.ColorValue, SignificantFigure = a.SignificantFigure }).ToList();
            var multiplierColorCodes = ColorResistances.OrderBy(a => a.VisibilityOrder).Select(a => new ColorCodeModel { ColorCode = a.ColorCode, ColorValue = a.ColorValue, SignificantFigure = a.SignificantFigure }).ToList();
            var toleranceColorCodeList = ColorResistances.OrderBy(a => a.VisibilityOrder).Where(a => a.Tolerance != null).Select(a => new ColorCodeModel { ColorCode = a.ColorCode, ColorValue = a.ColorValue, SignificantFigure = a.SignificantFigure }).ToList();

            //INSERT DEFAULTS
            colorCodeAandB.Insert(0, new ColorCodeModel { ColorCode = "", ColorValue = "--SELECT--" });
            multiplierColorCodes.Insert(0, new ColorCodeModel { ColorCode = "", ColorValue = "--SELECT--" });
            toleranceColorCodeList.Insert(0, new ColorCodeModel { ColorCode = "", ColorValue = "--SELECT--" });

            return new ResistorColorCodeResultModel { ResultCode = 0, ResultMessage = "Success", ColorAandBCodeList = colorCodeAandB, MultiplierColorCodeList = multiplierColorCodes, ToleranceColorCodeList = toleranceColorCodeList };
        }

        public decimal? GetToleranceValue(string bandDColor) => ColorResistances.FirstOrDefault(a => a.ColorCode == bandDColor).Tolerance;
        public string GetMultiplierDenominatorLetter(string bandColor) => ColorResistances.FirstOrDefault(a => a.ColorCode == bandColor).OhmValueLetter;
        public decimal? GetOhmValueDivisor(string bandColor) => ColorResistances.FirstOrDefault(a => a.ColorCode == bandColor).Divisor;

        private void ValidateColorCodes(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            if (ColorResistances == null || ColorResistances.Count == 0)
            {
                throw new Exception("Color code resitance not available");
            }

            if (!ColorResistances.Any(a => a.ColorCode == bandAColor) || ColorResistances.FirstOrDefault(a => a.ColorCode == bandAColor).SignificantFigure == null)
            {
                throw new Exception("Invalid band color A");
            }
            if (!ColorResistances.Any(a => a.ColorCode == bandBColor) || ColorResistances.FirstOrDefault(a => a.ColorCode == bandBColor).SignificantFigure == null)
            {
                throw new Exception("Invalid band color B");
            }
            if (!ColorResistances.Any(a => a.ColorCode == bandCColor))
            {
                throw new Exception("Invalid band color C for multiplier");
            }

            if (!ColorResistances.Any(a => a.ColorCode == bandDColor))
            {
                throw new Exception("Invalid band color D for tolerance");
            }
        }

        public decimal CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            //Validate color codes and throw exception if found incorrect
            ValidateColorCodes(bandAColor, bandBColor, bandCColor, bandDColor);

            //Get Resistator initial value from Band A and Band B
            int resistorValue = Convert.ToInt32(ColorResistances.FirstOrDefault(a => a.ColorCode == bandAColor).SignificantFigure + "" + ColorResistances.FirstOrDefault(a => a.ColorCode == bandBColor).SignificantFigure);

            //Get multiplier and tolerance values from the color resitance list
            decimal multiplier = ColorResistances.FirstOrDefault(a => a.ColorCode == bandCColor).Multiplier;
            decimal? tolerance = GetToleranceValue(bandDColor);

            //ohm default value before tolerance
            decimal ohmValue = resistorValue * multiplier;

            //ohm value after appyliyng negative tolerance
            if (tolerance != null)
            {
                decimal ohmLeftValue = ohmValue - (ohmValue * tolerance.Value / 100);

                //ohm value after appyliyng positive tolerance
                decimal ohmRightValue = ohmValue + (ohmValue * tolerance.Value / 100);

                //taking maximum value (generally max value is consider for ohm value)
                ohmValue = ohmRightValue > ohmLeftValue ? ohmRightValue : ohmLeftValue;
            }

            return ohmValue;
        }


    }
}