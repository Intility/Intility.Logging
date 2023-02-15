using Microsoft.Extensions.Hosting;
using Serilog;

namespace Intility.Extensions.Logging
{
    public class LoggerBuilder : ILoggerBuilder
    {
        private readonly HostBuilderContext _host;
        private readonly LoggerConfiguration _config;
        private readonly IHostBuilder _hostBuilder;
        public ConsoleFormat ConsoleFormat { get; private set; } = ConsoleFormat.Pretty;

        public LoggerBuilder(HostBuilderContext host, LoggerConfiguration config, IHostBuilder hostBuilder)
        {
            _host = host;
            _config = config;
            _hostBuilder = hostBuilder;
        }

        public HostBuilderContext Host => _host;
        public IHostBuilder HostBuilder => _hostBuilder;
        public LoggerConfiguration Configuration => _config;

        public ILoggerBuilder UseConsoleFormat(ConsoleFormat format)
        {
            ConsoleFormat = format;

            return this;
        }
    }
}
