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
        public static ILoggerBuilder UseSentry(this ILoggerBuilder builder, string configSection = "Sentry")
        {
            // both SentryAspNetCoreOptions and SentrySerilogOptions can be bound to
            // this section because they both inherit from Sentry.SentryOptions
            var configuration = builder.Host.Configuration.GetSection(configSection);

            // ConfigureWebHostDefaults can be called multiple times with additive effect
            builder.HostBuilder.ConfigureWebHostDefaults(webHostBuilder =>
            {
                // runtime instrumentation
                webHostBuilder.UseSentry((SentryAspNetCoreOptions options) =>
                    configuration.Bind(options));
            });

            // configure serilog logger
            builder.Configuration.WriteTo.Sentry((SentrySerilogOptions options) =>
            {
                configuration.Bind(options);

                // SDK already initialized by webHostBuilder
                options.InitializeSdk = false;
            });

            return builder;
        }
    }
}
