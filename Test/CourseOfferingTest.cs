using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SRSWepApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRSTest
{
    [TestClass]
    public class CourseOfferingTest
    {
        [TestMethod]
        public void SearchCourseOfferingTest()
        {
            string subject = "";
            TimeSpan? startTime = null;
            TimeSpan? endTime = null;

            List<CourseOffering> actualCourseOfferings = CourseOffering.SearchCourseOfferings(subject, startTime, endTime);

            List<CourseOffering> allCourseOfferings = CourseOffering.PopulateCourseOffering();

            int courseOfferingIdValue = 1;

            foreach (CourseOffering eachCourseOffering in allCourseOfferings)
            {
                eachCourseOffering.CourseOfferingId = courseOfferingIdValue;
                courseOfferingIdValue++;
            }

            List<CourseOffering> expectedCourseOfferings = allCourseOfferings;

            Assert.IsTrue(expectedCourseOfferings.SequenceEqual(actualCourseOfferings));

            Assert.AreEqual(expectedCourseOfferings.Count, actualCourseOfferings.Count);

            endTime = new TimeSpan(9, 30, 0); //changing paremeter. shows just the first 3 courses

            expectedCourseOfferings = allCourseOfferings.GetRange(0,9);

            actualCourseOfferings =
                CourseOffering.SearchCourseOfferings //have to run the method all over again
                (subject, startTime, endTime);

            Assert.AreEqual(expectedCourseOfferings.Count, actualCourseOfferings.Count);

            Assert.IsTrue(expectedCourseOfferings.SequenceEqual(actualCourseOfferings));


        }
    }
}
