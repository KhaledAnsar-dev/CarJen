using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class ImageCollection
{
    public int ImageCollectionId { get; set; }

    public int CarId { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
