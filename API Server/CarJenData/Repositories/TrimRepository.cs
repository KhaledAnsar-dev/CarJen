using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class TrimRepository
    {
        public static bool GetTrimByID(int? trimID, ref string trim, ref int? modelID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetTrimByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TrimID", trimID);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            trim = reader["Trim"]?.ToString();
                            modelID = reader["ModelID"] as int?;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return isFound;
        }

        public static bool GetTrimIDByFullName(string trim, string model, string brand, ref int? trimID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetTrimIDByNameAndModelAndBrand", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Trim", trim);
                    command.Parameters.AddWithValue("@Model", model);
                    command.Parameters.AddWithValue("@Brand", brand);

                    SqlParameter outputID = new SqlParameter("@TrimID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputID);

                    connection.Open();
                    command.ExecuteNonQuery();

                    trimID = command.Parameters["@TrimID"].Value != DBNull.Value
                        ? (int?)command.Parameters["@TrimID"].Value
                        : null;

                    isFound = trimID.HasValue;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
                isFound = false;
            }

            return isFound;
        }

        public static DataTable GetAllTrims()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAllTrims", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return dt;
        }
    }
}
