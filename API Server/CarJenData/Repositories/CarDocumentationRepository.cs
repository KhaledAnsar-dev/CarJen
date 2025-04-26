using CarJenData.DataModels;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;

using CarJenShared.Dtos.CarDocumentationDtos;
using CarJenShared.Dtos.CarDtos;
using CarJenShared.Dtos.SellerDtos;
using Microsoft.EntityFrameworkCore;
using CarJenShared.Dtos.PersonDtos;
using static CarJenShared.Helpers.Logger;

namespace CarJenData.Repositories
{
    public class CarDocumentationRepository
    {
        public static CarDocumentationDto? GetCarDocumentationByID(int? carDocumentationID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.CarDocumentations
                        .Where(cd => cd.CarDocumentationId == carDocumentationID)
                        .Select(cd => new CarDocumentationDto
                        {
                            CarDocumentationID = cd.CarDocumentationId,
                            CreatedDate = cd.CreatedDate,

                            Seller = new SellerDto
                            {
                                SellerId = cd.Seller.SellerId,
                                NationalNumber = cd.Seller.NationalNumber,
                                Earnings = cd.Seller.Earnings,
                                Person = new PersonDto
                                {
                                    PersonID = cd.Seller.Person.PersonId,
                                    FirstName = cd.Seller.Person.FirstName,
                                    MiddleName = cd.Seller.Person.MiddleName,
                                    LastName = cd.Seller.Person.LastName,
                                    Gender = (short)cd.Seller.Person.Gender,
                                    DateOfBirth = cd.Seller.Person.DateOfBirth,
                                    Email = cd.Seller.Person.Email,
                                    Phone = cd.Seller.Person.Phone,
                                    Address = cd.Seller.Person.Address,
                                    JoinDate = cd.Seller.Person.JoinDate,
                                    IsActive = cd.Seller.Person.IsActive,
                                    Image = cd.Seller.Person.Image,
                                    UserName = cd.Seller.Person.UserName,
                                    Password = cd.Seller.Person.Password
                                }
                            },

                            Car = new CarDto
                            {
                                CarID = cd.Car.CarId,
                                Brand = new BrandDto
                                {
                                    BrandID = cd.Car.Trim.Model.Brand.BrandId,
                                    Brand = cd.Car.Trim.Model.Brand.Brand1
                                },
                                Model = new ModelDto
                                {
                                    ModelID = cd.Car.Trim.Model.ModelId,
                                    Model = cd.Car.Trim.Model.Model1,
                                    BrandID = cd.Car.Trim.Model.BrandId
                                },
                                Trim = new TrimDto
                                {
                                    TrimID = cd.Car.Trim.TrimId,
                                    Trim = cd.Car.Trim.Trim1,
                                    ModelID = cd.Car.Trim.ModelId
                                },
                                FuelType = cd.Car.FuelType,
                                Mileage = cd.Car.Mileage,
                                TransmissionType = cd.Car.TransmissionType,
                                Year = cd.Car.Year,
                                Color = cd.Car.Color,
                                Price = cd.Car.Price,
                                PlateNumber = cd.Car.PlateNumber,
                                RegistrationExp = cd.Car.RegistrationExp,
                                TechInspectionExp = cd.Car.TechInspectionExp
                            }
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetCarDocumentationByID));
                return null;
            }
        }
        public static int? AddCarDocumentation(int? sellerID, int? carID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var carDocumentation = new CarDocumentation
                    {
                        SellerId = sellerID ?? 0,
                        CarId = carID ?? 0,
                        CreatedDate = DateTime.Now
                    };

                    context.CarDocumentations.Add(carDocumentation);
                    context.SaveChanges();

                    return carDocumentation.CarDocumentationId;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(AddCarDocumentation));
                return null;
            }
        }
        public static List<CarDocumentationDto> GetAllCarDocumentations()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.CarDocumentations
                        .Select(cd => new CarDocumentationDto
                        {
                            CarDocumentationID = cd.CarDocumentationId,
                            Seller = new SellerDto
                            {
                                SellerId = cd.Seller.SellerId,
                                Person = new PersonDto
                                {
                                    FirstName = cd.Seller.Person.FirstName,
                                    LastName = cd.Seller.Person.LastName,
                                }
                            },
                            Car = new CarDto
                            {
                                Brand = new BrandDto
                                {
                                    Brand = cd.Car.Trim.Model.Brand.Brand1,// brand name
                                },
                                Model = new ModelDto
                                {
                                    Model = cd.Car.Trim.Model.Model1,// model name
                                },
                                Trim = new TrimDto
                                {
                                    Trim = cd.Car.Trim.Trim1,// trim name
                                },
                                Mileage = cd.Car.Mileage,
                                Year = cd.Car.Year
                            }
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllCarDocumentations));
                return new List<CarDocumentationDto>();
            }
        }
        public static bool IsCarDocumentationExist(int carDocumentationID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.CarDocumentations.Any(cd => cd.CarDocumentationId == carDocumentationID);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(IsCarDocumentationExist));
                return false;
            }
        }
    }
}


