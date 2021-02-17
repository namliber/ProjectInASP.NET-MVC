using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DoctorsLogic
    {
        DAL.Doctors dal = new DAL.Doctors();
        public string InsertDoctors(Doctor d)
        {
            dal.InsertDoctors(d);
            MailMessage mail;
            SmtpClient smtp;
            //sending mail
            mail = new MailMessage();
            string mailAdress = d.Email;
            mail.To.Add(mailAdress);
            mail.From = new MailAddress("medical333@gmail.com");
            mail.Subject = " Welcome to medical website! ";
            mail.Body ="<b>Hi "+ d.FirstName+ ", Welcome to Medically website!</b>" + "<br/>" + "<br/>" + "Here is your login information: " + "<br/>" + "<br/>" + "<b>User Name: </b>" +d.UserName + 
                "<br/>" +"<b>Password: </b>"+ d.Password+"<br/>" + "<br/>"+ "<img src='https://lh3.googleusercontent.com/-2kyAPb3f4zM/X4x6XfrWO2I/AAAAAAAAZRQ/_uyqVy32bVMPtpR2f8gZgxrfLFOJfPCrACK8BGAsYHg/s0/2020-10-18.png'  width='100' />";
            mail.IsBodyHtml = true;
            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("medicalproject333@gmail.com", "medical333");
            string localPath = System.AppDomain.CurrentDomain.BaseDirectory.Substring(0, System.AppDomain.CurrentDomain.BaseDirectory.Length - 11);
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
                string message = "The doctor was successfully added";
                return message;
            }
            catch
            {
                string message = "Something went wrong while sending the email :(";
                return message;
            }
            //try
            //{
            //    mail.To.Clear();
            //    mail.To.Add(senderMail);
            //    mail.Subject = "תודה על פנייתך לזכרון מנחם:)";
            //    mail.Body = "פנייתך בנושא" + ": " + subject + ", " + " התקבלה בהצלחה!";
            //    smtp.Send(mail);
            //}
            //catch
            //{
            //    ViewBag.Message = "משהו השתבש בשליחת המייל:(";
            //}
        }
        public void RemoveDoctors(int id)
        {
            dal.RemoveDoctors(id);
        }
        public void UpdateDoctors(Doctor doc, int id)
        {
            dal.UpdateDoctors(doc, id);
        }
        public IEnumerable<Doctor> GetDoctors()
        {
            return dal.GetDoctors();
        }
        public Doctor returnDoctor(User user)
        {
            return dal.returnDoctor(user);
        }
    }
}


