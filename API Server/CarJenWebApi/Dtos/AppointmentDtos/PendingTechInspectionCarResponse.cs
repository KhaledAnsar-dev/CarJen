namespace CarJenWebApi.Dtos.AppointmentDtos
{
    public class PendingTechInspectionCarResponse
    {
        public int? CarInspectionID { get; set; }
        public string CarOwner { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public int? Year { get; set; }
        public string Fuel { get; set; }
        public string Status { get; set; }
    }
}
