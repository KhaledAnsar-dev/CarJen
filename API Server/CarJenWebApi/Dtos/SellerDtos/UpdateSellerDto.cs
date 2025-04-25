using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.SellerDtos
{
    public class UpdateSellerDto
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "NationalNumber is required.")]
        public string NationalNumber { get; set; }
    }
}
