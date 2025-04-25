using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.PackageDtos;
using CarJenShared.Dtos.SellerDtos;
using CarJenWebApi.Dtos.PackageDto;
using CarJenWebApi.Dtos.SellerDtos;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class SellerMapper
    {
        public static SellerResponseDto ToSellerResponseDto(SellerDto seller)
        {
            return new SellerResponseDto
            {
                SellerID = seller.SellerId,
                NationalNumber = seller.NationalNumber,
                Earnings = seller.Earnings,
                Gender = seller.Person.Gender == 0 ? "Female" : "Male",
                Phone = seller.Person.Phone,
                FullName = seller.Person.FullName
            };
        }
    }
}
