using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class Drugs
    {
        public void InsertDrugs(Medicine m)
        {
            try
            {
                using (var ctx = new mediDB())
                {
                    ctx.Drugs.Add(m);
                    ctx.SaveChanges();

                    
                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        public void AddImageDrugs(int id, HttpPostedFileBase file)
        {
            try
            {
              //  string path =
               // Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
               // Path.GetFileName(file.FileName));
               // file.SaveAs(path);
                using (var ctx = new mediDB())
                {
                    Medicine tmp = ctx.Drugs.First(m => m.Id == id);
                    if (tmp.ImageUrl==null)
                    {
                        tmp.ImageUrl = @"/images/" + file.FileName;
                        ctx.SaveChanges();

                        //Google Drive API
                        GoogleDriveAPIHelper gd = new GoogleDriveAPIHelper();
                        gd.UplaodFileOnDriveInFolder(file, file.FileName, "MedicinesImages");
                    }
                    else
                    {
                        GoogleDriveAPIHelper gd = new GoogleDriveAPIHelper();
                        gd.deleteFile(tmp.ImageUrl);
                        tmp.ImageUrl = @"/images/" + file.FileName;
                        ctx.SaveChanges();

                        //Google Drive API
                        gd.UplaodFileOnDriveInFolder(file, file.FileName, "MedicinesImages");
                    }

                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public void RemoveDrugs(int id)
        {
            try
            {
                using (var ctx = new mediDB())
                {
                    //Google Drive API
                    GoogleDriveAPIHelper gd = new GoogleDriveAPIHelper();
                    Medicine medicine = ctx.Drugs.Find(id);
                    gd.deleteFile(medicine.ImageUrl);
                    ctx.Drugs.Remove(medicine);
                    ctx.SaveChanges();

                    

                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        
        public void UpdateDrugs(Medicine medicine, int id)
        {
            try
            {
                using (var ctx = new mediDB())
                {
                    Medicine tmp = ctx.Drugs.First(m=>m.Id==id);
                    tmp.CommercialName = medicine.CommercialName;
                    tmp.GenericName = medicine.GenericName;
                    tmp.Producer = medicine.Producer;
                    tmp.ActiveIngredients = medicine.ActiveIngredients;
                    tmp.DoseCharacteristic = medicine.DoseCharacteristic;
                    tmp.ImageUrl = medicine.ImageUrl;
                    ctx.SaveChanges();

                    //Google Drive API
                    GoogleDriveAPIHelper gd = new GoogleDriveAPIHelper();
                    //gd.deleteAllFiles(medicineId.ToString());
                    //gd.UplaodFileOnDriveInFolder(file, medicineId.ToString(), "cloudComputing");

                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        
        public IEnumerable<Medicine> GetMedicines()
        {
            try
            {
                List<Medicine> result = new List<Medicine>();
                using (var ctx = new mediDB())
                {
                    foreach (var drug in ctx.Drugs)
                    {
                        result.Add(drug);
                    }
                }
                return result;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public Medicine GetMedicineByName(string name)
        {
            try
            {
                using (var ctx = new mediDB())
                {
                    foreach (var drug in ctx.Drugs)
                    {
                        if(drug.CommercialName==name)
                        {
                            return drug;
                        }
                    }
                }
                return null;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
    }
}
