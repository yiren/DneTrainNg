using DneTrainNg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.APIResources
{
    public class StudentResource
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public SectionResource Section { get; set; }
    }

    public class SectionResource
    {
        
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public int SectionId { get; set; }
    }
}
