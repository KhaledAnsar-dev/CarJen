using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.Inspections
{
    public class clsBaseInspection
    {
        public string Name { get; set; }
        public short? Condition { get; set; }
        public string Recommendation { get; set; }

    }
}
