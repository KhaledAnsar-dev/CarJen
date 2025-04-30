using CarJenShared.Dtos.CarInspectionDtos;
using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.CarInspectionDtos
{
    public class AddInspectionBatchDto
    {
        [Required(ErrorMessage = "CarInspectionID is required")]
        public int? CarInspectionID { get; set; }
        [Required(ErrorMessage = "Resume is required")]
        public string Resume { get; set; }
        [Required(ErrorMessage = "Inspection are required")]
        public List<InspectionDto> Inspections { get; set; }
    }
}
