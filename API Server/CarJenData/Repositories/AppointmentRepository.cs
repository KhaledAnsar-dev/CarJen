using CarJenData.DataModels;
using CarJenShared.Dtos.AppointmentDtos;
using CarJenShared.Dtos.CarDocumentationDtos;
using CarJenShared.Dtos.CarDtos;
using CarJenShared.Dtos.CarInspectionDtos;
using CarJenShared.Dtos.PersonDtos;
using CarJenShared.Dtos.SellerDtos;
using Microsoft.Data.SqlClient; 
using System;
using System.Data;
using static CarJenShared.Helpers.Logger;

namespace CarJenData.Repositories
{
    public class AppointmentRepository
    {
        public static AppointmentDto? GetAppointmentByID(int? appointmentID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Appointments
                        .Where(a => a.AppointmentId == appointmentID)
                        .Select(a => new AppointmentDto
                        {
                            AppointmentID = a.AppointmentId,
                            CarDocumentationID = a.CarDocumentationId,
                            CarDocumentation = new CarDocumentationDto
                            {
                                Seller = new SellerDto
                                {
                                    SellerId = a.CarDocumentation.Seller.SellerId,
                                    Person = new PersonDto
                                    {
                                        FirstName = a.CarDocumentation.Seller.Person.FirstName,
                                        MiddleName = a.CarDocumentation.Seller.Person.MiddleName,
                                        LastName = a.CarDocumentation.Seller.Person.LastName
                                    }
                                },
                                Car = new CarDto
                                {
                                    CarID = a.CarDocumentation.Car.CarId
                                }
                            },
                            Status = a.Status == 0 ? "Scheduled" :
                                     a.Status == 1 ? "Under Inspection" :
                                     a.Status == 2 ? "Approved" :
                                     a.Status == 3 ? "Rejected" : "Cancelled",
                            AppointmentDate = a.AppointmentDate,
                            PublishFee = a.PublishFeeId.HasValue ? a.PublishFee.Amount : 0
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAppointmentByID));
                return null;
            }
        }
        public static AppointmentDto? GetAppointmentByCarDocID(int? carDocumentationID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Appointments
                        .Where(a => a.CarDocumentationId == carDocumentationID && a.Status != 4)
                        .Select(a => new AppointmentDto
                        {
                            AppointmentID = a.AppointmentId,
                            CarDocumentationID = a.CarDocumentationId,
                            CarDocumentation = new CarDocumentationDto
                            {
                                Seller = new SellerDto
                                {
                                    SellerId = a.CarDocumentation.Seller.SellerId,
                                    Person = new PersonDto
                                    {
                                        FirstName = a.CarDocumentation.Seller.Person.FirstName,
                                        MiddleName = a.CarDocumentation.Seller.Person.MiddleName,
                                        LastName = a.CarDocumentation.Seller.Person.LastName
                                    }
                                },
                                Car = new CarDto
                                {
                                    CarID = a.CarDocumentation.Car.CarId
                                }
                            },
                            Status = a.Status == 0 ? "Scheduled" :
                                     a.Status == 1 ? "Under Inspection" :
                                     a.Status == 2 ? "Approved" :
                                     a.Status == 3 ? "Rejected" : "Cancelled",
                            AppointmentDate = a.AppointmentDate,
                            PublishFee = a.PublishFeeId.HasValue ? a.PublishFee.Amount : 0
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAppointmentByCarDocID));
                return null;
            }
        }
        public static int? AddAppointment(int? carDocumentationID, DateTime? appointmentDate)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var appointment = new Appointment
                    {
                        CarDocumentationId = carDocumentationID ?? 0,
                        Status = 0, // Scheduled
                        AppointmentDate = Convert.ToDateTime(appointmentDate),
                        CreatedDate = DateTime.Now,
                        PublishFeeId = null
                    };

                    context.Appointments.Add(appointment);
                    context.SaveChanges();

                    return appointment.AppointmentId;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(AddAppointment));
                return null;
            }
        }
        public static bool UpdateAppointmentStatus(int? appointmentID, short? status)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var appointment = context.Appointments.Find(appointmentID);

                    if (appointment == null)
                        return false;

                    appointment.Status = (byte)status;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdateAppointmentStatus));
                return false;
            }
        }
        public static bool UpdatePublishFee(int? appointmentID, int? publishFeeID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var appointment = context.Appointments.Find(appointmentID);

                    if (appointment == null)
                        return false;

                    appointment.PublishFeeId = publishFeeID;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdatePublishFee));
                return false;
            }
        }
        public static List<AppointmentDto> GetAllAppointments()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Appointments
                        .Select(a => new AppointmentDto
                        {
                            AppointmentID = a.AppointmentId,
                            CarDocumentationID = a.CarDocumentationId,
                            CarDocumentation = new CarDocumentationDto
                            {
                                Seller = new SellerDto
                                {
                                    SellerId = a.CarDocumentation.Seller.SellerId,
                                    Person = new PersonDto
                                    {
                                        FirstName = a.CarDocumentation.Seller.Person.FirstName,
                                        MiddleName = a.CarDocumentation.Seller.Person.MiddleName,
                                        LastName = a.CarDocumentation.Seller.Person.LastName
                                    }                                    
                                },
                                Car = new CarDto
                                {
                                    CarID = a.CarDocumentation.Car.CarId
                                }
                            },
                            Status = a.Status == 0 ? "Scheduled" :
                                     a.Status == 1 ? "Under Inspection" :
                                     a.Status == 2 ? "Approved" :
                                     a.Status == 3 ? "Rejected" : "Cancelled",
                            AppointmentDate = a.AppointmentDate,
                            PublishFee = a.PublishFeeId.HasValue ? a.PublishFee.Amount : 0
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllAppointments));
                return new List<AppointmentDto>();
            }
        }
        public static bool IsAppointmentExist(int appointmentID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Appointments.Any(a => a.AppointmentId == appointmentID);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(IsAppointmentExist));
                return false;
            }
        }
        public static int? GetAppointmentIDByCarDocID(int carDocumentationID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Appointments
                        .Where(a => a.CarDocumentationId == carDocumentationID)
                        .Select(a => a.AppointmentId)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAppointmentIDByCarDocID));
                return null;
            }
        }
        public static bool Delete(int appointmentID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var appointment = context.Appointments.Find(appointmentID);

                    if (appointment == null)
                        return false;

                    context.Appointments.Remove(appointment);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(Delete));
                return false;
            }
        }
        static public List<PendingTechnicalInspectionCarDto> GetCarsReadyForTechnicalInspection()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.PreApprovedCarsViews
                        .Select(v => new PendingTechnicalInspectionCarDto
                        {
                            CarInspectionID = v.FileId,
                            CarOwner = v.CarOwner,
                            Brand = v.Brand,
                            Model = v.Model,
                            Trim = v.Trim,
                            Year = v.Year,
                            Fuel = v.Fuel,
                            Status = v.Status
                        })
                        .ToList(); ;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetCarsReadyForTechnicalInspection));
                return null;
            }
        }


    }
}


