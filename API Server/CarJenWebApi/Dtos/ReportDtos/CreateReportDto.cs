using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.ReportDtos
{
    public class CreateReportDto
    {
        [Required(ErrorMessage = "CarDocumentationID is required.")]
        public int CarDocumentationID { get; set; }
    }
}
