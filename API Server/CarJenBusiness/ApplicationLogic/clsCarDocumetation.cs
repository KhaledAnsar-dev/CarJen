using CarJenData.Repositories;
using CarJenShared.Dtos.CarDocumentationDtos;
using CarJenShared.Dtos.CarDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsCarDocumentation
    {
        public int? CarDocumentationID { get; set; }
        public int? SellerID { get; set; }
        public int? CarID { get; set; }
        public DateTime? CreatedDate { get; set; }

        public clsSeller Seller;
        public clsCar Car;

        public CarDocumentationDto ToCarDocDto
        {
            get
            {
                return new CarDocumentationDto
                {
                    CarDocumentationID = this.CarDocumentationID,
                    CreatedDate = this.CreatedDate,
                    Seller = this.Seller.ToSellerDto,
                    Car = this.Car.ToCarDto
                };
            }
        }

        private clsCarDocumentation(CarDocumentationDto carDocDto)
        {
            CarDocumentationID = carDocDto.CarDocumentationID;
            SellerID = carDocDto.Seller.SellerId;
            CarID = carDocDto.Car.CarID;
            CreatedDate = carDocDto.CreatedDate;

            Seller = clsSeller.Find(carDocDto.Seller.SellerId);
            Car = clsCar.Find(carDocDto.Car.CarID);
        }
        public clsCarDocumentation()
        {
            CarDocumentationID = null;
            SellerID = null;
            CarID = null;
            CreatedDate = null;
        }

        public bool AddCarDocumentation()
        {
            this.CarDocumentationID = CarDocumentationRepository.AddCarDocumentation(SellerID, CarID);
            return this.CarDocumentationID != null;
        }
        static public clsCarDocumentation? Find(int? CarDocumentationID)
        {
            var carDocDto = CarDocumentationRepository.GetCarDocumentationByID(CarDocumentationID);

            if(carDocDto != null)
                return new clsCarDocumentation(carDocDto);
            else
                return null;
        }
        static public List<CarDocumentationDto> GetAllCarDocumentations()
        {
            return CarDocumentationRepository.GetAllCarDocumentations();
        }
        public static bool isCarDocumentationExist(int CarDocumentationID)
        {
            return CarDocumentationRepository.IsCarDocumentationExist(CarDocumentationID);
        }
    }

}
