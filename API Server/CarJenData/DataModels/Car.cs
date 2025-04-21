using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Car
{
    public int CarId { get; set; }

    public int TrimId { get; set; }

    public byte FuelType { get; set; }

    public int Mileage { get; set; }

    public byte TransmissionType { get; set; }

    public short Year { get; set; }

    public string? Color { get; set; }

    public decimal? Price { get; set; }

    public string? PlateNumber { get; set; }

    public DateTime? RegistrationExp { get; set; }

    public DateTime? TechInspectionExp { get; set; }

    public virtual ICollection<CarDocumentation> CarDocumentations { get; set; } = new List<CarDocumentation>();

    public virtual ICollection<ImageCollection> ImageCollections { get; set; } = new List<ImageCollection>();

    public virtual Trim Trim { get; set; } = null!;
}
