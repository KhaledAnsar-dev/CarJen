using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class AppointmentsWatcherLog
{
    public int Id { get; set; }

    public string? LogType { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }
}
