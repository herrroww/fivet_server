using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Fivet.Dao;
using Fivet.ZeroIce;
using Fivet.ZeroIce.model;

namespace Fivet.Server
{

    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            return Host
            .CreateDefaultBuilder(args)
            .UseEnvironment("Development")
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.TimestampFormat = "[yyyy-MM-dd HH:mm:ss.fff]";
                    options.DisableColors = false;
                });
                logging.SetMinimumLevel(LogLevel.Trace);

            })
            .UseConsoleLifetime()
            .ConfigureServices((context, services) =>
            {
                //FiveContext
                services.AddDbContext<FivetContext>();
                // The FivetService 
                services.AddHostedService<FivetService>();
                //TheSystem
                services.AddSingleton<TheSystemDisp_,TheSystemImpl>();
                //Contratos
                services.AddSingleton<ContratosDisp_,ContratosImpl>();
                services.AddLogging();
                services.Configure<HostOptions>(option =>
                {
                    option.ShutdownTimeout = System.TimeSpan.FromSeconds(15);
                });

            });

        }

    }
}



