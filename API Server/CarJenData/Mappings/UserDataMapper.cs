using CarJenData.DataModels;
using CarJenShared.Dtos.PersonDtos;
using CarJenShared.Dtos.RoleDtos;
using CarJenShared.Dtos.TeamDtos;
using CarJenShared.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenData.Mappings
{
    public class UserDataMapper
    {
        public static UserDto ToDto(User entity)
        {
            return new UserDto
            {
                UserId = entity.UserId,
                NationalNumber = entity.NationalNumber,
                EvaluationScore = entity.EvaluationScore,
                CreatedByUserId = entity.CreatedByUserId,

                // Bonus can be calculated here if needed
                Bonus = entity.EvaluationScore >= 90 ? (float)entity.Role.Salary * 0.15f :
                        entity.EvaluationScore >= 70 ? (float)entity.Role.Salary * 0.10f :
                        entity.EvaluationScore >= 40 ? (float)entity.Role.Salary * 0.05f :
                        0,

                Person = new PersonDto
                {
                    PersonID = entity.Person.PersonId,
                    FirstName = entity.Person.FirstName,
                    MiddleName = entity.Person.MiddleName,
                    LastName = entity.Person.LastName,
                    Gender = (short)entity.Person.Gender,
                    DateOfBirth = entity.Person.DateOfBirth,
                    Email = entity.Person.Email,
                    Phone = entity.Person.Phone,
                    Address = entity.Person.Address,
                    JoinDate = entity.Person.JoinDate,
                    IsActive = entity.Person.IsActive,
                    Image = entity.Person.Image,
                    UserName = entity.Person.UserName,
                    Password = entity.Person.Password
                },

                Role = new RoleDto
                {
                    roleID = (short)entity.Role.RoleId,
                    roleTitle = entity.Role.RoleTitle,
                    salary = (float)entity.Role.Salary
                }
            };
        }
        public static User ToEntity(UserDto dto)
        {
            return new User
            {
                UserId = dto.UserId ?? 0,
                NationalNumber = dto.NationalNumber,
                EvaluationScore = dto.EvaluationScore,
                CreatedByUserId = dto.CreatedByUserId ?? 0,
                PersonId = (int)(dto.Person?.PersonID ?? 0),
                RoleId = (int)(dto.Role?.roleID ?? 0)
            };
        }

    }
}
