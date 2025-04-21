using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? PublishFeeId { get; set; }

    public int CarDocumentationId { get; set; }

    public byte Status { get; set; }

    public DateTime AppointmentDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual CarDocumentation CarDocumentation { get; set; } = null!;

    public virtual ICollection<InspectionPayment> InspectionPayments { get; set; } = new List<InspectionPayment>();

    public virtual MainFee? PublishFee { get; set; }
}
