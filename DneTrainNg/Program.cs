using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DneTrainNg.Data.SeedData;
using DneTrainNg.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DneTrainNg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host=BuildWebHost(args);

            //using (var scope=host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var context = services.GetRequiredService<TrainingDbContext>();
            //        TrainingSeedData.Initialize(context);
            //        Console.WriteLine("Course Count:"+context.Courses.Count());
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError("There is an internal error");
            //    }
            //};
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
