using CarJenBusiness.ApplicationLogic;
using CarJenData.DataModels;
using CarJenShared.Dtos.TeamDtos;
using CarJenShared.Dtos.UserDtos;
using CarJenWebApi.Dtos.TeamDtos;
using CarJenWebApi.Dtos.UserDtos;
using Mapster;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class TeamMapper
    {
        public static clsTeam ToclsTeam(CreateTeamDto team)
        {
            return new clsTeam
            {
                TeamCode = team.TeamCode,
                TeamType = team.TeamType,
                CreatedByUserID = team.CreatedByUserId,
                CreatedDate = DateTime.Now
            };
        }
        public static TeamResponseDto ToTeamResponseDto(TeamDto team)
        {
            return new TeamResponseDto
            {
                TeamId = Convert.ToInt32(team.TeamID),
                TeamCode = team.TeamCode,
                TeamType = team.TeamType == 1 ? "Initial Inspection Team" : "Technical Inspection Team",
                CreatedByUser = clsUser.Find(team.CreatedByUserID).FullName,
                CreatedDate = (DateTime)team.CreatedDate
            };
        }
    }
}
