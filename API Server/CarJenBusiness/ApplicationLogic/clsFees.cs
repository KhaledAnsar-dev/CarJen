using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsFees
    {
        public int? FeeID { get; set; }
        public byte? FeeTypeID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        private clsFees(int? feeID, byte? feeTypeID, decimal? amount, DateTime? startDate, DateTime? endDate)
        {
            FeeID = feeID;
            FeeTypeID = feeTypeID;
            Amount = amount;
            StartDate = startDate;
            EndDate = endDate;
        }

        private static Dictionary<enFeeType, string> _FeeTypeText = new Dictionary<enFeeType, string>
        {
            {enFeeType.InspectionFee,"Inspection Fee" },
            {enFeeType.ReportFee,"Report Fee" },
            {enFeeType.PublishingFee,"Publish Fee" },
            {enFeeType.ServiceFee,"Service Fee" }
        };

        public enum enFeeType
        {
            InspectionFee = 1,
            ReportFee = 2,
            PublishingFee = 3,
            ServiceFee = 4
        }

        public static string GetFeeTypeText(enFeeType feeType)
        {
            //return _FeeTypeText[feeType];

            switch (feeType)
            {
                case enFeeType.InspectionFee:
                    {
                        return "Inspection Fee";
                    }
                case enFeeType.ReportFee:
                    {
                        return "Report Fee";
                    }
                case enFeeType.PublishingFee:
                    {
                        return "Publish Fee";
                    }
                default:
                    {
                        return "Service Fee";
                    }
            }
        }
        public static bool Renew(byte MainFeeType, decimal NewAmountFee)
        {
            return FeeRepository.RenewFeeFor(MainFeeType, NewAmountFee);
        }
        public static bool Delete(int MainFeeID)
        {
            return FeeRepository.DeleteUnusedFee(MainFeeID);
        }
        public static clsFees Find(enFeeType MainFeeType)
        {
            int? FeeID = null; decimal? Amount = null; DateTime? StartDate = null; DateTime? EndDate = null;

            if (FeeRepository.GetCurrentFeeByType(ref FeeID, (byte)MainFeeType, ref StartDate, ref EndDate, ref Amount))
            {
                return new clsFees(FeeID, (byte)MainFeeType, Amount, StartDate, EndDate);
            }
            else
                return null;
        }
        static public DataTable GetAllFees()
        {
            return FeeRepository.GetAllFees();
        }
    }

}
