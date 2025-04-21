using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.PersonDtos
{
    public class PersonDto
    {
        public int? PersonID { get; set; } // (Auto-Implemented Properties)
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + (MiddleName ?? "") + " " + LastName;
        public short Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? JoinDate { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
