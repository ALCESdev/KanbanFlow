using KanbanFlow.Projects.Domain.Common;

namespace KanbanFlow.Projects.Domain.Aggregates.ProjectAggregate;

public class Project : Entity
{
    public string Name { get; private set; }
    private readonly List<TaskItem> _taskItems = [];
    public IReadOnlyCollection<TaskItem> TaskItems => _taskItems.AsReadOnly();

    // Constructor privado para EF Core y para forzar la creación a través de métodos de negocio
    private Project(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public void AddTask(TaskItem taskItem)
    {
        if (taskItem == null)
        {
            throw new ArgumentNullException(nameof(taskItem), "Task item cannot be null.");
        }

        if (_taskItems.Any(t => t.Id == taskItem.Id))
        {
            throw new InvalidOperationException("Task item with the same ID already exists in the project.");
        }

        if (_taskItems.Count >= 50)
        {
            throw new InvalidOperationException("Cannot add more than 50 task items to a project.");
        }

        _taskItems.Add(taskItem);
    }

    public static Project Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Project name cannot be empty.", nameof(name));
        }

        return new Project(Guid.NewGuid(), name);
    }

}