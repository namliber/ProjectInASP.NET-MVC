using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Admin:User
    {
        public string Phone { get; set; }
        public string Mail { get; set; }


        public Admin()
        {

        }
        public Admin(string username ,string password,string firstname,string lastname, string phone, string mail)
        {
            UserName = username;
            Password = password;
            FirstName = firstname;
            LastName = lastname;
            Phone = phone;
            Mail = mail;
        }
        public Admin(Admin A)
        {
            UserName = A.UserName;
            Password = A.Password;
            FirstName = A.FirstName;
            LastName = A.LastName;
            Phone = A.Phone;
            Mail = A.Mail;
        }
        public override string ReturnType()
        {
            return "Admin";
        }

    }
    
}
