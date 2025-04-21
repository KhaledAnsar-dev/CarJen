using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class SellerRepository
    {
        public static bool GetSellerByID(int? sellerID, ref int? personID,
            ref string firstName, ref string middleName, ref string lastName,
            ref short gender, ref DateTime? dateOfBirth, ref string email, ref string phone,
            ref string address, ref DateTime? joinDate, ref bool isActive, ref string image,
            ref string userName, ref string password, ref string nationalNumber, ref decimal? earnings)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetSellerByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SellerID", sellerID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            nationalNumber = reader["NationalNumber"].ToString();
                            earnings = Convert.ToDecimal(reader["Earnings"]);
                            personID = Convert.ToInt32(reader["PersonID"]);
                            firstName = reader["FirstName"].ToString();
                            middleName = reader["MiddleName"] as string ?? "";
                            lastName = reader["LastName"].ToString();
                            gender = Convert.ToInt16(reader["Gender"]);
                            dateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                            email = reader["Email"] as string ?? "";
                            phone = reader["Phone"].ToString();
                            address = reader["Address"].ToString();
                            joinDate = Convert.ToDateTime(reader["JoinDate"]);
                            isActive = Convert.ToBoolean(reader["IsActive"]);
                            image = reader["Image"] as string ?? "";
                            userName = reader["UserName"].ToString();
                            password = reader["Password"].ToString();
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

        public static bool GetSellerByNO(string nationalNumber, ref int? sellerID, ref int? personID,
            ref string firstName, ref string middleName, ref string lastName,
            ref short gender, ref DateTime? dateOfBirth, ref string email, ref string phone,
            ref string address, ref DateTime? joinDate, ref bool isActive, ref string image,
            ref string userName, ref string password, ref decimal? earnings)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetSellerByNO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NationalNumber", nationalNumber);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            sellerID = Convert.ToInt32(reader["SellerID"]);
                            earnings = Convert.ToDecimal(reader["Earnings"]);
                            personID = Convert.ToInt32(reader["PersonID"]);
                            firstName = reader["FirstName"].ToString();
                            middleName = reader["MiddleName"] as string ?? "";
                            lastName = reader["LastName"].ToString();
                            gender = Convert.ToInt16(reader["Gender"]);
                            dateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                            email = reader["Email"] as string ?? "";
                            phone = reader["Phone"].ToString();
                            address = reader["Address"].ToString();
                            joinDate = Convert.ToDateTime(reader["JoinDate"]);
                            isActive = Convert.ToBoolean(reader["IsActive"]);
                            image = reader["Image"] as string ?? "";
                            userName = reader["UserName"].ToString();
                            password = reader["Password"].ToString();
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

        public static bool GetSellerByCredentials(ref int? sellerID, ref int? personID,
            ref string firstName, ref string middleName, ref string lastName,
            ref short gender, ref DateTime? dateOfBirth, ref string email, ref string phone,
            ref string address, ref DateTime? joinDate, ref bool isActive, ref string image,
            string userName, string password, ref string nationalNumber,
            ref decimal? earnings)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("GetSellerByCredentials", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@Password", password);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            sellerID = Convert.ToInt32(reader["SellerID"]);
                            nationalNumber = reader["NationalNumber"].ToString();
                            earnings = Convert.ToDecimal(reader["Earnings"]);
                            personID = Convert.ToInt32(reader["PersonID"]);
                            firstName = reader["FirstName"].ToString();
                            middleName = reader["MiddleName"] as string ?? "";
                            lastName = reader["LastName"].ToString();
                            gender = Convert.ToInt16(reader["Gender"]);
                            dateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                            email = reader["Email"] as string ?? "";
                            phone = reader["Phone"].ToString();
                            address = reader["Address"].ToString();
                            joinDate = Convert.ToDateTime(reader["JoinDate"]);
                            isActive = Convert.ToBoolean(reader["IsActive"]);
                            image = reader["Image"] as string ?? "";
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

        public static int? AddSeller(int? personID, string nationalNumber)
        {
            int? createdID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_AddNewSeller", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", personID);
                    command.Parameters.AddWithValue("@NationalNumber", nationalNumber);
                    command.Parameters.AddWithValue("@Earnings", 0);

                    SqlParameter outputID = new SqlParameter("@SellerID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output,
                    };
                    command.Parameters.Add(outputID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    createdID = (int)command.Parameters["@SellerID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return createdID;
        }

        public static bool UpdateSeller(int? sellerID, string nationalNumber)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_UpdateSeller", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SellerID", sellerID);
                    command.Parameters.AddWithValue("@NationalNumber", nationalNumber);

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

        public static bool DeleteSeller(int sellerID)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_DeleteSeller", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SellerID", sellerID);
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

        public static DataTable GetAllSellers()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAllSellers", connection))
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

        public static bool IsSellerExist(int sellerID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsSellerExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SellerID", sellerID);
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

        public static bool IsSellerExist(string nationalNumber)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsSellerExistNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NationalNumber", nationalNumber);
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
