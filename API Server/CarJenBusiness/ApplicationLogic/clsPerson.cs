using CarJenBusiness.Helpers;
using CarJenData.Repositories;
using CarJenShared.Dtos.PersonDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsPerson
    {
        public int? PersonID { get; set; } // (Auto-Implemented Properties)
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + MiddleName + " " + LastName; }
        }
        public short Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? JoinDate { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public PersonDto? toPersonDto
        {
            get
            {
                return new PersonDto
                {
                    PersonID = this.PersonID,
                    FirstName = this.FirstName,
                    MiddleName = this.MiddleName,
                    LastName = this.LastName,
                    Gender = this.Gender,
                    DateOfBirth = this.DateOfBirth,
                    Email = this.Email,
                    Phone = this.Phone,
                    Address = this.Address,
                    JoinDate = this.JoinDate,
                    IsActive = this.IsActive,
                    Image = this.Image,
                    UserName = this.UserName,
                    Password = this.Password
                };
            }
        }

        public clsPerson(int? personID, string firstName, string middleName, string lastName,
                        short gender, DateTime? dateOfBirth, string email, string phone, string address,
                        DateTime? joinDate, bool isActive, string image, string userName, string password)
        {
            PersonID = personID;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Email = email;
            Phone = phone;
            Address = address;
            JoinDate = joinDate;
            IsActive = isActive;
            Image = image;
            UserName = userName;
            Password = password;
        }
        static protected int? AddPerson(string firstName, string middleName, string lastName,
                   short gender, DateTime? dateOfBirth, string email, string phone, string address,
                   bool isActive, string image, string userName, string password)
        {
            return PersonRepository.AddPerson(firstName, middleName, lastName, gender, dateOfBirth, email,
                phone, address, isActive, image, userName, clsSecurity.ComputeHash(password));
        }
        static protected bool UpdatePerson(int? personID, string firstName, string middleName, string lastName,
                 short gender, DateTime? dateOfBirth, string email, string phone, string address,
                 DateTime? joinDate, bool isActive, string image, string userName, string password)
        {
            return PersonRepository.UpdatePerson(personID, firstName, middleName, lastName, gender, dateOfBirth, email,
                phone, address, joinDate, isActive, image, userName, password);
        }


        public clsPerson(PersonDto person)
        {
            PersonID = person.PersonID;
            FirstName = person.FirstName;
            MiddleName = person.MiddleName;
            LastName = person.LastName;
            Gender = person.Gender;
            DateOfBirth = person.DateOfBirth;
            Email = person.Email;
            Phone = person.Phone;
            Address = person.Address;
            JoinDate = person.JoinDate;
            IsActive = person.IsActive;
            Image = person.Image;
            UserName = person.UserName;
            Password = person.Password;
        }
        public clsPerson()
        {
            PersonID = null;
            FirstName = "";
            MiddleName = "";
            LastName = "";
            Gender = 0;
            DateOfBirth = null;
            Email = "";
            Phone = "";
            Address = "";
            JoinDate = null;
            IsActive = false;
            Image = "";
            UserName = "";
            Password = "";
        }

        static public clsPerson? FindPerson(int? PersonID)
        {
            var person = PersonRepository.GetPersonByID(PersonID);
            if (person != null)
            {
                return new clsPerson(person);
            }
            return null;

        }
        static protected int? AddPerson(PersonDto personDto)
        {
            personDto.Password = clsSecurity.ComputeHash(personDto.Password);
            return PersonRepository.AddPerson(personDto);
           
        }
        static protected bool UpdatePerson(PersonDto personDto)
        {
            return PersonRepository.UpdatePerson(personDto);
        }
        static protected bool DeletePerson(int? personID)
        {
            return PersonRepository.DeletePerson(personID);
        }
    }

}
