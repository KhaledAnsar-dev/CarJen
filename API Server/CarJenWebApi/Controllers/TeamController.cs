using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.TeamDtos;
using CarJenWebApi.Dtos.TeamDtos;
using CarJenWebApi.Dtos.UserDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/Team")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllTeams")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TeamResponseDto>> GetAllTeams()
        {
            var teams = clsTeam.GetAllTeams()
                .Select(team => TeamMapper.ToTeamResponseDto(team))
                .ToList();

            if (!teams.Any())
                return NotFound("No teams found.");

            return Ok(teams);
        }

        [HttpGet("ID/{teamID:int}", Name = "GetTeamByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TeamResponseDto> GetTeamByID(int teamID)
        {
            if (teamID < 1)
                return BadRequest("Invalid team ID.");

            var team = clsTeam.Find(teamID);

            if (team == null)
                return NotFound($"Team with ID {teamID} was not found.");

            var response = TeamMapper.ToTeamResponseDto(team.toTeamDto);
            return Ok(response);
        }

        [HttpGet("Code/{teamCode}", Name = "GetTeamByCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TeamResponseDto> GetTeamByCode(string teamCode)
        {
            if (string.IsNullOrWhiteSpace(teamCode))
                return BadRequest("Team code is required.");

            var team = clsTeam.Find(teamCode);

            if (team == null)
                return NotFound($"Team with code '{teamCode}' was not found.");

            var response = TeamMapper.ToTeamResponseDto(team.toTeamDto);
            return Ok(response);
        }

        [HttpGet("ByUserID/{userID:int}", Name = "FindTeamByUserID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TeamResponseDto> FindTeamByUserID(int userID)
        {
            if (userID < 1)
                return BadRequest("Invalid user ID.");

            var team = clsTeam.FindTeamByUserID(userID);

            if (team == null)
                return NotFound($"User with ID {userID} has no team");

            var response = TeamMapper.ToTeamResponseDto(team.toTeamDto);
            return Ok(response);
        }

        [HttpPost("Add", Name = "AddTeam")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TeamResponseDto> AddTeam(CreateTeamDto newTeam)
        {
            var team = TeamMapper.ToclsTeam(newTeam);

            if (team.Save())
            {
                var response = TeamMapper.ToTeamResponseDto(team.toTeamDto);
                return CreatedAtRoute("GetTeamByID", new { teamID = team.TeamID }, response);
            }

            return BadRequest("Failed to add the team.");
        }

        [HttpPut("Update/{teamID:int}", Name = "UpdateTeam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TeamResponseDto> UpdateTeam(int teamID, [FromBody] UpdateTeamDto updatedTeam)
        {
            if (teamID < 1)
                return BadRequest("Invalid team ID.");

            var currentTeam = clsTeam.Find(teamID);

            if (currentTeam == null)
                return NotFound($"Team with ID {teamID} does not exist.");

            currentTeam.TeamCode = updatedTeam.TeamCode;

            currentTeam.Save();

            var response = TeamMapper.ToTeamResponseDto(currentTeam.toTeamDto);
            return Ok(response);
        }

        [HttpDelete("Delete/{teamID:int}", Name = "DeleteTeam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteTeam(int teamID)
        {
            if (teamID <= 0)
                return BadRequest("Invalid team ID.");

            if (clsTeam.Delete(teamID))
                return Ok($"Team with ID {teamID} has been deleted.");

            return NotFound($"Team with ID {teamID} does not exist.");
        }


        [HttpGet("IsComplete/{teamID:int}", Name = "IsTeamComplete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsTeamComplete(int teamID)
        {
            if (teamID <= 0)
                return BadRequest("Invalid team ID.");

            var team = clsTeam.Find(teamID); 

            if (team == null) 
                return NotFound("Team not found.");

            return Ok(team.IsTeamComplete());
        }


        [HttpGet("Log/{carDocumentationID:int}/{teamID:int}", Name = "LogInitialTeamUpdates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult LogInitialTeamUpdates(int carDocumentationID, int teamID)
        {
            if (teamID <= 0 && carDocumentationID <= 0)
                return BadRequest("Enter valid inputs.");

            var result = clsTeam.LogInitialTeamUpdates(carDocumentationID, teamID);

            if (!result)
                return BadRequest("Failed to log initial team update.");


            return Ok("Team update logged successfully.");

        }
    }
}
