using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class EvaluationRepository
    {
        public static bool UpdateScore(int? UserID, int EvaluationScore)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateScore", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@UserID", UserID);
                    Command.Parameters.AddWithValue("@EvaluationScore", EvaluationScore);

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
        public static bool ResetCurrent(int? UserID)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_ResetCurrent", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@UserID", UserID);

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
        public static bool ResetAll()
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_ResetAll", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

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
        public static int GetUserScore(int? userID)
        {
            int Score = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetUserScore", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@UserID", userID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            Score = Convert.ToInt32(Reader["EvaluationScore"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return Score;
        }
    }
}

