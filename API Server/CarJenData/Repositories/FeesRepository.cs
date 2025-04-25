using CarJenData.DataModels;
using CarJenShared.Dtos.FeeDtos;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Security.AccessControl;
using static CarJenShared.Helpers.Logger;

namespace CarJenData.Repositories
{
    public class FeeRepository
    {
        public static FeeDto? GetCurrentFeeByType(byte? mainFeeType)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.MainFees.
                        Where(f => f.MainFeeType == mainFeeType && f.EndDate == null)
                        .Select(f => new FeeDto
                        {
                            FeeID = f.MainFeeId,
                            FeeTypeID = f.MainFeeType,
                            Amount = f.Amount,
                            StartDate = f.StartDate,
                            EndDate = f.EndDate
                        })
                        .FirstOrDefault();
                    
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetCurrentFeeByType));
                return null;

            }

        }
        public static bool RenewFeeFor(byte mainFeeType, decimal newAmountFee)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_RenewFee", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@MainFeeType", mainFeeType);
                    Command.Parameters.AddWithValue("@NewAmountFee", newAmountFee);

                    Connection.Open();
                    RowAffected = Command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(RenewFeeFor));
            }

            return RowAffected > 0;
        }
        public static bool DeleteUnusedFee(int mainFeeId)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var fee = context.MainFees
                        .FirstOrDefault(f => f.MainFeeId == mainFeeId && f.EndDate == null);

                    if (fee == null)
                        return false;

                    context.MainFees.Remove(fee);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(DeleteUnusedFee));
                return false;
            }
        }
        public static List<FeeDto> GetAllFees()
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.MainFees
                        .Select(f => new FeeDto
                        {
                            FeeID = f.MainFeeId,
                            FeeTypeID = f.MainFeeType,
                            Amount = f.Amount,
                            StartDate = f.StartDate,
                            EndDate = f.EndDate
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAllFees));
                return new List<FeeDto>();
            }
        }
    }
}
