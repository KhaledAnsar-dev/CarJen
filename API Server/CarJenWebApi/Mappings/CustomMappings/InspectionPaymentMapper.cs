using CarJenBusiness.ApplicationLogic;
using CarJenWebApi.Dtos.AppointmentDtos;
using CarJenWebApi.Dtos.InspectionPaymentDtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class InspectionPaymentMapper
    {
        public static clsInspectionPayment ToclsInspectionPayment(CreateInspectionPaymentDto createDto)
        {
            return new clsInspectionPayment
            {
                AppointmentID = createDto.AppointmentID,
                InspectionFeeID = createDto.InspectionFeeID,
                PaymentMethod = createDto.PaymentMethod
            };

        }

    }
}
