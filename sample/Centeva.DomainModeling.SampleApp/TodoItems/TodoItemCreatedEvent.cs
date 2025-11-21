using Centeva.DomainModeling.Mediator;

namespace Centeva.DomainModeling.SampleApp.TodoItems;

public class TodoItemCreatedEvent(TodoItem item) : BaseDomainEvent
{
    public TodoItem Item { get; } = item;
}
