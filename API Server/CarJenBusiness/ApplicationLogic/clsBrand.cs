using CarJenData.Repositories;
using CarJenShared.Dtos.CarDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsBrand
    {
        public int? BrandID { get; set; }
        public string Brand { get; set; }
        public BrandDto ToBrandDto
        {
            get
            {
                return new BrandDto
                {
                    BrandID = this.BrandID,
                    Brand = this.Brand
                };                
            }
        }

        private clsBrand(BrandDto brandDto)
        {
            this.BrandID = brandDto.BrandID;
            this.Brand = brandDto.Brand;
        }

        static public clsBrand? Find(int? BrandID)
        {
            var brandDto = BrandRepository.GetBrandByID(BrandID);
            if (brandDto != null)
                return new clsBrand(brandDto);
            else
                return null;
        }
        static public clsBrand Find(string Brand)
        {
            var brandDto = BrandRepository.GetBrandByName(Brand);
            if (brandDto != null)
                return new clsBrand(brandDto);
            else
                return null;
        }

        static public List<BrandDto> GetAllBrands()
        {
            return BrandRepository.GetAllBrands();
        }

    }

}
