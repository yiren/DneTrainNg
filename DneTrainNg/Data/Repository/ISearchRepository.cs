using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.Repository
{
    public interface ISearchRepository
    {
        string GetDepAverageTrainHour(ReportForAverageTrainHoursViewModel averageVM);
        IEnumerable<CourseQueryExportViewModel> ExportTrainingRecord(QueryForTrainingRecord queryVM);
        IEnumerable<Course> SearchCourse(CourseSearchViewModel searchViewModel);
        IEnumerable<Course> SearchCourseByStudent(CourseSearchByStduentViewModel studentSearchViewModel);
    }
}
