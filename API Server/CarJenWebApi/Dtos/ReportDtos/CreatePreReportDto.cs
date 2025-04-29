using CarJenShared.Dtos.CarInspectionDtos;
using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.ReportDtos
{

    public class CreatePreReportDto
    {
        [Required(ErrorMessage = "CarInspectionID is required.")]
        public int CarInspectionID { get; set; }

        [Required(ErrorMessage = "Condition is required.")]
        [Range(1, 3, ErrorMessage = "Condition must be between 1 or 3")]
        public short? Condition { get; set; }

        [Required(ErrorMessage = "Resume is required.")]
        public string Resume { get; set; }

        [Required(ErrorMessage = "Inspections is required.")]
        public List<InspectionDto> Inspections { get; set; }

    }
}
