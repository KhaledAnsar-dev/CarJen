using CarJenShared.Dtos.CarDtos;
using CarJenShared.Dtos.SellerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.CarDocumentationDtos
{
    public class CarDocumentationDto
    {
        public int? CarDocumentationID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public SellerDto Seller { get; set; }
        public CarDto Car { get; set; }
    }
}
