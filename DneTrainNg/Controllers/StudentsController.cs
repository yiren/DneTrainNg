using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DneTrainNg.Data.Repository;
using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DneTrainNg.Controllers
{
    [Produces("application/json")]
    [Route("api/Students")]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository repo;

        public StudentsController(IStudentRepository _repo)
        {
            repo = _repo;
        }
        // GET: api/Students
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return repo.GetStudents();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public Student GetStudentById(int id)
        {
            return repo.GetStudentById(id);
        }
        
        // POST: api/Students
        [HttpPost]
        public Student Post([FromBody]Student student)
        {
            return repo.CreateStudent(student);
        }
        
        // PUT: api/Students/5
        [HttpPut("{id}")]
        public Student Put(int id, [FromBody]Student student)
        {
            return repo.UpdateStudent(id, student);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repo.DeleteStudent(id);
        }
    }
}
