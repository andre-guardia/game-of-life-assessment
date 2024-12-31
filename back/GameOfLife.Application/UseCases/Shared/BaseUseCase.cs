using GameOfLife.Core.UseCases.Outputs;
using Microsoft.Extensions.Logging;

namespace GameOfLife.Application.UseCases.Shared
{
    public abstract class BaseUseCase<T> where T : class
    {
        protected readonly ILogger<T> _logger;

        public BaseUseCase(ILogger<T> logger)
        {
            _logger = logger;
        }

        public Output HandleError(string message)
        {
            var output = new Output();
            _logger.LogInformation(message);
            output.AddErrorMessages(message);
            return output;
        }
    }
}
