using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using CarJenShared.Dtos.PersonDtos;
using CarJenShared.Dtos.RoleDtos;

namespace CarJenShared.Dtos.UserDtos
{
    public class UserDto
    {
        public int? UserId { get; set; }
        public string NationalNumber { get; set; }
        public int EvaluationScore { get; set; }
        public int? CreatedByUserId { get; set; }
        public float? Bonus { get; set; }
        public PersonDto Person { get; set; }
        public RoleDto Role { get; set; }
    }
}
