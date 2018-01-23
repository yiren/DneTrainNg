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

    public class QueryForTrainingRecord
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? SectionId { get; set; }
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
    }
}
