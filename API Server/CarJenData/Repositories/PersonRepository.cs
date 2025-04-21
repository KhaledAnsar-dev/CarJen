using CarJenData.DataModels;
using CarJenShared.Dtos.PersonDtos;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;
using static CarJenShared.Helpers.Logger;
namespace CarJenData.Repositories
{
    public class PersonRepository
    {
        public static PersonDto? GetPersonByID(int? personID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.People
                        .Where(p => p.PersonId == personID)
                        .Select(p => new PersonDto
                        {
                            PersonID = p.PersonId,
                            FirstName = p.FirstName,
                            MiddleName = p.MiddleName,
                            LastName = p.LastName,
                            Gender = p.Gender,
                            DateOfBirth = p.DateOfBirth,
                            Email = p.Email,
                            Phone = p.Phone,
                            Address = p.Address,
                            JoinDate = p.JoinDate,
                            IsActive = p.IsActive,
                            Image = p.Image,
                            UserName = p.UserName,
                            Password = p.Password
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetPersonByID));
                return null;
            }
        }
        public static int? AddPerson(PersonDto personDto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var personEntity = new Person
                    {
                        FirstName = personDto.FirstName,
                        MiddleName = personDto.MiddleName,
                        LastName = personDto.LastName,
                        Gender = Convert.ToByte(personDto.Gender),
                        DateOfBirth = Convert.ToDateTime(personDto.DateOfBirth),
                        Email = personDto.Email,
                        Phone = personDto.Phone,
                        Address = personDto.Address,
                        JoinDate = Convert.ToDateTime(personDto.JoinDate),
                        IsActive = personDto.IsActive,
                        Image = personDto.Image,
                        UserName = personDto.UserName,
                        Password = personDto.Password
                    };

                    context.People.Add(personEntity);
                    context.SaveChanges();
                    return personEntity.PersonId;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(AddPerson));
                return 0;
            }

        }
        public static bool UpdatePerson(PersonDto personDto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var personEnrtity = context.People.Find(personDto.PersonID);

                    if (personEnrtity == null)
                        return false;

                    personEnrtity.PersonId = Convert.ToInt32(personDto.PersonID);
                    personEnrtity.FirstName = personDto.FirstName;
                    personEnrtity.MiddleName = personDto.MiddleName;
                    personEnrtity.LastName = personDto.LastName;
                    personEnrtity.Gender = Convert.ToByte(personDto.Gender);
                    personEnrtity.DateOfBirth = Convert.ToDateTime(personDto.DateOfBirth);
                    personEnrtity.Email = personDto.Email;
                    personEnrtity.Address = personDto.Address;
                    personEnrtity.Phone = personDto.Phone;
                    personEnrtity.JoinDate = Convert.ToDateTime(personDto.JoinDate);
                    personEnrtity.IsActive = personDto.IsActive;
                    personEnrtity.Image = personDto.Image;

                    context.SaveChanges();
                    return true;

                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdatePerson));
                return false;
            }

        }
        public static bool DeletePerson(int? personID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var personEntity = context.People.Find(personID);

                    if (personEntity == null)
                        return false;

                    context.People.Remove(personEntity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(DeletePerson));
                return false;
            }
        }

        static public int? AddPerson(string firstName, string middleName, string lastName,
                   short gender, DateTime? dateOfBirth, string email, string phone, string address,
                   bool isActive, string image, string userName, string password)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                {

                    using (SqlCommand Command = new SqlCommand("SP_AddPerson", Connection))
                    {
                        Command.CommandType = System.Data.CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@FirstName", firstName);
                        if (middleName != "" && middleName != null)
                            Command.Parameters.AddWithValue("@MiddleName", middleName);
                        else
                            Command.Parameters.AddWithValue("@MiddleName", System.DBNull.Value);

                        Command.Parameters.AddWithValue("@LastName", lastName);
                        Command.Parameters.AddWithValue("@Gender", gender);
                        Command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                        if (email != "" && email != null)
                            Command.Parameters.AddWithValue("@Email", email);
                        else
                            Command.Parameters.AddWithValue("@Email", System.DBNull.Value);

                        Command.Parameters.AddWithValue("@Phone", phone);
                        Command.Parameters.AddWithValue("@Address", address);
                        Command.Parameters.AddWithValue("@JoinDate", DateTime.Now);
                        Command.Parameters.AddWithValue("@IsActive", isActive);

                        if (image != "" && image != null)
                            Command.Parameters.AddWithValue("@Image", image);
                        else
                            Command.Parameters.AddWithValue("@Image", System.DBNull.Value);

                        Command.Parameters.AddWithValue("@UserName", userName);
                        Command.Parameters.AddWithValue("@Password", password);

                        SqlParameter OutputID = new SqlParameter("@PersonID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        Command.Parameters.Add(OutputID);

                        Connection.Open();
                        Command.ExecuteNonQuery();
                        CreatedID = (int)Command.Parameters["@PersonID"].Value;


                    }
                }

            }
            catch (Exception ex)
            {
                //string methodName = MethodBase.GetCurrentMethod().Name;
                //clsEventLogger.LogError(ex.Message, methodName);
            }
            return CreatedID;
        }

        static public bool UpdatePerson(int? personID, string firstName, string middleName, string lastName,
                 short gender, DateTime? dateOfBirth, string email, string phone, string address,
                 DateTime? joinDate, bool isActive, string image, string userName, string password)
        {
            int RowAffected = 0;


            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                {

                    using (SqlCommand Command = new SqlCommand("SP_UpdatePerson", Connection))
                    {
                        Command.CommandType = System.Data.CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@PersonID", personID);

                        Command.Parameters.AddWithValue("@FirstName", firstName);

                        if (middleName != "" && middleName != null)
                            Command.Parameters.AddWithValue("@MiddleName", middleName);
                        else
                            Command.Parameters.AddWithValue("@MiddleName", System.DBNull.Value);

                        Command.Parameters.AddWithValue("@LastName", lastName);
                        Command.Parameters.AddWithValue("@Gender", gender);
                        Command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                        if (email != "" && email != null)
                            Command.Parameters.AddWithValue("@Email", email);
                        else
                            Command.Parameters.AddWithValue("@Email", System.DBNull.Value);

                        Command.Parameters.AddWithValue("@Phone", phone);
                        Command.Parameters.AddWithValue("@Address", address);
                        Command.Parameters.AddWithValue("@JoinDate", joinDate);
                        Command.Parameters.AddWithValue("@IsActive", isActive);

                        if (image != "" && image != null)
                            Command.Parameters.AddWithValue("@Image", image);
                        else
                            Command.Parameters.AddWithValue("@Image", System.DBNull.Value);

                        Command.Parameters.AddWithValue("@UserName", userName);
                        Command.Parameters.AddWithValue("@Password", password);


                        Connection.Open();

                        RowAffected = Command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                //string methodName = MethodBase.GetCurrentMethod().Name;
                //clsEventLogger.LogError(ex.Message, methodName);
            }
            return RowAffected > 0;
        }

    }
}
