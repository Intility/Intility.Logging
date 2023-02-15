using Microsoft.Extensions.Hosting;
using System;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog.Formatting.Compact;

namespace Intility.Extensions.Logging
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
                logger.ReadFrom.Configuration(ctx.Configuration, sectionName: "Serilog");

                var loggerBuilder = new LoggerBuilder(ctx, logger, builder);
                configure?.Invoke(ctx, loggerBuilder);

                if (loggerBuilder.ConsoleFormat == ConsoleFormat.Pretty)
                {
                    logger.WriteTo.Console(theme: AnsiConsoleTheme.Code);
                }
                else if (loggerBuilder.ConsoleFormat == ConsoleFormat.Structured)
                {
                    logger.WriteTo.Console(new CompactJsonFormatter());
                }
            },
            preserveStaticLogger: false,
            writeToProviders: false);

            return builder;
        }
    }
}
