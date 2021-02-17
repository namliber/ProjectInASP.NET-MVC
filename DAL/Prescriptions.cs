using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Prescriptions
    {
        public void InsertPrescription(Prescription p)
        {
            try
            {
                using (var ctx = new mediDB())
                {
                    ctx.Prescriptions.Add(p);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public IEnumerable<Prescription> GetPrescriptions()
        {
            try
            {
                List<Prescription> result = new List<Prescription>();
                using (var ctx = new mediDB())
                {
                    foreach (var prescription in ctx.Prescriptions)
                    {
                        result.Add(prescription);
                    }
                }
                return result;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public IEnumerable<Prescription> GetPrescriptionsById(int id)
        {
            try
            {
                List<Prescription> result = new List<Prescription>();
                using (var ctx = new mediDB())
                {
                    foreach (var prescription in ctx.Prescriptions)
                    {
                        if (prescription.PatientId == id)
                        {
                            result.Add(prescription);
                        }
                    }
                }
                return result;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        public IEnumerable<Prescription> GetPrescriptionsByNameMed(string medicine)
        {
            try
            {
                List<Prescription> result = new List<Prescription>();
                using (var ctx = new mediDB())
                {
                    foreach (var prescription in ctx.Prescriptions)
                    {
                        if (prescription.Medicine == medicine)
                        {
                            result.Add(prescription);
                        }
                    }
                }
                return result;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        public IEnumerable<string> GetNDCById(int id)
        {


            IEnumerable<Prescription> prescriptions = GetPrescriptionsById(id);
            List<string> NDC = new List<string>();
            foreach (var item in prescriptions)
            {
                if (item.PatientId == id)
                {
                    NDC.Add(item.Ndc);
                }
            }
            return NDC;

        }
    }
}
