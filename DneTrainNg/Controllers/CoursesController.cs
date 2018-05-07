using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DneTrainNg.Models;
using DneTrainNg.Data.Repository;
using DneTrainNg.Models.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DneTrainNg.Data.APIResources;

namespace DneTrainNg.Controllers
{
    [Produces("application/json")]
    [Route("api/Courses")]
    
    //[Authorize]
    public class CoursesController : Controller
    {
        private readonly ICourseRepository repo;
        private readonly IMapper mapper;

        public CoursesController(ICourseRepository _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        // GET: api/Courses
        [HttpGet]
        [AllowAnonymous]
        public List<CourseListResource> GetCourses()
        {
            var courses = repo.GetCourseList();

            return mapper.Map<List<Course>, List<CourseListResource>>(courses);
            //return repo.GetCourseList();
        }

        

        [HttpPost("getpaginatedcourses")]
        [AllowAnonymous]
        public IActionResult GetPaginatedCourses([FromBody]PaginatedCoursesViewModel p)
        {
            var queryData= repo.GetPaginatedCourses(p);
            
            return Ok(queryData);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public CourseResource GetCourseById([FromRoute] Guid id)
        {

            return mapper.Map<Course, CourseResource>(repo.GetCourseDetailById(id));
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public IActionResult PutCourse([FromRoute] Guid id, [FromBody] CourseChangeViewModel courseToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = repo.GetCourseDetailById(id);
            if (course==null)
            {
                ModelState.AddModelError("error", "無法更新，請確認填入資訊是否正確");
                return BadRequest(ModelState);
            }
            var courseToDb = mapper.Map<CourseChangeViewModel, Course>(courseToUpdate,course);
            var result = repo.UpdateMapperCourse(courseToDb);

            return Ok(result);
            //return Ok(repo.UpdateCourse(id, course));
        }

        // POST: api/Courses
        [HttpPost]
        public IActionResult PostCourse([FromBody] CourseChangeViewModel course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(repo.AddCourse(course));
            
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var course = await _context.Courses.SingleOrDefaultAsync(m => m.CourseId == id);
            //if (course == null)
            //{
            //    return NotFound();
            //}

            //_context.Courses.Remove(course);
            //await _context.SaveChangesAsync();

            return Ok(repo.DeleteCourse(id));
        }


        [HttpPut("{id}/score")]
        public IEnumerable<StudentCourse> UpdateScore([FromRoute]Guid id, [FromBody]CourseScoreViewModel scoreData)
        {
            return repo.UpdateScore(id, scoreData);
        }

        private bool CourseExists(Guid id)
        {
            //return _context.Courses.Any(e => e.CourseId == id);
            return false;
        }


        [HttpGet("{id}/StudentCourse")]
        public IEnumerable<StudentCourse> GetStudentCourseById(Guid courseId)
        {
            return repo.GetStudentCoursesById(courseId);
        }

       

    }
}