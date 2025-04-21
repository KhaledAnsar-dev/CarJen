using CarJenData.Repositories;
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

        clsUser CreatedByUser;
        clsFees Fee;

        private clsPackage(int? PackageID, string Title, int? NumberOfReports, int? CreatedByUserID)
        {
            this.PackageID = PackageID;
            this.Title = Title;
            this.NumberOfReports = NumberOfReports;
            this.CreatedByUserID = CreatedByUserID;
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
            this.PackageID = PackageRepository.AddPackage(Title, NumberOfReports, CreatedByUserID, Fee.FeeID);
            return this.PackageID != null;
        }
        private bool _UpdatePackage()
        {
            return PackageRepository.UpdatePackage(PackageID, Title, NumberOfReports);
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

        static public clsPackage Find(int? PackageID)
        {
            string Title = ""; int? NumberOfReports = null;
            int? CreatedByUserID = null;

            if (PackageRepository.GetPackageByID(PackageID, ref Title, ref NumberOfReports, ref CreatedByUserID))
                return new clsPackage(PackageID, Title, NumberOfReports, CreatedByUserID);
            else
                return null;
        }

        public static bool IsPackageExist(string Title)
        {
            return PackageRepository.IsPackageExist(Title);
        }

        static public DataTable GetAllPackages()
        {
            return PackageRepository.GetAllPackages();
        }
    }

}
