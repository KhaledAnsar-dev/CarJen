using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Report
{
    public int ReportId { get; set; }

    public int CarDocumentationId { get; set; }

    public DateTime ReleaseDate { get; set; }

    public byte Status { get; set; }

    public virtual CarDocumentation CarDocumentation { get; set; } = null!;

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
