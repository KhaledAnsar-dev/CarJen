using CarJenBusiness;
using CarJenBusiness.ApplicationLogic;
using CarJenData.Repositories;
using CarJenWebApi.Dtos.AppointmentDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllAppointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AppointmentResponseDto>> GetAllAppointments()
        {
            var appointments = clsAppointment.GetAllAppointments()
                .Select(AppointmentMapper.ToAppointmentResponseDto)
                .ToList();

            return Ok(appointments);
        }

        [HttpGet("ByID/{appointmentID:int}", Name = "GetAppointmentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AppointmentResponseDto> GetAppointmentByID(int appointmentID)
        {
            if (appointmentID <= 0)
                return BadRequest("Invalid appointment ID.");

            var appointment = clsAppointment.Find(appointmentID);

            if (appointment == null)
                return NotFound("Appointment not found.");

            var response = AppointmentMapper.ToAppointmentResponseDto(appointment.ToAppointmentDto);
            return Ok(response);
        }

        [HttpGet("ByCarDocID/{carDocID:int}", Name = "GetAppointmentByCarDocID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AppointmentResponseDto> GetAppointmentByCarDocID(int carDocID)
        {
            if (carDocID <= 0)
                return BadRequest("Invalid car documentation ID.");

            var appointment = clsAppointment.FindByCarDocID(carDocID);

            if (appointment == null)
                return NotFound("Appointment not found for the specified Car Documentation ID.");

            var response = AppointmentMapper.ToAppointmentResponseDto(appointment.ToAppointmentDto);
            return Ok(response);
        }

        [HttpPost("Add", Name = "AddAppointment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AppointmentResponseDto> AddAppointment(CreateAppointmentDto newAppointment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appointment = AppointmentMapper.ToclsAppointment(newAppointment);

            if (appointment._AddAppointment())
            {
                var createdAppointment = clsAppointment.Find(appointment.AppointmentID);

                var response = AppointmentMapper.ToAppointmentResponseDto(createdAppointment.ToAppointmentDto);
                
                return CreatedAtRoute("GetAppointmentByID", new { appointmentID = response.AppointmentID }, response);
            }

            return BadRequest("Failed to add appointment.");
        }

        [HttpPut("UpdateStatus/{appointmentID:int}/{status:int}", Name = "UpdateAppointmentStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateAppointmentStatus(int appointmentID, int status)
        {
            if (appointmentID <= 0)
                return BadRequest("Invalid appointment ID.");

            if (!Enum.IsDefined(typeof(clsAppointment.enStatus), status))
                return BadRequest("Invalid status value.");

            if (!clsAppointment.isAppointmentExist(appointmentID))
                return NotFound("Appointment not found.");

            if (clsAppointment.UpdateStatus(appointmentID,(short)status))
                return Ok("Appointment status updated successfully.");

            return BadRequest("Failed to update appointment status.");
        }

        [HttpPut("UpdatePublishFee/{appointmentID:int}", Name = "UpdatePublishFee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdatePublishFee(int appointmentID)
        {
            if (appointmentID <= 0)
                return BadRequest("Invalid appointment ID.");

            if (!clsAppointment.isAppointmentExist(appointmentID))
                return NotFound("Appointment not found.");

            if (clsAppointment.UpdatePublishFee(appointmentID))
                return Ok("Publish fee updated successfully.");

            return BadRequest("Failed to update publish fee.");
        }

        [HttpDelete("Delete/{appointmentID:int}", Name = "DeleteAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteAppointment(int appointmentID)
        {
            if (appointmentID <= 0)
                return BadRequest("Invalid appointment ID.");

            if (!clsAppointment.isAppointmentExist(appointmentID))
                return NotFound("Appointment not found.");

            if (clsAppointment.Delete(appointmentID))
                return Ok("Appointment deleted successfully.");

            return BadRequest("Failed to delete appointment.");
        }


        [HttpGet("ReadyForTechnicalInspection", Name = "GetCarsReadyForTechnicalInspection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<PendingTechInspectionCarResponse>> GetCarsReadyForTechnicalInspection()
        {
            var cars = clsAppointment.GetCarsReadyForTechnicalInspection();

            if (cars == null)
                return NotFound("Failed to retrieve data.");

            var response = cars.Select(AppointmentMapper.ToPendingTechInspectionCarResponse).ToList();
            return Ok(response);
        }
    }
}
