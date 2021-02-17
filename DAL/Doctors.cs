using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Doctors
    {
        public void InsertDoctors(Doctor d)
        {
            try
            {
                using (var ctx = new mediDB())
                {
                    ctx.Doctors.Add(d);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public void RemoveDoctors(int id)
        {
            try
            {
                using (var ctx = new mediDB())
                {
                    Doctor doc = ctx.Doctors.Find(id);
                    ctx.Doctors.Remove(doc);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public void UpdateDoctors(Doctor doc, int id)
        {

            try
            {
                using (var ctx = new mediDB())
                {
                    Doctor tmp = ctx.Doctors.First(m => m.Id == id);
                    tmp.LastName = doc.LastName;
                    tmp.FirstName = doc.FirstName;
                    tmp.Phone = doc.Phone;
                    tmp.LicenceNumber = doc.LicenceNumber;
                    tmp.Expertist = doc.Expertist;
                    tmp.Email = doc.Email;
                    ctx.SaveChanges();
                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }


        public IEnumerable<Doctor> GetDoctors()
        {
            try
            {
                List<Doctor> result = new List<Doctor>();
                using (var ctx = new mediDB())
                {
                    foreach (var doc in ctx.Doctors)
                    {
                        result.Add(doc);
                    }
                }
                return result;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public Doctor returnDoctor(User user)
        {
            IEnumerable<Doctor> result = GetDoctors();
            Doctor doc = result.FirstOrDefault(d => d.UserName == user.UserName && d.Password == user.Password);
            return doc;
        }
    }

}
