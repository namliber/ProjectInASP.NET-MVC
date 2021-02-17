using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using BE;
using BL;

namespace Medical.Models
{
    public class MedicineModel
    {
        DrugAdminLogic bl = new DrugAdminLogic();
        PrescriptionsLogic bl1 =new PrescriptionsLogic();

        public MedicineModel()
        {

        }
        public IEnumerable<Medicine> GetMedicines()
        {
            return bl.GetMedicines();
        }
        public void Update(int id, string CommercialName, string GenericName, string Producer, string ActiveIngredients, string DoseCharacteristic, string image, string ndc)
        {
            Medicine medicine = new Medicine(CommercialName, GenericName, Producer, ActiveIngredients, DoseCharacteristic, image, ndc);
            bl.UpdateDrugs(medicine, id);
        }
        public string Add(string CommercialName, string GenericName, string Producer, string ActiveIngredients, string DoseCharacteristic, string ndc)
        {
            
            Medicine medicine = new Medicine(CommercialName, GenericName, Producer, ActiveIngredients, DoseCharacteristic, null, ndc);
            
            string message=bl.AddDrugs(medicine);
            return message;
        }
        public void AddImage(HttpPostedFileBase file, int id)
        {
            bl.AddImgDrug(id, file);
        }

        public void delete(int id)
        {
            bl.RemoveDrugs(id);
        }
        public List<string> GetMedicineChart(int id)
        {
            int Count = 0;
            List<string>CountList=new List<string>();
            Medicine med = bl.GetMedicines().FirstOrDefault(m => m.Id == id);
            IEnumerable <Prescription> Prescriptions = bl1.GetPrescriptionsByNameMed(med.CommercialName);
            for (int i = DateTime.Now.Month-7; i < DateTime.Now.Month+1; i++)
            {
                foreach (var prescription in Prescriptions)
                {
                    if (prescription.BeginTime.Month == i)
                    {
                        Count++;
                    }
                }
                CountList.Add(Count.ToString());
                Count = 0;
            }
            return CountList;
        }
    }
}