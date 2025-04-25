namespace CarJenWebApi.Dtos.SellerDtos
{
    public class SellerResponseDto
    {
        public int? SellerID { get; set; }
        public string NationalNumber { get; set; }
        public decimal? Earnings { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
    }
}
