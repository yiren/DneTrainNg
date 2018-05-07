using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.APIResources
{
    public class CourseListResource
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
        public double TrainHours { get; set; }
       
    }
}
