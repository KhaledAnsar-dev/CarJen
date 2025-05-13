using Azure;
using CarJenBusiness.ApplicationLogic;
using CarJenData.DataModels;
using CarJenShared.Dtos.UserDtos;
using CarJenWebApi.Dtos.UserDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("AllUsers",Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UserSummaryDto>> GetAllUsers()
        {
            // Using our custom converting method

            var users = clsUser.GetAllUsers()
              .Select(user => UserMapper.ToUserSummaryDto(user))
              .ToList();


            if (!users.Any())
                return NotFound("Users list was empty");

            return Ok(users);
        }


        [HttpGet("ByID/{userID}", Name = "GetUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserResponseDto> GetUserByID(int userID)
        {
            if(userID < 1)
                return BadRequest("Enter a valid id");

            var user = clsUser.Find(userID);

            if (user == null)
                return NotFound($"User with id {userID} was not found");

            UserResponseDto response = user.toUserDto.Adapt<UserResponseDto>();

            return Ok(response);
        }


        [HttpGet("ByNo/{nationalNumber}", Name = "GetUserByNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserResponseDto> GetUserByNo(string nationalNumber)
        {
            if (string.IsNullOrEmpty(nationalNumber))
                return BadRequest("National number was empty");

            var user = clsUser.Find(nationalNumber);

            if (user == null)
                return NotFound($"User with national number : {nationalNumber} was not found");

            //UserResponseDto response = user.toUserDto.Adapt<UserResponseDto>();
            UserResponseDto response = UserMapper.ToUserResponseDto(user.toUserDto);

            return Ok(response);
        }


        [HttpPost("Add" , Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserResponseDto> AddUser([FromBody] CreateUserDto newUser)
        {
            // Convert to business object , so we can use the save method
            var user = UserMapper.ToclsUser(newUser);

            if (user.Save())
            {
                // Convert to response dto using mapster

                var response = user.toUserDto.Adapt<UserResponseDto>();

                return CreatedAtRoute("GetUserByID", new { userID = user.UserID }, response);
            }
            else
                return BadRequest("Cannot add user.");
        }


        [HttpDelete("{userID}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int userID)
        {
            //we validate the data here
            if (userID <= 0)
            {
                return BadRequest("Invalid student Id.");
            }

            if (clsUser.Delete(userID))
            {
                return Ok($"User with ID {userID} Has deleted");
            }
            else
            {
                return NotFound($"User with ID {userID} is not exists");
            }
        }

        // From body removed for updatedUser (for delete)
        [HttpPut("{userID}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserResponseDto> UpdateUser(int userID, UpdateUserDto updatedUser)
        {
            // Get the current User
            var currentUser = clsUser.Find(userID);

            if (currentUser == null)
            {
                return NotFound("User does not exists");
            }

            currentUser.NationalNumber = updatedUser.NationalNumber;
            currentUser.FirstName = updatedUser.FirstName;
            currentUser.MiddleName = updatedUser.MiddleName;
            currentUser.LastName = updatedUser.LastName;
            currentUser.Gender = updatedUser.Gender;
            currentUser.DateOfBirth = updatedUser.DateOfBirth;
            currentUser.Email = updatedUser.Email;
            currentUser.Phone = updatedUser.Phone;
            currentUser.Address = updatedUser.Address;
            currentUser.IsActive = updatedUser.IsActive;
            currentUser.Image = updatedUser.Image;
            currentUser.RoleID = (short)updatedUser.RoleID;
            currentUser.Save();

            // Convert the business object to UserDto than to UserresponseDto
            UserResponseDto response = UserMapper.ToUserResponseDto(currentUser.toUserDto);

            return Ok(response);
        }

    }
}
