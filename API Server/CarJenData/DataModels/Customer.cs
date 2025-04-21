using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int PersonId { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
