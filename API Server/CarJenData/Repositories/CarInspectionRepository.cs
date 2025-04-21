using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class CarInspectionRepository
    {
        public static bool GetCarInspectionByID(int? CarInspectionID, ref int? TeamID, ref int? CarDocumentationID, ref short? Status, ref DateTime? ExpDate)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetCarInspectionByID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.Parameters.AddWithValue("@CarInspectionID", CarInspectionID);

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;

                            TeamID = Reader["TeamID"] != DBNull.Value ? Convert.ToInt32(Reader["TeamID"]) : (int?)null;
                            CarDocumentationID = Convert.ToInt32(Reader["CarDocumentationID"]);
                            Status = Convert.ToInt16(Reader["Status"]);
                            ExpDate = Reader["ExpDate"] != DBNull.Value ? Convert.ToDateTime(Reader["ExpDate"]) : (DateTime?)null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return IsFound;
        }
        public static int? AddCarInspection(int? CarDocumentationID, short? Status)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddCarInspection", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CarDocumentationID", CarDocumentationID);
                    Command.Parameters.AddWithValue("@Status", Status);

                    SqlParameter OutputID = new SqlParameter("@CarInspectionID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputID);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                    CreatedID = (int)Command.Parameters["@CarInspectionID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return CreatedID;
        }

        public static bool UpdateCarInspection(int? CarInspectionID, int? TeamID, short? Status)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateCarInspection", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CarInspectionID", CarInspectionID);
                    Command.Parameters.AddWithValue("@TeamID", TeamID);
                    Command.Parameters.AddWithValue("@ExpDate", DateTime.Now.AddMonths(3));
                    Command.Parameters.AddWithValue("@Status", Status);

                    Connection.Open();
                    RowAffected = Command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return RowAffected > 0;
        }

        public static bool UpdateCarInspectionStatus(int? CarInspectionID, short? Status)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateCarInspectionStatus", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CarInspectionID", CarInspectionID);
                    Command.Parameters.AddWithValue("@Status", Status);

                    Connection.Open();
                    RowAffected = Command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return RowAffected > 0;
        }

        public static DataTable GetAllReports()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllReports", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
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

        public static bool GetInspectionIDsForReport(int? carInspectionID, Dictionary<string, int> report)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetInspectionIDsForReport", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.Parameters.AddWithValue("@carInspectionID", carInspectionID);

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;
                            for (int i = 0; i < Reader.FieldCount; i++)
                            {
                                var columnName = Reader.GetName(i);
                                var columnValue = Reader.GetValue(i);

                                if (!report.ContainsKey(columnName))
                                {
                                    report.Add(columnName, Convert.ToInt32(columnValue));
                                }
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

            return IsFound;
        }

        public static bool GetSingleInspectionResultByID(string procedure, int ID, ref short? condition, ref string recommendation)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand(procedure, Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.Parameters.AddWithValue("@ID", ID);

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;
                            condition = Convert.ToInt16(Reader["Condition"]);
                            recommendation = Reader["Recommendation"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return IsFound;
        }

        public static bool GetResumeByCarInspectionID(int? carInspectionID, ref string resume)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetResumeByCarInspectionID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.Parameters.AddWithValue("@ID", carInspectionID);

                    SqlParameter outputParam = new SqlParameter("@Resume", SqlDbType.NVarChar, 350)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParam);

                    Command.ExecuteNonQuery();

                    if (outputParam.Value != DBNull.Value)
                    {
                        resume = outputParam.Value.ToString();
                        IsFound = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return IsFound;
        }
    }
}
