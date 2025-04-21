using CarJenData.Repositories;
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
        public DateTime? releaseDate { get; set; }
        public short? status { get; set; }

        public clsCarInspection carInspection { get; set; }
        public enum enStatus
        {
            Active = 1,
            DisabledBySeller = 2,
            DisabledByTeam = 3,
            Sold = 4,
            Canceled = 5

        }
        private clsReport(int? reportID, int? carDocumentationID, DateTime? releasedDate, short? status)
        {
            this.reportID = reportID;
            this.carDocumentationID = carDocumentationID;
            this.releaseDate = releasedDate;
            this.status = status;

            //carInspection = clsCarDocumentation.Find(carDocumentationID).;
        }

        public clsReport()
        {
            reportID = null;
            carDocumentationID = null;
            releaseDate = null;
            status = null;
        }

        public bool AddApprovedReport(int carDocumentation)
        {
            this.reportID = ReportRepository.AddApprovedReport(carDocumentation);
            return this.reportID != null;
        }
        public static bool UpdateStatus(int reportID, short status)
        {
            return ReportRepository.UpdateReportStatus(reportID, status); ;
        }
        static public DataTable GetAllApprovedReports()
        {
            return ReportRepository.GetAllApprovedReports();
        }
        static public clsReport Find(int? reportID)
        {
            int? carDocumentationID = null;
            short? status = null;
            DateTime? releasedDate = null;

            if (ReportRepository.GetReportByID(reportID, ref carDocumentationID,
                ref releasedDate, ref status))
            {
                return new clsReport(reportID, carDocumentationID, releasedDate, status);
            }
            else
                return null;

        }

    }

}
