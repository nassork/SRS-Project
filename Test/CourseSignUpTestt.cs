using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SRSWepApp.Models;

namespace SRSTest
{
    [TestClass]
    public class CourseSignUpTestt
    {
        [TestMethod]
        public void CalculateGradePointTest()
        {
            string lettergrade = "A";

            decimal expectedGradePoint = 4;

            decimal actualGradePoint = CourseSignUp.CalculateLetterGrade(lettergrade);

            Assert.AreEqual(expectedGradePoint, actualGradePoint);
        }
    }
}
