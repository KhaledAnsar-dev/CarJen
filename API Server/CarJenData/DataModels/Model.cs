using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Model
{
    public int ModelId { get; set; }

    public int BrandId { get; set; }

    public string Model1 { get; set; } = null!;

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Trim> Trims { get; set; } = new List<Trim>();
}
