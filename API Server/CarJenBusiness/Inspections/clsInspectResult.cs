using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.Inspections
{
    // Class responsible for managing and saving inspection results for this form.
    public class clsInspectResult
    {
        public string Condition { get; set; }
        public string Recommendation { get; set; }
        public decimal? Measurement { get; set; }
        public int? CarInspectionID { get; set; } // only for serialization 
        public clsInspectResult()
        {
            Recommendation = "";
            Condition = null;
            Measurement = null;
            CarInspectionID = null;
        }
    }
}
