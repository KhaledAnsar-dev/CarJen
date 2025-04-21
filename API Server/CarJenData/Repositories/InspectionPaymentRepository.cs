using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

namespace CarJenData.Repositories
{
    public class InspectionPaymentRepository
    {
        public static int? AddPayment(int? appointmentID, int? inspectionFeeID, byte? paymentMethod)
        {
            int? createdID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddInspectionPayment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@AppointmentID", appointmentID);
                        command.Parameters.AddWithValue("@InspectionFeeID", inspectionFeeID);
                        command.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                        command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);

                        SqlParameter outputID = new SqlParameter("@InspectionPaymentID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputID);

                        connection.Open();

                        command.ExecuteNonQuery();
                        createdID = (int)command.Parameters["@InspectionPaymentID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // string methodName = MethodBase.GetCurrentMethod().Name;
                // clsEventLogger.LogError(ex.Message, methodName);
            }

            return createdID;
        }
    }
}
