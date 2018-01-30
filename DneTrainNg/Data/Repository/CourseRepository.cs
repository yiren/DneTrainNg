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
                var student = db.Students.Find(studentId);
                
                if (student != null)
                {
                    StudentCourse studentCourse = new StudentCourse()
                    {
                        StudentId = studentId,
                        //CourseName=course.CourseName,
                        CourseId = courseId,
                        StudentName = student.StudentName,
                        SectionName = student.SectionName,
                        SectionCode = student.SectionCode
                        //TrainHours=course.TrainHours
                    };
                    studentCourses.Add(studentCourse);
                }
            }
            
            var record = new Course()
            {
                CourseId= courseId,
                CourseName= course.CourseName,
                CourseStartDate= course.CourseStartDate,
                CourseEndDate= course.CourseEndDate,
                TrainHours= course.TrainHours,
                CreateDate= DateTime.Now,
                StudentCourses= studentCourses
            };
            db.Courses.Add(record);
            db.SaveChanges();
            
            return db.Courses.Find(record.CourseId);
        }

        public Boolean DeleteCourse(Guid id)
        {
            var course = db.Courses.Include(c=>c.StudentCourses).FirstOrDefault(s=>s.CourseId.Equals(id));
            db.Courses.Remove(course); 
            return db.SaveChanges() != -1 ? true : false;
            
        }

        public Course GetCourseDetailById(Guid courseId)
        {
            return db.Courses.AsNoTracking().Include(c => c.StudentCourses)
                             
                             .FirstOrDefault(c => c.CourseId.Equals(courseId));
        }

        public Task<List<Course>> GetCourseList()
        {
            return db.Courses.AsNoTracking().OrderByDescending(c => c.CourseStartDate).ThenByDescending(c=>c.CourseEndDate).ToListAsync();
        }

        public IEnumerable<StudentCourse> GetStudentCoursesById(Guid courseId)
        {
            return db.StudentCourses.AsNoTracking().Where(sc => sc.CourseId.Equals(courseId)).ToList();
        }

        public Course UpdateCourse(Guid id, CourseChangeViewModel course)
        {
            var record = db.Courses.Find(id);
            var sts = db.StudentCourses.Where(st => st.CourseId.Equals(id));

            record.CourseName = course.CourseName;
            record.CourseStartDate = course.CourseStartDate;
            record.CourseEndDate = course.CourseEndDate;
            record.TrainHours = course.TrainHours;

            List<StudentCourse> studentCourses = new List<StudentCourse>();

            var fromDb = sts.Select(st => st.StudentId);

            var fromWeb = course.Students;

            IEnumerable<int> studentsToAdd = fromWeb.Except(fromDb);
            foreach (var studentId in studentsToAdd)
            {
                var student = db.Students.Find(studentId);

                if (student != null)
                {
                    StudentCourse studentCourse = new StudentCourse()
                    {
                        StudentId = studentId,
                        //CourseName=course.CourseName,
                        CourseId = id,
                        StudentName = student.StudentName,
                        SectionName = student.SectionName,
                        SectionCode = student.SectionCode,
                        Score="N/A"
                        //TrainHours=course.TrainHours
                    };
                    studentCourses.Add(studentCourse);
                };
            }
            
            IEnumerable<int> studentsToDelete = fromDb.Except(fromWeb);
            foreach (var studentId in studentsToDelete)
            {
                var courseStudent = db.StudentCourses.FirstOrDefault(cs => cs.CourseId.Equals(record.CourseId) && cs.StudentId.Equals(studentId));
                db.StudentCourses.Remove(courseStudent);
            }
            
            db.StudentCourses.AddRange(studentCourses);
            if (db.SaveChanges() != -1)
                return db.Courses.Include(c=>c.StudentCourses).FirstOrDefault(c=>c.CourseId.Equals(record.CourseId));
            else { return null; }
            
        }

        public IEnumerable<StudentCourse> UpdateScore(Guid courseId, CourseScoreViewModel scoreData)
        {
            var dbStudentCourses = db.StudentCourses.Where(sc => sc.CourseId.Equals(courseId));

            foreach (var updateData in scoreData.StudentScoreData)
            {
                foreach (var dbData in dbStudentCourses)
                {
                    if (dbData.StudentId == updateData.Key)
                    {
                        dbData.Score = updateData.Value;
                        dbData.LastModifiedDate = DateTime.Now.ToString("yyyy/MM/dd");
                        dbData.LastModifiedBy = scoreData.Modifier;
                    }
                }
            }

            db.SaveChanges();
            
            return db.StudentCourses.Where(sc => sc.CourseId.Equals(courseId)).ToList();
        }
    }
}
