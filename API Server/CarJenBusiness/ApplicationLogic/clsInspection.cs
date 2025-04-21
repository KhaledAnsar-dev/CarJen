using CarJenBusiness.Interfaces;
using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsInspection
    {
        public void AddPrimaryDataToTable(IPrimaryInspectionData inspection, DataTable Report)
        {
            // The table is passed as a reference , so every inspection will add a row
            inspection.SavePrimaryData(Report);
        }
        public bool SaveFullReport(DataTable Report, string Resume, int CarInspectionID)
        {
            return InspectionRepository.SaveFullReport(Report, Resume, CarInspectionID);
        }

        public void AddDetailedDataToTable(IDetailedInspectionData inspection, DataTable Report)
        {
            // The table is passed as a reference , so every inspection will add a row
            inspection.SaveDetailedData(Report);
        }
        public bool SaveFullReport(DataTable PrimaryReport, DataTable DetailedData, string Resume)
        {
            // Some logic
            return true;
        }


    }

}
