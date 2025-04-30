using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.CarInspectionDtos
{
    public class UpdateCarInspectionDto
    {
        [Required(ErrorMessage = "CarDocumentationID is required.")]
        public int CarDocumentationID { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public short Status { get; set; }

    }
}
