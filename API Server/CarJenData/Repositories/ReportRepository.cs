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
        public static List<FinalReportDto> GetAllApprovedReports()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var reports = context.Reports
                        .Select(r => new FinalReportDto
                        {
                            ReportId = r.ReportId,
                            FileId = r.CarDocumentation.CarInspections
                                        .FirstOrDefault().CarInspectionId, // Assuming one inspection per documentation
                            Released = r.ReleaseDate.Date,
                            SellerId = r.CarDocumentation.Seller.SellerId,
                            Brand = r.CarDocumentation.Car.Trim.Model.Brand.Brand1.Trim(),
                            Model = r.CarDocumentation.Car.Trim.Model.Model1.Trim(),
                            Trim = r.CarDocumentation.Car.Trim.Trim1.Trim(),
                            Year = r.CarDocumentation.Car.Year,
                            Mileage = r.CarDocumentation.Car.Mileage,
                            Status = r.Status == 1 ? "Active" :
                                     r.Status == 2 ? "Disabled by Seller" :
                                     r.Status == 3 ? "Disabled by Team" :
                                     r.Status == 4 ? "Sold" : "Canceled"
                        })
                        .ToList();

                    return reports;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllApprovedReports));
                return null;
            }
        }

    }
}
