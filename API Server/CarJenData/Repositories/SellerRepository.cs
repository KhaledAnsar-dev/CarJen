using CarJenData.DataModels;
using CarJenShared.Dtos.PersonDtos;
using CarJenShared.Dtos.SellerDtos;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static CarJenShared.Helpers.Logger;
namespace CarJenData.Repositories
{
    public class SellerRepository
    {
        public static SellerDto? GetSellerByID(int? sellerID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Sellers
                        .Where(s => s.SellerId == sellerID)
                        .Select(s => new SellerDto
                        {
                            SellerId = s.SellerId,
                            NationalNumber = s.NationalNumber,
                            Earnings = s.Earnings,
                            Person = new PersonDto
                            {
                                PersonID = s.PersonId,
                                FirstName = s.Person.FirstName,
                                MiddleName = s.Person.MiddleName,
                                LastName = s.Person.LastName,
                                Gender = (short)s.Person.Gender,
                                Phone = s.Person.Phone
                            }
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetSellerByID));
                return null;
            }
        }
        public static SellerDto? GetSellerByNO(string nationalNumber)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Sellers
                        .Where(s => s.NationalNumber == nationalNumber)
                        .Select(s => new SellerDto
                        {
                            SellerId = s.SellerId,
                            NationalNumber = s.NationalNumber,
                            Earnings = s.Earnings,
                            Person = new PersonDto
                            {
                                FirstName = s.Person.FirstName,
                                MiddleName = s.Person.MiddleName,
                                LastName = s.Person.LastName,
                                Gender = (short)s.Person.Gender,
                                Phone = s.Person.Phone
                            }
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetSellerByNO));
                return null;
            }
        }
        public static SellerDto? GetSellerByCredentials(string userName, string password)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Sellers
                        .Where(s => s.Person.UserName == userName && s.Person.Password == password)
                        .Select(s => new SellerDto
                        {
                            SellerId = s.SellerId,
                            NationalNumber = s.NationalNumber,
                            Earnings = s.Earnings,
                            Person = new PersonDto
                            {
                                FirstName = s.Person.FirstName,
                                MiddleName = s.Person.MiddleName,
                                LastName = s.Person.LastName,
                                Gender = (short)s.Person.Gender,
                                Phone = s.Person.Phone
                            }
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetSellerByCredentials));
                return null;
            }
        }
        public static List<SellerDto> GetAllSellers()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Sellers
                        .Select(s => new SellerDto
                        {
                            SellerId = s.SellerId,
                            NationalNumber = s.NationalNumber,
                            Earnings = s.Earnings,
                            Person = new PersonDto
                            {
                                FirstName = s.Person.FirstName,
                                MiddleName = s.Person.MiddleName,
                                LastName = s.Person.LastName,
                                Gender = (short)s.Person.Gender,
                                Phone = s.Person.Phone
                            }
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllSellers));
                return new List<SellerDto>();
            }
        }
        public static bool IsSellerExist(int sellerID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Sellers.Any(s => s.SellerId == sellerID);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(IsSellerExist));
                return false;
            }
        }
        public static bool IsSellerExist(string nationalNumber)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Sellers.Any(s => s.NationalNumber == nationalNumber);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(IsSellerExist));
                return false;
            }
        }
        public static int? AddSeller(int? personID, string nationalNumber)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var seller = new Seller
                    {
                        PersonId = personID ?? 0,
                        NationalNumber = nationalNumber,
                        Earnings = 0
                    };

                    context.Sellers.Add(seller);
                    context.SaveChanges();

                    return seller.SellerId;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(AddSeller));
                return null;
            }
        }
        public static bool UpdateSeller(int? sellerID, string nationalNumber)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var seller = context.Sellers.Find(sellerID);

                    if (seller == null)
                        return false;

                    seller.NationalNumber = nationalNumber;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdateSeller));
                return false;
            }
        }
        public static bool DeleteSeller(int sellerID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var seller = context.Sellers.Find(sellerID);

                    if (seller == null)
                        return false;

                    context.Sellers.Remove(seller);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(DeleteSeller));
                return false;
            }
        }

    }
}
