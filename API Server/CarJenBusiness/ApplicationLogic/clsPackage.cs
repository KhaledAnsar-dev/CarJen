using CarJenData.Repositories;
using CarJenShared.Dtos.PackageDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsPackage
    {
        enum enMode { Add = 0, Update = 1 };
        enMode Mode;

        public int? PackageID { get; set; }
        public string Title { get; set; }
        public int? NumberOfReports { get; set; }
        public int? CreatedByUserID { get; set; }

        public PackageDto ToPackageDto
        {
            get
            {
                return new PackageDto
                {
                    PackageID = this.PackageID,
                    Title = this.Title,
                    NumberOfReports = this.NumberOfReports,
                    CreatedByUserID = this.CreatedByUserID
                };
            }
        }

        clsUser CreatedByUser;
        clsFees Fee;

        private clsPackage(PackageDto packageDto)
        {
            this.PackageID = packageDto.PackageID;
            this.Title = packageDto.Title;
            this.NumberOfReports = packageDto.NumberOfReports;
            this.CreatedByUserID = packageDto.CreatedByUserID;
            Fee = clsFees.Find(clsFees.enFeeType.ReportFee);
            CreatedByUser = clsUser.Find(this.CreatedByUserID);
            Mode = enMode.Update;
        }
        public clsPackage()
        {
            PackageID = null;
            Title = "";
            NumberOfReports = null;
            CreatedByUserID = null;

            Fee = clsFees.Find(clsFees.enFeeType.ReportFee);

            Mode = enMode.Add;
        }



        // CRUD Opperations
        private bool _AddPackage()
        {
            this.PackageID = PackageRepository.AddPackage(ToPackageDto, Fee.FeeID);
            return this.PackageID != null;
        }
        private bool _UpdatePackage()
        {
            return PackageRepository.UpdatePackage(ToPackageDto);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddPackage())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePackage();

            }
            return false;
        }

        static public clsPackage? Find(int? packageID)
        {
            var packageDto = PackageRepository.GetPackageByID(packageID);

            if(packageDto != null)
                return new clsPackage(packageDto);
            else
                return null;
        }
        public static bool IsPackageExist(string Title)
        {
            return PackageRepository.IsPackageExist(Title);
        }
        static public List<PackageDto> GetAllPackages()
        {
            return PackageRepository.GetAllPackages();
        }
    }

}
