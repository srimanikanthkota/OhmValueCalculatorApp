using OhmValueCalculatorApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace OhmValueCalculatorApp.DataLayer
{
    public class OhmValueRepository : IOhmValueRepository
    {
        public string ConnectionString => ConfigurationManager.ConnectionStrings["OhmDbConnectionString"].ConnectionString;
        public List<ColorResistanceDbModel> GetColorResistanceValues()
        {
            List<ColorResistanceDbModel> colorCodes = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_GetColorResitanceValues", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }

                        colorCodes = new List<ColorResistanceDbModel>();

                        while (reader.Read())
                        {
                            colorCodes.Add(new ColorResistanceDbModel
                            {
                                ColorCode = reader["ColorCode"] != DBNull.Value ? Convert.ToString(reader["ColorCode"]) : string.Empty,
                                ColorValue = reader["ColorValue"] != DBNull.Value ? Convert.ToString(reader["ColorValue"]) : string.Empty,
                                Multiplier = reader["Multiplier"] != DBNull.Value ? Convert.ToDecimal(reader["Multiplier"]) : 0,
                                Tolerance = reader["Tolerance"] != DBNull.Value ? Convert.ToDecimal(reader["Tolerance"]) : default(decimal?),
                                SignificantFigure = reader["SignificantFigure"] != DBNull.Value ? Convert.ToInt32(reader["SignificantFigure"]) : default(int?),
                                OhmValueLetter = reader["SignificantFigure"] != DBNull.Value ? Convert.ToString(reader["OhmValueLetter"]) : string.Empty,
                                Divisor = reader["Divisor"] != DBNull.Value ? Convert.ToDecimal(reader["Divisor"]) : default(decimal?),
                                VisibilityOrder = reader["VisibilityOrder"] != DBNull.Value ? Convert.ToInt32(reader["VisibilityOrder"]) : default
                            });
                        }
                    }
                }

                connection.Close();
            }
            return colorCodes;
        }
    }
}