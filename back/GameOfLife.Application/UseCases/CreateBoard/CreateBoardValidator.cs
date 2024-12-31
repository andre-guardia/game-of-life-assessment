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
            CellsMustBeValid();
        }

        public void WidthMustBeInformed() =>
            RuleFor(d => d.Width)
            .Must(width => width > 0)
            .WithMessage(nameof(CreateBoardInput.Width).IsRequired());


        public void HeightMustBeInformed() =>
            RuleFor(d => d.Height)
            .Must(height => height > 0)
            .WithMessage(nameof(CreateBoardInput.Height).IsRequired());

        public void CellsMustBeValid() =>
            RuleFor(d => d.Cells)
            .Must(cells => cells?.All(c => c.X > 0 && c.Y > 0) is true)
            .When(d => d.Cells != null && d.Cells.Count > 0)
            .WithMessage(nameof(CreateBoardInput.Cells).IsNotValid());
    }
}
