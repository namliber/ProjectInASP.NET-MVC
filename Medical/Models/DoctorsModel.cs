using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Medical.Models
{
    public class DoctorsModel
    {
        private readonly Random _random = new Random();
        DoctorsLogic bl = new DoctorsLogic();
        AdminsLogic bl1 = new AdminsLogic();
        public DoctorsModel()
        {

        }
        public IEnumerable<Doctor> GetDoctors()
        {
            return bl.GetDoctors();
        }
        public string AddD(string firstname, string lastname, string phone, string email, string LicenceNumber, string Expertist)
        {
            string Password = RandomPassword();
            Doctor doctor = new Doctor(lastname, Password, firstname, lastname, phone, email, LicenceNumber, Expertist);
            return bl.InsertDoctors(doctor);
        }
        public void Update(int id, string firstname, string lastname, string phone, string email, string LicenceNumber, string Expertist)
        {

            Doctor doctor = new Doctor(null, null, firstname, lastname, phone, email, LicenceNumber, Expertist);
            bl.UpdateDoctors(doctor, id);
        }
        public void delete(int id)
        {
            bl.RemoveDoctors(id);

        }
        public User ReturnUser(string U,string P)
        {
            User user = new User(U, P,null,null);
            user = bl.returnDoctor(user);
            if (user==null)
            {
                user = new User(U, P,null,null);
                user = bl1.returnAdmin(user); 
            }
            return user;
        }
        // Generates a random string with a given size.    
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        // Generates a random password.  
        // 4-LowerCase + 4-Digits + 2-UpperCase 
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(4, true));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case  
            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }
    }
}