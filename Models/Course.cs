using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace SRSWepApp.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        public int CourseCredits { get; set; }

        public string CourseNumber { get; set;}

        public string CourseTitle { get; set; }

        public Course() { }

        public Course (int courseCredits, string courseNumber, string courseTitle)
        {
            this.CourseCredits = courseCredits;
            this.CourseNumber = courseNumber;
            this.CourseTitle = courseTitle;
        }

        public static List<Course> PopulateCourse()
        {
            List<Course> courseList = new List<Course>();

            Course course = new Course(3, "MIST 450", "Systems Analysis");
            courseList.Add(course);

            course = new Course(3, "ACCT 331", "Managerial Accounting");
            courseList.Add(course);

            course = new Course(3, "MIST 352", "Business Application Programming");
            courseList.Add(course);

            course = new Course(2, "SOCA 101", "Introduction to Sociology");
            courseList.Add(course);

            course = new Course(3, "Bowl 101", "Introduction to Bowling");
            courseList.Add(course);


            return courseList;



            //context.SaveChanges();

            

        }

        public static List<Course> SearchCourseByName(string courseName)
        {
            ApplicationDbContext database = new ApplicationDbContext();
            List<Course> Results = new List<Course>();

            Results = (from course in database.Courses.Include("CourseOfferings")
                       where course.CourseNumber.Contains(courseName)
                       select course).ToList<Course>();

            return Results;
        }

        public override bool Equals(object obj)
        {
            bool areEqual = false;

            if (obj != null)
            {
                Course secondCourse = (Course)obj;

                if(this.CourseId == secondCourse.CourseId)
                {
                    areEqual = true;
                }
            }
            return areEqual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        ApplicationDbContext database = new ApplicationDbContext();
        public bool? AddingCourse(out string errorMessage)
        {
            bool? AddSuccess = null;
            errorMessage = "";

            try
            {
                database.Courses.Add(this);
                database.SaveChanges();

                AddSuccess = true;
                errorMessage = "None";
            }
            catch (DbEntityValidationException dbException)
            {
                string dbExceptionError = "";

                foreach(var entityValidationErrors in dbException.EntityValidationErrors)
                {
                    foreach(var validationError in entityValidationErrors.ValidationErrors)
                    {
                        dbExceptionError += validationError.ErrorMessage;
                    }
                }
                errorMessage = dbExceptionError;
                AddSuccess = false;
            }
            return AddSuccess;
        }

        public bool? DeleteCourse()
        {
            bool? courseDeleted = null;

            try
            {
                Course course = database.Courses.Find(this.CourseId);
                database.Courses.Remove(course);
                database.SaveChanges();

                courseDeleted = true;
            }

            catch (Exception exception)
            {
                courseDeleted = false;
            }
            return courseDeleted;
        }

        public bool? UpdateCourse()
        {
            bool? updateSuccess = null;

            try
            {
                database.Entry(this).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                updateSuccess = true;
            }
            catch
            {
                updateSuccess = false;
            }
            return updateSuccess;
        }

        
        

        


    }
}