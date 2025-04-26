using CarJenData.DataModels;
using CarJenShared.Dtos.CarDto;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static CarJenShared.Helpers.Logger;

namespace CarJenData.Repositories
{
    public class CarRepository
    {
        public static CarDto? GetCarByID(int? carID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Cars
                        .Where(c => c.CarId == carID)
                        .Select(c => new CarDto
                        {
                            CarID = c.CarId,
                            Brand = new BrandDto
                            {
                                BrandID = c.Trim.Model.Brand.BrandId,
                                Brand = c.Trim.Model.Brand.Brand1
                            },
                            Model = new ModelDto
                            {
                                ModelID = c.Trim.Model.ModelId,
                                Model = c.Trim.Model.Model1,
                                BrandID = c.Trim.Model.Brand.BrandId
                            },
                            Trim = new TrimDto
                            {
                                TrimID = c.Trim.TrimId,
                                Trim = c.Trim.Trim1,
                                ModelID = c.Trim.ModelId
                            },
                            FuelType = c.FuelType,
                            Mileage = c.Mileage,
                            TransmissionType = c.TransmissionType,
                            Year = c.Year,
                            Color = c.Color,
                            Price = c.Price,
                            PlateNumber = c.PlateNumber,
                            RegistrationExp = c.RegistrationExp,
                            TechInspectionExp = c.TechInspectionExp
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetCarByID));
                return null;
            }
        }
        public static CarDto? GetCarByPlateNumber(string plateNumber)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Cars
                        .Where(c => c.PlateNumber == plateNumber)
                        .Select(c => new CarDto
                        {
                            CarID = c.CarId,
                            Brand = new BrandDto
                            {
                                BrandID = c.Trim.Model.Brand.BrandId,
                                Brand = c.Trim.Model.Brand.Brand1
                            },
                            Model = new ModelDto
                            {
                                ModelID = c.Trim.Model.ModelId,
                                Model = c.Trim.Model.Model1,
                                BrandID = c.Trim.Model.Brand.BrandId
                            },
                            Trim = new TrimDto
                            {
                                TrimID = c.Trim.TrimId,
                                Trim = c.Trim.Trim1,
                                ModelID = c.Trim.ModelId
                            },
                            FuelType = c.FuelType,
                            Mileage = c.Mileage,
                            TransmissionType = c.TransmissionType,
                            Year = c.Year,
                            Color = c.Color,
                            Price = c.Price,
                            PlateNumber = c.PlateNumber,
                            RegistrationExp = c.RegistrationExp,
                            TechInspectionExp = c.TechInspectionExp
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetCarByPlateNumber));
                return null;
            }
        }
        public static int? AddCar(CarDto carDto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var car = new Car
                    {
                        TrimId = carDto.Trim.TrimID ?? 0,
                        FuelType = (byte)carDto.FuelType,
                        Mileage = (int)carDto.Mileage,
                        TransmissionType = (byte)carDto.TransmissionType,
                        Year = (short)carDto.Year,
                        Color = carDto.Color,
                        Price = carDto.Price,
                        PlateNumber = carDto.PlateNumber,
                        RegistrationExp = carDto.RegistrationExp,
                        TechInspectionExp = carDto.TechInspectionExp
                    };

                    context.Cars.Add(car);
                    context.SaveChanges();

                    return car.CarId;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(AddCar));
                return null;
            }
        }

        public static bool UpdateCar(CarDto carDto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var car = context.Cars.Find(carDto.CarID);

                    if (car == null)
                        return false;

                    car.TrimId = carDto.Trim.TrimID ?? 0;
                    car.FuelType = (byte)carDto.FuelType;
                    car.Mileage = (int)carDto.Mileage;
                    car.TransmissionType = (byte)carDto.TransmissionType;
                    car.Year = (short)carDto.Year;
                    car.Color = carDto.Color;
                    car.Price = carDto.Price;
                    car.PlateNumber = carDto.PlateNumber;
                    car.RegistrationExp = carDto.RegistrationExp;
                    car.TechInspectionExp = carDto.TechInspectionExp;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdateCar));
                return false;
            }
        }

        public static bool DeleteCar(int carID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var car = context.Cars.Find(carID);

                    if (car == null)
                        return false;

                    context.Cars.Remove(car);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(DeleteCar));
                return false;
            }
        }

        public static List<CarDto> GetAllCars()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Cars
                        .Select(c => new CarDto
                        {
                            CarID = c.CarId,
                            Brand = new BrandDto
                            {
                                BrandID = c.Trim.Model.Brand.BrandId,
                                Brand = c.Trim.Model.Brand.Brand1
                            },
                            Model = new ModelDto
                            {
                                ModelID = c.Trim.Model.ModelId,
                                Model = c.Trim.Model.Model1,
                                BrandID = c.Trim.Model.Brand.BrandId
                            },
                            Trim = new TrimDto
                            {
                                TrimID = c.Trim.TrimId,
                                Trim = c.Trim.Trim1,
                                ModelID = c.Trim.ModelId
                            },
                            FuelType = c.FuelType,
                            Mileage = c.Mileage,
                            TransmissionType = c.TransmissionType,
                            Year = c.Year,
                            Color = c.Color,
                            Price = c.Price,
                            PlateNumber = c.PlateNumber,
                            RegistrationExp = c.RegistrationExp,
                            TechInspectionExp = c.TechInspectionExp
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllCars));
                return new List<CarDto>();
            }
        }

        public static bool IsCarExist(int carID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Cars.Any(c => c.CarId == carID);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(IsCarExist));
                return false;
            }
        }

        public static bool IsCarExist(string plateNumber)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Cars.Any(c => c.PlateNumber == plateNumber);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(IsCarExist));
                return false;
            }
        }
    }
}



//public static int? AddCar(int? TrimID, short? FuelType, int? Mileage,
//    short? TransmissionType, short? Year, string Color, decimal? Price,
//    string PlateNumber, DateTime? RegistrationExp, DateTime? TechInspectionExp)
//{
//    int? CreatedID = null;

//    try
//    {
//        using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
//        using (SqlCommand Command = new SqlCommand("SP_AddCar", Connection))
//        {
//            Command.CommandType = CommandType.StoredProcedure;

//            Command.Parameters.AddWithValue("@TrimID", TrimID);
//            Command.Parameters.AddWithValue("@FuelType", FuelType);
//            Command.Parameters.AddWithValue("@Mileage", Mileage);
//            Command.Parameters.AddWithValue("@TransmissionType", TransmissionType);
//            Command.Parameters.AddWithValue("@Year", Year);
//            Command.Parameters.AddWithValue("@Color", Color);
//            Command.Parameters.AddWithValue("@Price", Price);
//            Command.Parameters.AddWithValue("@PlateNumber", PlateNumber);
//            Command.Parameters.AddWithValue("@RegistrationExp", RegistrationExp);
//            Command.Parameters.AddWithValue("@TechInspectionExp", TechInspectionExp);

//            SqlParameter OutputID = new SqlParameter("@CarID", SqlDbType.Int)
//            {
//                Direction = ParameterDirection.Output
//            };
//            Command.Parameters.Add(OutputID);

//            Connection.Open();
//            Command.ExecuteNonQuery();
//            CreatedID = (int)Command.Parameters["@CarID"].Value;
//        }
//    }
//    catch (Exception ex)
//    {
//        // string methodName = MethodBase.GetCurrentMethod().Name;
//        // clsEventLogger.LogError(ex.Message, methodName);
//    }

//    return CreatedID;
//}
