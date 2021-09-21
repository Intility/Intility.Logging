using System;
using Intility.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sentry.AspNetCore;
using Sentry.Serilog;
using Serilog;

namespace Intility.Extensions.Logging
{
    public static class LoggerBuilderExtensions
    {
        /// <summary>
        /// Add Sentry instrumentation if Dsn is defined in the <paramref name="configSection"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configSection"></param>
        /// <returns></returns>
        public static ILoggerBuilder UseSentry(this ILoggerBuilder builder, string configSection = "Sentry")
        {
            // both SentryAspNetCoreOptions and SentrySerilogOptions can be bound to
            // this section because they both inherit from Sentry.SentryOptions
            var configuration = builder.Host.Configuration.GetSection(configSection);

            if (string.IsNullOrWhiteSpace(configuration["Dsn"]))
            {
                return builder;
            }

            // ConfigureWebHostDefaults can be called multiple times with additive effect
            builder.HostBuilder.ConfigureWebHostDefaults(webHostBuilder =>
            {
                // runtime instrumentation
                webHostBuilder.UseSentry((SentryAspNetCoreOptions options) =>
                    configuration.Bind(options));
            });

            // configure serilog logger
            builder.Configuration.WriteTo.Sentry((SentrySerilogOptions options) => configuration.Bind(options));

            return builder;
        }
    }
}
