using CarJenData.DataModels;
using CarJenShared.Dtos.CarDto;
using Microsoft.Data.SqlClient; // استخدام Microsoft.Data.SqlClient
using System;
using System.Data;
using static CarJenShared.Helpers.Logger;

namespace CarJenData.Repositories
{
    public static class BrandRepository
    {
        public static BrandDto? GetBrandByID(int? brandID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Brands
                        .Where(b => b.BrandId == brandID)
                        .Select(b => new BrandDto
                        {
                            BrandID = b.BrandId,
                            Brand = b.Brand1
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetBrandByID));
                return null;
            }
        }

        public static BrandDto? GetBrandByName(string brandName)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Brands
                        .Where(b => b.Brand1 == brandName)
                        .Select(b => new BrandDto
                        {
                            BrandID = b.BrandId,
                            Brand = b.Brand1
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetBrandByName));
                return null;
            }
        }

        public static List<BrandDto> GetAllBrands()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Brands
                        .Select(b => new BrandDto
                        {
                            BrandID = b.BrandId,
                            Brand = b.Brand1
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllBrands));
                return new List<BrandDto>();
            }
        }
    }
}
