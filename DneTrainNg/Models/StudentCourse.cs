using System;

namespace DneTrainNg.Models
{
    public class StudentCourse
    {
        public Guid StudentCourseId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public string Location { get; set; }
        public string CourseName { get; set; }
        public short CourseYear { get; set; }
        public short TrainHours { get; set; }
        public string Score { get; set; }
    }
}