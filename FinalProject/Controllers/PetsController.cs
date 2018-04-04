using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class PetsController : Controller
    {
        private PetDBEntities db = new PetDBEntities();

        IList<Weight> weightList = new List<Weight>();
       
        // GET: Pets
        public ActionResult Index()
        {
            
            var pets = db.Pets.Include(p => p.Document).Include(p => p.Image).Include(p => p.Surgery).Include(p => p.Vaccination).Include(p => p.Visit).Include(p => p.Weight);

            return View(pets.ToList());
        }

        // GET: Pets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }
        

        // GET: Pets/Create
        public ActionResult Create()
        {
            ViewBag.DocumentID = new SelectList(db.Documents, "DocumentID", "Title");
            ViewBag.ImageID = new SelectList(db.Images, "ImageID", "ImageID");
            ViewBag.SurgeryID = new SelectList(db.Surgeries, "SurgeryID", "description");
            ViewBag.VaccinationID = new SelectList(db.Vaccinations, "VaccinationID", "Type");
            ViewBag.VisitID = new SelectList(db.Visits, "VisitID", "ClinicName");
            ViewBag.WeightID = new SelectList(db.Weights, "WeightID", "WeightID");
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetID,Name,Birthdate,Type,Photo,IsFixed,SurgeryID,VaccinationID,WeightID,DocumentID,VisitID,ImageID")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Pets.Add(pet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocumentID = new SelectList(db.Documents, "DocumentID", "Title", pet.DocumentID);
            ViewBag.ImageID = new SelectList(db.Images, "ImageID", "ImageID", pet.ImageID);
            ViewBag.SurgeryID = new SelectList(db.Surgeries, "SurgeryID", "description", pet.SurgeryID);
            ViewBag.VaccinationID = new SelectList(db.Vaccinations, "VaccinationID", "Type", pet.VaccinationID);
            ViewBag.VisitID = new SelectList(db.Visits, "VisitID", "ClinicName", pet.VisitID);
            ViewBag.WeightID = new SelectList(db.Weights, "WeightID", "WeightID", pet.WeightID);
            return View(pet);
        }

        // GET: Pets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentID = new SelectList(db.Documents, "DocumentID", "Title", pet.DocumentID);
            ViewBag.ImageID = new SelectList(db.Images, "ImageID", "ImageID", pet.ImageID);
            ViewBag.SurgeryID = new SelectList(db.Surgeries, "SurgeryID", "description", pet.SurgeryID);
            ViewBag.VaccinationID = new SelectList(db.Vaccinations, "VaccinationID", "Type", pet.VaccinationID);
            ViewBag.VisitID = new SelectList(db.Visits, "VisitID", "ClinicName", pet.VisitID);
            ViewBag.WeightID = new SelectList(db.Weights, "WeightID", "WeightID", pet.WeightID);
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetID,Name,Birthdate,Type,Photo,IsFixed,SurgeryID,VaccinationID,WeightID,DocumentID,VisitID,ImageID")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentID = new SelectList(db.Documents, "DocumentID", "Title", pet.DocumentID);
            ViewBag.ImageID = new SelectList(db.Images, "ImageID", "ImageID", pet.ImageID);
            ViewBag.SurgeryID = new SelectList(db.Surgeries, "SurgeryID", "description", pet.SurgeryID);
            ViewBag.VaccinationID = new SelectList(db.Vaccinations, "VaccinationID", "Type", pet.VaccinationID);
            ViewBag.VisitID = new SelectList(db.Visits, "VisitID", "ClinicName", pet.VisitID);
            ViewBag.WeightID = new SelectList(db.Weights, "WeightID", "WeightID", pet.WeightID);
            return View(pet);
        }

        // GET: Pets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pet pet = db.Pets.Find(id);
            db.Pets.Remove(pet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
