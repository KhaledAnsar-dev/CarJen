using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CarJenData.Repositories
{
    public class ImageRepository
    {
        public static bool GetImageByCollectionIDAndView(int? imageCollectionID, short? viewType, ref int? imageID, ref string imagePath)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetImageByCollectionIDAndView", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@imageCollectionID", imageCollectionID);
                    Command.Parameters.AddWithValue("@viewType", viewType);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;
                            imageID = Convert.ToInt32(Reader["ImageID"]);
                            imagePath = Reader["ImagePath"].ToString();
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

        public static int? AddImage(int? imageCollectionID, string imagePath, short? viewType)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddImage", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@imageCollectionID", imageCollectionID);
                    Command.Parameters.AddWithValue("@imagePath", imagePath);
                    Command.Parameters.AddWithValue("@viewType", viewType);

                    SqlParameter OutputID = new SqlParameter("@imageID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputID);

                    Connection.Open();
                    Command.ExecuteNonQuery();

                    CreatedID = (int)Command.Parameters["@imageID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return CreatedID;
        }
    }
}
