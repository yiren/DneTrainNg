using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DneTrainNg.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly TrainingDbContext db;
        private readonly IMapper mapper;

        public CourseRepository(TrainingDbContext _context, IMapper mapper)
        {
            db = _context;
            this.mapper = mapper;
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

        public List<Course> GetCourseList()
        {
            return GetCourses().ToList();
        }

        public IQueryable<Course> GetCourses()
        {
            return db.Courses.AsNoTracking().OrderByDescending(c => c.CourseStartDate).ThenByDescending(c => c.CourseEndDate);
        }

        public List<Course> GetPaginatedCourses(PaginatedCoursesViewModel p)
        {
            if (String.IsNullOrEmpty(p.Keyword)){
                return GetCourses().Skip(p.PageSize * p.PageIndex).Take(p.PageSize).ToList();
            }
            else
            {
                return GetCourses().Where(c=>c.CourseName.ToLower().Contains(p.Keyword)).Skip(p.PageSize * p.PageIndex).Take(p.PageSize).ToList();
            }
            
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

        public Course UpdateMapperCourse(Course courseToDb)
        {
            var updatedStudentCourses = new List<StudentCourse>();
            foreach (var student in courseToDb.StudentCourses)
            {
                var dbStudent = db.Students.Find(student.StudentId);
               updatedStudentCourses.Add(mapper.Map<Student, StudentCourse>(dbStudent, student));
            }

            courseToDb.StudentCourses = updatedStudentCourses;
            db.Courses.Update(courseToDb);
            db.SaveChanges();
            return db.Courses.AsNoTracking().Include(c => c.StudentCourses).FirstOrDefault(c => c.CourseId.Equals(courseToDb.CourseId));
            //return courseToDb;
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
