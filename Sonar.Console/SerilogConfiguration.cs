using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;

namespace Sonar.Console
{
    [ExcludeFromCodeCoverage]
    public static class SerilogConfiguration
    {
        public static void ConfigureLogging(HostBuilderContext context, LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console(new RenderedCompactJsonFormatter());
        }
    }
}
