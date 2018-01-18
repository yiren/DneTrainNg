using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DneTrainNg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DneTrainNg.Controllers
{


    public class CoursesImportViewModel
    {
        public string CourseName { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }

        public double TrainHours { get; set; }
        public string Score { get; set; }
        public string StudentName { get; set; }
    }
    [Produces("application/json")]
    [Route("api/BatchImport")]
    public class BatchImportController : Controller
    {
        private readonly TrainingDbContext db;

        public BatchImportController(TrainingDbContext _db)
        {
            db = _db;
        }
        // GET: api/BatchImport
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BatchImport/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpPost("courses")]
        public IEnumerable<Course> ImportCourse([FromBody]IEnumerable<CoursesImportViewModel> courses)
        {
            List<Course> dataToPersist = new List<Course>();
            foreach (var item in courses)
            {
                var student = db.Students.AsNoTracking().First(s => s.StudentName.Equals(item.StudentName));
                Course course = dataToPersist.SingleOrDefault(c => c.CourseName.Equals(item.CourseName));
                if (course == null)
                {
                    course = new Course
                    {
                        CourseId=Guid.NewGuid(),
                        CourseName = item.CourseName,
                        CourseStartDate = item.CourseStartDate,
                        CourseEndDate = item.CourseEndDate,
                        CreateDate = new DateTime(2015,12,31),
                        LastModifiedBy = "批次匯入",
                        LastModifiedDate = DateTime.Now.ToString("YYYY/MM/DD"),
                        TrainHours = item.TrainHours,
                        StudentCourses=new List<StudentCourse>()
                    };
                    course.StudentCourses.Add(new StudentCourse
                    {
                        CourseId = course.CourseId,
                        StudentId=student.StudentId,
                        Score=item.Score,
                        SectionCode=student.SectionCode,
                        SectionName=student.SectionName,
                        StudentName = student.StudentName,
                        LastModifiedBy = "批次匯入",
                        LastModifiedDate = DateTime.Now.ToString("YYYY/MM/DD")
                    });
                    dataToPersist.Add(course);
                }
                else
                {
                    if (course.StudentCourses == null)
                    {
                        course.StudentCourses=new List<StudentCourse>{
                            new StudentCourse
                        {
                            CourseId = course.CourseId,
                            StudentId=student.StudentId,
                            StudentName=student.StudentName,
                            Score = item.Score,
                            SectionCode = student.SectionCode,
                            SectionName = student.SectionName,
                            LastModifiedBy = "批次匯入",
                            LastModifiedDate = DateTime.Now.ToString("YYYY/MM/DD")
                        }
                        };
                    }
                    else
                    {
                        course.StudentCourses.Add(new StudentCourse
                        {
                            CourseId = course.CourseId,
                            StudentId = student.StudentId,
                            Score = item.Score,
                            SectionCode = student.SectionCode,
                            SectionName = student.SectionName,
                            StudentName = student.StudentName,
                            LastModifiedBy = "批次匯入",
                            LastModifiedDate = DateTime.Now.ToString("YYYY/MM/DD")
                        });
                    }
                }

                
            }
            db.Courses.AddRange(dataToPersist);
            db.SaveChanges();
            return db.Courses.OrderByDescending(d=>d.CourseEndDate).ToList();
        }

        // POST: api/BatchImport
        [HttpPost("students")]
        public IEnumerable<Student> ImportStudents([FromBody]IEnumerable<Student> students)
        {
            List<Student> dataToPersist = new List<Student>();
            Student newRecord;
            foreach (var record in students)
            {
                var section = db.Sections.AsNoTracking().First(s => s.SectionId == record.SectionId);
                newRecord = new Student
                {
                    StudentName = record.StudentName,
                    SectionId = record.SectionId,
                    SectionName = section.SectionName,
                    SectionCode = section.SectionCode,
                };
                dataToPersist.Add(newRecord);
            }

            db.Students.AddRange(dataToPersist);
            db.SaveChanges();
            return db.Students.ToList();
        }

        [HttpPost("sections")]
        public IEnumerable<Section> ImportSections([FromBody]IEnumerable<Section> sections)
        {
            List<Student> dataToPersist = new List<Student>();
            Section newRecord;
            foreach (var record in sections)
            {
                newRecord = new Section
                {
                    SectionCode = record.SectionCode,
                    SectionName = record.SectionName
                };
                db.Sections.Add(newRecord);
            }

            db.SaveChanges();
            return db.Sections.ToList();
        }

        // PUT: api/BatchImport/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
