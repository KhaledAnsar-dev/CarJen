using CarJenShared.Dtos.CarInspectionDtos;

namespace CarJenWebApi.Dtos.CarInspectionDtos
{
    public class InspectionBatchResponseDto
    {
        public string Resume { get; set; }
        public List<InspectionDto> InspectionBatch { get; set; }
    }
}
