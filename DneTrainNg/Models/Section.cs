using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
