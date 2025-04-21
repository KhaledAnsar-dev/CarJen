using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsBackgroundServiceLogReader
    {
        static public DataTable GetAppointmentValidationLogs()
        {
            return BackgroundServiceLogRepository.GetAppointmentValidationLogs();
        }
    }
}
