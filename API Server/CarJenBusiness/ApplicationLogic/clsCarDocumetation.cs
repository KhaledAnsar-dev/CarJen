using CarJenData.Repositories;
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


        private clsCarDocumentation(int? carDocumentationID, int? sellerID, int? carID, DateTime? createdDate)
        {
            CarDocumentationID = carDocumentationID;
            SellerID = sellerID;
            CarID = carID;
            CreatedDate = createdDate;

            Seller = clsSeller.Find(SellerID);
            Car = clsCar.Find(CarID);
        }
        public clsCarDocumentation()
        {
            CarDocumentationID = null;
            SellerID = null;
            CarID = null;
            CreatedDate = null;
        }

        public bool _AddCarDocumentation()
        {
            this.CarDocumentationID = CarDocumentationRepository.AddCarDocumentation(SellerID, CarID);
            return this.CarDocumentationID != null;
        }
        static public clsCarDocumentation Find(int? CarDocumentationID)
        {
            int? SellerID = null; int? CarID = null; DateTime? CreatedDate = null;

            if (CarDocumentationRepository.GetCarDocumentationByID(CarDocumentationID, ref SellerID,
                   ref CarID, ref CreatedDate))
            {
                return new clsCarDocumentation(CarDocumentationID, SellerID, CarID, CreatedDate);
            }
            else
                return null;

        }
        static public DataTable GetAllCarDocumentations()
        {
            return CarDocumentationRepository.GetAllCarDocumentations();
        }
        public static bool isCarDocumentationExist(int CarDocumentationID)
        {
            return CarDocumentationRepository.IsCarDocumentationExist(CarDocumentationID);
        }


    }

}
