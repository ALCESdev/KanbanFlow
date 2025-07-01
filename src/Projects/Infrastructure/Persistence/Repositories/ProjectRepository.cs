using KanbanFlow.Projects.Application.Common.Interfaces;
using KanbanFlow.Projects.Domain.Aggregates.ProjectAggregate;
using KanbanFlow.Projects.Infrastructure.Persistence.Data;

namespace KanbanFlow.Projects.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Project project, CancellationToken cancellationToken)
    {
        await _context.Projects.AddAsync(project, cancellationToken);
        //await _context.SaveChangesAsync(cancellationToken);
    }
}