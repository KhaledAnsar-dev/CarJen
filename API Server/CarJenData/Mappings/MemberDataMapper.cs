using CarJenData.DataModels;
using CarJenShared.Dtos.MemberDtos;
using CarJenShared.Dtos.TeamDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CarJenData.Mappings
{
    public static class MemberDataMapper
    {
        public static MemberDto ToDto(TeamMember entity)
        {
            return new MemberDto
            {
                TeamMemberId = entity.TeamMemberId,
                JoinDate = entity.JoinDate,
                ExitDate = entity.ExitDate,

                Team = entity.Team != null 
                    ? TeamDataMapper.ToDto(entity.Team)
                    : null,

                User = entity.User != null 
                    ? UserDataMapper.ToDto(entity.User)
                    : null
            };
        }
        public static TeamMember ToEntity(MemberDto dto)
        {
            return new TeamMember
            {
                TeamMemberId = dto.TeamMemberId,
                JoinDate = dto.JoinDate,
                ExitDate = dto.ExitDate,
                TeamId = dto.Team?.TeamID ?? 0,
                UserId = dto.User?.UserId ?? 0
            };
        }
    }
}
