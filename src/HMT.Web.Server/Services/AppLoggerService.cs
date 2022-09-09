using Microsoft.Extensions.Logging;
using HMT.Web.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMT.Web.Server.Services
{
    public class AppLoggerService<T> : IAppLoggerService<T>
    {
        private readonly ILogger<T> _logger;
        public AppLoggerService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}