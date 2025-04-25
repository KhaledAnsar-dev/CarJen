using CarJenShared.Dtos.PersonDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.SellerDtos
{
    public class SellerDto
    {
        public int SellerId { get; set; }
        public string NationalNumber { get; set; } = null!;
        public decimal Earnings { get; set; }
        public PersonDto Person { get; set; }
    }
}
