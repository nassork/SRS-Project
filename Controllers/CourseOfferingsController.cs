using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SRSWepApp.Models;

namespace SRSWepApp.Controllers
{
    public class CourseOfferingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ViewResult SearchCourseOfferings()
        {
            return View();
        }

        [HttpPost]
        public ViewResult SearchCourseOfferings(string subject, TimeSpan? startTime, TimeSpan? endTime)
        {
            List<CourseOffering> courseOfferings = CourseOffering.SearchCourseOfferings(subject, startTime, endTime);

            return View("SearchCourseOfferingsResults", courseOfferings);
        }

        // GET: CourseOfferings
        public ActionResult Index()
        {
            var courseOfferings = db.CourseOfferings.Include(c => c.Course);
            return View(courseOfferings.ToList());
        }

        // GET: CourseOfferings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseOffering courseOffering = db.CourseOfferings.Find(id);
            if (courseOffering == null)
            {
                return HttpNotFound();
            }
            return View(courseOffering);
        }

        // GET: CourseOfferings/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseNumber");
            return View();
        }

        // POST: CourseOfferings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseOfferingId,CourseId,CRN,WeekDays,NumberOfOpennings")] CourseOffering courseOffering)
        {
            if (ModelState.IsValid)
            {
                db.CourseOfferings.Add(courseOffering);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseNumber", courseOffering.CourseId);
            return View(courseOffering);
        }

        // GET: CourseOfferings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseOffering courseOffering = db.CourseOfferings.Find(id);
            if (courseOffering == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseNumber", courseOffering.CourseId);
            return View(courseOffering);
        }

        // POST: CourseOfferings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseOfferingId,CourseId,CRN,WeekDays,NumberOfOpennings")] CourseOffering courseOffering)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseOffering).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseNumber", courseOffering.CourseId);
            return View(courseOffering);
        }

        // GET: CourseOfferings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseOffering courseOffering = db.CourseOfferings.Find(id);
            if (courseOffering == null)
            {
                return HttpNotFound();
            }
            return View(courseOffering);
        }

        // POST: CourseOfferings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseOffering courseOffering = db.CourseOfferings.Find(id);
            db.CourseOfferings.Remove(courseOffering);
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
