using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SRSWepApp.Models;
using System.Linq;

namespace SRSTest
{
    [TestClass]
    public class CourseTest
    {
        ApplicationDbContext database = new ApplicationDbContext();
        [TestMethod]
        public void AddandDeleteCourse()
        {
            ApplicationDbContext database = new ApplicationDbContext();

            string courseNumber = "Test 101";
            string courseTitle = "Test Course";
            int courseCredits = 2;

            bool? expectedResult = true;
            bool? actualResult = true;

            Course course = new Course(courseCredits, courseTitle, courseNumber);

            //act
            //actualResult = course.AddingCourse(errorMessage);

            Assert.AreEqual(expectedResult, actualResult);

            //Assert
            course = new Course();
            course = database.Courses.Where(c => c.CourseNumber.Contains(courseNumber)).FirstOrDefault<Course>();

            //Actt
            actualResult = course.DeleteCourse();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        
        }
    }
}
