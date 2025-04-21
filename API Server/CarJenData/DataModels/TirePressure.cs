using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class TirePressure
{
    public int PressureId { get; set; }

    public int CarInspectionId { get; set; }

    public byte Condition { get; set; }

    public string? Recommendation { get; set; }

    public virtual CarInspection CarInspection { get; set; } = null!;
}
