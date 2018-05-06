using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.APIResources
{
    public class StudentCourseResource
    {
        public Guid CourseId { get; set; }
       

        public int StudentId { get; set; }
        public string Score { get; set; }
    }
}
