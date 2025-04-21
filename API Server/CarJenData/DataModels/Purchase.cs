using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int ReportId { get; set; }

    public int CustomerId { get; set; }

    public DateTime PurchaseDate { get; set; }

    public decimal ReportPrice { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Report Report { get; set; } = null!;
}
