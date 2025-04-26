namespace CarJenWebApi.Dtos.CarDocumentationDtos
{
    public class CarDocSummaryDto
    {
        public int? CarDocumentationID { get; set; }
        public int? SellerID { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Trim { get; set; } = string.Empty;
        public int? Mileage { get; set; }
        public short? Year { get; set; }
    }
}
