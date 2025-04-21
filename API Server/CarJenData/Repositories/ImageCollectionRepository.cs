using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CarJenData.Repositories
{
    public class ImageCollectionRepository
    {
        public static bool GetImageCollectionByID(int? imageCollectionID, bool? status, ref int? carID, ref DateTime? createdDate)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetImageCollectionByID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@ImageCollectionID", imageCollectionID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;
                            carID = Convert.ToInt32(Reader["carID"]);
                            status = Convert.ToBoolean(Reader["status"]);
                            createdDate = Convert.ToDateTime(Reader["createdDate"]);
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

        public static bool GetImageCollectionByCarID(int? carID, ref int? imageCollectionID, ref bool? status, ref DateTime? createdDate)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetImageCollectionByCarID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@CarID", carID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;
                            imageCollectionID = Convert.ToInt32(Reader["imageCollectionID"]);
                            status = Convert.ToBoolean(Reader["status"]);
                            createdDate = Convert.ToDateTime(Reader["createdDate"]);
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

        public static int? SaveImageCollection(int? carID, string frontImage, string rearImage, string sideImage, string interiorImage)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddImageCollection", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@carID", carID);
                    Command.Parameters.AddWithValue("@createdDate", DateTime.Now);
                    Command.Parameters.AddWithValue("@frontImage", frontImage);
                    Command.Parameters.AddWithValue("@rearImage", rearImage);
                    Command.Parameters.AddWithValue("@sideImage", sideImage);
                    Command.Parameters.AddWithValue("@interiorImage", interiorImage);

                    SqlParameter OutputID = new SqlParameter("@imageCollectionID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputID);

                    Connection.Open();
                    Command.ExecuteNonQuery();

                    CreatedID = (int)Command.Parameters["@imageCollectionID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return CreatedID;
        }

        public static int? AddImageCollection(int? carID, string frontImage, string rearImage, string sideImage, string interiorImage)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddImgCollection", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@carID", carID);
                    Command.Parameters.AddWithValue("@createdDate", DateTime.Now);
                    Command.Parameters.AddWithValue("@frontImage", frontImage);
                    Command.Parameters.AddWithValue("@rearImage", rearImage);
                    Command.Parameters.AddWithValue("@sideImage", sideImage);
                    Command.Parameters.AddWithValue("@interiorImage", interiorImage);

                    SqlParameter OutputID = new SqlParameter("@imageCollectionID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputID);

                    Connection.Open();
                    Command.ExecuteNonQuery();

                    CreatedID = (int)Command.Parameters["@imageCollectionID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return CreatedID;
        }

        public static bool UpdateImageCollection(int? imageCollectionID, string frontImage, string rearImage, string sideImage, string interiorImage)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateImgCollection", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@imageCollectionID", imageCollectionID);
                    Command.Parameters.AddWithValue("@frontImage", frontImage);
                    Command.Parameters.AddWithValue("@rearImage", rearImage);
                    Command.Parameters.AddWithValue("@sideImage", sideImage);
                    Command.Parameters.AddWithValue("@interiorImage", interiorImage);

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
    }
}
