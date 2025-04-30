using CarJenBusiness;
using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.CarInspectionDtos;
using CarJenShared.Dtos.ReportDtos;
using CarJenWebApi.Dtos.CarInspectionDtos;
using CarJenWebApi.Dtos.ReportDtos;
using Mapster;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class CarInspectionMapper
    {
        public static clsCarInspection ToclsCarInspection(CreateCarInspectionDto carInspection)
        {
            return new clsCarInspection
            {
                CarDocumentationID = carInspection.CarDocumentationID,
                Status = (short)clsCarInspection.enStatus.Pending
            };
        }
        public static InspectionBatchDto ToInspectionBatchDto(AddInspectionBatchDto batch)
        {
            return batch.Adapt<InspectionBatchDto>();
        }
        public static InspectionBatchResponseDto ToInspectionBatchResponseDto(InspectionBatchDto batch)
        {
            return new InspectionBatchResponseDto
            {
                Resume = batch.Resume,
                InspectionBatch = batch.Inspections
            };
        }
    }
}
