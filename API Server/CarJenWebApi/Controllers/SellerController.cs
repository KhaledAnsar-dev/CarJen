using CarJenBusiness.ApplicationLogic;
using CarJenWebApi.Dtos.SellerDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllSellers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<SellerResponseDto>> GetAllSellers()
        {
            var sellers = clsSeller.GetAllSeller()
                .Select(SellerMapper.ToSellerResponseDto)
                .ToList();

            if (!sellers.Any())
                return NotFound("No sellers found.");

            return Ok(sellers);
        }

        [HttpGet("{sellerID:int}", Name = "GetSellerByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SellerResponseDto> GetSellerByID(int sellerID)
        {
            if (sellerID < 1)
                return BadRequest("Invalid seller ID.");

            var seller = clsSeller.Find(sellerID);

            if (seller == null)
                return NotFound($"Seller with ID {sellerID} not found.");

            var response = SellerMapper.ToSellerResponseDto(seller.ToSellerDto);

            return Ok(response);
        }

        [HttpDelete("{sellerID:int}", Name = "DeleteSeller")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteSeller(int sellerID)
        {
            if (sellerID <= 0)
                return BadRequest("Invalid seller ID.");

            if (clsSeller.Delete(sellerID))
                return Ok($"Seller with ID {sellerID} has been deleted successfully.");

            return NotFound($"Seller with ID {sellerID} not found.");
        }

        [HttpPut("Update/{sellerID:int}", Name = "UpdateSeller")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SellerResponseDto> UpdateSeller(int sellerID, UpdateSellerDto updatedSeller)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentSeller = clsSeller.Find(sellerID);

            if (currentSeller == null)
                return NotFound($"Seller with ID {sellerID} not found.");

            currentSeller.FirstName = updatedSeller.FirstName;
            currentSeller.LastName = updatedSeller.LastName;
            currentSeller.Phone = updatedSeller.Phone;
            currentSeller.NationalNumber = updatedSeller.NationalNumber;

            if (currentSeller.Save())
            {
                var response = SellerMapper.ToSellerResponseDto(currentSeller.ToSellerDto);
                return Ok(response);
            }

            return BadRequest("Failed to update seller.");
        }
    }
}
