using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public int CustomerId { get; set; }

    public int ServiceId { get; set; }

    public int ReportUnitFeeId { get; set; }

    public DateTime SubscriptionDate { get; set; }

    public byte PaymentMethod { get; set; }

    public int AvailbleReports { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ReportUnitFee ReportUnitFee { get; set; } = null!;

    public virtual MainFee Service { get; set; } = null!;
}
