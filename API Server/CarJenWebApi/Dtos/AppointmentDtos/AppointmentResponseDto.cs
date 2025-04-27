namespace CarJenWebApi.Dtos.AppointmentDtos
{
    public class AppointmentResponseDto
    {
        public int? AppointmentID { get; set; }
        public int? CarDocumentationID { get; set; }
        public int? SellerID { get; set; }
        public string SellerName { get; set; }
        public int? CarID { get; set; }
        public string Status { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PublishFee { get; set; }
    }
}
