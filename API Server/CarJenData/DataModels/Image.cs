using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Image
{
    public int ImageId { get; set; }

    public int ImageCollectionId { get; set; }

    public string ImagePath { get; set; } = null!;

    public byte ViewType { get; set; }

    public virtual ImageCollection ImageCollection { get; set; } = null!;
}
