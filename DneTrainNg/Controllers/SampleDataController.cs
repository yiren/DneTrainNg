using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DneTrainNg.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DneTrainNg.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly UserManager<ApplicationUser> userManager;

        public SampleDataController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<string> GetSimpleData()
        {
            if (User == null)
            {
                return new List<string>() { "Not Authorized"};
             
            }
            var userId=User.FindFirst(ClaimTypes.NameIdentifier).Value;
            foreach (var claim in User.Claims)
            {
                Summaries.Append(claim.Type+":"+claim.Value);
            }
            
            var roles = new List<string>();
            
            return Summaries.Append(userId);
        }

        //[HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
