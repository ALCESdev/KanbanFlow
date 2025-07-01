using FluentValidation;
using KanbanFlow.Projects.Application.Commands.CreateProject;

namespace KanbanFlow.Projects.Application.Validators;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name cannot be empty.")
            .MaximumLength(100).WithMessage("Project name cannot exceed 100 characters.");
    }
}