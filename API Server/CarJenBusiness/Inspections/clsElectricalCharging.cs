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
    public class clsElectricalCharging : clsBaseInspection, IPrimaryInspectionData
    {
        public clsElectricalCharging()
        {
            base.Name = "ElectricalCharging";
            base.Condition = null;
            base.Recommendation = null;
        }
        private clsElectricalCharging(short? condition, string recommendation)
        {
            Condition = condition;
            Recommendation = recommendation;
        }

        public IPrimaryInspectionData Find(int inspectionID)
        {
            short? condition = null; string recommendation = "";
            if (CarInspectionRepository.GetSingleInspectionResultByID("SP_GetElectricalChargingByID", inspectionID, ref condition, ref recommendation))
                return new clsElectricalCharging(condition, recommendation);
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
