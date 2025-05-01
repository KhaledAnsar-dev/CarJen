using CarJenData.DataModels;
using CarJenShared.Dtos.CarInspectionDtos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using static CarJenShared.Helpers.Logger;
namespace CarJenData.Repositories
{
    public class CarInspectionRepository
    {
        public static int? CreateInitialCarInspection(int? carDocumentationID, short? status)
        {
            int? createdID = null;

            try
            {
                using (var context = new CarJenDbContext())
                {
                    var inspection = new CarInspection
                    {
                        CarDocumentationId = (int)carDocumentationID,
                        Status = (byte)status
                    };

                    context.CarInspections.Add(inspection);
                    context.SaveChanges();

                    createdID = inspection.CarInspectionId;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(CreateInitialCarInspection));
                return null;
            }

            return createdID;
        }
        public static bool UpdateCarInspectionStatus(int? carInspectionID, short? status)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var inspection = context.CarInspections.Find(carInspectionID);
                    if (inspection == null)
                        return false;

                    inspection.Status = (byte)status;
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdateCarInspectionStatus));
            }

            return false;
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
                LogError(ex, nameof(AddCarInspectionBatch));
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
                    Command.Parameters.AddWithValue("@CarInspectionID", CarInspectionID);
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
        static public string GetResumeByCarInspectionID(int? carInspectionID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var resumeRecord = context.Resumes
                        .FirstOrDefault(r => r.CarInspectionId == carInspectionID);

                    return resumeRecord?.Resume1;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetResumeByCarInspectionID));
                return null;
            }
        }

    }
}
