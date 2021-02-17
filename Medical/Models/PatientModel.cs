using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medical.Models
{
    public class PatientModel
    {
        PatientsLogic bl = new PatientsLogic();
        PrescriptionsLogic bl1= new PrescriptionsLogic();
        DrugAdminLogic  blm = new DrugAdminLogic();

        public PatientModel()
        {

        }
        public IEnumerable<Patient> GetPatients()
        {
            return bl.GetPatients();

        }
        public void Update(int id, string firstname, string lastname,string date, string phone, string email)
        {
            List<Medicine> drugs = new List<Medicine>();
            DateTime d = Convert.ToDateTime(date);
            Patient patient = new Patient(firstname, lastname, d, phone, email, drugs);
            bl.UpdatePatients(patient, id);
        }
        public void Add(string firstname, string lastname, string date, string phone, string email)
        {
            List<Medicine> Drugs = new List<Medicine>();
            DateTime d = Convert.ToDateTime(date);
            Patient patient = new Patient(firstname, lastname, d, phone, email, Drugs);
            bl.InsertPatients(patient);
        }

        public void delete(int id)
        {
            bl.Removepatients(id);

        }
        public IEnumerable<Prescription> GetPrescriptions()
        {
            return bl1.GetPrescriptions();

        }
        public IEnumerable<Prescription> GetPrescriptionsById(int id)
        {
            return bl1.GetPrescriptionsById(id);
        }
        public void AddPrescription(string PatientId,string docfirst,string doclast,string medicine,string begin,string finish ,string ndc,string frequency,string comments)
        {
            int patientID = Convert.ToInt32(PatientId);
            DateTime begindate = Convert.ToDateTime(begin);
            DateTime finishdate = Convert.ToDateTime(finish);
            //Medicine newm = new Medicine(blm.GetMedicines().FirstOrDefault(m => m.CommercialName == medicine));
            Prescription prescription = new Prescription(patientID, docfirst, doclast, medicine, begindate, finishdate,ndc, frequency,comments);
            bl1.InsertPrescription(prescription);
        }

    }
}