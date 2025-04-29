using CarJenWebApi.Dtos.ReportDtos;
using CarJenWebApi.Dtos.TeamDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/Report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpPost("AddPre", Name = "AddPreApprovedReport")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TeamResponseDto> AddPreApprovedReport(CreatePreReportDto preReport)
        {
            var team = TeamMapper.ToclsTeam(newTeam);

            if (team.Save())
            {
                var response = TeamMapper.ToTeamResponseDto(team.toTeamDto);
                return CreatedAtRoute("GetTeamByID", new { teamID = team.TeamID }, response);
            }

            return BadRequest("Failed to add the team.");
        }
    }
}
