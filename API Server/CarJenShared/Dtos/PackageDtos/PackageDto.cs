using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.PackageDtos
{
    public class PackageDto
    {
        public int? PackageID { get; set; }
        public string Title { get; set; }
        public int? NumberOfReports { get; set; }
        public int? CreatedByUserID { get; set; }
    }
}
