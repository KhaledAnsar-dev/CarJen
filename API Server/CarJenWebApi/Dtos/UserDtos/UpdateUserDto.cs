using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.UserDtos
{
    using System.ComponentModel.DataAnnotations;

    // We use data annotation for fast validation
    public class UpdateUserDto
    {

        [Required(ErrorMessage = "National Number is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "National Number must be between 5 and 20 characters.")]
        public string NationalNumber { get; set; }

        // Person info
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Range(0, 1, ErrorMessage = "Gender must be 0 (Male) or 1 (Female).")]
        public short Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime? DateOfBirth { get; set; }

        [EmailAddress(ErrorMessage = "Email format is invalid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(50)]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        [StringLength(300)]
        public string Image { get; set; }

        // Role info
        [Required(ErrorMessage = "Role ID is required.")]
        [Range(1, 9, ErrorMessage = "Role ID must be a positive number.")]
        public int RoleID { get; set; }
    }

}
