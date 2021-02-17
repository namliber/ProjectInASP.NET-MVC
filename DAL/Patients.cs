using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Patients
    {
        public void InsertPatients(Patient p)
        {
            try
            {
                using (var ctx = new mediDB())
                {
                    ctx.Patients.Add(p);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public void Removepatients(int id)
        {
            try
            {
                using (var ctx = new mediDB())
                {
                    Patient patient = ctx.Patients.Find(id);
                    ctx.Patients.Remove(patient);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public void UpdatePatients(Patient patient, int id)
        {

            try
            {
                using (var ctx = new mediDB())
                {
                    Patient tmp = ctx.Patients.First(m => m.Id == id);
                    tmp.FirstName = patient.FirstName;
                    tmp.LastName = patient.LastName;
                    tmp.DateOfBirth = patient.DateOfBirth;
                    tmp.Phone = patient.Phone;
                    tmp.Email = patient.Email;
                    tmp.Drugs = patient.Drugs;
                    ctx.SaveChanges();
                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        public IEnumerable<Patient> GetPatients()
        {
            try
            {
                List<Patient> result = new List<Patient>();
                using (var ctx = new mediDB())
                {
                    foreach (var patient in ctx.Patients)
                    {
                        result.Add(patient);
                    }
                }
                return result;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
    }

}

