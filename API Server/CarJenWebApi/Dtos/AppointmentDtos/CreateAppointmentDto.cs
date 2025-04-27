using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.AppointmentDtos
{
    public class CreateAppointmentDto
    {
        [Required(ErrorMessage = "CarDocumentationID is required")]
        public int CarDocumentationID { get; set; }

        [Required(ErrorMessage = "AppointmentDate is required")]
        public DateTime AppointmentDate { get; set; }
    }
}
