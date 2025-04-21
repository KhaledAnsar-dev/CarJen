using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class InspectionRepository
    {
        private static void _CreateTempTable(SqlConnection connection)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_CreateTempForInspections", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }
        }

        private static void _InsertIntoTempTable(SqlConnection connection, DataTable inspections)
        {
            try
            {
                foreach (DataRow row in inspections.Rows)
                {
                    using (SqlCommand command = new SqlCommand("SP_InsertIntoTempTable", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TargetTable", row["Name"]);
                        command.Parameters.AddWithValue("@Condition", row["Condition"]);
                        command.Parameters.AddWithValue("@Recommandation", row["Recommandation"]);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }
        }

        private static void _InsertIntoTempTableUsingType(SqlConnection connection, DataTable inspections)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP_InsertIntoTempTableUsingType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InspectionsType", inspections);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }
        }

        private static bool _SaveInspectionsTemp(SqlConnection connection, int carInspectionID, string resume)
        {
            bool isSaved = false;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_SaveInspectionsTemp", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarInspectionID", carInspectionID);
                    command.Parameters.AddWithValue("@Resume", resume);
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

        public static bool SaveFullReport(DataTable report, string resume, int carInspectionID)
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
    }
}

