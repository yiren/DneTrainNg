using DneTrainNg.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.Repository
{
    public interface IReportRepository
    {
        string GetDepAverageTrainHour(ReportForAverageTrainHoursViewModel averageVM);
    }
}
