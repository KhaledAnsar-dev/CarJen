using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.CarDocumentationDtos;
using CarJenShared.Dtos.CarDtos;
using CarJenWebApi.Dtos.CarDocumentationDtos;
using CarJenWebApi.Dtos.CarDtos;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class CarDocumentationMapper
    {
        public static clsCarDocumentation ToclsCarDocumentation(CreateCarDocDto carDocDto)
        {
            return new clsCarDocumentation
            {
                CarID = carDocDto.CarID,
                SellerID = carDocDto.SellerID,

                Car = clsCar.Find(carDocDto.CarID),
                Seller = clsSeller.Find(carDocDto.SellerID)
            };

        }
        public static CarDocResponseDto ToCarDocResponseDto(CarDocumentationDto car)
        {
            return new CarDocResponseDto
            {
                CarDocumentationID = car.CarDocumentationID,
                SellerID = car.Seller?.SellerId,
                Brand = car.Car?.Brand?.Brand,
                Model = car.Car?.Model?.Model,
                Trim = car.Car?.Trim?.Trim,
                FuelType = car.Car?.FuelType,
                Mileage = car.Car?.Mileage,
                TransmissionType = car.Car?.TransmissionType,
                Year = car.Car?.Year,
                Color = car.Car?.Color,
                Price = car.Car?.Price,
                PlateNumber = car.Car?.PlateNumber,
                RegistrationExp = car.Car?.RegistrationExp,
                TechInspectionExp = car.Car?.TechInspectionExp,
                CreatedDate = car.CreatedDate
            };
        }
        public static CarDocSummaryDto ToCarDocSummaryDto(CarDocumentationDto car)
        {
            return new CarDocSummaryDto
            {
                CarDocumentationID = car.CarDocumentationID,
                SellerID = car.Seller?.SellerId,
                SellerName = car.Seller?.Person?.FullName ?? string.Empty,
                Brand = car.Car?.Brand?.Brand ?? string.Empty,
                Model = car.Car?.Model?.Model ?? string.Empty,
                Trim = car.Car?.Trim?.Trim ?? string.Empty,
                Mileage = car.Car?.Mileage,
                Year = car.Car?.Year
            };
        }
    }
}
