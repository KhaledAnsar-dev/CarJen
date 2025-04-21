using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class InspectionPayment
{
    public int InspectionPaymentId { get; set; }

    public int AppointmentId { get; set; }

    public int InspectionFeeId { get; set; }

    public DateTime PaymentDate { get; set; }

    public byte PaymentMethod { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual MainFee InspectionFee { get; set; } = null!;
}
