using CarJenData.DataModels;
using CarJenShared.Dtos.TeamDtos;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenData.Mappings
{
    public static class TeamDataMapper
    {
        public static TeamDto ToDto(Team entity)
        {
            return new TeamDto
            {
                TeamID = entity.TeamId,
                TeamCode = entity.TeamCode,
                TeamType = entity.TeamType,
                CreatedByUserID = entity.CreatedByUserId,
                CreatedDate = entity.CreatedDate
            };
        }
        public static Team ToEntity(TeamDto dto)
        {
            return new Team
            {
                TeamId = dto.TeamID ?? 0,
                TeamCode = dto.TeamCode,
                TeamType = (byte)(dto.TeamType ?? 0),
                CreatedByUserId = dto.CreatedByUserID ?? 0,
                CreatedDate = dto.CreatedDate ?? DateTime.Now
            };
        }
    }

}
