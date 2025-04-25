using CarJenBusiness.ApplicationLogic;
using CarJenWebApi.Dtos.FeeDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/Fee")]
    [ApiController]
    public class FeeController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllFees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<FeeResponseDto>> GetAllFees()
        {
            var fees = clsFees.GetAllFees()
                .Select(FeeMapper.ToTeamResponseDto)
                .ToList();

            if (!fees.Any())
                return NotFound("No fees found.");

            return Ok(fees);
        }

        [HttpGet("Current/{feeType:int}", Name = "GetCurrentFeeByType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<FeeResponseDto> GetCurrentFeeByType(int feeType)
        {
            if (feeType < 1 || feeType > 4)
                return BadRequest("Invalid fee type.");

            var fee = clsFees.Find((clsFees.enFeeType)feeType);

            if (fee == null)
                return NotFound($"No current fee found for type {feeType}.");

            return Ok(FeeMapper.ToTeamResponseDto(fee.ToFeeDto));
        }

        [HttpPost("Renew", Name = "RenewFee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult RenewFee(UpdateFeeDto request)
        {
            if (request.Amount <= 0)
                return BadRequest("Invalid input.");

            var result = clsFees.Renew(request.FeeTypeID, Convert.ToDecimal(request.Amount));

            if (!result)
                return BadRequest("Failed to renew fee.");

            return Ok("Fee renewed successfully.");
        }

        [HttpDelete("Delete/{feeID:int}", Name = "DeleteFee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteFee(int feeID)
        {
            if (feeID <= 0)
                return BadRequest("Invalid fee ID.");

            var result = clsFees.Delete(feeID);

            if (!result)
                return NotFound("Fee not found or already closed.");

            return Ok("Fee deleted successfully.");
        }
    }
}
