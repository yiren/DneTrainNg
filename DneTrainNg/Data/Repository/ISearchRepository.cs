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
        IEnumerable<CourseQueryExportViewModel> SearchBySection(QueryForTrainingRecordViewModel queryVM);
        IEnumerable<Course> SearchCourse(QueryForTrainingRecordViewModel searchViewModel);
        IEnumerable<CourseQueryExportViewModel> SearchCourseByStudent(QueryForTrainingRecordViewModel studentSearchViewModel);
    }
}
