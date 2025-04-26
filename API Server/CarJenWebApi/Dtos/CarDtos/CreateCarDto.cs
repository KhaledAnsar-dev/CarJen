using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.CarDtos
{
    public class CreateCarDto
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
    }
}
