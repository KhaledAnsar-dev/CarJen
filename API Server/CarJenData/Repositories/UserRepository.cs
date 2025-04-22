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
        public static UserDto? GetUserById(int userId)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var entity = context.Users
                        .Include(u => u.Person)
                        .Include(u => u.Role)
                        .FirstOrDefault(u => u.UserId == userId && u.Person != null && u.Role != null);

                    return entity != null ? UserDataMapper.ToDto(entity) : null;
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
                    var entity = context.Users
                       .Include(u => u.Person)
                       .Include(u => u.Role)
                       .FirstOrDefault(u => u.NationalNumber == nationalNumber && u.Person != null && u.Role != null);

                    return entity != null ? UserDataMapper.ToDto(entity) : null;
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
                    // ✅ Direct projection: Fastest and most efficient for listing users
                    // Only required fields are selected, no need for Include()
                    // Reduces memory usage and generates optimized SQL

                    return context.Users
                        .Where(u => u.Person != null && u.Role != null)
                        .Select(u => new UserDto
                        {
                            UserId = u.UserId,
                            Role = new RoleDto
                            {
                                roleTitle = u.Role.RoleTitle
                            },
                            NationalNumber = u.NationalNumber,

                            Person = new PersonDto
                            {
                                Phone = u.Person.Phone,
                                FirstName = u.Person.FirstName,
                                MiddleName = u.Person.MiddleName ?? "",
                                LastName = u.Person.LastName
                            },
                            EvaluationScore = u.EvaluationScore,
                            Bonus = u.EvaluationScore >= 90 ? Convert.ToSingle(u.Role.Salary * 0.15m) :
                                    u.EvaluationScore >= 70 ? Convert.ToSingle(u.Role.Salary * 0.10m) :
                                    u.EvaluationScore >= 40 ? Convert.ToSingle(u.Role.Salary * 0.05m) :
                                    0
                        })
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
