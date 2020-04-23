using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace _2PAC.WebApp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

/*
//  Angular single-page application
TODO     client-side validation
//      error handling on requests to APIs
//      deployed to Azure App Service
//     supports deep links
TODO     at least 50% of user stories must be fulfilled by the Angular app (but ideally all)
//  ASP.NET Core REST service
//      follow standard HTTP uniform interface, except hypermedia
//      architecture with separation of concerns between domain/business logic, data access, and API; repository pattern
//      deployed to Azure App Service
//      Entity Framework Core
//         DB should be on the cloud
//          all DB/network access should be async
//      server-side validation
TODO     support filtering or pagination on at least one resource
//      logging
//     implement hypermedia, or, implement an API Description Language, e.g. using Swashbuckle
TODO     (optional: implement a custom filter, health check, or middleware, e.g. exception-handling middleware)
//  Azure Pipelines
//     CI pipelines
//          Unit tests
//          SonarCloud
TODO             Code coverage at least 30% for API, at least 20% for Angular app
//             Reliability/Security/Maintainability at least B
TODO (optional: deploy in release definition or separate job instead of in build job, and use health checks)
TODO (optional: calls an external API, or integrates with some other service)
//  (optional: authentication and authorization with e.g. Auth0 or Okta)
// Scrum processes
//      Project board to track user stories across team. (no requirements on how detailed)
!     Standup at least two or three times a week
// any other tech you want within reason
// the data model (how many tables, what kind of complex relationship like N to N) must be at least as complicated as project 1.
// the user interaction model (what are the user stories, what inputs/interactions can the user make) must be at least as complicated as project 1.
// a project proposal
//     MVP minimum viable product
//     potentially stretch goals, extra features
*/

/*
* in package manager console..
* 1. add-migration migrationname
* 2. update-database migrationname
* */
