namespace Centeva.DomainModeling.SampleApp.Models;

public class TodoItemCreatedEvent(TodoItem item) : BaseDomainEvent
{
    public TodoItem Item { get; } = item;
}
