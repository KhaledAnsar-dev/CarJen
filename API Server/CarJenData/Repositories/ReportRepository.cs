using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CarJenData.Repositories
{
    public class ReportRepository
    {
        public static bool GetReportByID(int? reportID, ref int? carDocumentationID, ref DateTime? releasedDate, ref short? status)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetReportByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.Parameters.AddWithValue("@ReportID", reportID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                carDocumentationID = reader["CarDocumentationID"] != DBNull.Value ? Convert.ToInt32(reader["CarDocumentationID"]) : (int?)null;
                                status = Convert.ToInt16(reader["Status"]);
                                releasedDate = Convert.ToDateTime(reader["ReleaseDate"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // Log or handle the exception if needed
            }

            return isFound;
        }

        public static int? AddApprovedReport(int carDocumentationID)
        {
            int? createdID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddApprovedReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CarDocumentationID", carDocumentationID);
                        command.Parameters.AddWithValue("@ReleasedDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Status", 1);

                        SqlParameter outputID = new SqlParameter("@ReportID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputID);

                        connection.Open();
                        command.ExecuteNonQuery();

                        createdID = (int)command.Parameters["@ReportID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // Log or handle the exception if needed
            }

            return createdID;
        }

        public static bool UpdateReportStatus(int? reportID, short? status)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateReportStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ReportID", reportID);
                        command.Parameters.AddWithValue("@Status", status);

                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // Log or handle the exception if needed
            }

            return rowAffected > 0;
        }

        public static DataTable GetAllApprovedReports()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllApprovedReports", connection))
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
                // Log or handle the exception if needed
            }

            return dt;
        }
    }
}
