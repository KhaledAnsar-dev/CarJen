using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Helpers
{
    public class Common
    {
        public static string GetFeeTypeName(byte feeType)
        {
            return feeType switch
            {
                1 => "Inspection Fee",
                2 => "Report Fee",
                3 => "Publish Fee",
                _ => "Service Fee"
            };
        }

    }
}
