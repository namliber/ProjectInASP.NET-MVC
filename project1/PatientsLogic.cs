using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PatientsLogic
    {
        DAL.Patients dal = new DAL.Patients();
        public void InsertPatients(Patient p)
        {
            dal.InsertPatients(p);
        }
        public void Removepatients(int id)
        {
            dal.Removepatients(id);
        }
        public void UpdatePatients(Patient patient, int id)
        {
            dal.UpdatePatients(patient,id);
        }
        public IEnumerable<Patient> GetPatients()
        {
            return dal.GetPatients();
        }
    }
}