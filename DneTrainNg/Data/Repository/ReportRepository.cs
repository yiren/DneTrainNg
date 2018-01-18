using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly TrainingDbContext db;

        public ReportRepository(TrainingDbContext _db)
        {
            db = _db;
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
