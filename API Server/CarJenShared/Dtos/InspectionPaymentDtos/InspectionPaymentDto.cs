using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.InspectionPaymentDtos
{
    public class InspectionPaymentDto
    {
        public int? InspectionPaymentID { get; set; }
        public int? AppointmentID { get; set; }
        public int? InspectionFeeID { get; set; }
        public byte? PaymentMethod { get; set; }
    }
}
