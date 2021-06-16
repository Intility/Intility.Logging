using Intility.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LoggerBuilderExtensions
    {
        /// <summary>
        /// Enriches logs with SourceContext, EnvironmentName, HostName, AssemblyName and MemoryUsage
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILoggerBuilder UseDefaultEnrichers(this ILoggerBuilder builder)
        {
            builder.Configuration
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithMachineName()
                .Enrich.WithAssemblyName()
                .Enrich.WithMemoryUsage();

            return builder;
        }
    }
}
