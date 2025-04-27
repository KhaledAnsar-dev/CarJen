using CarJenData.DataModels;
using CarJenShared.Dtos.InspectionPaymentDtos;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;
using static CarJenShared.Helpers.Logger;

namespace CarJenData.Repositories
{
    public class InspectionPaymentRepository
    {
        public static int? AddPayment(InspectionPaymentDto inspectionPaymentDto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var payment = new InspectionPayment
                    {
                        AppointmentId = inspectionPaymentDto.AppointmentID?? 0,
                        InspectionFeeId = inspectionPaymentDto.InspectionFeeID ?? 0,
                        PaymentDate = DateTime.Now,
                        PaymentMethod = inspectionPaymentDto.PaymentMethod ?? 0
                    };

                    context.InspectionPayments.Add(payment);
                    context.SaveChanges();

                    return payment.InspectionPaymentId;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(AddPayment));
                return null;
            }
        }
    }
}
