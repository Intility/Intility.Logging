using Intility.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LoggerBuilderExtensions
    {
        /// <summary>
        /// Use Elasticsearch log sink.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configSection"></param>
        /// <returns></returns>
        public static ILoggerBuilder UseElasticsearch(this ILoggerBuilder builder, string configSection = "Elasticsearch")
        {
            var elasticConfig = builder.Host.Configuration.GetSection(configSection);
            var elasticEndpoints = elasticConfig["Endpoints"];

            if (string.IsNullOrWhiteSpace(elasticEndpoints))
            {
                return builder;
            }

            var username = elasticConfig["Username"];
            var password = elasticConfig["Password"];
            var indexFormat = elasticConfig["IndexFormat"];
            var versionString = elasticConfig["Version"];

            if(string.IsNullOrWhiteSpace(indexFormat))
            {
                throw new Exception("Failed to initialize Elasticsearch sink", 
                    new ArgumentException($"missing elastic config: {configSection}:IndexFormat"));
            }

            var endpoints = elasticEndpoints.Split(',')
                .Select(endpoint => new Uri(endpoint));

            var versionFormat = string.IsNullOrWhiteSpace(versionString)
                ? AutoRegisterTemplateVersion.ESv7
                : Enum.Parse<AutoRegisterTemplateVersion>(versionString);

            var options = new ElasticsearchSinkOptions(endpoints)
            {
                TypeName = "logevent",
                IndexFormat = indexFormat,
                NumberOfShards = 1,
                NumberOfReplicas = 1,
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = versionFormat
            };

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                options.ModifyConnectionSettings = c => c.BasicAuthentication(username, password);
            }

            builder.Configuration.WriteTo.Elasticsearch(options);

            return builder;
        }
    }
}
