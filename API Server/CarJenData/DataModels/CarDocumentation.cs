using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class CarDocumentation
{
    public int CarDocumentationId { get; set; }

    public int SellerId { get; set; }

    public int CarId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Car Car { get; set; } = null!;

    public virtual ICollection<CarInspection> CarInspections { get; set; } = new List<CarInspection>();

    public virtual ICollection<CarTeamUpdate> CarTeamUpdates { get; set; } = new List<CarTeamUpdate>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual Seller Seller { get; set; } = null!;
}
