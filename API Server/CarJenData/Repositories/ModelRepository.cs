using CarJenData.DataModels;
using CarJenShared.Dtos.CarDto;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static CarJenShared.Helpers.Logger;
namespace CarJenData.Repositories
{
    public static class ModelRepository
    {
        // Note : Brand1 , Model1 refer to the name
        public static ModelDto? GetModelByID(int? modelID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Models
                        .Where(m => m.ModelId == modelID)
                        .Select(m => new ModelDto
                        {
                            ModelID = m.ModelId,
                            Model = m.Model1,
                            BrandID = m.BrandId
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetModelByID));
                return null;
            }
        }

        public static ModelDto? GetModelByFullName(string brand, string model)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Models
                        .Where(m => m.Brand.Brand1 == brand && m.Model1 == model)
                        .Select(m => new ModelDto
                        {
                            ModelID = m.ModelId,
                            Model = m.Model1,
                            BrandID = m.BrandId
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetModelByFullName));
                return null;
            }
        }

        public static List<ModelDto> GetAllModels()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Models
                        .Select(m => new ModelDto
                        {
                            ModelID = m.ModelId,
                            Model = m.Model1,
                            BrandID = m.BrandId
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllModels));
                return new List<ModelDto>();
            }
        }
    }
}

