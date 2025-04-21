using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class CarTeamUpdate
{
    public int CarTeamUpdateId { get; set; }

    public int CarDocumentationId { get; set; }

    public int TeamId { get; set; }

    public DateTime CheckedDate { get; set; }

    public virtual CarDocumentation CarDocumentation { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
