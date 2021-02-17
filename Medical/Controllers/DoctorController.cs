using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medical.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Doctors()
        {
            DoctorsModel model = new DoctorsModel();
            var Doctors = model.GetDoctors();
            return View(Doctors);
        }

        public ActionResult AddDoctor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDoctor(FormCollection collection)
        {
            DoctorsModel model = new DoctorsModel();
            try
            {
                string mailMessage = model.AddD(collection["FirstName"], collection["LastName"],  collection["Phone"], collection["Email"], collection["LicenceNumber"], collection["Expertist"]);
                Response.Write("<script>alert(mailMessage);</script>");
                return RedirectToAction("Doctors");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            DoctorsModel model = new DoctorsModel();
            var Doctor = model.GetDoctors().First(m => m.Id == id);
            return View(Doctor);
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            DoctorsModel model = new DoctorsModel();
            try
            {
                model.Update(id,collection["FirstName"], collection["LastName"], collection["Phone"], collection["Email"], collection["LicenceNumber"], collection["Expertist"]);
                return RedirectToAction("Doctors");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctor/Delete/5
        public ActionResult Delete(int id)
        {
            DoctorsModel model = new DoctorsModel();
            model.delete(id);
            return RedirectToAction("Doctors");
        }

        // POST: Doctor/Delete/5
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
    }
}
