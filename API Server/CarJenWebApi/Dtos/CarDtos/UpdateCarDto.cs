using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.CarDtos
{
    public class UpdateCarDto
    {
        [Required(ErrorMessage = "TrimID is required.")]
        public int? TrimID { get; set; }

        [Required(ErrorMessage = "FuelType is required.")]
        public short? FuelType { get; set; }

        [Required(ErrorMessage = "Mileage is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Mileage must be a non-negative number.")]
        public int? Mileage { get; set; }

        [Required(ErrorMessage = "TransmissionType is required.")]
        public short? TransmissionType { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1900, 2050, ErrorMessage = "Year must be a valid year.")]
        public short? Year { get; set; }
        [Required(ErrorMessage = "Color is required.")]
        [StringLength(50, ErrorMessage = "Color cannot be longer than 50 characters.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "PlateNumber is required.")]
        [StringLength(20, ErrorMessage = "PlateNumber cannot be longer than 20 characters.")]
        public string PlateNumber { get; set; }

        [Required(ErrorMessage = "RegistrationExp is required.")]
        public DateTime? RegistrationExp { get; set; }

        [Required(ErrorMessage = "TechInspectionExp is required.")]
        public DateTime? TechInspectionExp { get; set; }
    }
}
