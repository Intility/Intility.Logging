using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intility.Extensions.Logging
{
    public class LoggerBuilder : ILoggerBuilder
    {
        private readonly HostBuilderContext _host;
        private readonly LoggerConfiguration _config;
        private readonly IHostBuilder _hostBuilder;

        public LoggerBuilder(HostBuilderContext host, LoggerConfiguration config, IHostBuilder hostBuilder)
        {
            _host = host;
            _config = config;
            _hostBuilder = hostBuilder;
        }

        public HostBuilderContext Host => _host;
        public IHostBuilder HostBuilder => _hostBuilder;
        public LoggerConfiguration Configuration => _config;
    }
}
