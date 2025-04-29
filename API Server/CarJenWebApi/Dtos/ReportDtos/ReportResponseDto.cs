namespace CarJenWebApi.Dtos.ReportDtos
{
    public class ReportResponseDto
    {
        public int FileId { get; set; }
        public DateTime ExpDate { get; set; }
        public string InspectedBy { get; set; }
        public int SellerId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Status { get; set; }
    }
}
