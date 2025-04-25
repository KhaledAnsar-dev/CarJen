using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.FeeDtos;
using CarJenShared.Dtos.TeamDtos;
using CarJenWebApi.Dtos.FeeDtos;
using CarJenWebApi.Dtos.TeamDtos;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class FeeMapper
    {
        public static clsFees ToclsFee(UpdateFeeDto fee)
        {
            return new clsFees
            {
                Amount = fee.Amount,
                FeeTypeID = fee.FeeTypeID
            };
        }
        public static FeeResponseDto ToTeamResponseDto(FeeDto feeDto)
        {
            return new FeeResponseDto
            {
                FeeID = feeDto.FeeID,
                FeeType = CarJenShared.Helpers.Common.GetFeeTypeName(feeDto.FeeTypeID),
                Amount = feeDto.Amount,
                StartDate = feeDto.StartDate,
                EndDate = feeDto.EndDate?? null
            };
        }
    }
}
