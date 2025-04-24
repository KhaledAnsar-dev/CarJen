using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.MemeberDtos
{
    public class CreateMemberDto
    {
        [Required(ErrorMessage = "Team Id is required")]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "User Id is required")]
        public int UserId { get; set; }
    }
}
