using CarJenBusiness.ApplicationLogic;
using CarJenData.DataModels;
using CarJenShared.Dtos.UserDtos;
using CarJenWebApi.Dtos.UserDtos;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class UserMapper
    {
        public static clsUser ToclsUser(CreateUserDto user)
        {
            return new clsUser
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Image = user.Image,
                UserName = user.UserName,
                Password = user.Password,
                IsActive = user.IsActive,
                NationalNumber = user.NationalNumber,
                RoleID = (short)user.RoleId,
                CreatedByUserID = user.CreatedByUserId?? 0,
                Role = clsRole.Find((short)user.RoleId),
                JoinDate = DateTime.Now
            };
        }
        public static UserSummaryDto ToUserSummaryDto(UserDto user)
        {
            return new UserSummaryDto
            {
                UserId = (int)user.UserId,
                NationalNumber = user.NationalNumber,
                EvaluationScore = user.EvaluationScore,
                Bonus = Convert.ToDecimal(user.Bonus),
                Phone = user.Person.Phone,
                FullName = user.Person.FullName,
                RoleTitle = user.Role.roleTitle
            };
        }
        public static UserResponseDto ToUserResponseDto(UserDto user)
        {
            return new UserResponseDto
            {
                UserID = (int)user.UserId,
                NationalNumber = user.NationalNumber,
                EvaluationScore = (short)user.EvaluationScore,
                CreatedByUser = clsUser.Find(user.CreatedByUserId)?.FullName ?? "",
                FirstName = user.Person.FirstName,
                MiddleName = user.Person.MiddleName,
                LastName = user.Person.LastName,
                Gender = user.Person.Gender == 0 ? "Female" : "Male",
                DateOfBirth = user.Person.DateOfBirth,
                Email = user.Person.Email,
                Phone = user.Person.Phone,
                Address = user.Person.Address,
                IsActive = user.Person.IsActive,
                Image = user.Person.Image,
                RoleTitle = user.Role.roleTitle,
                Salary = user.Role.salary
            };
        }
    }
}
