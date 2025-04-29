using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.CarInspectionDtos
{
    public class InspectionBatchDto
    {
        public int? CarInspectionID { get; set; }
        public string Resume { get; set; }
        public List<InspectionDto> Inspections { get; set; }
    }
}
