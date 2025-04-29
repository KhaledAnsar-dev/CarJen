using CarJenData.DataModels;
using CarJenShared.Dtos.CarInspectionDtos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using static CarJenShared.Helpers.Logger;
namespace CarJenData.Repositories
{
    public class CarInspectionRepository
    {
        public static int? CreateInitialCarInspection(int? CarDocumentationID, short? Status)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddCarInspection", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CarDocumentationID", CarDocumentationID);
                    Command.Parameters.AddWithValue("@Status", Status);

                    SqlParameter OutputID = new SqlParameter("@CarInspectionID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputID);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                    CreatedID = (int)Command.Parameters["@CarInspectionID"].Value;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return CreatedID;
        }
        public static bool UpdateCarInspectionStatus(int? CarInspectionID, short? Status)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateCarInspectionStatus", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CarInspectionID", CarInspectionID);
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
        public static bool AddCarInspectionBatch(DataTable report, string resume, int carInspectionID)
        {
            bool isSaved = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_SaveInspections", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarInspectionID", carInspectionID);
                    command.Parameters.AddWithValue("@InspectionsType", report);
                    command.Parameters.AddWithValue("@Resume", resume);

                    connection.Open();
                    command.ExecuteNonQuery();

                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return isSaved;
        } 
        static public List<InspectionDto> GetCarInspectionBatch(int? CarInspectionID)
        {
            List<InspectionDto> inspections = new List<InspectionDto>();

            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetCarInspectionBatch", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var inspection = new InspectionDto
                            {
                                Name = reader["Name"].ToString(), 
                                Condition = Convert.ToInt16(reader["Condition"]),
                                Recommendation = reader["Recommendation"].ToString()
                            };

                            inspections.Add(inspection);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetCarInspectionBatch));
                return null;
            }
            return inspections;
        }
    }
}
