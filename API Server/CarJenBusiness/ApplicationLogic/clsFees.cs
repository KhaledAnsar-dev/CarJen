using CarJenData.Repositories;
using CarJenShared.Dtos.FeeDtos;
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
        public FeeDto ToFeeDto
        {
            get
            {
                return new FeeDto
                {
                    FeeID = this.FeeID,
                    FeeTypeID = (byte)this.FeeTypeID,
                    Amount = this.Amount,
                    StartDate = this.StartDate,
                    EndDate = this.EndDate,
                };
            }
        }
        private clsFees(FeeDto feeDto)
        {
            FeeID = feeDto.FeeID;
            FeeTypeID = feeDto.FeeTypeID;
            Amount = feeDto.Amount;
            StartDate = feeDto.StartDate;
            EndDate = feeDto.EndDate;
        }
        public clsFees()
        {
            
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
        public static bool Renew(byte mainFeeType, decimal newAmountFee)
        {
            return FeeRepository.RenewFeeFor(mainFeeType, newAmountFee);
        }
        public static bool Delete(int mainFeeID)
        {
            return FeeRepository.DeleteUnusedFee(mainFeeID);
        }
        public static clsFees? Find(enFeeType mainFeeType)
        {

            var feeDto = FeeRepository.GetCurrentFeeByType((byte)mainFeeType);

            if(feeDto != null)
                return new clsFees(feeDto);
            else
                return null;
        }
        static public List<FeeDto> GetAllFees()
        {
            return FeeRepository.GetAllFees();
        }
    }

}
