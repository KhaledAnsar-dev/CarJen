using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenData.DataModels
{
    public class clsDataSettings
    {
        static public string ConnectionString
        {
            get
            {
                return "Data Source=.;Initial Catalog=CarJenDB;Integrated Security=True;TrustServerCertificate=True";
            }
        }
    }
}
