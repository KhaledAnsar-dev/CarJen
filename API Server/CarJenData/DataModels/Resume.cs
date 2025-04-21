using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Resume
{
    public int ResumeId { get; set; }

    public int CarInspectionId { get; set; }

    public string? Resume1 { get; set; }

    public virtual CarInspection CarInspection { get; set; } = null!;
}
