using CarJenBusiness.ApplicationLogic;
using CarJenWebApi.Dtos.CarDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/Car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllCars")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CarResponseDto>> GetAllCars()
        {
            var cars = clsCar.GetAllCars()
                .Select(CarMapper.ToCarResponseDto)
                .ToList();

            if (!cars.Any())
                return NotFound("No cars found.");

            return Ok(cars);
        }

        [HttpGet("{carID:int}", Name = "GetCarByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CarResponseDto> GetCarByID(int carID)
        {
            if (carID <= 0)
                return BadRequest("Invalid car ID.");

            var car = clsCar.Find(carID);

            if (car == null)
                return NotFound($"Car with ID {carID} was not found.");

            var response = CarMapper.ToCarResponseDto(car.ToCarDto);
            return Ok(response);
        }

        [HttpPost("Add", Name = "AddCar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CarResponseDto> AddCar(CreateCarDto newCar)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = CarMapper.ToclsCar(newCar);

            if (car.Save())
            {
                var response = CarMapper.ToCarResponseDto(car.ToCarDto);
                return CreatedAtRoute("GetCarByID", new { carID = car.CarID }, response);
            }

            return BadRequest("Failed to add car.");
        }

        [HttpPut("Update/{carID:int}", Name = "UpdateCar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CarResponseDto> UpdateCar(int carID, UpdateCarDto updatedCar)
        {
            if (carID <= 0)
                return BadRequest("Invalid car ID.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = clsCar.Find(carID);

            if (car == null)
                return NotFound("Car does not exist.");

            car.TrimID = updatedCar.TrimID;
            car.FuelType = updatedCar.FuelType;
            car.Mileage = updatedCar.Mileage;
            car.TransmissionType = updatedCar.TransmissionType;
            car.Year = updatedCar.Year;
            car.Color = updatedCar.Color;
            car.Price = updatedCar.Price;
            car.PlateNumber = updatedCar.PlateNumber;
            car.RegistrationExp = updatedCar.RegistrationExp;
            car.TechInspectionExp = updatedCar.TechInspectionExp;

            if (car.Save())
            {
                var response = CarMapper.ToCarResponseDto(car.ToCarDto);
                return Ok(response);
            }

            return BadRequest("Failed to update car.");
        }

        [HttpDelete("Delete/{carID:int}", Name = "DeleteCar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteCar(int carID)
        {
            if (carID <= 0)
                return BadRequest("Invalid car ID.");

            if (clsCar.Delete(carID))
                return Ok($"Car with ID {carID} has been deleted.");

            return NotFound($"Car with ID {carID} does not exist.");
        }

        [HttpGet("ExistByPlate/{plateNumber}", Name = "IsCarExistByPlate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> IsCarExistByPlate(string plateNumber)
        {
            if (string.IsNullOrWhiteSpace(plateNumber))
                return BadRequest("Plate number is required.");

            var exists = clsCar.IsCarExist(plateNumber);
            return Ok(exists);
        }
    }
}
