using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly TrainingDbContext db;

        public SearchRepository(TrainingDbContext _db)
        {
            db = _db;
        }

        public IEnumerable<CourseQueryExportViewModel> SearchBySection(QueryForTrainingRecordViewModel queryVM)
        {
            var queryCourses = db.Courses.AsNoTracking();
            var querySections = db.Sections.AsNoTracking();
            var querySC = db.StudentCourses.AsNoTracking();

            if (!string.IsNullOrEmpty(queryVM.CourseStartDate))
            {
                queryCourses = queryCourses.Where(c=>string.Compare(c.CourseStartDate, queryVM.CourseStartDate) >= 0);
            }

            if (!string.IsNullOrEmpty(queryVM.CourseEndDate))
            {
                queryCourses = queryCourses.Where(c => string.Compare(queryVM.CourseEndDate, c.CourseEndDate)<=0);
            }

            if (!(queryVM.SectionId == null))
            {
                var section = querySections.SingleOrDefault(s => s.SectionId == queryVM.SectionId);
                querySC = querySC.Where(sc => sc.SectionCode.Equals(section.SectionCode));
            }

            var data=from c in queryCourses
                     join sc in querySC on c.CourseId equals sc.CourseId
                     select new CourseQueryExportViewModel
                     {
                         CourseName = c.CourseName,
                         CourseStartDate= c.CourseStartDate,
                         CourseEndDate= c.CourseEndDate,
                         TrainHours= c.TrainHours,
                         StudentName=sc.StudentName,
                         SectionName=sc.SectionName,
                         Score=sc.Score
                     };

            return data.OrderByDescending(d=>d.SectionName)
                       .ThenByDescending(d=>d.StudentName)
                       .ThenByDescending(d=>d.CourseStartDate)
                       .ThenByDescending(d=>d.CourseEndDate)
                       .ToList();

        }
        public IEnumerable<Course> SearchCourse(QueryForTrainingRecordViewModel searchViewModel)
        {
            var c1 = db.Courses.AsNoTracking();

            if (!string.IsNullOrEmpty(searchViewModel.CourseName))
            {
                c1 = c1.Where(c => c.CourseName.Contains(searchViewModel.CourseName));
            }

            if (!string.IsNullOrEmpty(searchViewModel.CourseStartDate))
            {
                c1 = c1.Where(c => string.Compare(c.CourseStartDate, searchViewModel.CourseStartDate) >=0);
            }

            if (!string.IsNullOrEmpty(searchViewModel.CourseEndDate))
            {
                c1 = c1.Where(c=>string.Compare(searchViewModel.CourseEndDate,c.CourseEndDate) <= 0);
            }

            return c1.Include(c => c.StudentCourses)
                     .OrderByDescending(c => c.CourseStartDate)
                     .ThenByDescending(c => c.CourseEndDate)
                     .ToList();
        }

        public IEnumerable<CourseQueryExportViewModel> SearchCourseByStudent(QueryForTrainingRecordViewModel studentSearchViewModel)
        {
            //var s1 = db.Students.Include(s=>s.StudentCourses)
            //                    .ThenInclude(g=>g.Select(d=>d.Course))
            //    .AsNoTracking().First(s=>s.StudentId==studentSearchViewModel.StudentId);

            var data = from s in db.Students.AsNoTracking().Where(s => s.StudentName.Equals(studentSearchViewModel.StudentName))
                       join sc in db.StudentCourses.AsNoTracking() on s.StudentId equals sc.StudentId
                       join c in db.Courses.AsNoTracking() on sc.CourseId equals c.CourseId
                       select new CourseQueryExportViewModel
                       {
                           CourseName = c.CourseName,
                           CourseStartDate = c.CourseStartDate,
                           CourseEndDate = c.CourseEndDate,
                           TrainHours = c.TrainHours,
                           StudentName = sc.StudentName,
                           SectionName = sc.SectionName,
                           Score = sc.Score
                       };

            if (!string.IsNullOrEmpty(studentSearchViewModel.CourseStartDate))
            {
                data = data.Where(c => string.Compare(c.CourseStartDate,studentSearchViewModel.CourseStartDate) >=0);
            }

            if (!string.IsNullOrEmpty(studentSearchViewModel.CourseEndDate))
            {
                data = data.Where(c => string.Compare(studentSearchViewModel.CourseEndDate, c.CourseEndDate) <= 0);
            }

            return data.OrderByDescending(c => c.CourseStartDate).ThenByDescending(c => c.CourseEndDate).ToList();

        }

        public string GetDepAverageTrainHour(ReportForAverageTrainHoursViewModel averageVM)
        {
            var studentCount = db.Students.AsNoTracking().Where(s => s.SectionCode != "O").Count();
            if (studentCount == 0)
            {
                return "系統錯誤";
            }

            if (!string.IsNullOrEmpty(averageVM.StartDate) || !string.IsNullOrEmpty(averageVM.EndDate))
            {
                var startDate = Convert.ToDateTime(averageVM.StartDate);
                var endDate = Convert.ToDateTime(averageVM.EndDate);

                var queriedCourses = db.Courses.AsNoTracking();

                if (endDate < startDate)
                {
                    return "結束日期大於起始日期";
                }

                queriedCourses = queriedCourses.Where(c => Convert.ToDateTime(c.CourseStartDate) >= startDate && Convert.ToDateTime(c.CourseEndDate) <= endDate);
                double totalHours = 0;

                foreach (var course in queriedCourses)
                {
                    totalHours += course.TrainHours;
                }

                return (totalHours / studentCount).ToString();
            }
            else
            {
                return "未選擇日期";
            }
        }
    }
}
