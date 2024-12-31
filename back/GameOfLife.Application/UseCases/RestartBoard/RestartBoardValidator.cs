using FluentValidation;
using GameOfLife.Core.Extensions;

namespace GameOfLife.Application.UseCases.RestartBoard
{
    public class RestartBoardValidator : AbstractValidator<RestartBoardInput>
    {
        public RestartBoardValidator()
        {
            BoardIdMustBeInformed();
        }

        public void BoardIdMustBeInformed() =>
            RuleFor(d => d.BoardId)
            .Must(id => id != Guid.Empty)
            .WithMessage(nameof(RestartBoardInput.BoardId).IsRequired());
    }
}
