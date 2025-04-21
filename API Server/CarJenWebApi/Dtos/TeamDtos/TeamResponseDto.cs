using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.TeamDtos
{
    public class TeamResponseDto
    {
        [Required(ErrorMessage = "")]
        public int TeamId { get; set; }

        public string TeamCode { get; set; } = null!;

        public string TeamType { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
