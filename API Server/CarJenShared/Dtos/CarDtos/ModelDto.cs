using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.CarDtos
{
    public class ModelDto
    {
        public int? ModelID { get; set; }
        public string Model { get; set; }
        public int? BrandID { get; set; }

        public static implicit operator ModelDto(BrandDto v)
        {
            throw new NotImplementedException();
        }
    }
}
