using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.FeeDtos
{
    public class UpdateFeeDto
    {
        [Required(ErrorMessage = "Fee typeID is required")]
        public byte FeeTypeID { get; set; }

        [Required(ErrorMessage ="Amount is required")]
        public decimal? Amount { get; set; }
    }
}
