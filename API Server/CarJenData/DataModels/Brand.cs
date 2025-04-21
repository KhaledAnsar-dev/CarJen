using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Brand
{
    public int BrandId { get; set; }

    public string Brand1 { get; set; } = null!;

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
