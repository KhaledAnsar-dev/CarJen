using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Seller
{
    public int SellerId { get; set; }

    public int PersonId { get; set; }

    public string NationalNumber { get; set; } = null!;

    public decimal Earnings { get; set; }

    public virtual ICollection<CarDocumentation> CarDocumentations { get; set; } = new List<CarDocumentation>();

    public virtual Person Person { get; set; } = null!;
}
