namespace KanbanFlow.Projects.Domain.Aggregates.ProjectAggregate;

public class TaskItem : Common.Entity
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; }

    // Constructor privado para EF Core y para forzar la creación a través de métodos de negocio
    private TaskItem(Guid id, string title, string? description) : base(id)
    {
        Title = title;
        Description = description;
        IsCompleted = false;
    }

    // Factory method para la creación controlada
    public static TaskItem Create(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        }

        return new TaskItem(Guid.NewGuid(), title, description);
    }

    public void MarkAsCompleted()
    {
        if (IsCompleted)
        {
            throw new InvalidOperationException("Task is already completed.");
        }

        IsCompleted = true;
    }
}