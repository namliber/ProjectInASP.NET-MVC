using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Doctor : User
    {
        //[DisplayName("First Name")]
        //public string FirstName { get; set; }
        //[DisplayName("Last Name")]
        //public string LastName { get; set; }

        [DisplayName("Phone Number")]

        public string Phone { get; set; }
        public string Email { get; set; }
        [DisplayName("Licence Number ")]

        public string LicenceNumber { get; set; }

        public string Expertist { get; set; }

        

        public Doctor(string userName, string password, string firstName, string lastName, string phone, string email, string licenceNumber, string expertist)
        {
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            LicenceNumber = licenceNumber;
            Expertist = expertist;
            Email = email;

        }
        public string ToString(string lastName)
        {
            return lastName + " ";
        }
        public Doctor()
        {

        }
        public Doctor(Doctor D)
        {
            UserName = D.UserName;
            Password = D.Password;
            FirstName = D.FirstName;
            LastName = D.LastName;
            Phone = D.Phone;
            LicenceNumber = D.LicenceNumber;
            Expertist = D.Expertist;
            Email = D.Email;
        }
        public override string ReturnType()
        {
            return "Doctor";
        }

    }
}

