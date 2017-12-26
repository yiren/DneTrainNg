﻿using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.Repository
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetCourseList();
        Course AddCourse(CourseChangeViewModel course);
        Course UpdateCourse(Guid id, CourseChangeViewModel course);
        Boolean DeleteCourse(int id);
        Course GetCourseDetailById(Guid courseId);
    }
}
