using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class PreApprovedCarsView
{
    public int FileId { get; set; }

    public string CarOwner { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Trim { get; set; } = null!;

    public short Year { get; set; }

    public string Fuel { get; set; } = null!;

    public string Status { get; set; } = null!;
}
