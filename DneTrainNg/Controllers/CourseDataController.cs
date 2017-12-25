
using DneTrainNg.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DneTrainNg.Controllers
{
    [Route("api/CourseData")]
    [Produces("application/json")]
    public class CourseDataController : Controller
    {
        private readonly TrainingDbContext _context;

        public CourseDataController(TrainingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //TODO: Implement Realistic Implementation
            
            return Ok();
        }

        [HttpGet("{CourseId}")]
        public IActionResult Get(Guid CourseId)
        {
            //TODO: Implement Realistic Implementation

            return Ok();
        }

        [HttpPost()]
        public IActionResult Post()
        {
            //TODO: Implement Realistic Implementation

            return Ok();
        }
        [HttpPut()]
        public IActionResult Put()
        {
            //TODO: Implement Realistic Implementation

            return Ok();
        }
        [HttpDelete("{CourseId}")]
        public IActionResult Delete(Guid CourseId)
        {
            //TODO: Implement Realistic Implementation

            return Ok();
        }
    }
}


