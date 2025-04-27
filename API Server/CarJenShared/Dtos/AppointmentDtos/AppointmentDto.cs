using CarJenShared.Dtos.CarDocumentationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.AppointmentDtos
{
    public class AppointmentDto
    {
        public int? AppointmentID { get; set; }
        public int? PublishFeeID { get; set; }
        public decimal PublishFee { get; set; }
        public int? CarDocumentationID { get; set; }
        public string Status { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public CarDocumentationDto CarDocumentation { get; set; }
    }
}
