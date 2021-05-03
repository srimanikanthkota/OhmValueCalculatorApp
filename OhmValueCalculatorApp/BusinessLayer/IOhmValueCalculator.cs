using OhmValueCalculatorApp.Models;

namespace OhmValueCalculatorApp.BusinessLayer
{
    public interface IOhmValueCalculator
    {
        /// <summary>
        /// Get Resistor Color code list containing Color code, Color name and significant figure
        /// </summary>
        /// <returns>ResistorColorCodeCollection of ColorAandBCodeList, MultiplierColorCodeList, ToleranceColorCodeList</returns>
        ResistorColorCodeResultModel GetResitorColorCodeList();

        /// <summary>
        /// Get Tolerance value for given band D color code
        /// </summary>
        /// <param name="bandDColor">The color of the tolerance value band.</param>
        /// <returns>Percentage of tolerance value</returns>
        decimal? GetToleranceValue(string bandDColor);

        /// <summary>
        /// Get denominator letter for ohm value. i.e. k = 1,000, M = 10,00,000, G = 100,00,00,000
        /// </summary>
        /// <param name="multiplierBandColor">The band color of multiplier</param>
        /// <returns>The denominator letter</returns>
        string GetMultiplierDenominatorLetter(string multiplierBandColor);

        /// <summary>
        /// Get divisor value for denominating ohm value. i.e. k = 1,000, M = 10,00,000, G = 100,00,00,000
        /// </summary>
        /// <param name="multiplierBandColor">The band color of multiplier</param>
        /// <returns>The ohm value after denomination</returns>
        decimal? GetOhmValueDivisor(string multiplierBandColor);

        /// <summary>
        /// Calculates the Ohm value of a resistor based on the band colors
        /// </summary>
        /// <param name="bandAColor">The color of the first figure of component value band.</param>
        /// <param name="bandBColor">The color of the second significant figure band.</param>
        /// <param name="bandCColor">The color of the decimal multiplier band.</param>
        /// <param name="bandDColor">The color of the tolerance value band.</param>
        /// <returns>Ohm value</returns>
        decimal CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor);
    }
}
