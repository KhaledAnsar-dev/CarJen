using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.CarDocumentationDtos;
using CarJenWebApi.Dtos.CarDocumentationDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/CarDocumentation")]
    [ApiController]
    public class CarDocumentationController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllCarDocumentations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CarDocSummaryDto>> GetAllCarDocumentations()
        {
            var carDocs = clsCarDocumentation.GetAllCarDocumentations()
                .Select(CarDocumentationMapper.ToCarDocSummaryDto)
                .ToList();

            if (!carDocs.Any())
                return NotFound("No car documentations found.");

            return Ok(carDocs);
        }

        [HttpGet("{carDocumentationID:int}", Name = "GetCarDocumentationByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CarDocResponseDto> GetCarDocumentationByID(int carDocumentationID)
        {
            if (carDocumentationID <= 0)
                return BadRequest("Invalid car documentation ID.");

            var carDoc = clsCarDocumentation.Find(carDocumentationID);

            if (carDoc == null)
                return NotFound($"Car documentation with ID {carDocumentationID} was not found.");

            var response = CarDocumentationMapper.ToCarDocResponseDto(carDoc.ToCarDocDto);

            return Ok(response);
        }

        [HttpPost("Add", Name = "AddCarDocumentation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CarDocResponseDto> AddCarDocumentation(CreateCarDocDto createCarDocDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newDoc = CarDocumentationMapper.ToclsCarDocumentation(createCarDocDto);

            if (newDoc.AddCarDocumentation())
            {
                var createdDoc = clsCarDocumentation.Find(newDoc.CarDocumentationID);

                var response = CarDocumentationMapper.ToCarDocResponseDto(createdDoc.ToCarDocDto);

                return CreatedAtRoute("GetCarDocumentationByID", new { carDocumentationID = createdDoc.CarDocumentationID }, response);
            }

            return BadRequest("Failed to add car documentation.");
        }
    }
}
