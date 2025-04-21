using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsTrim
    {
        public int? TrimID { get; set; }
        public string Trim { get; set; }
        public int? ModelID { get; set; }

        private clsTrim(int? TrimID, string Trim, int? ModelID)
        {
            this.ModelID = ModelID;
            this.Trim = Trim;
            this.ModelID = ModelID;
        }

        static public clsTrim Find(int? TrimID)
        {
            string Trim = ""; int? ModelID = null;
            if (TrimRepository.GetTrimByID(TrimID, ref Trim, ref ModelID))
                return new clsTrim(ModelID, Trim, ModelID);
            else
                return null;
        }
        static public int? FindTrimID(string Trim, string Model, string Brand)
        {
            int? TrimID = null;
            return TrimRepository.GetTrimIDByFullName(Trim, Model, Brand, ref TrimID) ? TrimID : null;
        }

        static public DataTable GetAllTrims()
        {
            return TrimRepository.GetAllTrims();
        }
    }

}
