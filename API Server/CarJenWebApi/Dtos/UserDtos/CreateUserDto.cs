namespace CarJenWebApi.Dtos.UserDtos
{
    using System.ComponentModel.DataAnnotations;

    public class CreateUserDto
    {
        // Person Info
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Range(0, 1, ErrorMessage = "Gender must be 0 (Male) or 1 (Female).")]
        public short Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(300)]
        public string? Image { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        [StringLength(50, MinimumLength = 4)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        // User Info
        [Required(ErrorMessage = "Role ID is required.")]
        [Range(1, 9, ErrorMessage = "Role ID must be between 1 and 9.")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "National Number is required.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "National Number must be between 5 and 20 characters.")]
        public string NationalNumber { get; set; }

        public int? CreatedByUserId { get; set; }
    }
}
