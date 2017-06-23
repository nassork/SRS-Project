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
    public class CourseSignUpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CourseSignUps
        public ActionResult Index()
        {
            var courseSignUps = db.CourseSignUps.Include(c => c.CourseOffering).Include(c => c.Student);
            return View(courseSignUps.ToList());
        }

        // GET: CourseSignUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSignUp courseSignUp = db.CourseSignUps.Find(id);
            if (courseSignUp == null)
            {
                return HttpNotFound();
            }
            return View(courseSignUp);
        }

        // GET: CourseSignUps/Create
        public ActionResult Create()
        {
            ViewBag.CourseOfferingId = new SelectList(db.CourseOfferings, "CourseOfferingId", "CRN");
            ViewBag.StudentId = new SelectList(db.Users, "Id", "UserFullName");
            return View();
        }

        // POST: CourseSignUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseSignUpId,GradePoints,LetterGrade,StudentId,CourseOfferingId,SignUpDate")] CourseSignUp courseSignUp)
        {
            if (ModelState.IsValid)
            {
                courseSignUp.SignUpDate = DateTime.Today;

                try
                {
                    db.CourseSignUps.Add(courseSignUp);
                    db.SaveChanges();

                    CourseOffering courseOffering = db.CourseOfferings.Find(courseSignUp.CourseOffering);
                    courseOffering.NumberOfOpennings -= 1;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch(Exception exception)
                {
                    string errorMessage = exception.InnerException.ToString();

                    if (errorMessage.Contains("ukCourseRegistrations"))
                    {
                        errorMessage = "Student is already signed up";
                    }
                    if (errorMessage.Contains("chValidAvailableOpennings"))
                    {
                        errorMessage = "Class is Full";
                    }                      
                    ViewBag.ErrorMessage = errorMessage;
                    return View("ErrorMessage");
                }

                
                
            }

            ViewBag.CourseOfferingId = new SelectList(db.CourseOfferings, "CourseOfferingId", "CRN", courseSignUp.CourseOfferingId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "UserFullName", courseSignUp.StudentId);
            return View(courseSignUp);
        }

        // GET: CourseSignUps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSignUp courseSignUp = db.CourseSignUps.Find(id);
            if (courseSignUp == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseOfferingId = new SelectList(db.CourseOfferings, "CourseOfferingId", "CRN", courseSignUp.CourseOfferingId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "UserFullName", courseSignUp.StudentId);
            return View(courseSignUp);
        }

        // POST: CourseSignUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseSignUpId,LetterGrade,StudentId,CourseOfferingId")] CourseSignUp courseSignUp)
        {
            if (ModelState.IsValid)
            {

                CourseSignUp courseSignUpFromDataBase = db.CourseSignUps.Find(courseSignUp.CourseSignUpId);

                string letterGrade = courseSignUp.LetterGrade;

                courseSignUpFromDataBase.LetterGrade = letterGrade;

                courseSignUpFromDataBase.GradePoints = CourseSignUp.CalculateLetterGrade(letterGrade);
                db.SaveChanges();
               
                
                return RedirectToAction("Index");
            }
            ViewBag.CourseOfferingId = new SelectList(db.CourseOfferings, "CourseOfferingId", "CRN", courseSignUp.CourseOfferingId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "UserFullName", courseSignUp.StudentId);
            return View(courseSignUp);
        }

        // GET: CourseSignUps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSignUp courseSignUp = db.CourseSignUps.Find(id);
            if (courseSignUp == null)
            {
                return HttpNotFound();
            }
            return View(courseSignUp);
        }

        // POST: CourseSignUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseSignUp courseSignUp = db.CourseSignUps.Find(id);
            db.CourseSignUps.Remove(courseSignUp);
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
