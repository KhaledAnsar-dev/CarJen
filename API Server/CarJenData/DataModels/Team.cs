using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamCode { get; set; } = null!;

    public byte TeamType { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<CarInspection> CarInspections { get; set; } = new List<CarInspection>();

    public virtual ICollection<CarTeamUpdate> CarTeamUpdates { get; set; } = new List<CarTeamUpdate>();

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
}
