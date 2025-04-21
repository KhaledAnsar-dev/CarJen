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
    public class clsEngineSounds : clsBaseInspection, IPrimaryInspectionData
    {

        public clsEngineSounds()
        {
            base.Name = "EngineSounds";
            base.Condition = null;
            base.Recommendation = null;
        }
        private clsEngineSounds(short? condition, string recommendation)
        {
            Condition = condition;
            Recommendation = recommendation;
        }

        public IPrimaryInspectionData Find(int inspectionID)
        {
            short? condition = null; string recommendation = "";
            if (CarInspectionRepository.GetSingleInspectionResultByID("SP_GetEngineSoundsByID", inspectionID, ref condition, ref recommendation))
                return new clsEngineSounds(condition, recommendation);
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
