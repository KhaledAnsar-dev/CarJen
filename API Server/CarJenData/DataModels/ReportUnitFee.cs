using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class ReportUnitFee
{
    public int ReportUnitFeeId { get; set; }

    public int PackageId { get; set; }

    public int UnitFeeId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Package Package { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual MainFee UnitFee { get; set; } = null!;
}
