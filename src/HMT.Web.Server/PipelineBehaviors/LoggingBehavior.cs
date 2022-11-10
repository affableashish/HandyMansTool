using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR.Pipeline;
using HMT.Web.Server.Interfaces;

namespace HMT.Web.Server.PipelineBehaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //Request
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await _identityService.GetUserNameAsync(userId);
            }

            _logger.LogInformation("MPCMechanicsTool Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();

            //Response
            var responseName = typeof(TResponse).Name;
            _logger.LogInformation("MPCMechanicsTool Response: {Name} {@UserId} {@UserName} {@Response} {ResponseTimeInMs}",
                responseName, userId, userName, response, timer.ElapsedMilliseconds);
            return response;
        }
    }
}
