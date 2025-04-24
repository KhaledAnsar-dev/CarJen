using CarJenData.DataModels;
using CarJenData.Mappings;
using CarJenShared.Dtos.MemberDtos;
using CarJenShared.Dtos.PersonDtos;
using CarJenShared.Dtos.TeamDtos;
using CarJenShared.Dtos.UserDtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Reflection;
using static CarJenShared.Helpers.Logger;

namespace CarJenData.Repositories
{
    public class TeamMemberRepository
    {
        public static MemberDto? GetTeamMemberByID(int? teamMemberId)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.TeamMembers
                        .Where(tm => tm.TeamMemberId == teamMemberId)
                        .Select(tm => new MemberDto
                        {
                            TeamMemberId = tm.TeamMemberId,
                            JoinDate = tm.JoinDate,
                            ExitDate = tm.ExitDate,

                            Team = new TeamDto
                            {
                                TeamID = tm.TeamId,
                                TeamType = tm.Team.TeamType
                            },

                            User = new UserDto
                            {
                                UserId = tm.UserId,
                                Role = new CarJenShared.Dtos.RoleDtos.RoleDto
                                {
                                    roleTitle = tm.User.Role.RoleTitle
                                },
                                Person = new PersonDto
                                {
                                    FirstName = tm.User.Person.FirstName,
                                    MiddleName = tm.User.Person.LastName,
                                    LastName = tm.User.Person.LastName
                                }
                            }
                        })
                        .FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetTeamMemberByID));
                return null;
            }
        }
        public static int? AddTeamMember(int? teamId, int? userId)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var entity = new TeamMember
                    {
                        TeamId = (int)teamId,
                        UserId = (int)userId,
                        JoinDate = DateTime.Now,
                        ExitDate = null
                    };

                    context.TeamMembers.Add(entity);
                    context.SaveChanges();

                    return entity.TeamMemberId;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(AddTeamMember));
                return null;
            }
        }
        public static bool DeleteTeamMember(int teamMemberId)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var entity = context.TeamMembers.Find(teamMemberId);

                    if (entity == null)
                        return false;

                    context.TeamMembers.Remove(entity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(DeleteTeamMember));
                return false;
            }
        }
        public static List<MemberDto> GetAllTeamMembers()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    // ✅ Direct projection: generates optimized SQL, ideal for large datasets.
                    // Avoids Include overhead and loads only needed fields.

                    return context.TeamMembers
                        .Select(tm => new MemberDto
                        {
                            TeamMemberId = tm.TeamMemberId,
                            JoinDate = tm.JoinDate,
                            ExitDate = tm.ExitDate,

                            Team = new TeamDto
                            {
                                TeamID = tm.TeamId,
                                TeamType = tm.Team.TeamType
                            },

                            User = new UserDto
                            {
                                Person = new PersonDto
                                {
                                    FirstName = tm.User.Person.FirstName,
                                    MiddleName = tm.User.Person.LastName,
                                    LastName = tm.User.Person.LastName
                                },
                                Role = new CarJenShared.Dtos.RoleDtos.RoleDto
                                {
                                    roleTitle = tm.User.Role.RoleTitle
                                }
                            }
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllTeamMembers));
                return new List<MemberDto>();
            }
        }
        public static bool ExitTeam(int teamMemberID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var teamMember = context.TeamMembers.Find(teamMemberID);

                    if (teamMember == null) return false;

                    teamMember.ExitDate = DateTime.Now;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(ExitTeam));
                return false;
            }
        }
        public static bool? IsUserMember(int? userID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.TeamMembers
                        .Any(tm => tm.UserId == userID && tm.ExitDate == null);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(IsUserMember));
                return null;
            }
        }
        public static bool? ReplaceMember(int? newUserID, int? oldUserID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var teamMember = context.TeamMembers
                        .FirstOrDefault(tm => tm.UserId == oldUserID && tm.ExitDate == null);

                    if (teamMember == null) return null;

                    teamMember.UserId = (int)newUserID;
                    teamMember.JoinDate = DateTime.Now;
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(ReplaceMember));
                return null;
            }
        }
        public static int? GetTeamRoleMemberCount(int? teamID, short? roleID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var count = context.TeamMembers.
                        Include(tm => tm.User)
                        .Where(tm => tm.TeamId == teamID && tm.User.RoleId == roleID && tm.ExitDate == null)
                        .Count();
                    return count;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetTeamRoleMemberCount));
                return null;
            }

        }
    }
}

