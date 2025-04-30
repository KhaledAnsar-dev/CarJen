using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.PackageDtos;
using CarJenShared.Dtos.ReportDtos;
using CarJenWebApi.Dtos.PackageDto;
using CarJenWebApi.Dtos.ReportDtos;
using Mapster;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class ReportMapper
    {
        public static clsReport ToclsReport(CreateReportDto report)
        {
            return new clsReport
            {
                CarDocumentationID = report.CarDocumentationID
            };
        }
        public static ReportResponseDto ToReportResponseDto(ReportDto report)
        {
            return report.Adapt<ReportResponseDto>();
        }
    }
}
