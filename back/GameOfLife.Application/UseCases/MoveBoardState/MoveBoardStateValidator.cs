using FluentValidation;
using GameOfLife.Core.Extensions;

namespace GameOfLife.Application.UseCases.MoveBoardState
{
    public class MoveBoardStateValidator : AbstractValidator<MoveBoardStateInput>
    {
        public MoveBoardStateValidator()
        {
            BoardIdMustBeInformed();
            LenghtMustBeValid();
        }

        public void BoardIdMustBeInformed() =>
            RuleFor(d => d.BoardId)
            .Must(id => id != Guid.Empty)
            .WithMessage(nameof(MoveBoardStateInput.BoardId).IsRequired());

        public void LenghtMustBeValid() =>
            RuleFor(d => d.Lenght)
            .Must(lenght => lenght > 0)
            .When(d => d.Lenght.HasValue)
            .WithMessage(nameof(MoveBoardStateInput.Lenght).IsNotValid());
    }
}
