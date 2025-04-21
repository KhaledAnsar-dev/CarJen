using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class CoolantLeakage
{
    public int CoolantLeakageId { get; set; }

    public int CarInspectionId { get; set; }

    public byte Condition { get; set; }

    public string? Recommendation { get; set; }

    public virtual CarInspection CarInspection { get; set; } = null!;
}
