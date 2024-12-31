using FluentValidation;
using GameOfLife.Core.UseCases.Outputs;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Serilog.Core.Enrichers;
using Serilog.Events;
using SerilogTimings;

namespace GameOfLife.Core.UseCases
{
    public abstract class UseCaseHandlerBase<TRequest, TResponse> : IUseCase<TRequest, TResponse>
       where TRequest : notnull, IRequest<TResponse>
       where TResponse : notnull
    {
        private readonly ILogger<UseCaseHandlerBase<TRequest, TResponse>> _logger;
        private readonly IValidator<TRequest> _validator;

        protected UseCaseHandlerBase(ILogger<UseCaseHandlerBase<TRequest, TResponse>> logger, IValidator<TRequest> validator = null)
        {
            _logger = logger;
            _validator = validator;
        }

        public async Task<Output<TResponse>> ExecuteAsync(TRequest request, CancellationToken cancellationToken)
        {
            using var _ = LogContext.PushProperties(new PropertyEnricher("CorrelationId", request?.CorrelationId));
            using var __ = Operation.At(LogEventLevel.Information).Time($"Timing for {GetType().Name}");

            _logger.LogInformation("Start : {HandlerName} ", GetType().Name);

            if (_validator != default)
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var responseError = new Output<TResponse>();
                    responseError.AddValidationResult(validationResult);

                    _logger.LogInformation("Finalize : [{NameRequest}] : {Success}", typeof(TRequest).Name, responseError.IsValid);

                    return responseError;
                }
            }

            Output<TResponse> response = await HandleAsync(request, cancellationToken);

            _logger.LogInformation("Finalize : [{NameRequest}] : {Success}", typeof(TRequest).Name, response.IsValid);

            return response;
        }

        protected abstract Task<Output<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }


    public abstract class UseCaseHandlerBase<TRequest> : IUseCase<TRequest>
        where TRequest : notnull, IRequest
    {
        private readonly ILogger<UseCaseHandlerBase<TRequest>> _logger;
        private readonly IValidator<TRequest> _validator;

        protected UseCaseHandlerBase(ILogger<UseCaseHandlerBase<TRequest>> logger, IValidator<TRequest> validator = null)
        {
            _logger = logger;
            _validator = validator;
        }

        public async Task<Output> ExecuteAsync(TRequest request, CancellationToken cancellationToken)
        {
            using var _ = LogContext.PushProperties(new PropertyEnricher("CorrelationId", request?.CorrelationId));
            using var __ = Operation.At(LogEventLevel.Information).Time($"Timing for {GetType().Name}");

            _logger.LogInformation("Start : {HandlerName} ", GetType().Name);

            var response = new Output();

            if (_validator != default)
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.AddValidationResult(validationResult);

                    _logger.LogInformation("Finalize : [{NameRequest}] : {Success}", typeof(TRequest).Name, response.IsValid);

                    return response;
                }
            }

            await HandleAsync(request, cancellationToken);

            _logger.LogInformation("Finalize : [{NameRequest}] : {Success}", typeof(TRequest).Name, response.IsValid);

            return response;
        }


        protected abstract Task HandleAsync(TRequest request, CancellationToken cancellationToken);
    }

}
