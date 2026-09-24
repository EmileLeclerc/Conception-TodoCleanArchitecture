using CleanTodo.Domain.DTOS;
using FluentValidation;

namespace CleanTodo.Application.Validators;

// Valide automatiquement CreateTodoDto quand il est créé dans le controller
// Validator ci-dessous
public class CreateTodoValidation : AbstractValidator<CreateTodoDTO>
{
    public CreateTodoValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);

    }
}