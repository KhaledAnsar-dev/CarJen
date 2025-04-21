using CarJenData.DataModels;
using Microsoft.Data.SqlClient; 
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class AppointmentRepository
    {
        static public bool GetAppointmentByID(int? AppointmentID, ref int? PublishFeeID, ref int? CarDocumentationID, ref short? Status, ref DateTime? AppointmentDate, ref DateTime? CreatedDate)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAppointmentByID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;
                            PublishFeeID = Convert.ToInt32(Reader["fee"]);
                            CarDocumentationID = Convert.ToInt32(Reader["CarDocumentationID"]);
                            Status = Convert.ToInt16(Reader["Status"]);
                            AppointmentDate = Convert.ToDateTime(Reader["AppointmentDate"]);
                            CreatedDate = Convert.ToDateTime(Reader["CreatedDate"]);
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

        static public bool GetAppointmentByCarDocID(ref int? AppointmentID, ref int? PublishFeeID, int? CarDocumentationID, ref short? Status, ref DateTime? AppointmentDate, ref DateTime? CreatedDate)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAppointmentByCarDocID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.Parameters.AddWithValue("@CarDocumentationID", CarDocumentationID);

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;
                            PublishFeeID = (Reader["PublishFeeID"] == DBNull.Value) ? 0 : Convert.ToInt32(Reader["PublishFeeID"]);
                            AppointmentID = Convert.ToInt32(Reader["AppointmentID"]);
                            Status = Convert.ToInt16(Reader["Status"]);
                            AppointmentDate = Convert.ToDateTime(Reader["AppointmentDate"]);
                            CreatedDate = Convert.ToDateTime(Reader["CreatedDate"]);
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

        static public int? AddAppointment(int? CarDocumentationID, int? Status, DateTime? AppointmentDate)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddAppointment", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@PublishFeeID", DBNull.Value);
                    Command.Parameters.AddWithValue("@CarDocumentationID", CarDocumentationID);
                    Command.Parameters.AddWithValue("@Status", Status);
                    Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);

                    SqlParameter OutputID = new SqlParameter("@AppointmentID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output,
                    };
                    Command.Parameters.Add(OutputID);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                    CreatedID = (int)Command.Parameters["@AppointmentID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return CreatedID;
        }

        static public bool UpdateAppointmentStatus(int? AppointmentID, short? Status)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateAppointmentStatus", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
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

        static public bool UpdatePublishFee(int? AppointmentID, int? publishFeeID)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdatePublishFee", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    Command.Parameters.AddWithValue("@publishFeeID", publishFeeID);

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

        static public DataTable GetAllAppointments()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllAppointments", Connection))
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

        public static bool IsAppointmentExist(int AppointmentID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsAppointmentExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        isFound = reader.HasRows;
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

        public static int GetAppointmentIDByCarDocID(int CarDocumentationID)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAppointmentIDByCarDocID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarDocumentationID", CarDocumentationID);

                    SqlParameter OutputID = new SqlParameter("@AppointmentID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(OutputID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    CreatedID = (int)command.Parameters["@AppointmentID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return CreatedID.Value;
        }

        static public bool Delete(int AppointmentID)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_DeleteAppointment", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

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

        static public DataTable GetAllPreApprovedCars()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllPreApprovedCars", Connection))
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
    }
}
