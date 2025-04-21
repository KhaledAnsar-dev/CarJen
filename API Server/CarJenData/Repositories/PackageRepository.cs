using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class PackageRepository
    {
        public static bool GetPackageByID(int? packageID, ref string title, ref int? numberOfReports, ref int? createdByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPackageByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.Parameters.AddWithValue("@PackageID", packageID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                title = reader["Title"]?.ToString();
                                numberOfReports = Convert.ToInt32(reader["NumberOfReports"]);
                                createdByUserID = reader["CreatedByUser"] as int?;
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

        public static int? AddPackage(string title, int? numberOfReports, int? createdByUserID, int? feeID)
        {
            int? createdID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddPackage", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@NumberOfReports", numberOfReports);
                        command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
                        command.Parameters.AddWithValue("@FeeID", feeID);

                        SqlParameter outputID = new SqlParameter("@PackageID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputID);
                        connection.Open();
                        command.ExecuteNonQuery();

                        createdID = (int)command.Parameters["@PackageID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return createdID;
        }

        public static bool UpdatePackage(int? packageID, string title, int? numberOfReports)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdatePackage", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PackageID", packageID);
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@NumberOfReports", numberOfReports);

                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return rowAffected > 0;
        }

        public static DataTable GetAllPackages()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllPackages", connection))
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

        public static bool IsPackageExist(string title)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsPackageExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Title", title);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
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
    }
}
