using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DneTrainNg.Data.Repository;
using DneTrainNg.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DneTrainNg.Controllers
{
    [Produces("application/json")]
    [Route("api/Sections")]
    [EnableCors("angular")]
    //[Authorize]
    public class SectionsController : Controller
    {
        private readonly ISectionRepository repo;

        public SectionsController(ISectionRepository _repo)
        {
            repo = _repo;
        }
        // GET: api/Sections
        [HttpGet("getSections")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Section> GetSections()
        {
            return repo.GetSections();
        }

        // GET: api/Sections/5
        [HttpGet("{id}")]
        public string GetSectionById(int id)
        {
            return "value";
        }
        
        // POST: api/Sections
        [HttpPost]
        public Section Post([FromBody]Section section)
        {
            return repo.CreateSection(section);
        }
        
        // PUT: api/Sections/5
        [HttpPut("{id}")]
        public Section Put(int id, [FromBody]Section section)
        {
            return repo.UpdateSection(id, section);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repo.DeleteSection(id);
        }
    }
}
