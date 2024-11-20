using MediatR;
using Microsoft.Extensions.Logging;
using QRMenu.Application.Interfaces;

namespace QRMenu.Application.Behaviors
{
    public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType().Name;
            var userId = _currentUserService.UserId;
            var userName = _currentUserService.UserName;

            _logger.LogInformation(
                "Handling {RequestName} - User: {UserName} ({UserId})",
                requestName, userName, userId);

            var response = await next();

            _logger.LogInformation(
                "Handled {RequestName} - User: {UserName} ({UserId})",
                requestName, userName, userId);

            return response;
        }
    }
}
