using CarJenData.Repositories;
using CarJenShared.Dtos.CarDtos;
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

        private clsTrim(TrimDto trimDto)
        {
            this.ModelID = trimDto.ModelID;
            this.Trim = trimDto.Trim;
            this.ModelID = trimDto.ModelID;
        }

        static public clsTrim? Find(int? TrimID)
        {
            var trimDto = TrimRepository.GetTrimByID(TrimID);
            if(trimDto != null)
                return new clsTrim(trimDto);
            else
                return null;
        }
        static public int? FindTrimID(string Trim, string Model, string Brand)
        {
            var trimDto = TrimRepository.GetTrimIDByFullName(Trim, Model, Brand);
            if (trimDto != null) return trimDto.TrimID; else return null;
        }

        static public List<TrimDto> GetAllTrims()
        {
            return TrimRepository.GetAllTrims();
        }
    }

}
