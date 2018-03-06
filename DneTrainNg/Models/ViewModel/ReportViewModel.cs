using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Models.ViewModel
{
    public class ReportForAverageTrainHoursViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class QueryForTrainingRecordViewModel
    {
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
        public int? SectionId { get; set; }
        public string CourseName { get; set; }
        public string StudentName { get; set; }
        public int? QueryOption { get; set; }
        public int? StudentId { get; set; }
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
    public class CourseQueryExportViewModel
    {
        public string CourseName { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
        public double TrainHours { get; set; }
        public string SectionName { get; set; }
        public string StudentName { get; set; }
        public string Score { get; set; }
        public int StudentId { get; set; }
    }
}
