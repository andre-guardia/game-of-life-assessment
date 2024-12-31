using FluentValidation;
using GameOfLife.Core.Extensions;

namespace GameOfLife.Application.UseCases.GetBoard
{
    public class GetBoardValidator : AbstractValidator<GetBoardInput>
    {
        public GetBoardValidator()
        {
            BoardIdMustBeInformed();
        }

        public void BoardIdMustBeInformed() =>
            RuleFor(d => d.BoardId)
            .Must(id => id != Guid.Empty)
            .WithMessage(nameof(GetBoardInput.BoardId).IsRequired());
    }
}
