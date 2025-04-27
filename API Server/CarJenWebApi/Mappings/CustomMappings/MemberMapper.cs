using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.MemberDtos;
using CarJenShared.Dtos.TeamDtos;
using CarJenWebApi.Dtos.MemeberDtos;
using CarJenWebApi.Dtos.TeamDtos;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class MemberMapper
    {
        public static clsTeamMember ToclsMember(CreateMemberDto member)
        {
            return new clsTeamMember
            {
                TeamID = member.TeamId,
                UserID = member.UserId,
            };
        }
        public static MemberResponseDto ToMemberResponseDto(MemberDto member)
        {
            return new MemberResponseDto
            {
                MemberID = Convert.ToInt32(member.TeamMemberId),
                MemberName = member.User.Person.FullName,
                TeamID = Convert.ToInt32(member.Team.TeamID),
                TeamType = member.Team.TeamType == 1 ? "Initial Inspection Team" : "Technical Inspection Team",
                RoleTitle = member.User.Role.roleTitle,
                JoinDate = member.JoinDate,
                ExitDate = member.ExitDate
            };
        }
    }
}
