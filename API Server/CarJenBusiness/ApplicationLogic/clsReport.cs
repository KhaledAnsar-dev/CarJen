using CarJenData.DataModels;
using CarJenData.Repositories;
using CarJenShared.Dtos.ReportDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsReport
    {
        public int? reportID { get; set; }
        public int? carDocumentationID { get; set; }
        public enum enStatus
        {
            Active = 1,
            DisabledBySeller = 2,
            DisabledByTeam = 3,
            Sold = 4,
            Canceled = 5

        }

        public clsReport()
        {
        }

        public bool AddReport(int carDocumentation)
        {
            this.reportID = ReportRepository.AddReport(carDocumentation);
            return this.reportID != null;
        }
        public static bool UpdateStatus(int reportID, short status)
        {
            return ReportRepository.UpdateReportStatus(reportID, status); ;
        }
        static public List<ReportDto> GetAllReports()
        {
            return ReportRepository.GetAllReports();
        }

    }

}
