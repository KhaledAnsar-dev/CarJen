using CarJenBusiness.Helpers;
using CarJenData.Repositories;
using CarJenShared.Dtos.PersonDtos;
using CarJenShared.Dtos.RoleDtos;
using CarJenShared.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsUser : clsPerson
    {
        enum enMode { Add = 0, Update = 1 };
        enMode Mode;

        public int? UserID { get; set; }
        public short? RoleID { get; set; }
        public clsRole Role;
        public string NationalNumber { get; set; }
        public short EvaluationScore { get; set; }
        public float? Bonus
        {
            get;
        }
        public int? CreatedByUserID { get; set; }
        public clsUser CreatedByUser;
        public UserDto toUserDto
        {
            get {
                return new UserDto
                {
                    UserId = this.UserID?? 0,
                    NationalNumber = this.NationalNumber,
                    EvaluationScore = this.EvaluationScore,
                    CreatedByUserId = this.CreatedByUserID ?? 0,
                    Person = this.toPersonDto,
                    Role = this.Role.toRoleDto
                };
            }
        }
       


        private clsUser(UserDto userDto):base(userDto.Person)
        {
            this.UserID = userDto.UserId;
            this.RoleID = (short)userDto.Role.roleID;
            this.Role = clsRole.Find((int)this.RoleID);
            this.NationalNumber = userDto.NationalNumber;
            this.EvaluationScore = (short)userDto.EvaluationScore;
            this.CreatedByUserID = userDto.CreatedByUserId;
            this.Bonus = clsUser.GetBonus(this.UserID) ?? 0;

            if (userDto.CreatedByUserId != null)
                CreatedByUser = clsUser.Find(userDto.CreatedByUserId);
            else
                CreatedByUser = null;

            Mode = enMode.Update;
        }
        public clsUser()
        {
            this.UserID = null;
            this.RoleID = null;
            this.NationalNumber = "";
            this.EvaluationScore = 0;
            this.CreatedByUserID = null;
            this.Bonus = 0;
            Mode = enMode.Add;
        }


        // CRUD Opperations

        private bool _AddUser()
        {

            this.PersonID = clsPerson.AddPerson(this.toPersonDto);

            if (PersonID.HasValue)
            {
                // Convert the current User business object to simple use dto using "toDto"
                this.UserID = UserRepository.AddUser(this.toUserDto);
                return this.UserID != null;
            }
            return false;
        }
        private bool _UpdateUser()
        {
            bool Result = false;

            if (clsPerson.UpdatePerson(this.toPersonDto))
            {
                // Update the current user
                Result = UserRepository.UpdateUser(this.toUserDto);
            }

            return Result;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }
            return false;
        }
        static public bool Delete(int UserID)
        {
            bool Result = false;
            int? CurrentPersonID = null;

            // Find User First
            clsUser User = clsUser.Find(UserID);

            if (User != null)
            {
                //Get PersonID that linked to this user
                CurrentPersonID = User.PersonID;
            }

            if (CurrentPersonID.HasValue)
            {
                // Delete User First than delete person because of data integrity
                if (UserRepository.DeleteUser(UserID))
                    Result = clsPerson.DeletePerson(CurrentPersonID);
            }

            return Result;
        }
        static public clsUser? Find(int? UserID)
        {
            var userDto = UserRepository.GetUserById((int)UserID);

            if(userDto != null)
            {
                return new clsUser(userDto);
            }
            return null;
        }
        static public clsUser Find(string NationalNumber)
        {
            var userDto = UserRepository.GetUserByNO(NationalNumber);

            if (userDto != null)
            {
                return new clsUser(userDto);
            }
            return null;
        }

        // move toward clsPerson
        //static public clsUser FindUserByCredentials(string userName, string password)
        //{
        //    int? userID = null; int? personID = null; string firstName = ""; string middleName = ""; string lastName = "";
        //    short gender = 0; DateTime? dateOfBirth = null; string email = "";
        //    string phone = ""; string address = ""; DateTime? joinDate = null; bool isActive = false; string image = "";
        //    short? roleID = null; string nationalNumber = ""; short evaluationScore = 0;
        //    int? createdByUserID = null;

        //    if (UserRepository.GetUserByCredentials(ref userID, ref personID,
        //           ref firstName, ref middleName, ref lastName,
        //           ref gender, ref dateOfBirth, ref email, ref phone,
        //           ref address, ref joinDate, ref isActive, ref image,
        //           userName, clsSecurity.ComputeHash(password), ref roleID, ref nationalNumber,
        //           ref evaluationScore, ref createdByUserID))
        //    {
        //        return new clsUser(userID, personID, firstName, middleName, lastName,
        //        gender, dateOfBirth, email, phone, address, joinDate,
        //        isActive, image, userName, password, roleID,
        //        nationalNumber, evaluationScore, createdByUserID);
        //    }
        //    else
        //        return null;

        //}

        static public List<UserDto> GetAllUsers()
        {
            return UserRepository.GetAllUsers();
        }
        public static bool isUserExist(int UserID)
        {
            return UserRepository.IsUserExist(UserID);
        }
        public static bool isUserExist(string NationlNo)
        {
            return UserRepository.IsUserExist(NationlNo);
        }
        public static bool IsAnyUserRegistered()
        {
            return UserRepository.IsAnyUserRegistered();
        }
        static public float? GetBonus(int? UserID)
        {
            float? bonus = null;

            if (UserRepository.GetUserBonus(UserID, ref bonus))
            {
                return bonus;
            }
            else
                return null;

        }

    }

}
