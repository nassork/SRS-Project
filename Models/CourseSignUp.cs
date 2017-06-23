using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRSWepApp.Models
{
    public class CourseSignUp
    {
        [Key]

        public int CourseSignUpId { get; set; }
        public decimal? GradePoints { get; set; }

        public string LetterGrade { get; set; }

        public string StudentId { get; set; }

        [Index("CourseRegistrationIndex", 1, IsUnique = true)]
        public int CourseOfferingId { get; set; }

        [Index("CourseRegistrationIndex", 2, IsUnique = true)]

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [ForeignKey("CourseOfferingId")]

        public CourseOffering CourseOffering { get; set; }

        public DateTime SignUpDate { get; set; }

        public CourseSignUp() { }

        public CourseSignUp(int courseOfferingId, string studentId )
        {
            this.StudentId = studentId;
            this.CourseOfferingId = courseOfferingId;
            this.SignUpDate = DateTime.Today;          
        }

        public static List<CourseSignUp> PopulateCourseSignUp()
        {
            List<CourseSignUp> courseSignUpList = new List<CourseSignUp>();

            ApplicationDbContext database = new ApplicationDbContext();

            List<Student> studentList = database.Students.ToList<Student>();

            string stu = studentList[0].StudentId;

            CourseSignUp courseSignUp = new CourseSignUp(1, stu);
            courseSignUpList.Add(courseSignUp);

            courseSignUp = new CourseSignUp(2, stu);
            courseSignUpList.Add(courseSignUp);

            stu = studentList[1].StudentId;

            courseSignUp = new CourseSignUp(3, stu);
            courseSignUpList.Add(courseSignUp);

            courseSignUp = new CourseSignUp(4, stu);
            courseSignUpList.Add(courseSignUp);

            stu = studentList[3].StudentId;

            courseSignUp = new CourseSignUp(5, stu);
            courseSignUpList.Add(courseSignUp);

            courseSignUp = new CourseSignUp(6, stu);
            courseSignUpList.Add(courseSignUp);

            stu = studentList[4].StudentId;

            courseSignUp = new CourseSignUp(7, stu);
            courseSignUpList.Add(courseSignUp);

            courseSignUp = new CourseSignUp(8, stu);
            courseSignUpList.Add(courseSignUp);

            stu = studentList[2].StudentId;

            courseSignUp = new CourseSignUp(9, stu);
            courseSignUpList.Add(courseSignUp);


            return courseSignUpList;

        }

        public static decimal CalculateLetterGrade(string letterGrade)
        {
            decimal GradePoints = 0;

            if (letterGrade == "A")
            {
                GradePoints = 4;
            }
            else if (letterGrade == "B")
            {
                GradePoints = 3;
            }
            else if (letterGrade == "C")
            {
                GradePoints = 2;
            }
            else if (letterGrade == "D")
            {
                GradePoints = 1;
            }
            else
            {
                GradePoints = 0;
            }
            return GradePoints;
        }

        //public void CalculateGradePointAlternate()
        //{
        //    if (letterGrade == "A")
        //    {
        //        gradePoint = 4;
        //    }
        //    else if (letterGrade == "B")
        //    {
        //        gradePoint = 3;
        //    }
        //    else if (letterGrade == "C")
        //    {
        //        gradePoint = 2;
        //    }
        //    else if (letterGrade == "D")
        //    {
        //        gradePoint = 1;
        //    }
        //    else
        //    {
        //        gradePoint = 0;
        //    }
        //    return gradePoint;
        //}



    }
}