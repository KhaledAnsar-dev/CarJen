using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsInspectionPayment
    {
        public int? InspectionPaymentID { get; set; }
        public int? AppointmentID { get; set; }
        public int? InspectionFeeID { get; set; }
        public DateTime? PaymentDate { get; set; }
        public byte? PaymentMethod { get; set; }

        public enum enPaymentMethod { ElDahabia = 0, CIB = 1, Visa = 2, MasterCard = 3, Paypal = 4, Cash = 5 };

        public clsInspectionPayment()
        {
            InspectionPaymentID = null;
            AppointmentID = null;
            InspectionFeeID = null;
            PaymentMethod = null;
            PaymentDate = null;
        }

        public bool AddPayment()
        {
            InspectionPaymentID = InspectionPaymentRepository.AddPayment(AppointmentID, InspectionFeeID, PaymentMethod);
            return InspectionPaymentID != null;
        }

    }

}
