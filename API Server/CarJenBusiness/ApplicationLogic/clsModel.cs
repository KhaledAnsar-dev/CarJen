using CarJenData.Repositories;
using CarJenShared.Dtos.CarDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsModel
    {
        public int? ModelID { get; set; }
        public string Model { get; set; }
        public int? BrandID { get; set; }

        private clsModel(ModelDto modelDto)
        {
            this.ModelID = modelDto.ModelID;
            this.Model = modelDto.Model;
            this.BrandID = modelDto.BrandID;
        }

        static public clsModel? Find(int? ModelID)
        {
            var modelDto = ModelRepository.GetModelByID(ModelID);

            if (modelDto != null)
                return new clsModel(modelDto);
            return null;
        }
        static public clsModel Find(string Brand, string Model)
        {
            var modelDto = ModelRepository.GetModelByFullName(Brand, Model);

            if (modelDto != null)
                return new clsModel(modelDto);
            return null;
        }

        static public List<ModelDto> GetAllModels()
        {
            return ModelRepository.GetAllModels();
        }
    }

}
