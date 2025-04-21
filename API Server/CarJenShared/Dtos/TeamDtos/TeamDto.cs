using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.TeamDtos
{
    public class TeamDto
    {
        public int? TeamID { get; set; }
        public string TeamCode { get; set; }
        public short? TeamType { get; set; }
        public int? CreatedByUserID { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
