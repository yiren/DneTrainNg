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
        Boolean DeleteCourse(Guid id);
        Course GetCourseDetailById(Guid courseId);
        IEnumerable<StudentCourse> UpdateScore(Guid courseId, CourseScoreViewModel scoreData);
        IEnumerable<StudentCourse> GetStudentCoursesById(Guid courseId);
        IEnumerable<Course> SearchCourse(CourseSearchViewModel searchViewModel);

    }
}
