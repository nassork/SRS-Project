using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRSWepApp.Models
{
    public class CourseOffering
    {
        [Key]
        public int CourseOfferingId { get; set; }
        public int CourseId { get; set; }
        public string CRN { get; set; }

        public string WeekDays { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "No Oppenings")]
        public int NumberOfOpennings { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public CourseOffering() { }

        public CourseOffering(int courseId, string crn, string weekdays, int numberOfOpennings, TimeSpan startTime, TimeSpan endTime)
        {
            this.CourseId = courseId;
            this.CRN = crn;
            this.WeekDays = weekdays;
            this.NumberOfOpennings = numberOfOpennings;
            this.StartTime = startTime;
            this.EndTime = endTime;
 
        }

        public static List<CourseOffering> PopulateCourseOffering()
        {
            List<CourseOffering> courseOfferingList = new List<CourseOffering>();

            TimeSpan Start = new TimeSpan(8, 30, 00);
            TimeSpan End = new TimeSpan(9, 30, 00);
            CourseOffering courseOffering = new CourseOffering(1, "83455", "M,W,F", 15, Start, End);
            courseOfferingList.Add(courseOffering);

            Start = new TimeSpan(8, 30, 00);
            End = new TimeSpan(9, 30, 00);
            courseOffering = new CourseOffering(1, "83456", "M,W,F", 12, Start, End);
            courseOfferingList.Add(courseOffering);

            Start = new TimeSpan(2, 30, 00);
            End = new TimeSpan(3, 30, 00);
            courseOffering = new CourseOffering(2, "83456", "M,W,F", 11, Start, End);
            courseOfferingList.Add(courseOffering);

            Start = new TimeSpan(3, 30, 00);
            End = new TimeSpan(4, 30, 00);
            courseOffering = new CourseOffering(1, "83256", "T,TR", 19, Start, End);
            courseOfferingList.Add(courseOffering);

            Start = new TimeSpan(5, 30, 00);
            End = new TimeSpan(6, 30, 00);
            courseOffering = new CourseOffering(3, "13456", "M,W,F", 13, Start, End);
            courseOfferingList.Add(courseOffering);

            Start = new TimeSpan(8, 30, 00);
            End = new TimeSpan(9, 30, 00);
            courseOffering = new CourseOffering(5, "83356", "M,W,F", 9, Start, End);
            courseOfferingList.Add(courseOffering);

            Start = new TimeSpan(9, 30, 00);
            End = new TimeSpan(10, 30, 00);
            courseOffering = new CourseOffering(4, "83656", "T,TR", 3, Start, End);
            courseOfferingList.Add(courseOffering);
        
            Start = new TimeSpan(12, 30, 00);
            End = new TimeSpan(1, 30, 00);
            courseOffering = new CourseOffering(5, "83056", "T,TR", 7, Start, End);
            courseOfferingList.Add(courseOffering);

            Start = new TimeSpan(10, 30, 00);
            End = new TimeSpan(11, 30, 00);
            courseOffering = new CourseOffering(3, "83996", "T,TR", 2, Start, End);
            courseOfferingList.Add(courseOffering);

            return courseOfferingList;
        }

        public static List<CourseOffering> SearchCourseOfferings (string subject, TimeSpan? startTime, TimeSpan? endTime)
        {

            ApplicationDbContext database = new ApplicationDbContext();
            
            startTime = null;

            endTime = null;

            List<CourseOffering> courseOfferings = new List<CourseOffering>();

            //courseOfferings = (from courseOffering in database.CourseOfferings.Include("Course")
            //                    where
            //                        courseOffering.Course.CourseNumber.Contains(subject)
            //                        &&
            //                        (courseOffering.StartTime >= startTime)
            //                        &&
            //                        (courseOffering.EndTime >= endTime)
            //                    select courseOffering).ToList<CourseOffering>();

            courseOfferings = database.CourseOfferings.Include("Course").ToList<CourseOffering>();

            //if (subject != null)
            //{
            //    courseOfferings = courseOfferings.FindAll(co => co.Course.CourseNumber.Contains(subject));

            if (String.IsNullOrEmpty(subject))
            {
                courseOfferings = courseOfferings.FindAll(co => co.Course.CourseNumber.Contains(subject)).ToList<CourseOffering>();
            }
            if (startTime != null)
            {
                courseOfferings = courseOfferings.FindAll(co => co.StartTime >= startTime);
            }
            if (endTime != null)
            {
                courseOfferings = courseOfferings.FindAll(co => co.EndTime >= endTime);
            }


            return courseOfferings;

            
        }

        public override bool Equals(object obj)
        {
            bool areEqual = false;

            if (obj != null)
            {
               
                CourseOffering secondCourseOffering = (CourseOffering)obj;

                if (this.CourseOfferingId == secondCourseOffering.CourseOfferingId)
                {
                    areEqual = true;
                }
                
            }
            return areEqual;

        }





    }
}