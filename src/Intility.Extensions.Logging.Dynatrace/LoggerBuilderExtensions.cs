using Serilog;
using System;

namespace Intility.Extensions.Logging
{
    public static class LoggerBuilderExtensions
    {
        /// <summary>
        /// Uses Dynatrace sink if IngestUrl is defined in the <paramref name="configSection"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configSection"></param>
        /// <returns></returns>
        public static ILoggerBuilder UseDynatrace(this ILoggerBuilder builder, string configSection = "Dynatrace")
        {
            var configuration = builder.Host.Configuration.GetSection(configSection);
            var ingestUrl = configuration["IngestUrl"];
            if (string.IsNullOrWhiteSpace(ingestUrl))
            {
                return builder;
            }

            var accessToken = configuration["AccessToken"];
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new Exception("Failed to initialize Dynatrace sink", new ArgumentException("missing dynatrace config: " + configSection + ":AccessToken"));
            }

            var hostName = configuration["HostName"] ?? null;
            var applicationId = configuration["ApplicationId"] ?? null;

            builder.Configuration.WriteTo.Dynatrace(accessToken, ingestUrl, applicationId: applicationId, hostName: hostName);
            return builder;
        }
    }
}
