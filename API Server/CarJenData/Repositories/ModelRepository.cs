using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class ModelRepository
    {
        public static bool GetModelByID(int? modelID, ref string model, ref int? brandID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetModelByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.Parameters.AddWithValue("@ModelID", modelID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                model = reader["Model"]?.ToString();
                                brandID = reader["BrandID"] as int?;
                            }
                            else
                            {
                                isFound = false;
                            }
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

        public static bool GetModelByFullName(string brand, string model, ref int? modelID, ref int? brandID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetModelByFullName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.Parameters.AddWithValue("@Brand", brand);
                        command.Parameters.AddWithValue("@Model", model);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                brandID = reader["BrandID"] as int?;
                                modelID = reader["ModelID"] as int?;
                            }
                            else
                            {
                                isFound = false;
                            }
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

        public static DataTable GetAllModels()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllModels", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
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
