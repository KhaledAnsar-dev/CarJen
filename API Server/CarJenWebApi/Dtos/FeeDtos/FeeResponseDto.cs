using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.FeeDtos
{
    public class FeeResponseDto
    {
        public int? FeeID { get; set; }

        public string FeeType { get; set; }

        public decimal? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
