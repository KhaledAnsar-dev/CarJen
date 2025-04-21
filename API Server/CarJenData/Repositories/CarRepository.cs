using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class CarRepository
    {
        public static bool GetCarByID(int? CarID, ref int? BrandID, ref string Brand, ref int? ModelID,
            ref string Model, ref int? TrimID, ref string Trim, ref short? FuelType, ref int? Mileage,
            ref short? TransmissionType, ref short? Year, ref string Color, ref decimal? Price,
            ref string PlateNumber, ref DateTime? RegistrationExp, ref DateTime? TechInspectionExp)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetCarByID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.Parameters.AddWithValue("@CarID", CarID);

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;

                            BrandID = Reader["BrandID"] as int?;
                            Brand = Reader["Brand"]?.ToString();
                            ModelID = Reader["ModelID"] as int?;
                            Model = Reader["Model"]?.ToString();
                            TrimID = Reader["TrimID"] as int?;
                            Trim = Reader["Trim"]?.ToString();
                            FuelType = Reader["FuelType"] != DBNull.Value ? Convert.ToInt16(Reader["FuelType"]) : (short?)null;
                            Mileage = Reader["Mileage"] as int?;
                            TransmissionType = Reader["TransmissionType"] != DBNull.Value ? Convert.ToInt16(Reader["TransmissionType"]) : (short?)null;
                            Year = Reader["Year"] as short?;
                            Color = Reader["Color"]?.ToString();
                            Price = Reader["Price"] as decimal?;
                            PlateNumber = Reader["PlateNumber"]?.ToString();
                            RegistrationExp = Reader["RegistrationExp"] as DateTime?;
                            TechInspectionExp = Reader["TechInspectionExp"] as DateTime?;
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

        public static bool GetCarByPlateNumber(string PlateNumber, ref int? CarID, ref int? BrandID,
            ref string Brand, ref int? ModelID, ref string Model, ref int? TrimID, ref string Trim,
            ref short? FuelType, ref int? Mileage, ref short? TransmissionType, ref short? Year,
            ref string Color, ref decimal? Price, ref bool? IsNegotiable, ref DateTime? RegistrationExp,
            ref DateTime? TechInspectionExp)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetCarByPlateNumber", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.Parameters.AddWithValue("@PlateNumber", PlateNumber);

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;

                            CarID = Reader["CarID"] as int?;
                            BrandID = Reader["BrandID"] as int?;
                            Brand = Reader["Brand"]?.ToString();
                            ModelID = Reader["ModelID"] as int?;
                            Model = Reader["Model"]?.ToString();
                            TrimID = Reader["TrimID"] as int?;
                            Trim = Reader["Trim"]?.ToString();
                            FuelType = Reader["FuelType"] as short?;
                            Mileage = Reader["Mileage"] as int?;
                            TransmissionType = Reader["TransmissionType"] as short?;
                            Year = Reader["Year"] as short?;
                            Color = Reader["Color"]?.ToString();
                            Price = Reader["Price"] as decimal?;
                            IsNegotiable = Reader["IsNegotiable"] as bool?;
                            RegistrationExp = Reader["RegistrationExp"] as DateTime?;
                            TechInspectionExp = Reader["TechInspectionExp"] as DateTime?;
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

        public static int? AddCar(int? TrimID, short? FuelType, int? Mileage,
            short? TransmissionType, short? Year, string Color, decimal? Price,
            string PlateNumber, DateTime? RegistrationExp, DateTime? TechInspectionExp)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddCar", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@TrimID", TrimID);
                    Command.Parameters.AddWithValue("@FuelType", FuelType);
                    Command.Parameters.AddWithValue("@Mileage", Mileage);
                    Command.Parameters.AddWithValue("@TransmissionType", TransmissionType);
                    Command.Parameters.AddWithValue("@Year", Year);
                    Command.Parameters.AddWithValue("@Color", Color);
                    Command.Parameters.AddWithValue("@Price", Price);
                    Command.Parameters.AddWithValue("@PlateNumber", PlateNumber);
                    Command.Parameters.AddWithValue("@RegistrationExp", RegistrationExp);
                    Command.Parameters.AddWithValue("@TechInspectionExp", TechInspectionExp);

                    SqlParameter OutputID = new SqlParameter("@CarID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputID);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                    CreatedID = (int)Command.Parameters["@CarID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return CreatedID;
        }

        public static int? AddCar2(int? TrimID, short? FuelType, int? Mileage,
            short? TransmissionType, short? Year)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddCar", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@TrimID", TrimID);
                    Command.Parameters.AddWithValue("@FuelType", FuelType);
                    Command.Parameters.AddWithValue("@Mileage", Mileage);
                    Command.Parameters.AddWithValue("@TransmissionType", TransmissionType);
                    Command.Parameters.AddWithValue("@Year", Year);

                    SqlParameter OutputID = new SqlParameter("@CarID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputID);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                    CreatedID = (int)Command.Parameters["@CarID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return CreatedID;
        }

        public static bool UpdateCar(int? CarID, int? TrimID, short? FuelType, int? Mileage,
            short? TransmissionType, short? Year, string Color, decimal? Price,
            string PlateNumber, DateTime? RegistrationExp, DateTime? TechInspectionExp)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateCar", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CarID", CarID);
                    Command.Parameters.AddWithValue("@TrimID", TrimID);
                    Command.Parameters.AddWithValue("@FuelType", FuelType);
                    Command.Parameters.AddWithValue("@Mileage", Mileage);
                    Command.Parameters.AddWithValue("@TransmissionType", TransmissionType);
                    Command.Parameters.AddWithValue("@Year", Year);
                    Command.Parameters.AddWithValue("@Color", Color);
                    Command.Parameters.AddWithValue("@Price", Price);
                    Command.Parameters.AddWithValue("@PlateNumber", PlateNumber);
                    Command.Parameters.AddWithValue("@RegistrationExp", RegistrationExp);
                    Command.Parameters.AddWithValue("@TechInspectionExp", TechInspectionExp);

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

        public static bool Delete(int CarID)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_DeleteCar", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@CarID", CarID);

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

        public static DataTable GetAllCars()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllCars", Connection))
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

        public static bool IsCarExist(int CarID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsCarExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarID", CarID);
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

        public static bool IsCarExist(string PlateNumber)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsCarExistByPlateNumber", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PlateNumber", PlateNumber);
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

