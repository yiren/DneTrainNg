using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();
        Student UpdateStudent(int studentId, Student student);
        Student CreateStudent(Student student);
        void DeleteStudent(int studentId);
        Student GetStudentById(int id);
    }
}
