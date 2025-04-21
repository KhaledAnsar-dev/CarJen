using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class CarDocumentationRepository
    {
        public static bool GetCarDocumentationByID(int? CarDocumentationID, ref int? SellerID, ref int? CarID, ref DateTime? CreatedDate)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetCarDocumentationByID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.Parameters.AddWithValue("@CarDocumentationID", CarDocumentationID);

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;
                            SellerID = Convert.ToInt32(Reader["SellerID"]);
                            CarID = Convert.ToInt32(Reader["CarID"]);
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

        public static int? AddCarDocumentation(int? SellerID, int? CarID)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddCarDocumentation", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@SellerID", SellerID);
                    Command.Parameters.AddWithValue("@CarID", CarID);

                    SqlParameter OutputID = new SqlParameter("@CarDocumentationID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputID);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                    CreatedID = (int)Command.Parameters["@CarDocumentationID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return CreatedID;
        }

        public static DataTable GetAllCarDocumentations()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllCarDocumentations", Connection))
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

        public static bool IsCarDocumentationExist(int CarDocumentationID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsCarDocumentationExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarDocumentationID", CarDocumentationID);
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
    }
}

