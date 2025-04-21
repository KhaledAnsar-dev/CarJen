using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using CarJenShared.Helpers;
using CarJenShared.Dtos.RoleDtos;


namespace CarJenData.Repositories
{
    public class RoleRepository
    {
        public static RoleDto? GetRoleByID(int roleID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Roles
                        .Where(role => role.RoleId == roleID)
                        .Select(role => new RoleDto
                        {
                            roleID = (short)role.RoleId,
                            roleTitle = role.RoleTitle,
                            salary = (float)role.Salary,
                            permission = role.Permission
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, nameof(GetRoleByID));
                return null;
            }
        }
        public static RoleDto? GetRoleByTitle(string roleTitle)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Roles
                        .Where(role => role.RoleTitle == roleTitle)
                        .Select(role => new RoleDto
                        {
                            roleID = (short)role.RoleId,
                            roleTitle = role.RoleTitle,
                            salary = (float)role.Salary,
                            permission = role.Permission
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, nameof(GetRoleByTitle));
                return null;
            }
        }
        public static List<RoleDto> GetRoles()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Roles
                        .Select(role => new RoleDto
                        {
                            roleID = (short)role.RoleId,
                            roleTitle = role.RoleTitle,
                            salary = (float)role.Salary,
                            permission = role.Permission
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, nameof(GetRoles));
                return new List<RoleDto>();
            }
        }
        public static List<RoleDto> GetRolesRequiringTeam()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Roles
                        .Where(r => r.RoleTitle != "Admin" &&
                                    r.RoleTitle != "Accounts and Teams Manager" &&
                                    r.RoleTitle != "Reports Manager")
                        .Select(role => new RoleDto
                        {
                            roleID = (short)role.RoleId,
                            roleTitle = role.RoleTitle,
                            salary = (float)role.Salary,
                            permission = role.Permission
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, nameof(GetRolesRequiringTeam));
                return new List<RoleDto>();
            }
        }

        public static bool UpdateRole(RoleDto roleDto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var role = context.Roles.Find((int)roleDto.roleID);

                    if (role == null)
                        return false;

                    role.RoleTitle = roleDto.roleTitle;
                    role.Salary = Convert.ToDecimal(roleDto.salary);

                    context.SaveChanges();
                    return true;

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, nameof(UpdateRole));
                return false;
            }

        }

    }
}

