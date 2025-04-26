using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.CarDto
{
    public class CarDto
    {
        public int? CarID { get; set; }
        public BrandDto Brand { get; set; }
        public ModelDto Model { get; set; }
        public TrimDto Trim { get; set; }
        public short? FuelType { get; set; }
        public int? Mileage { get; set; }
        public short? TransmissionType { get; set; }
        public short? Year { get; set; }
        public string Color { get; set; }
        public decimal? Price { get; set; }
        public string PlateNumber { get; set; }
        public DateTime? RegistrationExp { get; set; }
        public DateTime? TechInspectionExp { get; set; }
    }
}
