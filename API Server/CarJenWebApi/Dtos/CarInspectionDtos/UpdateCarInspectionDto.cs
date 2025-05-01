using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.CarInspectionDtos
{
    public class UpdateCarInspectionDto
    {
        [Required(ErrorMessage = "CarInspectionID is required.")]
        public int CarInspectionID { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public short Status { get; set; }

    }
}
