using CarJenShared.Dtos.InspectionPaymentDtos;
using CarJenWebApi.Dtos.InspectionPaymentDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionPaymentController : ControllerBase
    {
        [HttpPost("Add", Name = "AddInspectionPayment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<InspectionPaymentDto> AddInspectionPayment([FromBody] CreateInspectionPaymentDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var payment = InspectionPaymentMapper.ToclsInspectionPayment(createDto);

            if (payment.AddPayment())
            {
                var response = new InspectionPaymentDto
                {
                    InspectionPaymentID = payment.InspectionPaymentID,
                    AppointmentID = payment.AppointmentID,
                    InspectionFeeID = payment.InspectionFeeID,
                    PaymentMethod = payment.PaymentMethod
                };

                return CreatedAtRoute("AddInspectionPayment", new { id = response.InspectionPaymentID }, response);
            }

            return BadRequest("Failed to add inspection payment.");
        }
    }
}
