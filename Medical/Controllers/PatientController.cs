using BE;
using BL;
using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medical.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient1
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Patients()
        {
            PatientModel model = new PatientModel();
            var Patients = model.GetPatients();
            return View(Patients);
        }
        // GET: Patient/Details/5
        public ActionResult Prescriptions(int id)
        {
            PatientModel model = new PatientModel();
            var Prescriptions = model.GetPrescriptionsById(id);
            ViewBag.id = id;
            return View(Prescriptions);
        }
        public ActionResult Prescription_(int id)
        {
            TempData["ID"] = id;
            ViewBag.id = id;
            PatientModel m = new PatientModel();
            ViewBag.first = m.GetPatients().Where(x => x.Id == id).FirstOrDefault().FirstName;
            TempData["first"] = ViewBag.first;
            ViewBag.last = m.GetPatients().Where(x => x.Id == id).FirstOrDefault().LastName;
            TempData["last"] = ViewBag.first;
            MedicineModel model = new MedicineModel();
            return View(model.GetMedicines()) ;
        }
        [HttpPost]
        public ActionResult Prescription_(FormCollection collection)
        {
            var ID = TempData["ID"];
            var first = TempData["first"];
            var last = TempData["last"];
            //var ID = collection["PatientId"];
            if (DateTime.Parse(collection["BeginDate"]) > DateTime.Parse(collection["FinishDate"]))
            {
                ViewBag.errorDate = "Begin date should be before the finish date";
                TempData["ID"] = ID;
                ViewBag.id = ID;
                TempData["first"] = first;
                ViewBag.first = first;
                TempData["last"] = last;
                ViewBag.last = last;
                MedicineModel model = new MedicineModel();
                return View(model.GetMedicines());
            }
            var medicines = new MedicineModel();
            return RedirectToAction("AreYouSure", new Prescription
            {
                Id = int.Parse(ID.ToString()),
                DoctorFirstName = RouteConfig.user.FirstName,
                DoctorLastName = RouteConfig.user.LastName,
                Medicine = collection["Medicine"],
                BeginTime = DateTime.Parse(collection["BeginDate"]),
                FinishTime = DateTime.Parse(collection["FinishDate"]),
                Frequency= collection["Frequency"],
                Comments = collection["Comments"],
                Ndc =  medicines.GetMedicines().Where(x => x.CommercialName == collection["Medicine"].ToString()).FirstOrDefault().NDC //get current prescription ndc
        });
        }

        public ActionResult AreYouSure(Prescription pres)
        {
            
            //Drugs Service
            DrugsLogic DL = new DrugsLogic();
            PrescriptionsLogic BL = new PrescriptionsLogic();
            var NDC_List = BL.GetNDCById(pres.Id.ToString()).ToList();
            NDC_List.Add(pres.Ndc);
            
            string results = DL.GetDrugsResults(NDC_List.ToArray());
            ViewBag.message = results;
            return View(pres);
        }

        [HttpPost]
        public ActionResult AreYouSure(FormCollection collection, string submit)
        {
            if (submit == "Add Prescription")
            {
                PatientModel model = new PatientModel();
                model.AddPrescription(collection["id"], collection["first"], collection["last"], collection["medicine"], collection["begin"], collection["finish"], collection["ndc"],collection["Frequency"], collection["Comments"]);
            }
            var Model = new PatientModel();
            return View("Patients", Model.GetPatients());
        }

        // GET: Patient/Create
        public ActionResult AddPatient()
        {
            var Model = new PatientModel();
            return View(Model.GetPatients());
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult AddPatient(FormCollection collection)
        {
            PatientModel model = new PatientModel();
            try
            {
                model.Add(collection["FirstName"], collection["LastName"], collection["DateofBirth"], collection["Phone"], collection["Email"]);
                return RedirectToAction("Patients");

            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            PatientModel model = new PatientModel();
            var Patient = model.GetPatients().First(m => m.Id == id);
            return View(Patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            PatientModel model = new PatientModel();
            try
            {
                model.Update(id, collection["FirstName"], collection["LastName"], collection["DateofBirth"], collection["Phone"], collection["Email"]);
                return RedirectToAction("Patients");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            PatientModel model = new PatientModel();
            model.delete(id);
            return RedirectToAction("Patients");
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Print(int id)
        {
            PatientModel model = new PatientModel();

            var Prescription = model.GetPrescriptions().First(m => m.Id == id);
            Patient patient = model.GetPatients().First(p => p.Id == Prescription.PatientId);
            ViewBag.PatientName = patient.FirstName + " " + patient.LastName;
            ViewBag.PatientEmail = patient.Email;
            ViewBag.PatientPhone=patient.Phone;

            return View(Prescription);
        }
        //[HttpPost]
        //public ActionResult Print(FormCollection collection)
        //{
        //    try
        //    {
               
        //        return View(); 
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


    }
}
