using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Linq;
using Serilog.Sinks.Elasticsearch;
using Intility.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseIntilityLogging(this IHostBuilder builder)
        {
            return UseIntilityLogging(builder, null);
        }

        public static IHostBuilder UseIntilityLogging(this IHostBuilder builder, Action<HostBuilderContext,ILoggerBuilder> configure)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.UseSerilog((HostBuilderContext ctx, LoggerConfiguration logger) =>
            {
                logger.ReadFrom.Configuration(ctx.Configuration, sectionName: "Serilog")
                    .WriteTo.Console(theme: AnsiConsoleTheme.Code);

                var loggerBuilder = new LoggerBuilder(ctx, logger, builder);
                configure?.Invoke(ctx, loggerBuilder);
            },
            preserveStaticLogger: false,
            writeToProviders: false);

            return builder;
        }

        
    }
}
