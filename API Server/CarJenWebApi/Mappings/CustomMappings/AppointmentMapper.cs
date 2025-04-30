using CarJenBusiness.ApplicationLogic;
using CarJenData.DataModels;
using CarJenShared.Dtos.AppointmentDtos;
using CarJenShared.Dtos.CarDocumentationDtos;
using CarJenWebApi.Dtos.AppointmentDtos;
using CarJenWebApi.Dtos.CarDocumentationDtos;
using Mapster;
using System.Runtime.CompilerServices;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class AppointmentMapper
    {
        public static clsAppointment ToclsAppointment(CreateAppointmentDto createAppointmentDto)
        {
            return new clsAppointment
            {
                CarDocumentationID = createAppointmentDto.CarDocumentationID,
                AppointmentDate = createAppointmentDto.AppointmentDate
            };

        }
        public static AppointmentResponseDto ToAppointmentResponseDto(AppointmentDto appointmentDto)
        {
            return new AppointmentResponseDto
            {
                AppointmentID = appointmentDto.AppointmentID,
                CarDocumentationID = appointmentDto.CarDocumentationID,
                Status = appointmentDto.Status,
                AppointmentDate = Convert.ToDateTime(appointmentDto.AppointmentDate),
                PublishFee = appointmentDto.PublishFee,
                SellerID = appointmentDto.CarDocumentation.Seller.SellerId,
                SellerName = appointmentDto.CarDocumentation.Seller.Person.FullName,
                CarID = appointmentDto.CarDocumentation.Car.CarID
            };
        }
        public static PendingTechInspectionCarResponse ToPendingTechInspectionCarResponse(PendingTechnicalInspectionCarDto pendingDto)
        {
            return pendingDto.Adapt<PendingTechInspectionCarResponse>();
        }
    }
}
