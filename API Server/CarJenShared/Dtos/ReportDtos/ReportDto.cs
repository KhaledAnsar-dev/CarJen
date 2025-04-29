using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarJenShared.Dtos.CarInspectionDtos;

namespace CarJenShared.Dtos.ReportDtos
{
    public class ReportDto
    {
        public int FileId { get; set; }          
        public DateTime ExpDate { get; set; }    
        public string InspectedBy { get; set; }  
        public int SellerId { get; set; }        
        public string Brand { get; set; }        
        public string Model { get; set; }        
        public string Trim { get; set; }         
        public int Year { get; set; }            
        public int Mileage { get; set; }         
        public string Status { get; set; }       
    }
}
