using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.MemberDtos;
using CarJenWebApi.Dtos.MemeberDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace CarJenWebApi.Controllers
{
    [Route("api/Member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllTeamMembers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MemberResponseDto>> GetAllTeamMembers()
        {
            // ✅ Using centralized mapper to convert from DTO to response DTO
            // Improves maintainability and consistency across API responses

            var members = clsTeamMember.GetAllMembers()
                .Select(MemberMapper.ToMemberResponseDto)
                .ToList();


            if (!members.Any())
                return NotFound("No team members found.");

            return Ok(members);
        }

        [HttpGet("{teamMemberID:int}", Name = "GetTeamMemberByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MemberResponseDto> GetTeamMemberByID(int teamMemberID)
        {
            if (teamMemberID < 1)
                return BadRequest("Invalid team member ID.");

            var member = clsTeamMember.Find(teamMemberID);

            if (member == null)
                return NotFound($"Team member with ID {teamMemberID} was not found.");
            MemberResponseDto response = MemberMapper.ToMemberResponseDto(member.ToMemberDto);

            return Ok(response);
        }

        [HttpPost("Add", Name = "AddTeamMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MemberResponseDto> AddTeamMember(CreateMemberDto newMember)
        {
            var member = MemberMapper.ToclsMember(newMember);

            if (member.AddMember())
            {
                var createdMember = clsTeamMember.Find(member.TeamMemberID);

                MemberResponseDto response = MemberMapper.ToMemberResponseDto(createdMember.ToMemberDto);

                return CreatedAtRoute("GetTeamMemberByID", new { teamMemberID = response.MemberID }, response);
            }

            return BadRequest("Failed to add team member.");
        }

        [HttpDelete("{teamMemberID:int}", Name = "DeleteTeamMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteTeamMember(int teamMemberID)
        {
            if (teamMemberID <= 0)
                return BadRequest("Invalid team member ID.");

            if (clsTeamMember.Delete(teamMemberID))
                return Ok($"Team member with ID {teamMemberID} has been deleted.");

            return NotFound($"Team member with ID {teamMemberID} does not exist.");
        }

        [HttpPut("Exit/{teamMemberID:int}", Name = "ExitTeam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult ExitTeam(int teamMemberID)
        {
            if (teamMemberID <= 0)
                return BadRequest("Invalid team member ID.");

            if (clsTeamMember.ExitTeam(teamMemberID))
                return Ok("Team member exited successfully.");

            return NotFound("Team member not found.");
        }

        [HttpGet("IsUserMember/{userID:int}", Name = "IsUserMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool?> IsUserMember(int userID)
        {
            if (userID <= 0)
                return BadRequest("Invalid user ID.");

            var result = clsTeamMember.IsUserMember(userID);
            return Ok(result);
        }

        [HttpPut("Replace/{teamMemeberID:int}/{newUserID:int}", Name = "ReplaceTeamMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult ReplaceTeamMember(int teamMemeberID,int newUserID)
        {
            if (teamMemeberID <= 0 || newUserID <= 0)
                return BadRequest("Invalid inputs.");

            var member = clsTeamMember.Find(teamMemeberID); 

            if (member == null)
                return NotFound("New user is not associated with a team member.");

            var result = member.ReplaceMember(newUserID);

            if (result == true)
                return Ok("Team member replaced successfully.");

            return BadRequest("Failed to replace team member.");
        }

        [HttpGet("role-capacity-check/{teamID:int}/{roleID:int}", Name = "IsRoleCapacityReached")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool?> IsRoleCapacityReached(int teamID, short roleID)
        {
            if (teamID <= 0 || roleID <= 0)
                return BadRequest("Invalid input.");

            var count = clsTeamMember.IsRoleCapacityReached(teamID, roleID);
            return Ok(count);
        }
    }
}
