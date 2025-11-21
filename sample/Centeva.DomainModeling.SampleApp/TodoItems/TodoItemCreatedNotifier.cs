using Mediator;

namespace Centeva.DomainModeling.SampleApp.TodoItems;

public class TodoItemCreatedNotifier : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<TodoItemCreatedNotifier> _logger;

    public TodoItemCreatedNotifier(ILogger<TodoItemCreatedNotifier> logger)
    {
        _logger = logger;
    }

    public ValueTask Handle(TodoItemCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Created a new Todo Item: {item}", domainEvent.Item);

        return ValueTask.CompletedTask;
    }
}
