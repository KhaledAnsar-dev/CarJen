using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.PackageDto
{
    public class PackageResponseDto
    {
        public int? PackageID { get; set; }
        public string Title { get; set; }
        public int? NumberOfReports { get; set; }
        public string CreatedByUser { get; set; }
        public decimal? ReportFee { get; set; }
    }
}
