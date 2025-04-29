using CarJenData.DataModels;
using CarJenShared.Dtos.ReportDtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using static CarJenShared.Helpers.Logger;

namespace CarJenData.Repositories
{
    public class ReportRepository
    {
        //public static bool GetReportByID(int? reportID, ref int? carDocumentationID, ref DateTime? releasedDate, ref short? status)
        //{
        //    bool isFound = false;

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
        //        {
        //            using (SqlCommand command = new SqlCommand("SP_GetReportByID", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                connection.Open();
        //                command.Parameters.AddWithValue("@ReportID", reportID);

        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        isFound = true;
        //                        carDocumentationID = reader["CarDocumentationID"] != DBNull.Value ? Convert.ToInt32(reader["CarDocumentationID"]) : (int?)null;
        //                        status = Convert.ToInt16(reader["Status"]);
        //                        releasedDate = Convert.ToDateTime(reader["ReleaseDate"]);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // string methodName = MethodBase.GetCurrentMethod().Name;
        //        // Log or handle the exception if needed
        //    }

        //    return isFound;
        //}

        public static int? AddReport(int carDocumentationID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var report = new Report
                    {
                        CarDocumentationId = carDocumentationID,
                        ReleaseDate = DateTime.Now,
                        Status = 1 // Report should be active
                    };

                    context.Reports.Add(report);
                    context.SaveChanges();

                    return report.ReportId;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(AddReport));
                return null;
            }
        }
        public static bool UpdateReportStatus(int? reportID, short? status)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var report = context.Reports.Find(reportID);

                    if (report == null)
                        return false;

                    report.Status = (byte)status;
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdateReportStatus));
                return false;
            }
        }
        public static List<ReportDto> GetAllReports()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var reports = context.CarInspections
                        .Select(ci => new ReportDto
                        {
                            FileId = ci.CarInspectionId,
                            ExpDate = Convert.ToDateTime(ci.ExpDate),
                            InspectedBy = ci.Team.TeamCode,
                            SellerId = ci.CarDocumentation.Seller.SellerId,
                            Brand = ci.CarDocumentation.Car.Trim.Model.Brand.Brand1,
                            Model = ci.CarDocumentation.Car.Trim.Model.Model1,
                            Trim = ci.CarDocumentation.Car.Trim.Trim1,
                            Year = ci.CarDocumentation.Car.Year,
                            Mileage = ci.CarDocumentation.Car.Mileage,
                            Status = ci.Status == 0 ? "Pending" :
                                     ci.Status == 1 ? "Under Inspection" :
                                     ci.Status == 2 ? "Approved" :
                                     ci.Status == 3 ? "Completed" :
                                     ci.Status == 4 ? "Failed" : "Cancelled"
                        })
                        .ToList();

                    return reports;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllReports));
                return null;
            }
        }
    }
}
