using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.CarDtos;
using CarJenShared.Dtos.MemberDtos;
using CarJenWebApi.Dtos.CarDtos;
using CarJenWebApi.Dtos.MemeberDtos;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class CarMapper
    {
        public static clsCar ToclsCar(CreateCarDto car)
        {
            var trim = clsTrim.Find(car.TrimID);
            var model = clsModel.Find(trim.ModelID);

            return new clsCar
            {
                TrimID = car.TrimID,
                Trim = trim.Trim,
                ModelID = trim.ModelID,
                Model = model.Model,
                BrandID = model.BrandID,
                Brand = clsBrand.Find(model.BrandID).Brand,
                FuelType = car.FuelType,
                Mileage = car.Mileage,
                TransmissionType = car.TransmissionType,
                Year = car.Year
            };
        }
        public static CarResponseDto ToCarResponseDto(CarDto car)
        {
            return new CarResponseDto
            {
                CarID = car.CarID,
                Brand = car.Brand?.Brand,
                Model = car.Model?.Model,
                Trim = car.Trim?.Trim,   
                FuelType = car.FuelType,
                Mileage = car.Mileage,
                TransmissionType = car.TransmissionType,
                Year = car.Year,
                Color = car.Color,
                Price = car.Price,
                PlateNumber = car.PlateNumber,
                RegistrationExp = car.RegistrationExp,
                TechInspectionExp = car.TechInspectionExp
            };
        }
    }
}
