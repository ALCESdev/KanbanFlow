using KanbanFlow.Projects.Domain.Aggregates.ProjectAggregate;

namespace KanbanFlow.Projects.Application.Common.Interfaces;

public interface IProjectRepository
{
    Task AddAsync(Project project, CancellationToken cancellationToken);
}