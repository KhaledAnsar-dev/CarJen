using CarJenBusiness;
using CarJenShared.Dtos.CarInspectionDtos;
using CarJenWebApi.Dtos.CarInspectionDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/CarInspection")]
    [ApiController]
    public class CarInspectionController : ControllerBase
    {
        [HttpPost("Create", Name = "CreateCarInspection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int?> CreateCarInspection(CreateCarInspectionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inspection = CarInspectionMapper.ToclsCarInspection(dto);

            if (inspection.CreateInitialCarInspection())
                return Ok(inspection.CarInspectionID);

            return BadRequest("Failed to create car inspection.");
        }

        [HttpPost("AddBatch", Name = "AddInspectionBatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddBatch( AddInspectionBatchDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var batch = CarInspectionMapper.ToInspectionBatchDto(dto);

            if (clsCarInspection.AddCarInspectionBatch(batch))
                return Ok("Inspection batch added successfully.");

            return BadRequest("Failed to add inspection batch.");
        }

        [HttpGet("Batch/{carInspectionID:int}", Name = "GetInspectionBatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<InspectionDto>> GetInspectionBatch(int carInspectionID)
        {
            var batch = clsCarInspection.GetCarInspectionBatch(carInspectionID);

            if (batch == null || batch.Count == 0)
                return NotFound("No inspections found for this car inspection ID.");

            return Ok(batch);
        }

        [HttpPut("UpdateStatus/{carInspectionID:int}/{status:int}", Name = "UpdateCarInspectionStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateStatus(int carInspectionID, short status)
        {
            if (clsCarInspection.UpdateStatus(carInspectionID, status))
                return Ok("Car inspection status updated successfully.");

            return BadRequest("Failed to update status.");
        }

        [HttpPut("Cancel/{carInspectionID:int}", Name = "CancelCarInspection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Cancel(int carInspectionID)
        {
            if (clsCarInspection.Cancel(carInspectionID))
                return Ok("Car inspection cancelled.");

            return BadRequest("Failed to cancel inspection.");
        }

        [HttpGet("FullBatch/{carInspectionID:int}", Name = "GetFullInspectionBatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<InspectionBatchResponseDto> GetFullInspectionBatch(int carInspectionID)
        {
            var batch = clsCarInspection.GetFullCarInspectionBatch(carInspectionID);

            if (batch == null || batch.Inspections.Count == 0)
                return NotFound("No inspections found for this car inspection ID.");

            // Response used by client app 
            var response = CarInspectionMapper.ToInspectionBatchResponseDto(batch);

            return Ok(response);
        }

    }
}
