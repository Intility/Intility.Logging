using Serilog;

namespace Intility.Extensions.Logging
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

        /// <summary>
        /// Convenience method. Sets the ConsoleFormat to Structured.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILoggerBuilder UseOpenshiftLogging(this ILoggerBuilder builder)
        {
            return builder.UseConsoleFormat(ConsoleFormat.Structured);
        }
    }
}
