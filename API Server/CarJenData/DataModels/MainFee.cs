using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class MainFee
{
    public int MainFeeId { get; set; }

    public byte MainFeeType { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal Amount { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<InspectionPayment> InspectionPayments { get; set; } = new List<InspectionPayment>();

    public virtual ICollection<ReportUnitFee> ReportUnitFees { get; set; } = new List<ReportUnitFee>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
