using CarJenData.DataModels;
using CarJenData.Mappings;
using CarJenShared.Dtos.PersonDtos;
using CarJenShared.Dtos.RoleDtos;
using CarJenShared.Dtos.UserDtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Reflection;
using static CarJenShared.Helpers.Logger;
namespace CarJenData.Repositories
{
    public class UserRepository
    {
        //public static UserDto? GetUserById(int userId)
        //{
        //    try
        //    {
        //        using (var context = new CarJenDbContext())
        //        {
        //            var user = context.Users
        //                .Where(u => u.UserId == userId && u.Person != null && u.Role != null)
        //                .Select(u => new UserDto
        //                {
        //                    UserId = u.UserId,
        //                    NationalNumber = u.NationalNumber,
        //                    EvaluationScore = u.EvaluationScore,
        //                    CreatedByUserId = u.CreatedByUserId,
        //                    Bonus = u.EvaluationScore >= 90 ? (float)u.Role.Salary * 0.15f :
        //                            u.EvaluationScore >= 70 ? (float)u.Role.Salary * 0.10f :
        //                            u.EvaluationScore >= 40 ? (float)u.Role.Salary * 0.05f :
        //                            0,

        //                    Person = new PersonDto
        //                    {
        //                        PersonID = u.Person.PersonId,
        //                        FirstName = u.Person.FirstName,
        //                        MiddleName = u.Person.MiddleName,
        //                        LastName = u.Person.LastName,
        //                        Gender = (short)u.Person.Gender,
        //                        DateOfBirth = u.Person.DateOfBirth,
        //                        Email = u.Person.Email,
        //                        Phone = u.Person.Phone,
        //                        Address = u.Person.Address,
        //                        JoinDate = u.Person.JoinDate,
        //                        IsActive = u.Person.IsActive,
        //                        Image = u.Person.Image,
        //                        UserName = u.Person.UserName,
        //                        Password = u.Person.Password
        //                    },


        //                    Role = new RoleDto
        //                    {
        //                        roleID = (short)u.Role.RoleId,
        //                        roleTitle = u.Role.RoleTitle,
        //                        salary = (float)u.Role.Salary,
        //                    }
        //                })
        //                .FirstOrDefault();

        //            return user;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(ex, nameof(GetUserById));
        //        return null;
        //    }
        //}

        public static UserDto? GetUserById(int userId)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var user = context.Users
                        .Where(u => u.UserId == userId && u.Person != null && u.Role != null)
                        .Include (u => u.Person)
                        .Include (u => u.Role)
                        .Select(u => UserDataMapper.ToDto(u))
                        .FirstOrDefault();

                    return user;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetUserById));
                return null;
            }
        }
        public static UserDto? GetUserByNO(string nationalNumber)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var user = context.Users
                        .Where(u => u.NationalNumber == nationalNumber && u.Person != null && u.Role != null)
                        .Include(u => u.Person)
                        .Include(u => u.Role)
                        .Select(u => UserDataMapper.ToDto(u))
                        .FirstOrDefault();

                    return user;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetUserById));
                return null;
            }
        }

        // move toward person repostery
        //public static bool GetUserByCredentials(string userName, string password)
        //{
        //    //try
        //    //{
        //    //    using (var context = new CarJenDbContext())
        //    //    {
        //    //        return context.Users
        //    //            .Where(r => r.u == userName)
        //    //            .Select(r => new UserDto(r.RoleId, r.NationalNumber, (short)r.EvaluationScore, r.CreatedByUserId ?? 0, r.PersonId, r.RoleId))
        //    //            .FirstOrDefault();
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return null;
        //    //}
        //}

        public static int? AddUser(UserDto userDto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var userEntity = UserDataMapper.ToEntity(userDto);

                    context.Users.Add(userEntity);

                    context.SaveChanges();

                    return userEntity.UserId;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(AddUser));
                return 0;
            }

        }
        public static bool UpdateUser(UserDto userDto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var userEntity = context.Users.Find(userDto.UserId);
                    
                    if (userEntity == null)
                        return false;

                    userEntity.NationalNumber = userDto.NationalNumber;
                    userEntity.EvaluationScore = userDto.EvaluationScore;
                    userEntity.RoleId = (int)userDto.Role.roleID;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdateUser));
                return false;
            }

        }
        public static bool DeleteUser(int userID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var userEntity = context.Users.Find(userID);

                    if (userEntity == null)
                        return false;

                    context.Users.Remove(userEntity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(DeleteUser));
                return false;
            }

        }

        public static List<UserDto> GetAllUsers()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Users
                        .Include(u => u.Person)
                        .Include(u => u.Role)
                        .Where(u => u.Person != null && u.Role != null)
                        .Select(u => UserDataMapper.ToDto(u))
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllUsers));
                return new List<UserDto>();
            }

        }

        public static bool IsUserExist(int userID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Users.Any(u => u.UserId == userID);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(IsUserExist));
                return false;
            }

        }

        public static bool IsUserExist(string nationalNumber)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Users.Any(u => u.NationalNumber == nationalNumber);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(IsUserExist));
                return false;
            }
        }

        public static bool IsAnyUserRegistered()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Users.Any();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(IsAnyUserRegistered));
                return false;
            }

        }

        public static bool GetUserBonus(int? userID, ref float? bonus)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetUserBonus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            bonus = Convert.ToSingle(reader["Bonus"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetUserBonus));

            }

            return isFound;
        }
    }
}
