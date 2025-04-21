using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsAppointment
    {
        public int? AppointmentID { get; set; }
        public int? PublishFeeID { get; set; }
        public int? CarDocumentationID { get; set; }
        public short? Status { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public clsCarDocumentation CarDocumentaion { get; set; }
        public clsFees PublishFee { get; set; }

        static readonly Dictionary<short?, string> _StatusText = new Dictionary<short?, string>
        {
            {0, "Scheduled"},
            {1, "UnderInspection "},
            {2, "Approved"},
            {3, "Rejected"},
            {4, "Cancelled"}
        };
        public string GetStatusText()
        {
            if (_StatusText.TryGetValue(Status, out string text))
            { return text; }
            else
            { return string.Empty; }
        }
        public static string GetStatusText(enStatus Status)
        {
            if (_StatusText.TryGetValue((short?)Status, out string text))
            { return text; }
            else
            { return string.Empty; }
        }

        enum enMode { Add = 0, Update = 1 }
        private enMode Mode { get; set; }
        public enum enStatus
        {
            Scheduled = 0,
            UnderInspection = 1,
            Approved = 2,
            Rejected = 3,
            Cancelled = 4
        }

        private clsAppointment(int? appointmentID, int? publishFeeID, int? carDocumentationID, short? status, DateTime? appointmentDate, DateTime? createdDate)
        {
            AppointmentID = appointmentID;
            PublishFeeID = publishFeeID;
            CarDocumentationID = carDocumentationID;
            Status = status;
            AppointmentDate = appointmentDate;
            CreatedDate = createdDate;
            CarDocumentaion = clsCarDocumentation.Find(CarDocumentationID);
            PublishFee = clsFees.Find(clsFees.enFeeType.PublishingFee);
            Mode = enMode.Update;
        }
        public clsAppointment()
        {
            AppointmentID = null;
            PublishFeeID = null;
            CarDocumentationID = null;
            Status = null;
            AppointmentDate = null;
            CreatedDate = null;

            Mode = enMode.Update;
        }


        //private bool _AddAppointment()
        //{
        //    this.AppointmentID = clsAppointmentData.AddAppointment(CarDocumentationID, Status, AppointmentDate);
        //    return this.AppointmentID != null;
        //}
        //private bool _UpdateAppointment()
        //{
        //    return clsAppointmentData.ApproveAppointment(AppointmentID, Status, AppointmentDate); ;
        //}

        public static bool UpdateStatus(int AppointmentID, short Status)
        {
            return AppointmentRepository.UpdateAppointmentStatus(AppointmentID, Status);
        }
        public static bool UpdatePublishFee(int AppointmentID)
        {
            int? publishFeeID = clsFees.Find(clsFees.enFeeType.PublishingFee).FeeID;
            return AppointmentRepository.UpdatePublishFee(AppointmentID, publishFeeID);
        }
        static public clsAppointment Find(int? AppointmentID)
        {
            int? PublishFeeID = null; int? CarDocumentationID = null; short? Status = null;
            DateTime? AppointmentDate = null; DateTime? CreatedDate = null;


            if (AppointmentRepository.GetAppointmentByID(AppointmentID, ref PublishFeeID,
                   ref CarDocumentationID, ref Status, ref AppointmentDate, ref CreatedDate))
            {
                return new clsAppointment(AppointmentID, PublishFeeID, CarDocumentationID, Status, AppointmentDate, CreatedDate);
            }
            else
                return null;

        }
        static public clsAppointment FindByCarDocID(int? CarDocumentationID)
        {
            int? AppointmentID = null; int? PublishFeeID = null; short? Status = null;
            DateTime? AppointmentDate = null; DateTime? CreatedDate = null;


            if (AppointmentRepository.GetAppointmentByCarDocID(ref AppointmentID, ref PublishFeeID,
                    CarDocumentationID, ref Status, ref AppointmentDate, ref CreatedDate))
            {
                return new clsAppointment(AppointmentID, PublishFeeID, CarDocumentationID, Status, AppointmentDate, CreatedDate);
            }
            else
                return null;

        }
        static public bool Delete(int AppointmentID)
        {
            return AppointmentRepository.Delete(AppointmentID);
        }

        static public DataTable GetAllAppointments()
        {
            return AppointmentRepository.GetAllAppointments();
        }
        public static bool isAppointmentExist(int AppointmentID)
        {
            return AppointmentRepository.IsAppointmentExist(AppointmentID);
        }
        static public int GetAppointmentID(int CarDocumentationID)
        {
            return AppointmentRepository.GetAppointmentIDByCarDocID(CarDocumentationID);
        }
        static public DataTable GetAllPreApprovedCars()
        {
            return AppointmentRepository.GetAllPreApprovedCars();
        }

    }

}
