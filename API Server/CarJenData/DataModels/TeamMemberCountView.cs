using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class TeamMemberCountView
{
    public int TeamId { get; set; }

    public int? TotalMembers { get; set; }

    public byte TeamType { get; set; }
}
