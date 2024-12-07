namespace Centeva.DomainModeling.SampleApp.Models;

public class TodoItem : BaseEntity, IAggregateRoot
{
    public TodoItem(string name)
    {
        Name = name;
        RegisterDomainEvent(new TodoItemCreatedEvent(this));
    }

    public string Name { get; private set; }

    public string? Description { get; set; }
}
