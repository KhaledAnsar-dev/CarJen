using CarJenBusiness.Helpers;
using CarJenData.Repositories;
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

        private clsSeller(int? SellerID, int? personID, string firstName, string middleName, string lastName,
                   short gender, DateTime? dateOfBirth, string email, string phone, string address,
                   DateTime? joinDate, bool isActive, string image, string userName, string password,
                   string nationalNumber, decimal? Earnings)
        : base(personID, firstName, middleName, lastName, gender, dateOfBirth, email, phone, address, joinDate, isActive, image, userName, password)
        {
            this.SellerID = SellerID;
            this.NationalNumber = nationalNumber;
            this.Earnings = Earnings;

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
            this.PersonID = clsPerson.AddPerson(FirstName, MiddleName, LastName, Gender, DateOfBirth,
                Email, Phone, Address, IsActive, Image, UserName, Password);

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
            if (clsPerson.UpdatePerson(PersonID, FirstName, MiddleName, LastName, Gender, DateOfBirth,
                Email, Phone, Address, JoinDate, IsActive, Image, UserName, Password))
            {
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
                    Result = clsPerson.DeletePerson(CurrentPersonID);
            }

            return Result;
        }
        static public clsSeller Find(int? SellerID)
        {
            int? personID = null; string firstName = ""; string middleName = ""; string lastName = "";
            short gender = 0; DateTime? dateOfBirth = null; string email = "";
            string phone = ""; string address = ""; DateTime? joinDate = null; bool isActive = false; string image = "";
            string userName = ""; string password = ""; string nationalNumber = ""; decimal? earnings = null;

            if (SellerRepository.GetSellerByID(SellerID, ref personID,
                   ref firstName, ref middleName, ref lastName,
                   ref gender, ref dateOfBirth, ref email, ref phone,
                   ref address, ref joinDate, ref isActive, ref image,
                   ref userName, ref password, ref nationalNumber,
                   ref earnings))
            {
                return new clsSeller(SellerID, personID, firstName, middleName, lastName,
                gender, dateOfBirth, email, phone, address, joinDate,
                isActive, image, userName, password, nationalNumber, earnings);
            }
            else
                return null;

        }
        static public clsSeller Find(string NationalNumber)
        {
            int? SellerID = null; int? personID = null; string firstName = ""; string middleName = ""; string lastName = "";
            short gender = 0; DateTime? dateOfBirth = null; string email = "";
            string phone = ""; string address = ""; DateTime? joinDate = null; bool isActive = false; string image = "";
            string userName = ""; string password = ""; decimal? earnings = null;

            if (SellerRepository.GetSellerByNO(NationalNumber, ref SellerID, ref personID,
                   ref firstName, ref middleName, ref lastName,
                   ref gender, ref dateOfBirth, ref email, ref phone,
                   ref address, ref joinDate, ref isActive, ref image,
                   ref userName, ref password, ref earnings))
            {
                return new clsSeller(SellerID, personID, firstName, middleName, lastName,
                gender, dateOfBirth, email, phone, address, joinDate,
                isActive, image, userName, password, NationalNumber, earnings);
            }
            else
                return null;

        }
        static public clsSeller FindSellerByCredentials(string userName, string password)
        {
            int? SellerID = null; int? personID = null; string firstName = ""; string middleName = ""; string lastName = "";
            short gender = 0; DateTime? dateOfBirth = null; string email = "";
            string phone = ""; string address = ""; DateTime? joinDate = null; bool isActive = false; string image = "";
            string nationalNumber = ""; decimal? earnings = null;

            if (SellerRepository.GetSellerByCredentials(ref SellerID, ref personID,
                   ref firstName, ref middleName, ref lastName,
                   ref gender, ref dateOfBirth, ref email, ref phone,
                   ref address, ref joinDate, ref isActive, ref image,
                   userName, clsSecurity.ComputeHash(password), ref nationalNumber,
                   ref earnings))
            {
                return new clsSeller(SellerID, personID, firstName, middleName, lastName,
                gender, dateOfBirth, email, phone, address, joinDate,
                isActive, image, userName, password, nationalNumber, earnings);
            }
            else
                return null;

        }

        static public DataTable GetAllSeller()
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
