using CarJenData.DataModels;
using CarJenShared.Dtos.PackageDtos;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;
using static CarJenShared.Helpers.Logger;

namespace CarJenData.Repositories
{
    public class PackageRepository
    {
        public static PackageDto? GetPackageByID(int? packageID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Packages
                        .Where(p => p.PackageId == packageID)
                        .Select(p => new PackageDto
                        {
                            PackageID = p.PackageId,
                            Title = p.Title,
                            NumberOfReports = p.NumberOfReports,
                            // p.CreatedByUser refer to an ID not a object
                            CreatedByUserID = p.CreatedByUser                            
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetPackageByID));
                return null;
            }
        }

        static public int? AddPackage(PackageDto packageDto, int? FeeID)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                {

                    using (SqlCommand Command = new SqlCommand("SP_AddPackage", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@Title", packageDto.Title);
                        Command.Parameters.AddWithValue("@NumberOfReports", packageDto.NumberOfReports);
                        Command.Parameters.AddWithValue("@CreatedByUserID", packageDto.CreatedByUserID);
                        Command.Parameters.AddWithValue("@FeeID", FeeID);

                        SqlParameter OutputID = new SqlParameter("@PackageID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        Command.Parameters.Add(OutputID);


                        Connection.Open();

                        Command.ExecuteNonQuery();
                        CreatedID = (int)Command.Parameters["@PackageID"].Value;
                    }
                }

            }
            catch (Exception ex)
            {
                string methodName = MethodBase.GetCurrentMethod().Name;
                LogError(ex, methodName);
            }
            return CreatedID;
        }

        public static bool UpdatePackage(PackageDto dto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var package = context.Packages.Find(dto.PackageID);

                    if (package == null)
                        return false;

                    package.Title = dto.Title;
                    package.NumberOfReports = dto.NumberOfReports ?? 0;
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdatePackage));
                return false;
            }
        }

        public static List<PackageDto> GetAllPackages()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Packages
                        .Select(p => new PackageDto
                        {
                            PackageID = p.PackageId,
                            Title = p.Title,
                            NumberOfReports = p.NumberOfReports,
                            CreatedByUserID = p.CreatedByUser
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllPackages));
                return new List<PackageDto>();
            }
        }

        public static bool IsPackageExist(string title)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Packages.Any(p => p.Title == title);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(IsPackageExist));
                return false;
            }
        }
    }
}
