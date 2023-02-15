using Microsoft.Extensions.Hosting;
using Serilog;

namespace Intility.Extensions.Logging
{
    public interface ILoggerBuilder
    {
        public LoggerConfiguration Configuration { get; }
        public HostBuilderContext Host { get; }
        public IHostBuilder HostBuilder { get; }

        public ILoggerBuilder UseConsoleFormat(ConsoleFormat format);
    }
}
