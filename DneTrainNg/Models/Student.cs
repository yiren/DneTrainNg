using System;
using System.Collections.Generic;

namespace DneTrainNg.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public Section Section { get; set; }
        public List<StudentCourse> StudentCourses { get; set; }
    }
}