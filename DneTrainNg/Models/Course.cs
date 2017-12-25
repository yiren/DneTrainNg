using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DneTrainNg.Models
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public short CourseYear { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
        public float TrainHours { get; set; }
        public List<StudentCourse> StudentCourses { get; set; }
    }
}