using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DneTrainNg.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly TrainingDbContext db;

        public StudentRepository(TrainingDbContext _context)
        {
            db = _context;
        }

        public Student CreateStudent(Student student)
        {
            
            var sect = db.Sections.Find(student.SectionId);
            db.Entry(student).State = EntityState.Added;
            student.SectionCode = sect.SectionCode;
            student.SectionName = sect.SectionName;
            db.Students.Add(student);
            db.SaveChanges();
            var studentId = db.Entry(student).Entity.StudentId;
            return student;
        }

        public void DeleteStudent(int studentId)
        {
            var stud = db.Students.Include(s => s.StudentCourses).FirstOrDefault(s => s.StudentId.Equals(studentId));
            db.Students.Remove(stud);
            db.SaveChanges();
        }

        public Student GetStudentById(int id)
        {
            var data = db.Students.Find(id);
            return data;
        }

        public IEnumerable<Student> GetStudents()
        {
            return db.Students.Include(s=>s.StudentCourses).ToList();
        }

        public Student UpdateStudent(int studentId, Student student)
        {
            var dbStudent=db.Students.Include(s=>s.StudentCourses).FirstOrDefault(s=>s.StudentId.Equals(studentId));
            //var dbs2 = db.Students.Find(studentId);
            //var oldSection = db.Sections.Include(s => s.Students).FirstOrDefault(s => s.SectionId.Equals(dbStudent.SectionId));
            var newSection = db.Sections.AsNoTracking().Include(s=> s.Students).FirstOrDefault(s=> s.SectionId.Equals(student.SectionId));

            dbStudent.SectionId = newSection.SectionId;
            dbStudent.SectionName = newSection.SectionName;
            dbStudent.SectionCode = newSection.SectionCode;
            dbStudent.StudentName = student.StudentName;
            foreach (var item in dbStudent.StudentCourses)
            {
                dbStudent.SectionName = newSection.SectionName;
                dbStudent.SectionCode = newSection.SectionCode;
                dbStudent.StudentName = student.StudentName;
            }
            db.SaveChanges();
            
            return db.Students.Include(s => s.StudentCourses).FirstOrDefault(s => s.StudentId.Equals(studentId));
        }
    }
}
