using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.Interfaces
{
    public interface IPrimaryInspectionData
    {
        string Name { get; set; }
        short? Condition { get; set; }
        string Recommendation { get; set; }

        void SavePrimaryData(DataTable table);

        IPrimaryInspectionData Find(int inspectionID);
    }

}
