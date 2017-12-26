﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DneTrainNg.Models;
using DneTrainNg.Data.Repository;
using DneTrainNg.Models.ViewModel;

namespace DneTrainNg.Controllers
{
    [Produces("application/json")]
    [Route("api/Courses")]
    public class CoursesController : Controller
    {
        private readonly ICourseRepository repo;

        public CoursesController(ICourseRepository _repo)
        {
            repo = _repo;
        }

        // GET: api/Courses
        [HttpGet]
        public Task<List<Course>> GetCourses()
        {
            return repo.GetCourseList();
        }

        [HttpGet("getvalues")]
        public string[] GetValues()
        {
            return new string[]{"v1","v2"
            };
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public Course GetCourseById([FromRoute] Guid id)
        {
            

            return repo.GetCourseDetailById(id);
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public Course PutCourse([FromRoute] Guid id, [FromBody] CourseChangeViewModel course)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != course.CourseId)
            //{
            //    return BadRequest();
            //}



            //try
            //{

            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CourseExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return repo.UpdateCourse(id, course);
        }

        // POST: api/Courses
        [HttpPost]
        public Course PostCourse([FromBody] CourseChangeViewModel course)
        {

            return repo.AddCourse(course);
            
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] Guid id)
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

            return Ok();
        }

        private bool CourseExists(Guid id)
        {
            //return _context.Courses.Any(e => e.CourseId == id);
            return false;
        }
    }
}