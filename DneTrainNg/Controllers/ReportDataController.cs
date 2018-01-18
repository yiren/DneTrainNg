using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DneTrainNg.Data.Repository;
using DneTrainNg.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DneTrainNg.Controllers
{
    [Produces("application/json")]
    [Route("api/ReportData")]
    public class ReportDataController : Controller
    {
        private readonly IReportRepository repo;

        public ReportDataController(IReportRepository _repo)
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
