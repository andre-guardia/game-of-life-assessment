using FluentValidation;
using FluentValidation.Results;
using GameOfLife.Core.UseCases.Outputs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GameOfLife.Core.UseCases.Behaviors
{
    public sealed class FailFastValidationBehavior<TRequest, TOutput> : IPipelineBehavior<TRequest, TOutput>
        where TOutput : Output, new()
    {
        private readonly ILogger<FailFastValidationBehavior<TRequest, TOutput>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FailFastValidationBehavior(IEnumerable<IValidator<TRequest>> validator, ILogger<FailFastValidationBehavior<TRequest, TOutput>> logger)
        {
            _validators = validator;
            _logger = logger;
        }

        public async Task<TOutput> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TOutput> next)
        {
            var validationResults = ProcessValidations(request);

            var result = CreateResult(validationResults);

            return await VerifyNextStepAsync(request, result, next).ConfigureAwait(false);
        }

        private IEnumerable<ValidationResult> ProcessValidations(TRequest request)
        {
            foreach (var validator in _validators)
            {
                yield return validator.Validate(request);
            }
        }

        public static TOutput CreateResult(IEnumerable<ValidationResult> validationResult)
        {
            if (typeof(TOutput) == typeof(Output))
            {
                return (TOutput)new Output(validationResult);
            }

            var tOutput = CreateTOutputWithValidatonResult(validationResult);

            return tOutput;
        }

        private static TOutput CreateTOutputWithValidatonResult(IEnumerable<ValidationResult> validationResults)
        {
            var tOutput = new TOutput();
            tOutput.ProcessValidationResults(validationResults.ToArray());

            return tOutput;
        }

        private async Task<TOutput> VerifyNextStepAsync(TRequest request, Output output, RequestHandlerDelegate<TOutput> proceedToCommandHandler)
        {
            if (output.IsValid)
            {
                return await proceedToCommandHandler().ConfigureAwait(false);
            }

            var nextStep = output as TOutput;

            _logger.LogWarning("Validation for: {requestType} failed.", request.GetType().Name);

            return nextStep;

        }
    }
}
