using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Patient
    {
        public int Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public List<Medicine> Drugs { get; set; }

        public Patient(string firstname, string lastname, DateTime date, string phone, string email, List<Medicine> drugs)
        {
            FirstName = firstname;
            LastName = lastname;
            DateOfBirth = date;
            Phone = phone;
            Email = email;
            Drugs = drugs;
        }
        public Patient()
        {
           
        }
    }

}

