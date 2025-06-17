using FluentValidation;
using GameOfLife.Core.Extensions;

namespace GameOfLife.Application.UseCases.UpdateBoard
{
    public class UpdateBoardValidator : AbstractValidator<UpdateBoardInput>
    {
        public UpdateBoardValidator()
        {
            BoardIdMustBeInformed();
        }

        public void BoardIdMustBeInformed() =>
            RuleFor(d => d.BoardId)
            .Must(id => id != Guid.Empty)
            .WithMessage(nameof(UpdateBoardInput.BoardId).IsRequired());
    }
}
