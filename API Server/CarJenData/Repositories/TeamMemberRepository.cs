using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class TeamMemberRepository
    {
        public static bool GetTeamMemberByID(int? teamMemberID, ref int? teamID, ref int? userID, ref DateTime? joinDate, ref DateTime? exitDate)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetTeamMemberByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TeamMemberID", teamMemberID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            teamID = Convert.ToInt32(reader["TeamID"]);
                            userID = Convert.ToInt32(reader["UserID"]);
                            joinDate = Convert.ToDateTime(reader["JoinDate"]);
                            exitDate = reader["ExitDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExitDate"]) : null;
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

        public static int? AddTeamMember(int? teamID, int? userID)
        {
            int? createdID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_AddTeamMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TeamID", teamID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@JoinDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ExitDate", DBNull.Value);

                    SqlParameter outputID = new SqlParameter("@TeamMemberID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    createdID = (int)command.Parameters["@TeamMemberID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return createdID;
        }

        public static bool ReplaceMember(int? newUserID, int? oldUserID)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_ReplaceMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NewUserID", newUserID);
                    command.Parameters.AddWithValue("@OldUserID", oldUserID);
                    command.Parameters.AddWithValue("@JoinDate", DateTime.Now);

                    connection.Open();
                    rowAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return rowAffected > 0;
        }

        public static bool DeleteTeamMember(int teamMemberID)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_DeleteTeamMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TeamMemberID", teamMemberID);

                    connection.Open();
                    rowAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return rowAffected > 0;
        }

        public static bool ExitTeam(int teamMemberID)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_ExitTeam", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TeamMemberID", teamMemberID);
                    command.Parameters.AddWithValue("@ExitDate", DateTime.Now);

                    connection.Open();
                    rowAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return rowAffected > 0;
        }

        public static DataTable GetAllTeamMembers()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAllTeamMemebers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
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

        public static bool IsUserMember(int? userID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsUserMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
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

        public static short? GetTeamRoleMemberCount(int? teamID, short? roleID)
        {
            short? count = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetTeamRoleMemberCount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TeamID", teamID);
                    command.Parameters.AddWithValue("@RoleID", roleID);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && short.TryParse(result.ToString(), out short number))
                        count = number;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return count;
        }
    }
}

