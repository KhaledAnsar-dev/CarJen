namespace CarJenWebApi.Dtos.TeamDtos
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTeamDto
    {
        [Required(ErrorMessage = "Team Code is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Team Code must be between 3 and 30 characters.")]
        public string TeamCode { get; set; } = null!;

        [Required(ErrorMessage = "Team type is required.")]
        [Range(1, 2, ErrorMessage = "Team Type must be 1 or 2")]
        public short? TeamType { get; set; }

        [Required(ErrorMessage = "Created By User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Created By User ID must be greater than 0.")]
        public int CreatedByUserId { get; set; }
    }
}
