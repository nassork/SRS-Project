using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRSWepApp.Models
{
    public class Student : ApplicationUser
    {
        [Key]

        public string StudentId { get; set; }

        public int CreditsEarned { get; set; }

        public double GPA { get; set; }

        [ForeignKey("StudentId")]

        public ApplicationUser ApplicationUser { get; set; }

        public Student() { }

        public Student (string studentName, string email, string phoneNumber, int creditsEarned) : base(studentName, email, phoneNumber)
        {
            this.StudentId = base.Id;
            this.CreditsEarned = creditsEarned;
            this.GPA = 0.0;
        }

        public static List<Student> PopulateStudent()
        {
            List<Student> StudentList = new List<Student>();

            Student student = new Student("TestStudent", "TestStudent@mix.wvu.edu","123-456-7890", 100);
            StudentList.Add(student);

            student = new Student("Nassor Khalfani", "nbkhalfani@mix.wvu.edu", "818-309-9479", 89);
            StudentList.Add(student);

            student = new Student("Joni Zavolta", "Jdzavolta@mix.wvu.edu", "304-231-8595", 75);
            StudentList.Add(student);

            student = new Student("Nick Fogel", "NickFogel@mix.wvu.edu", "201-342-9843", 19);
            StudentList.Add(student);

            student = new Student("Brandon Wiseman", "BWise@mix.wvu.edu", "201-333-5422", 27);
            StudentList.Add(student);


            return StudentList;
        }

        public double CalculateGPA()
        {

            ApplicationDbContext database = new ApplicationDbContext();

            double gpa = 0;

            List<CourseSignUp> studentCourseSignUps =
            database.CourseSignUps.Include("CourseOfferings")/*.Include("Course")*/
                .Where(csu => csu.StudentId == this.StudentId)
                .ToList<CourseSignUp>();

            foreach(CourseSignUp eachCourseSigned in studentCourseSignUps)
            {
                database.Courses.Find(eachCourseSigned.CourseOffering.CourseId);

                if(eachCourseSigned.GradePoints !=null)
                {
                    
                }
            }


            return gpa;
        }





    }
}