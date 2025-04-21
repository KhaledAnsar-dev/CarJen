using CarJenBusiness.Inspections;
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
    public class clsCarInspection
    {
        public int? CarInspectionID { get; set; }
        public int? TeamID { get; set; }
        public int? CarDocumentationID { get; set; }
        public short? Status { get; set; }
        public DateTime? ExpDate { get; set; }

        // Dictionary to store all inspection IDs from the database, filtered by the current CarInspectionID.
        // This helps in mapping each inspection to its corresponding data.
        private Dictionary<string, int> inspectionsIDs = new Dictionary<string, int>();


        // Dictionary to contain all inspection objects, enabling retrieval of their results. 
        // Note: Both dictionaries share a common key, which is the inspection name.
        // This allows linking an inspection ID to its corresponding inspection object. 
        public Dictionary<string, IPrimaryInspectionData> inspections = new Dictionary<string, IPrimaryInspectionData>()
        {
            {new clsEngineSounds().Name,new clsEngineSounds()},
            {new clsEngineVibrations().Name,new clsEngineVibrations()},
            {new clsEngineAuthenticities().Name,new clsEngineAuthenticities()},
            {new clsBrakePads().Name,new clsBrakePads()},
            {new clsBrakePipes().Name,new clsBrakePipes()},
            {new clsBrakeRotors().Name,new clsBrakeRotors()},
            {new clsPaintConditions().Name,new clsPaintConditions()},
            {new clsBodyConditions().Name,new clsBodyConditions()},
            {new clsFrontLights().Name,new clsFrontLights()},
            {new clsRearLights().Name,new clsRearLights()},
            {new clsElectricalCharging().Name,new clsElectricalCharging()},
            {new clsElectricalWiring().Name,new clsElectricalWiring()},
            {new clsEngineTemperatures().Name,new clsEngineTemperatures()},
            {new clsCoolingConditions().Name,new clsCoolingConditions()},
            {new clsHeatingConditions().Name,new clsHeatingConditions()},
            {new clsAirbagConditions().Name,new clsAirbagConditions()},
            {new clsTireDepths().Name,new clsTireDepths()},
            {new clsTirePressures().Name,new clsTirePressures()},
            {new clsEngineOilLeakages().Name,new clsEngineOilLeakages()},
            {new clsCoolantLeakages().Name,new clsCoolantLeakages()},
            {new clsBrakeFluidLeakages().Name,new clsBrakeFluidLeakages()}
        };

        enum enMode { Add = 0, Update = 1 };
        enMode Mode;

        public enum enStatus
        {
            Pending = 0,
            UnderInspection = 1,
            Approved = 2,
            Completed = 3,
            Failed = 4,
            Cancelled = 5
        };
        public enum enCondition { Normal = 1, Acceptable = 2, Bad = 3 }
        private clsCarInspection(int? carInspectionID, int? teamID, int? carDocumentationID, short? status, DateTime? expDate)
        {
            CarInspectionID = carInspectionID;
            TeamID = teamID;
            CarDocumentationID = carDocumentationID;
            Status = status;
            ExpDate = expDate;


            // Retrieve all inspection IDs from their respective tables, allowing us to fetch their complete data later. 
            inspectionsIDs = FindReportInspectionIDs(CarInspectionID);


            foreach (var inspection in inspectionsIDs)
            {
                // Retrieve the appropriate inspection object using its corresponding key. 
                if (inspections.TryGetValue(inspection.Key, out var foundedInspect))
                {
                    // Retrieve actual inspection data from the database and populate the corresponding empty objects 
                    inspections[inspection.Key] = foundedInspect.Find(inspection.Value);
                }
            }

            Mode = enMode.Update;
        }
        public clsCarInspection()
        {
            CarInspectionID = null;
            TeamID = null;
            CarDocumentationID = null;
            Status = null;
            ExpDate = null;

            Mode = enMode.Add;
        }

        private bool _AddCarInspection()
        {
            this.CarInspectionID = CarInspectionRepository.AddCarInspection(CarDocumentationID, Status);
            return this.CarInspectionID != null;
        }
        private bool _UpdateCarInspection()
        {
            return CarInspectionRepository.UpdateCarInspection(CarInspectionID, TeamID, Status);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddCarInspection())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateCarInspection();

            }
            return false;
        }
        public static bool UpdateStatus(int CarInspectionID, short Status)
        {
            return CarInspectionRepository.UpdateCarInspectionStatus(CarInspectionID, Status); ;
        }
        static public clsCarInspection Find(int? CarInspectionID)
        {
            int? TeamID = null;
            int? CarDocumentationID = null;
            short? Status = null;
            DateTime? ExpDate = null;

            if (CarInspectionRepository.GetCarInspectionByID(CarInspectionID, ref TeamID,
                   ref CarDocumentationID, ref Status, ref ExpDate))
            {
                return new clsCarInspection(CarInspectionID, TeamID, CarDocumentationID, Status, ExpDate);
            }
            else
                return null;

        }
        static public bool Cancel(int CarInspectionID)
        {
            return UpdateStatus(CarInspectionID, (short)enStatus.Cancelled);
        }

        static public DataTable GetAllReports()
        {
            return CarInspectionRepository.GetAllReports();
        }
        public static Dictionary<string, int> FindReportInspectionIDs(int? carInspectionID)
        {
            Dictionary<string, int> report = new Dictionary<string, int>();

            return CarInspectionRepository.GetInspectionIDsForReport(carInspectionID, report) ? report : null;
        }
        static public string FindResumeByCarInspectionID(int? carInspectionID)
        {
            string resume = "";
            return CarInspectionRepository.GetResumeByCarInspectionID(carInspectionID, ref resume) ? resume : null;
        }

    }

}
