using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.CarInspectionDtos
{
    public class PendingTechnicalInspectionCarDto
    {
        public int? CarInspectionID { get; set; }
        public string CarOwner { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public int? Year { get; set; }
        public string Fuel { get; set; }
        public string Status { get; set; }
    }
}
