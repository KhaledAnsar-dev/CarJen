using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.CarInspectionDtos
{
    public class CreateCarInspectionDto
    {
        [Required(ErrorMessage = "CarDocumentationID is required.")]
        public int CarDocumentationID { get; set; }
    }
}
