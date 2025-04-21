using CarJenData.DataModels;
using CarJenShared.Dtos;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace CarJenWebApi.Dtos.UserDtos
{
    public class UserResponseDto
    {
        public int UserID { get; set; }
        public string NationalNumber { get; set; }
        public short EvaluationScore { get; set; }
        public string CreatedByUser { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }

        public string RoleTitle { get; set; }
        public float? Salary { get; set; }

    }
}
