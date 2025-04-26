namespace CarJenWebApi.Dtos.CarDtos
{
    public class CarResponseDto
    {
        public int? CarID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public short? FuelType { get; set; }
        public int? Mileage { get; set; }
        public short? TransmissionType { get; set; }
        public short? Year { get; set; }
        public string Color { get; set; }
        public decimal? Price { get; set; }
        public string PlateNumber { get; set; }
        public DateTime? RegistrationExp { get; set; }
        public DateTime? TechInspectionExp { get; set; }
    }
}
