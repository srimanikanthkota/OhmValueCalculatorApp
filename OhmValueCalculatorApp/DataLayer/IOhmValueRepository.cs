using OhmValueCalculatorApp.Models;
using System.Collections.Generic;

namespace OhmValueCalculatorApp.DataLayer
{
    public interface IOhmValueRepository
    {
        /// <summary>
        /// Get List of Color codes, its significant figure, tolerance and multiplier value
        /// </summary>
        /// <returns>List of ColorResistanceDbModel</returns>
        List<ColorResistanceDbModel> GetColorResistanceValues();
    }
}
