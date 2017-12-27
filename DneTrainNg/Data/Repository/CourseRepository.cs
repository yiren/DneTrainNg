using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DneTrainNg.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly TrainingDbContext db;

        public CourseRepository(TrainingDbContext _context)
        {
            db = _context;
        }

        public Course AddCourse(CourseChangeViewModel course)
        {
            List<StudentCourse> studentCourses = new List<StudentCourse>();
            Guid courseId = Guid.NewGuid();
            foreach(var studentId in course.Students)
            {
                var student = db.Students.Include(s => s.Section).AsNoTracking().FirstOrDefault(s => s.StudentId == studentId);
                if (student != null)
                {
                    StudentCourse studentCourse = new StudentCourse()
                    {
                        StudentId = studentId,
                        //CourseName=course.CourseName,
                        CourseId = courseId,
                        StudentName = student.StudentName,
                        SectionName = student.Section.SectionName,
                        SectionCode = student.Section.SectionCode
                        //TrainHours=course.TrainHours
                    };
                    studentCourses.Add(studentCourse);
                }
                
            }

            var record = new Course()
            {
                CourseId=courseId,
                CourseName=course.CourseName,
                CourseStartDate=course.CourseStartDate,
                CourseEndDate= course.CourseEndDate,
                TrainHours=course.TrainHours,
                StudentCourses=studentCourses
            };
            db.Courses.Add(record);
            db.SaveChangesAsync();
            
            return db.Courses.Find(record.CourseId);
        }

        public Boolean DeleteCourse(Guid id)
        {
            var course = db.Courses.Find(id);
            db.Courses.Remove(course);
            return db.SaveChanges() != -1 ? true : false;
            
        }

        public Course GetCourseDetailById(Guid courseId)
        {
            return db.Courses.AsNoTracking().Include(c => c.StudentCourses).FirstOrDefault(c => c.CourseId.Equals(courseId));
        }

        public Task<List<Course>> GetCourseList()
        {
            return db.Courses.AsNoTracking().ToListAsync();
        }

        public Course UpdateCourse(Guid id, CourseChangeViewModel course)
        {
            var record = db.Courses.Include(c => c.StudentCourses).FirstOrDefault(c => c.CourseId.Equals(id));

            record.CourseName = course.CourseName;
            record.CourseStartDate = course.CourseStartDate;
            record.CourseEndDate = course.CourseEndDate;
            record.TrainHours = course.TrainHours;

            List<StudentCourse> studentCourses = new List<StudentCourse>();

            var fromDb = record.StudentCourses.Select(st => st.StudentId);

            var fromWeb = course.Students;


            IEnumerable<int> studentsToAdd = fromWeb.Except(fromDb);
            foreach (var studentId in studentsToAdd)
            {
                var student = db.Students.Include(s => s.Section).AsNoTracking().FirstOrDefault(s => s.StudentId == studentId);
                if (student != null)
                {
                    StudentCourse studentCourse = new StudentCourse()
                    {
                        StudentId = studentId,
                        //CourseName=course.CourseName,
                        CourseId = record.CourseId,
                        StudentName = student.StudentName,
                        SectionName = student.Section.SectionName,
                        SectionCode = student.Section.SectionCode
                        //TrainHours=course.TrainHours
                    };
                    studentCourses.Add(studentCourse);
                }

            }
            IEnumerable<int> studentsToDelete = fromDb.Except(fromWeb);
            foreach (var studentId in studentsToDelete)
            {
                var courseStudent = db.CourseStudents.FirstOrDefault(cs => cs.CourseId.Equals(record.CourseId) && cs.StudentId.Equals(studentId));
                db.CourseStudents.Remove(courseStudent);
            }

            
            db.CourseStudents.AddRange(studentCourses);
            if (db.SaveChanges() != -1)
                return db.Courses.Include(c=>c.StudentCourses).FirstOrDefault(c=>c.CourseId.Equals(record.CourseId));
            else { return null; }
            
        }
    }
}
