using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Sonar.Console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration(Startup.Configure)
                .ConfigureServices(Startup.ConfigureService)
                .UseSerilog(SerilogConfiguration.ConfigureLogging);

            await builder.RunConsoleAsync();
        }
    }
}
