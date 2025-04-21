namespace CarJenWebApi.Dtos.TeamDtos
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateTeamDto
    {
        [Required(ErrorMessage = "Team Code is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Team Code must be between 3 and 30 characters.")]
        public string TeamCode { get; set; } = null!;
    }
}
