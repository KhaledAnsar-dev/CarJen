using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.InspectionPaymentDtos
{
    public class CreateInspectionPaymentDto
    {
        [Required(ErrorMessage = "Appointment ID is required.")]
        public int AppointmentID { get; set; }

        [Required(ErrorMessage = "Inspection Fee ID is required.")]
        public int InspectionFeeID { get; set; }

        [Required(ErrorMessage = "Payment Method is required.")]
        [Range(0, 5, ErrorMessage = "Payment method must be between 0 and 5.")]
        public byte PaymentMethod { get; set; }
    }
}
