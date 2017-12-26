using System;

namespace DneTrainNg.Models
{
    public class StudentCourse
    {
        public int StudentCourseId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string StudentName { get; set; }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }
        public string Score { get; set; }
    }
}