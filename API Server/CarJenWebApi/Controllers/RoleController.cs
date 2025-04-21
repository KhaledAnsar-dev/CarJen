using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos;
using CarJenWebApi.Dtos.RoleDtos;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/Role")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        [HttpGet("AllRoles", Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RoleResponseDto>> GetAllRoles()
        {
            //var roles = clsRole.GetRoles().Select(role => new RoleResponseDto { roleID = role.roleID, roleTitle = role.roleTitle, salary = role.salary });
            // Using mapster to convert from RoleDto to RoleResponseDto
            var roles = clsRole.GetRoles().Adapt<List<RoleResponseDto>>();

            if (!roles.Any())
                return NotFound("No roles found");

            return Ok(roles);

        }


        [HttpGet("TeamRoles", Name = "GetRolesRequiringTeam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RoleResponseDto>> GetRolesRequiringTeam()
        {
            var roles = clsRole.GetRolesRequiringTeam().Adapt<List<RoleResponseDto>>();

            if (!roles.Any())
                return NotFound("No roles found");

            return Ok(roles);

        }


        [HttpGet("FindByID/{roleID}", Name = "GetRoleByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RoleResponseDto> GetRoleByID(int roleID)
        {
            if (roleID < 1)
                return BadRequest($"Wrong input, {roleID} is not acceptable.");

            // Get the roleDto that used to send the data between the layers (projects) 
            var roleDto = clsRole.Find(roleID).toRoleDto;

            if (roleDto == null)
                return NotFound($"No role found with ID {roleID}");

            // Get the response object to consume by the client
            RoleResponseDto response = roleDto.Adapt<RoleResponseDto>();
            return Ok(response);
        }


        [HttpGet("FindByTitle/{title}", Name = "GetRoleByTitle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RoleResponseDto> GetRoleByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return BadRequest($"Wrong input, {title} is not correct.");

            // Get the roleDto that used to send the data between the layers (projects) 
            var roleDto = clsRole.Find(title).toRoleDto;

            if (roleDto == null)
                return NotFound($"No role found with title '{title}'");

            // Get the response object to consume by the client
            RoleResponseDto response  = roleDto.Adapt<RoleResponseDto>();
            return Ok(response);
        }


        [HttpPut("UpdateRole/{roleID}" , Name = "UpdateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RoleResponseDto> UpdateRole(int roleID , UpdateRoleDto role)
        {
            if (roleID < 1)
                return BadRequest($"Wrong input, {roleID} is not acceptable.");


            var currentRole = clsRole.Find(roleID);

            if (currentRole == null)
                return NotFound($"No role found with ID {roleID}");

            currentRole.RoleTitle = role.roleTitle;
            currentRole.Salry = role.salary;

            currentRole.Save();

            // Get the response object to consume by the client
            RoleResponseDto response = currentRole.toRoleDto.Adapt<RoleResponseDto>();

            return Ok(response);
        }
    }
}
