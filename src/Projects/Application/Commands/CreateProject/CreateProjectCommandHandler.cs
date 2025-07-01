using KanbanFlow.Projects.Application.Common.Interfaces;
using KanbanFlow.Projects.Domain.Aggregates.ProjectAggregate;
using MediatR;

namespace KanbanFlow.Projects.Application.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Project name cannot be empty.", nameof(request.Name));
        }

        var project = Project.Create(request.Name);
        await _projectRepository.AddAsync(project, cancellationToken);

        return project.Id;
    }
}