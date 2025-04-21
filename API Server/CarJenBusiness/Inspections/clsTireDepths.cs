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
    public class clsTireDepths : clsBaseInspection, IPrimaryInspectionData, IDetailedInspectionData
    {
        public decimal Measurement { get; set; }

        public clsTireDepths()
        {
            base.Name = "TireDepths";
            base.Condition = null;
            base.Recommendation = null;
        }
        private clsTireDepths(short? condition, string recommendation)
        {
            Condition = condition;
            Recommendation = recommendation;
        }

        public IPrimaryInspectionData Find(int inspectionID)
        {
            short? condition = null; string recommendation = "";
            if (CarInspectionRepository.GetSingleInspectionResultByID("SP_GetTireDepthsByID", inspectionID, ref condition, ref recommendation))
                return new clsTireDepths(condition, recommendation);
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
        public void SaveDetailedData(DataTable Report)
        {
            DataRow dataRow = Report.NewRow();
            dataRow["Measurement"] = Measurement;
            Report.Rows.Add(dataRow);
        }

    }

}
