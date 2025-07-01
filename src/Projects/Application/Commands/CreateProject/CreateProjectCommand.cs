using MediatR;

namespace KanbanFlow.Projects.Application.Commands.CreateProject;

public record CreateProjectCommand(string Name) : IRequest<Guid>;