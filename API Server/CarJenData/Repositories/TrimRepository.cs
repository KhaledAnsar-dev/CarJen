using CarJenData.DataModels;
using CarJenShared.Dtos.CarDto;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;
using static CarJenShared.Helpers.Logger;

namespace CarJenData.Repositories
{
    public static class TrimRepository
    {
        public static TrimDto? GetTrimByID(int? trimID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Trims
                        .Where(t => t.TrimId == trimID)
                        .Select(t => new TrimDto
                        {
                            TrimID = t.TrimId,
                            Trim = t.Trim1,
                            ModelID = t.ModelId
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetTrimByID));
                return null;
            }
        }
        public static TrimDto? GetTrimIDByFullName(string trim, string model, string brand)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Trims
                        .Where(t => t.Trim1 == trim &&
                                    t.Model.Model1 == model &&
                                    t.Model.Brand.Brand1 == brand)
                        .Select(t => new TrimDto
                        {
                            TrimID = t.TrimId,
                            Trim = t.Trim1,
                            ModelID = t.ModelId
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetTrimIDByFullName));
                return null;
            }
        }
        public static List<TrimDto> GetAllTrims()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Trims
                        .Select(t => new TrimDto
                        {
                            TrimID = t.TrimId,
                            Trim = t.Trim1,
                            ModelID = t.ModelId
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllTrims));
                return new List<TrimDto>();
            }
        }
    }
}
