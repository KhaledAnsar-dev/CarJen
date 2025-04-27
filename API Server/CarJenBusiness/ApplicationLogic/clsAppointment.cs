using CarJenData.Repositories;
using CarJenShared.Dtos.AppointmentDtos;
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

        public AppointmentDto ToAppointmentDto
        {
            get
            {
                return new AppointmentDto
                {
                    AppointmentID = AppointmentID,
                    PublishFee = (decimal)this.PublishFee.Amount,
                    PublishFeeID = this.PublishFeeID,
                    CarDocumentationID = this.CarDocumentationID,
                    Status = this.GetStatusText(),
                    AppointmentDate = this.AppointmentDate,
                    CarDocumentation = this.CarDocumentaion.ToCarDocDto
                };
            }
        }

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

        private clsAppointment(AppointmentDto appointmentDto)
        {
            AppointmentID = appointmentDto.AppointmentID;
            PublishFeeID = appointmentDto.PublishFeeID;
            CarDocumentationID = appointmentDto.CarDocumentationID;

            // Get status number using value
            Status = _StatusText.FirstOrDefault(s => s.Value == appointmentDto.Status).Key;

            AppointmentDate = appointmentDto.AppointmentDate;
            CreatedDate = appointmentDto.AppointmentDate;
            CarDocumentaion = clsCarDocumentation.Find(appointmentDto.CarDocumentationID);
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


        public bool _AddAppointment()
        {
            this.AppointmentID = AppointmentRepository.AddAppointment(CarDocumentationID,AppointmentDate);
            return this.AppointmentID != null;
        }
        public static bool UpdatePublishFee(int AppointmentID)
        {
            int? publishFeeID = clsFees.Find(clsFees.enFeeType.PublishingFee).FeeID;
            return AppointmentRepository.UpdatePublishFee(AppointmentID, publishFeeID);
        }
        public static bool UpdateStatus(int AppointmentID, short Status)
        {
            return AppointmentRepository.UpdateAppointmentStatus(AppointmentID, Status);
        }
        static public clsAppointment? Find(int? AppointmentID)
        {
            var appointmentDto = AppointmentRepository.GetAppointmentByID(AppointmentID);
            if(appointmentDto != null)
            {
                return new clsAppointment(appointmentDto);
            }
            else
                return null;
        }
        static public clsAppointment? FindByCarDocID(int? CarDocumentationID)
        {
            var appointmentDto = AppointmentRepository.GetAppointmentByCarDocID(CarDocumentationID);
            if (appointmentDto != null)
            {
                return new clsAppointment(appointmentDto);
            }
            else
                return null;

        }
        static public bool Delete(int AppointmentID)
        {
            return AppointmentRepository.Delete(AppointmentID);
        }

        static public List<AppointmentDto> GetAllAppointments()
        {
            return AppointmentRepository.GetAllAppointments();
        }
        public static bool isAppointmentExist(int AppointmentID)
        {
            return AppointmentRepository.IsAppointmentExist(AppointmentID);
        }
        static public int? GetAppointmentID(int CarDocumentationID)
        {
            return AppointmentRepository.GetAppointmentIDByCarDocID(CarDocumentationID);
        }
        //static public DataTable GetAllPreApprovedCars()
        //{
        //    return AppointmentRepository.GetAllPreApprovedCars();
        //}

    }

}
