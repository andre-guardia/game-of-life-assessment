using FluentValidation;
using GameOfLife.Core.Extensions;

namespace GameOfLife.Application.UseCases.CreateBoard
{
    public class CreateBoardValidator : AbstractValidator<CreateBoardInput>
    {
        public CreateBoardValidator()
        {
            WidthMustBeInformed();
            HeightMustBeInformed();
        }

        public void WidthMustBeInformed() =>
            RuleFor(d => d.Width)
            .Must(width => width > 0)
            .WithMessage(nameof(CreateBoardInput.Width).IsRequired());


        public void HeightMustBeInformed() =>
            RuleFor(d => d.Height)
            .Must(height => height > 0)
            .WithMessage(nameof(CreateBoardInput.Height).IsRequired());
    }
}
