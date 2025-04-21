using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.RoleDtos
{
    public class RoleDto
    {
        public short roleID { get; set; }
        public string roleTitle { get; set; }
        public float salary { get; set; }
        public int permission { get; set; }
    }
}
