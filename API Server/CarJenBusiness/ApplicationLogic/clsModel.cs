using CarJenData.Repositories;
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

        private clsModel(int? ModelID, string Model, int? BrandID)
        {
            this.ModelID = ModelID;
            this.Model = Model;
            this.BrandID = BrandID;
        }

        static public clsModel Find(int? ModelID)
        {
            string Model = ""; int? BrandID = null;
            if (ModelRepository.GetModelByID(ModelID, ref Model, ref BrandID))
                return new clsModel(ModelID, Model, BrandID);
            else
                return null;
        }
        static public clsModel Find(string Brand, string Model)
        {
            int? ModelID = null; int? BrandID = null;
            if (ModelRepository.GetModelByFullName(Brand, Model, ref ModelID, ref BrandID))
                return new clsModel(ModelID, Model, BrandID);
            else
                return null;
        }

        static public DataTable GetAllModels()
        {
            return ModelRepository.GetAllModels();
        }
    }

}
