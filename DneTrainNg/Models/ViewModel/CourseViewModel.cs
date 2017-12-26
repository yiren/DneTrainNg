﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Models.ViewModel
{
    public class CourseChangeViewModel
    {
        public string CourseName { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
        public Double TrainHours { get; set; }
        public IEnumerable<int> Students {get; set;}
    }

    public class CourseDetailViewModel
    {
        public string CourseName { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
        public Double TrainHours { get; set; }
    }
}