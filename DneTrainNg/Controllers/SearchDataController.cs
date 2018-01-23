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
    [Route("api/searchdata")]
    public class SearchDataController : Controller
    {
        private readonly ISearchRepository repo;

        public SearchDataController(ISearchRepository _repo)
        {
            repo = _repo;
        }
        // GET: api/ReportData
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("getaveragetrainhours")]
        public string GetAverageTrainHours([FromBody]ReportForAverageTrainHoursViewModel averageVM)
        {
            return repo.GetDepAverageTrainHour(averageVM);
        }

        [HttpPost("searchbysection")]
        public IEnumerable<CourseQueryExportViewModel> ExportTrainingRecord([FromBody]QueryForTrainingRecord queryVM)
        {
            return repo.ExportTrainingRecord(queryVM);

        }

        [HttpPost("searchbycourse")]
        public IEnumerable<Course> SearchCourse([FromBody]CourseSearchViewModel searchViewModel)
        {
            return repo.SearchCourse(searchViewModel);
        }

        [HttpPost("searchbystudent")]
        public IEnumerable<Course> SearchCourseByStudent([FromBody] CourseSearchByStduentViewModel searchViewModel)
        {
            return repo.SearchCourseByStudent(searchViewModel);
        }

        // GET: api/ReportData/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/ReportData
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/ReportData/5
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
