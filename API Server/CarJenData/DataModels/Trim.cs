using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Trim
{
    public int TrimId { get; set; }

    public int ModelId { get; set; }

    public string Trim1 { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual Model Model { get; set; } = null!;
}
