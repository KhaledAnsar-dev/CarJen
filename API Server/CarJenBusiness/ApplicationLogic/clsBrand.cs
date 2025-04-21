using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsBrand
    {
        public int? BrandID { get; set; }
        public string Brand { get; set; }

        private clsBrand(int? BrandID, string Brand)
        {
            this.BrandID = BrandID;
            this.Brand = Brand;
        }

        static public clsBrand Find(int? BrandID)
        {
            string Brand = "";
            if (BrandRepository.GetBrandByID(BrandID, ref Brand))
                return new clsBrand(BrandID, Brand);
            else
                return null;
        }
        static public clsBrand Find(string Brand)
        {
            int? BrandID = null;
            if (BrandRepository.GetBrandByName(Brand, ref BrandID))
                return new clsBrand(BrandID, Brand);
            else
                return null;
        }

        static public DataTable GetAllBrands()
        {
            return BrandRepository.GetAllBrands();
        }

    }

}
