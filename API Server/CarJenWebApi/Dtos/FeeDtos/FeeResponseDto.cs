using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.FeeDtos
{
    public class FeeResponseDto
    {
        [Required(ErrorMessage = "Amount is required")]
        public int? FeeID { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public string FeeType { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
