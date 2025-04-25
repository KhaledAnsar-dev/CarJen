using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.MemberDtos;
using CarJenShared.Dtos.PackageDtos;
using CarJenWebApi.Dtos.MemeberDtos;
using CarJenWebApi.Dtos.PackageDto;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class PackageMapper
    {
        public static clsPackage ToclsPackage(CreatePackageDto package)
        {
            return new clsPackage
            {
                Title = package.Title,
                NumberOfReports = package.NumberOfReports,
                CreatedByUserID = package.CreatedByUserID
            };
        }
        public static PackageResponseDto ToPackageResponseDto(PackageDto package)
        {
            return new PackageResponseDto
            {
                PackageID = package.PackageID,
                Title = package.Title,
                NumberOfReports = package.NumberOfReports,
                CreatedByUser = clsUser.Find(package.CreatedByUserID).FullName,
                ReportFee = clsFees.Find(clsFees.enFeeType.ReportFee).Amount
            };
        }
    }
}
