using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.ReportDtos
{
    public class UpdateReportDto
    {
        [Required(ErrorMessage = "ReportID is required.")]
        public int ReportID { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public short Status { get; set; }

    }
}
