using CarJenShared.Dtos.UserDtos;
using CarJenShared.Dtos.PersonDtos;
using CarJenShared.Dtos.RoleDtos;

using CarJenWebApi.Dtos.UserDtos;
using Mapster;
using CarJenBusiness.ApplicationLogic;
using CarJenData.DataModels;

namespace CarJenWebApi.Mappings
{
    public class MapsterConfig
    {
        public static void RegisterMappings()
        {
            _RegisterGetAllUserMapping();
            _RegisterGetUserMapping();
            _RegisterCreateUserMapping();
        }
        private static void _RegisterGetAllUserMapping()
        {
            TypeAdapterConfig<UserDto, UserSummaryDto>
                .NewConfig()
                .Map(dest => dest.UserId, src => src.UserId ?? 0)
                .Map(dest => dest.NationalNumber, src => src.NationalNumber)
                .Map(dest => dest.EvaluationScore, src => (short)src.EvaluationScore)
                .Map(dest => dest.Bonus, src => src.Bonus)
                .Map(dest => dest.FullName, src => src.Person.FullName)
                .Map(dest => dest.Phone, src => src.Person.Phone)
                .Map(dest => dest.RoleTitle, src => src.Role.roleTitle);
        }
        private static void _RegisterGetUserMapping()
        {
            TypeAdapterConfig<UserDto, UserResponseDto>
                .NewConfig()
                .Map(dest => dest.UserID, src => src.UserId ?? 0)
                .Map(dest => dest.NationalNumber, src => src.NationalNumber)
                .Map(dest => dest.EvaluationScore, src => (short)src.EvaluationScore)
                .Map(dest => dest.CreatedByUser, src => clsUser.Find(src.CreatedByUserId).FullName)
                .Map(dest => dest.FirstName, src => src.Person.FirstName)
                .Map(dest => dest.MiddleName, src => src.Person.MiddleName)
                .Map(dest => dest.LastName, src => src.Person.LastName)
                .Map(dest => dest.Gender, src => src.Person.Gender == 0 ? "Female" : "Male")
                .Map(dest => dest.DateOfBirth, src => src.Person.DateOfBirth)
                .Map(dest => dest.Email, src => src.Person.Email)
                .Map(dest => dest.Phone, src => src.Person.Phone)
                .Map(dest => dest.Address, src => src.Person.Address)
                .Map(dest => dest.IsActive, src => src.Person.IsActive)
                .Map(dest => dest.Image, src => src.Person.Image)
                .Map(dest => dest.RoleTitle, src => src.Role.roleTitle)
                .Map(dest => dest.Salary, src => src.Role.salary);
        }
        private static void _RegisterCreateUserMapping()
            {
            TypeAdapterConfig<CreateUserDto, clsUser>
            .NewConfig()
            .Map(dest => dest.NationalNumber, src => src.NationalNumber)
            .Map(dest => dest.EvaluationScore, src => 0) 
            .Map(dest => dest.CreatedByUserID, src => src.CreatedByUserId)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.MiddleName, src => src.MiddleName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Phone, src => src.Phone)
            .Map(dest => dest.Address, src => src.Address)
            .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.IsActive, src => src.IsActive)
            .Map(dest => dest.JoinDate, src => DateTime.Now) 
            .Map(dest => dest.UserName, src => src.UserName)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.Image, src => src.Image)
            .Map(dest => dest.RoleID, src => (short)src.RoleId);


        }
    }
}
