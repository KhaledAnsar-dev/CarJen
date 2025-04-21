using CarJenData.DataModels;
using Microsoft.Data.SqlClient; // استخدام Microsoft.Data.SqlClient
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class BrandRepository
    {
        public static bool GetBrandByID(int? BrandID, ref string Brand)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetBrandByID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.Parameters.AddWithValue("@BrandID", BrandID);

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;
                            Brand = Reader["Brand"]?.ToString();
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

        public static bool GetBrandByName(string Brand, ref int? BrandID)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetBrandByName", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.Parameters.AddWithValue("@Brand", Brand);

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;
                            BrandID = Convert.ToInt32(Reader["BrandID"]);
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

        public static DataTable GetAllBrands()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllBrands", Connection))
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
