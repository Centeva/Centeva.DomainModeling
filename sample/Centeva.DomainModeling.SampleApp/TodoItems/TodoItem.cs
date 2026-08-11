namespace Centeva.DomainModeling.SampleApp.TodoItems;

public class TodoItem : BaseEntity<int>, IAggregateRoot
{
    public TodoItem(string name)
    {
        Name = name;
        RegisterDomainEvent(new TodoItemCreatedEvent(this));
    }

    public string Name { get; private set; }

    public string? Description { get; set; }
}
