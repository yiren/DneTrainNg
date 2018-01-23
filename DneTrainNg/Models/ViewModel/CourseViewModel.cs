using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Models.ViewModel
{
    public class CourseChangeViewModel
    {
        public string CourseName { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
        public Double TrainHours { get; set; }
        public IEnumerable<int> Students {get; set;}
        public string LastModifiedBy { get; set; }
    }

    public class CourseDetailViewModel
    {
        public string CourseName { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
        public Double TrainHours { get; set; }
        public IEnumerable<StudentCourse> StudentCourses { get; set; }
    }

    public class CourseScoreViewModel
    {
        public string Modifier { get; set; }
        public Dictionary<int, string> StudentScoreData { get; set; }
    }

    public class CourseSearchViewModel
    {
        public string CourseName { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }

    }

    public class CourseSearchByStduentViewModel
    {
        public string StudentName { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
    }

}
