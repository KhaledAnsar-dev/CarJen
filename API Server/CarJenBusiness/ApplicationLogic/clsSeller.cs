using CarJenBusiness.Helpers;
using CarJenData.DataModels;
using CarJenData.Repositories;
using CarJenShared.Dtos.SellerDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsSeller : clsPerson
    {
        enum enMode { Add = 0, Update = 1 };
        enMode Mode;
        public int? SellerID { get; set; }
        public string NationalNumber { get; set; }
        public decimal? Earnings { get; set; }
        public SellerDto ToSellerDto
        { 
            get
            {
                return new SellerDto
                {
                    SellerId = (int)this.SellerID,
                    NationalNumber = this.NationalNumber,
                    Earnings = Convert.ToDecimal(this.Earnings),
                    Person = this.toPersonDto
                };

            }
        }

        private clsSeller(SellerDto seller)
        : base(seller.Person)
        {
            this.SellerID = seller.SellerId;
            this.NationalNumber = seller.NationalNumber;
            this.Earnings = seller.Earnings;
            this.FirstName = seller.Person.FirstName;
            this.MiddleName = seller.Person.MiddleName;
            this.LastName = seller.Person.LastName;
            this.Gender = seller.Person.Gender;
            this.Phone = seller.Person.Phone;

            Mode = enMode.Update;
        }


        public clsSeller()
        {
            this.SellerID = null;
            this.NationalNumber = "";
            this.Earnings = null;

            Mode = enMode.Add;
        }


        // CRUD Opperations
        private bool _AddSeller()
        {
            this.PersonID = PersonRepository.AddPerson(this.toPersonDto);

            if (PersonID.HasValue)
            {
                this.SellerID = SellerRepository.AddSeller(PersonID, NationalNumber);
                return this.SellerID != null;
            }
            return false;
        }
        private bool _UpdateSeller()
        {
            bool Result = false;

            if (PersonRepository.UpdatePerson(this.toPersonDto))
            {
                // Update the current user
                Result = SellerRepository.UpdateSeller(SellerID, NationalNumber);
            }

            return Result;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddSeller())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateSeller();

            }
            return false;
        }
        static public bool Delete(int SellerID)
        {
            bool Result = false;
            int? CurrentPersonID = null;

            // Find User First
            clsSeller Seller = clsSeller.Find(SellerID);

            if (Seller != null)
            {
                //Get PersonID that linked to that seller
                CurrentPersonID = Seller.PersonID;
            }

            if (CurrentPersonID.HasValue)
            {
                // Delete User First than delete person because of data integrity
                if (SellerRepository.DeleteSeller(SellerID))
                    Result = PersonRepository.DeletePerson(CurrentPersonID);
            }

            return Result;
        }
        static public clsSeller Find(int? SellerID)
        {
            var sellerDto = SellerRepository.GetSellerByID(SellerID);

            if(sellerDto != null)
            {
                return new clsSeller(sellerDto);
            }
            else
                return null;

        }
        static public clsSeller Find(string NationalNumber)
        {
            var sellerDto = SellerRepository.GetSellerByNO(NationalNumber);

            if (sellerDto != null)
            {
                return new clsSeller(sellerDto);
            }
            else
                return null;

        }
        static public clsSeller FindSellerByCredentials(string userName, string password)
        {

            var sellerDto = SellerRepository.GetSellerByCredentials(userName, password);

            if (sellerDto != null)
            {
                return new clsSeller(sellerDto);
            }
            else
                return null;

        }
        static public List<SellerDto> GetAllSeller()
        {
            return SellerRepository.GetAllSellers();
        }
        public static bool isSellerExist(int SellerID)
        {
            return SellerRepository.IsSellerExist(SellerID);
        }
        public static bool isSellerExist(string NationlNo)
        {
            return SellerRepository.IsSellerExist(NationlNo);
        }

    }

}
