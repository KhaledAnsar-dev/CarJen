using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.Interfaces
{
    public interface IDetailedInspectionData
    {
        string Name { get; set; }
        decimal Measurement { get; set; }

        void SaveDetailedData(DataTable table);
        //bool Update();
    }
}
