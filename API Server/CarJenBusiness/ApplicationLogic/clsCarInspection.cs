using CarJenBusiness.ApplicationLogic;
using CarJenData.DataModels;
using CarJenData.Repositories;
using CarJenShared.Dtos.CarDocumentationDtos;
using CarJenShared.Dtos.CarInspectionDtos;
using CarJenShared.Dtos.ReportDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness
{
    public class clsCarInspection
    {
        public int? CarInspectionID { get; set; }
        public short? Status { get; set; }
        public int? CarDocumentationID { get; set; }
        public enum enStatus
        {
            Pending = 0,
            UnderInspection = 1,
            Approved = 2,
            Completed = 3,
            Failed = 4,
            Cancelled = 5
        };


        /// <summary>
        /// Creates an initial car inspection record without any detailed inspections.
        /// This is used to register a vehicle as ready to be inspected.
        /// </summary>
        public bool CreateInitialCarInspection()
        {
            this.CarInspectionID = CarInspectionRepository.CreateInitialCarInspection(CarDocumentationID, Status);
            return this.CarInspectionID != null;
        }

        /// <summary>
        /// Adds a batch of inspection items related to a specific car inspection.
        /// These items are stored as a pre-approved report (not final).
        /// </summary>
        /// <returns>True if the batch was saved successfully.</returns>
        public static bool AddCarInspectionBatch(InspectionBatchDto inspectionBatch)
        {
            // Convert the list of inspections to a DataTable to match the repository input format
            // and prepare it for batch insertion.

            var table = new DataTable();
            table.Columns.Add("Name", typeof(string)).MaxLength = 50;
            table.Columns.Add("Condition", typeof(short));
            table.Columns.Add("Recommendation", typeof(string)).MaxLength = 250;

            foreach (var inspection in inspectionBatch.Inspections)
            {
                table.Rows.Add(inspection.Name, inspection.Condition, inspection.Recommendation);
            }

            return CarInspectionRepository.AddCarInspectionBatch(table, inspectionBatch.Resume, (int)inspectionBatch.CarInspectionID);
        }
        static public List<InspectionDto> GetCarInspectionBatch(int? CarInspectionID)
        {
            return CarInspectionRepository.GetCarInspectionBatch(CarInspectionID);
        }
        static public InspectionBatchDto GetFullCarInspectionBatch(int? CarInspectionID)
        {
            var fullBatch = new InspectionBatchDto();

            fullBatch.CarInspectionID = CarInspectionID;
            fullBatch.Resume = FindResumeByCarInspectionID(CarInspectionID);
            fullBatch.Inspections = GetCarInspectionBatch(CarInspectionID);

            return fullBatch;
        }
        static public string FindResumeByCarInspectionID(int? carInspectionID)
        {
            return CarInspectionRepository.GetResumeByCarInspectionID(carInspectionID);
        }
        public static bool UpdateStatus(int CarInspectionID, short Status)
        {
            return CarInspectionRepository.UpdateCarInspectionStatus(CarInspectionID, Status); ;
        }
        static public bool Cancel(int CarInspectionID)
        {
            return UpdateStatus(CarInspectionID, (short)enStatus.Cancelled);
        }
    }
}

