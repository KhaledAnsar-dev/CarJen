using CarJenShared.Dtos.TeamDtos;
using CarJenShared.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.MemberDtos
{
    public class MemberDto
    {
        public int TeamMemberId { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public TeamDto Team { get; set; }
        public UserDto User { get; set; }
    }
}
