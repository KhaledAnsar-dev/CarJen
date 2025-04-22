using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;
using CarJenShared.Dtos.TeamDtos;
using CarJenShared.Helpers;
using Mapster;
using  CarJenData.Mappings;
namespace CarJenData.Repositories
{
    public class TeamRepository
    {
        public static TeamDto? GetTeamByID(int? teamID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Teams
                        .Where(t => t.TeamId == teamID)
                        .Select(t => new TeamDto
                        {
                            TeamID = t.TeamId,
                            TeamCode = t.TeamCode,
                            TeamType = t.TeamType,
                            CreatedByUserID = t.CreatedByUserId,
                            CreatedDate = t.CreatedDate
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetTeamByID");
                return null;
            }

        }
        public static TeamDto? GetTeamByCode(string teamCode)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Teams
                        .Where(t => t.TeamCode == teamCode)
                        .Select(t => new TeamDto
                        {
                            TeamID = t.TeamId,
                            TeamCode = t.TeamCode,
                            TeamType = t.TeamType,
                            CreatedByUserID = t.CreatedByUserId,
                            CreatedDate = t.CreatedDate
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetTeamByCode");
                return null;
            }
        }
        public static TeamDto? GetTeamByUserID(int? userID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Teams
                        .Where(t => t.TeamMembers.Any(tm => tm.UserId == userID))
                        .Select(t => TeamDataMapper.ToDto(t))
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetTeamByUserID");
                return null;
            }
        }
        public static int? AddTeam(TeamDto teamDto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var team = TeamDataMapper.ToEntity(teamDto);
                    context.Teams.Add(team);
                    context.SaveChanges();
                    return team.TeamId;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,"AddTeam");
                return null;
            }
        }
        public static bool UpdateTeam(TeamDto teamDto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var team = context.Teams.Find(teamDto.TeamID);

                    if (team == null)
                        return false;

                    team.TeamCode = teamDto.TeamCode;
                    team.TeamType = Convert.ToByte(teamDto.TeamType);

                    //teamDto.Adapt(team);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "UpdateTeam");
                return false;
            }
        }
        public static bool DeleteTeam(int teamID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var team = context.Teams.Find(teamID);

                    if (team == null)
                        return false;

                    context.Teams.Remove(team);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "DeleteTeam");
                return false;
            }
        }
        public static List<TeamDto> GetAllTeams()
        {
            try
            {
                using(var context = new CarJenDbContext())
                {
                    return context.Teams
                        .Select(t => TeamDataMapper.ToDto(t))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetAllTeams");
                return new List<TeamDto>();
            }
        }

        public static bool IsTeamExist(string teamCode)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsTeamExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TeamCode", teamCode);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        isFound = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "IsTeamExist");
            }

            return isFound;
        }
        public static short? GetTeamMemberCount(int? teamID, short? teamType)
        {
            short? count = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetTeamMemberCount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TeamID", teamID);
                    command.Parameters.AddWithValue("@TeamType", teamType);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && short.TryParse(result.ToString(), out short number))
                        count = number;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetTeamMemberCount");
            }

            return count;
        }
        public static bool LogInitialTeamUpdates(int carDocumentationID, int teamID)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_LogInitialTeamUpdates", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarDocumentationID", carDocumentationID);
                    command.Parameters.AddWithValue("@TeamID", teamID);
                    command.Parameters.AddWithValue("@CheckedDate", DateTime.Now);

                    connection.Open();
                    rowAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "LogInitialTeamUpdates");
            }

            return rowAffected > 0;
        }
    }
}

