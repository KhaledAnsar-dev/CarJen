using CarJenBusiness.Interfaces;
using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.Inspections
{
    public class clsBrakeFluidLeakages : clsBaseInspection, IPrimaryInspectionData
    {
        public clsBrakeFluidLeakages()
        {
            base.Name = "BrakeFluidLeakages";
            base.Condition = null;
            base.Recommendation = null;
        }
        private clsBrakeFluidLeakages(short? condition, string recommendation)
        {
            Condition = condition;
            Recommendation = recommendation;
        }

        public IPrimaryInspectionData Find(int inspectionID)
        {
            short? condition = null; string recommendation = "";
            if (CarInspectionRepository.GetSingleInspectionResultByID("SP_GetBrakeFluidLeakagesByID", inspectionID, ref condition, ref recommendation))
                return new clsBrakeFluidLeakages(condition, recommendation);
            return null;
        }
        public void SavePrimaryData(DataTable Report)
        {
            DataRow dataRow = Report.NewRow();
            dataRow["Name"] = Name;
            dataRow["Condition"] = Condition;
            dataRow["Recommendation"] = Recommendation;
            Report.Rows.Add(dataRow);
        }
    }

}
