using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRSWepApp.Models
{
    public class Teacher : ApplicationUser
    {
        [Key]

        public string TeacherId { get; set; }

        public string OfficeLocation { get; set; }

        [ForeignKey("TeacherId")]

        public ApplicationUser ApplicationUser { get; set; }

        public Teacher() { }

        public Teacher(string teacherName, string email, string phoneNumber, string officeLocation) : base(teacherName, email, phoneNumber)
        {
            this.TeacherId = base.Id;
            this.OfficeLocation = officeLocation;
        }

        public static List<Teacher> PopulateTeacher()
        {
            List<Teacher> TeacherList = new List<Teacher>();

            Teacher teacher = new Teacher("TestTeacher", "TestTeacher@gmail.com","111-1234-1234", "422 B&E");
            TeacherList.Add(teacher);

            teacher = new Teacher("Robinson", "Robinsonr@gmail.com", "202-555-0168", "101 B&E");
            TeacherList.Add(teacher);

            teacher = new Teacher("Peace", "Peace@gmail.com", "202-555-0186", "304 B&E");
            TeacherList.Add(teacher);

            teacher = new Teacher("Nanda", "Nanda@gmail.com", "202-555-0107", "456 B&E");
            TeacherList.Add(teacher);

            teacher = new Teacher("Bryant", "Bryant@gmail.com", "202-532-0107", "312 WVU Collisium");
            TeacherList.Add(teacher);


            return TeacherList;

        }
    }
}