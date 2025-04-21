using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CarJenData.Repositories
{
    public class FeeRepository
    {
        public static bool GetCurrentFeeByType(ref int? FeeID, byte? MainFeeType, ref DateTime? StartDate, ref DateTime? EndDate, ref decimal? Amount)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetCurrentFeeByType", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@MainFeeType", MainFeeType);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;

                            FeeID = Convert.ToInt32(Reader["MainFeeID"]);
                            StartDate = Convert.ToDateTime(Reader["StartDate"]);
                            EndDate = Reader["EndDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(Reader["EndDate"]) : null;
                            Amount = Convert.ToDecimal(Reader["Amount"]);
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

        public static bool RenewFeeFor(byte MainFeeType, decimal NewAmountFee)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_RenewFee", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@MainFeeType", MainFeeType);
                    Command.Parameters.AddWithValue("@NewAmountFee", NewAmountFee);

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

        public static bool DeleteUnusedFee(int MainFeeID)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_DeleteFee", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@MainFeeID", MainFeeID);

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

        public static DataTable GetAllFees()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllFees", Connection))
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
