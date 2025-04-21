using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Package
{
    public int PackageId { get; set; }

    public int CreatedByUser { get; set; }

    public string Title { get; set; } = null!;

    public int NumberOfReports { get; set; }

    public virtual User CreatedByUserNavigation { get; set; } = null!;

    public virtual ICollection<ReportUnitFee> ReportUnitFees { get; set; } = new List<ReportUnitFee>();
}
